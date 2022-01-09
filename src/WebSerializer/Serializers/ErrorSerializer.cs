using System.Runtime.ExceptionServices;

namespace Cysharp.Web.Serializers
{
    public sealed class ErrorSerializer<T> : IWebSerializer<T>
    {
        readonly ExceptionDispatchInfo exception;

        public ErrorSerializer(Exception exception)
        {
            this.exception = ExceptionDispatchInfo.Capture(exception);
        }

        public void Serialize(ref WebSerializerWriter writer, T value, WebSerializerOptions options)
        {
            exception.Throw();
        }
    }

    public static class ErrorSerializer
    {
        public static IWebSerializer Create(Type type, Exception exception)
        {
            return (IWebSerializer)Activator.CreateInstance(typeof(ErrorSerializer<>).MakeGenericType(type), exception)!;
        }
    }
}