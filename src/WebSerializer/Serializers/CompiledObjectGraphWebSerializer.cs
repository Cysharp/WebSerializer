using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Encodings.Web;

namespace Cysharp.Web.Serializers;

internal sealed class CompiledObjectGraphWebSerializer<T> : IWebSerializer<T>
{
    delegate void SerializeMethod(ref WebSerializerWriter writer, string[] encodedNames, IWebSerializer?[]? alternateSerializers, T value, WebSerializerOptions options);

    static readonly string[] encodedNames; // encoded `name=`
    static readonly IWebSerializer?[]? alternateSerializers;
    static readonly SerializeMethod serialize;
    static readonly bool isReferenceType;
    static readonly string? encodedNamePrefix;


    static CompiledObjectGraphWebSerializer()
    {
        var typeContract = typeof(T).GetCustomAttribute<DataContractAttribute>();
        if (typeContract != null && !string.IsNullOrEmpty(typeContract.Namespace))
        {
            encodedNamePrefix = UrlEncoder.Default.Encode(typeContract.Namespace);
        }
        else
        {
            encodedNamePrefix = null;
        }
        isReferenceType = !typeof(T).IsValueType;

        var props = typeof(T).GetProperties();
        var fields = typeof(T).GetFields();
        var members = props.Cast<MemberInfo>().Concat(fields)
            .Where(x => x.GetCustomAttribute<IgnoreWebSerializeAttribute>() == null)
            .Select(x => new SerializableMemberInfo(x))
            .OrderBy(x => x.Order)
            .ThenBy(x => x.Name)
            .ToArray();

        encodedNames = members.Select(x => UrlEncoder.Default.Encode(x.Name) + "=").ToArray();
        if (members.Any(x => x.WebSerializer != null))
        {
            alternateSerializers = members.Select(x => x.WebSerializer).ToArray();
        }
        serialize = CompileSerializer(typeof(T), members);
    }

    public void Serialize(ref WebSerializerWriter writer, T value, WebSerializerOptions options)
    {
        if (isReferenceType)
        {
            if (value == null) return;
        }

        writer.EnterAndValidate(options);

        var originalNamePrefix = writer.namePrefix; // get field directly

        if (encodedNamePrefix != null)
        {
            if (writer.namePrefix == null)
            {
                writer.namePrefix = encodedNamePrefix;
            }
            else
            {
                writer.namePrefix = originalNamePrefix + encodedNamePrefix;
            }
        }

        serialize(ref writer, encodedNames, alternateSerializers, value, options);

        if (encodedNamePrefix != null)
        {
            writer.namePrefix = originalNamePrefix;
        }

        writer.Exit();
    }

