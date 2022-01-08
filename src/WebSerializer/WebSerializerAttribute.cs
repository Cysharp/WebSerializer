namespace Cysharp.Web;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false)]
public sealed class WebSerializerAttribute : Attribute
{
    public Type Type { get; }

    public WebSerializerAttribute(Type type)
    {
        Type = type;
    }

    internal void Validate(Type targetType)
    {
        var serializerType = Type.GetImplementedGenericType(typeof(IWebSerializer<>));
        if (serializerType == null)
        {
            throw new InvalidOperationException($"Type is not implemented IWebSerializer<T>, Type:{Type.FullName}");
        }

        var attrType = serializerType.GenericTypeArguments[0];
        if (attrType != targetType)
        {
            throw new InvalidOperationException($"Attribute WebSerializer type is not same as target type. AttrType:{attrType.FullName} TargetType:{targetType.FullName}");
        }
    }
}