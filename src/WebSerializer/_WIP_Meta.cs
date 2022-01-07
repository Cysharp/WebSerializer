using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Cysharp.Web
{
    public class MetaType<T>
    {
        MetaMember[]? members;

        public MetaType()
        {
            var props = typeof(T).GetProperties();

            var fields = typeof(T).GetFields();

            foreach (var item in props)
            {

            }
        }

        public IWebSerializer<T> CreateSerializer()
        {
            if (RuntimeFeature.IsDynamicCodeCompiled)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }



        }

        void EmitCompiled(StringBuilder stringBuilder, IWebSerializerProvider provider, ref T value)
        {
            // foreach(members)
            //   if (options.PrefixName != null) writer.Write(writer, options.PrefixName)
            //   writer.Write(.EncodedName)
            //   writer.Write('=')
            //   provider.GetSerializer<T>().Serialize(writer, options, ref value.Foo);

        }

        void EmitReflection(StringBuilder stringBuilder, IWebSerializerProvider provider, ref T value)
        {
            if (members == null) return;

            var encoder = UrlEncoder.Default; // TODO: Get from options.

            var writer = new StringWriter(stringBuilder);

            foreach (var member in members)
            {
                //encoder.Encode(member.Name)

                //stringBuilder.Append(

                //writer.Write(writer, member.Name);
                writer.Write('=');



                var serializer = provider.GetSerializer<int>();
                // serializer.Serialize(stringBuilder, provider, ref member.GetValue(




                // encoder.Encode(writer, 

                // encoder.Encode(member.na
            }


        }
    }

    public class MetaMember
    {
        public string Name { get; set; } = default!;


        public string EncodedName { get; set; } = default!;

        public string EncodedNamePlusEqual { get; set; } = default!;


        public Type DeclaringType { get; } = default!;
        PropertyInfo? propertyInfo;
        FieldInfo? fieldInfo;


        public object GetValue(object instance)
        {
            var guid = Guid.NewGuid();
            var sb = new StringBuilder();
            //OperationStatus
            throw new NotImplementedException();
        }



        void CreateEmitExpressionTree()
        {
            //  var sb = writer.GetStringBuilder();
            //  if (options.PrefixName != null) sb.Append(options.PrefixName)
            //  sb.Append(.EncodedNamePlusEqual)
            //  options.GetSerializer<T>().Serialize(writer, options, ref value.Foo);


        }

    }
}