    static SerializeMethod CompileSerializer(Type valueType, SerializableMemberInfo[] memberInfos)
    {
        // SerializeMethod(ref WebSerializerWriter writer, string[] encodedNames, IWebSerializer[]? alternateSerializers, T value, WebSerializerOptions options)
        // foreach(members)
        //   if (value.Foo != null) // reference type || nullable type
        //     if (i != 0) writer.AppendConcatenate()
        //     writer.AppendNamePrefix()
        //     writer.AppendRaw(encodedNames[i])
        //     options.GetRequiredSerializer<T>() || ((IWebSerialzier<T>)alternateSerializers[0] .Serialize(writer, value.Foo, options)

        var argWriter = Expression.Parameter(typeof(WebSerializerWriter).MakeByRefType());
        var argEncodedNames = Expression.Parameter(typeof(string[]));
        var argAlternateSerializers = Expression.Parameter(typeof(IWebSerializer[]));
        var argValue = Expression.Parameter(valueType);
        var argOptions = Expression.Parameter(typeof(WebSerializerOptions));

        var foreachBodies = new List<Expression>();

        var i = 0;
        foreach (var memberInfo in memberInfos)
        {
            var writeBody = new List<Expression>();

            if (i != 0)
            {
                var body2 = Expression.Call(argWriter, ReflectionInfos.WebSerializerWriter_AppendConcatenate);
                writeBody.Add(body2);
            }

            var body3 = Expression.Call(argWriter, ReflectionInfos.WebSerializerWriter_AppendNamePrefix);
            var body4 = Expression.Call(argWriter, ReflectionInfos.WebSerializerWriter_AppendRaw,
                Expression.ArrayIndex(argEncodedNames, Expression.Constant(i, typeof(int))));

            Expression serializer;
            if (memberInfo.WebSerializer == null)
            {
                serializer = Expression.Call(argOptions, ReflectionInfos.WebSerializerOptions_GetRequiredSerializer(memberInfo.MemberType));
            }
            else
            {
                serializer = Expression.Convert(
                    Expression.ArrayIndex(argAlternateSerializers, Expression.Constant(i, typeof(int))),
                    typeof(IWebSerializer<>).MakeGenericType(memberInfo.MemberType));
            }

            var body5 = Expression.Call(serializer, ReflectionInfos.IWebSerializer_Serialize(memberInfo.MemberType), argWriter, memberInfo.GetMemberExpression(argValue), argOptions);

            writeBody.Add(body3);
            writeBody.Add(body4);
            writeBody.Add(body5);

            var bodyBlock = Expression.Block(writeBody);

            if (!memberInfo.MemberType.IsValueType || memberInfo.MemberType.IsNullable())
            {
                var nullExpr = Expression.Constant(null, memberInfo.MemberType);
                var ifBody = Expression.IfThen(Expression.NotEqual(memberInfo.GetMemberExpression(argValue), nullExpr), bodyBlock);
                foreachBodies.Add(ifBody);
            }
            else
            {
                foreachBodies.Add(bodyBlock);
            }

            i++;
        }

        var body = Expression.Block(foreachBodies);
        var lambda = Expression.Lambda<SerializeMethod>(body, argWriter, argEncodedNames, argAlternateSerializers, argValue, argOptions);
        return lambda.Compile();
    }

    internal static class ReflectionInfos
    {
        internal static MethodInfo WebSerializerWriter_AppendConcatenate { get; } = typeof(WebSerializerWriter).GetMethod("AppendConcatenate")!;
        internal static MethodInfo WebSerializerWriter_AppendNamePrefix { get; } = typeof(WebSerializerWriter).GetMethod("AppendNamePrefix")!;
        internal static MethodInfo WebSerializerWriter_AppendRaw { get; } = typeof(WebSerializerWriter).GetMethod("AppendRaw")!;
        internal static MethodInfo WebSerializerOptions_GetRequiredSerializer(Type type) => typeof(WebSerializerOptions).GetMethod("GetRequiredSerializer", 1, Type.EmptyTypes)!.MakeGenericMethod(type);
        internal static MethodInfo IWebSerializer_Serialize(Type type) => typeof(IWebSerializer<>).MakeGenericType(type).GetMethod("Serialize")!;
    }
}

internal sealed class SerializableMemberInfo
{
    public string Name { get; }
    public int Order { get; }
    public IWebSerializer? WebSerializer { get; }
    public Type MemberType { get; }
    public MemberInfo MemberInfo { get; }

    public SerializableMemberInfo(MemberInfo member)
    {
        var dataMember = member.GetCustomAttribute<DataMemberAttribute>();

        MemberInfo = member;
        Name = dataMember?.Name ?? member.Name;
        Order = dataMember?.Order ?? int.MaxValue;

        MemberType = member switch
        {
            PropertyInfo pi => pi.PropertyType,
            FieldInfo fi => fi.FieldType,
            _ => throw new InvalidOperationException()
        };

        var serializerAttr = member.GetCustomAttribute<WebSerializerAttribute>();
        if (serializerAttr != null)
        {
            serializerAttr.Validate(MemberType);
            WebSerializer = (IWebSerializer?)Activator.CreateInstance(serializerAttr.Type);
        }
    }

    public MemberExpression GetMemberExpression(Expression expression)
    {
        if (MemberInfo is FieldInfo fi)
        {
            return Expression.Field(expression, fi);
        }
        else if (MemberInfo is PropertyInfo pi)
        {
            return Expression.Property(expression, pi);
        }
        throw new InvalidOperationException();
    }
}