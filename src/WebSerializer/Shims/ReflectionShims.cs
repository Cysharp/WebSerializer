#if NETSTANDARD2_0

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cysharp.Web;

internal static class ReflectionShims
{
    internal static MethodInfo? GetMethod(this Type type, string name, int genericParameterCount, Type[] types)
    {
        return type.GetMethods().FirstOrDefault(x =>
        {
            var genericArguments = x.GetGenericArguments();

            if (x.Name != name) return false;
            if (genericArguments.Length != genericParameterCount) return false;

            // Empty is ok.
            if (types.Length == 0) return true;

            // currently WebSerializer only uses Type.EmptyTypes so not implement it.
            throw new NotSupportedException();
        });
    }
}

#endif