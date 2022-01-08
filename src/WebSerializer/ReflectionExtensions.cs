using System.Reflection;
using System.Runtime.CompilerServices;

namespace Cysharp.Web
{
    internal static class ReflectionExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsAnonymous(this Type type)
        {
            return type.Namespace == null
                   && type.IsSealed
                   && (type.Name.StartsWith("<>f__AnonymousType", StringComparison.Ordinal)
                    || type.Name.StartsWith("<>__AnonType", StringComparison.Ordinal)
                    || type.Name.StartsWith("VB$AnonymousType_", StringComparison.Ordinal))
                   && type.IsDefined(typeof(CompilerGeneratedAttribute), false);
        }

        public static Type? GetImplementedGenericType(this Type type, Type genericTypeDefinition)
        {
            return type.GetInterfaces().FirstOrDefault(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == genericTypeDefinition);
        }
    }
}