using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebSerializerTests
{
    public class AttributeTest
    {
        [Fact]
        public void Attr1()
        {
            var req = new MyRequest { One = 9, Two = "hoge", ACustomInt = 100, NoMember = MyEnum.Boyo };

            var q = WebSerializer.ToQueryString(req);

            q.Should().Be("tweet.One=9&tweet.twooooo=hoge&tweet.ACustomInt=10000&tweet.NoMember=tako");
        }

        [Fact]
        public void Attr2()
        {
            var req = new Invalid { MyProperty = 10 };
            var req2 = new Valid { MyProperty = 10 };

            var msg = Assert.Throws<InvalidOperationException>(() => WebSerializer.ToQueryString(req));

            var ok = WebSerializer.ToQueryString(req2);

            ok.Should().Be("foobarbaz=100");
        }
    }

    [DataContract(Namespace = "tweet.")]
    public class MyRequest
    {
        [DataMember(Order = 0)]
        public int One { get; set; }

        [DataMember(Name = "twooooo", Order = 1)]
        public string? Two { get; set; }

        public MyEnum NoMember { get; set; }

        [WebSerializer(typeof(CustomFormatterForInt))]
        [DataMember(Order = 2)]
        public int ACustomInt { get; set; }
    }

    [WebSerializer(typeof(CustomFormatterForInt))]
    public class Invalid
    {
        public int MyProperty { get; set; }
    }


    [WebSerializer(typeof(CustomFormatterForValid))]
    public class Valid
    {
        public int MyProperty { get; set; }
    }

    public enum MyEnum
    {
        Foo,

        [EnumMember(Value = "tako")]
        Boyo
    }

    public class CustomFormatterForInt : IWebSerializer<int>
    {
        public void Serialize(ref WebSerializerWriter writer, int value, WebSerializerOptions options)
        {
            writer.GetStringBuilder().Append(value * value);
        }
    }

    public class CustomFormatterForValid : IWebSerializer<Valid>
    {
        public void Serialize(ref WebSerializerWriter writer, Valid value, WebSerializerOptions options)
        {
            writer.EnterAndValidate(options);

            writer.AppendNamePrefix();

            writer.Append("foobarbaz", options);
            writer.AppendEqual();
            writer.Append((value.MyProperty * value.MyProperty).ToString(), options);

            writer.Exit();
        }
    }
}
