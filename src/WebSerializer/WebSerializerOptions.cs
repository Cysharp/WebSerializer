using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cysharp.Web;

public record WebSerializerOptions(IWebSerializerProvider Provider)
{
    public IWebSerializer<T>? GetSerializer<T>()
    {
        return Provider.GetSerializer<T>();
    }

    public IWebSerializer<T> GetRequiredSerializer<T>()
    {
        var serializer = Provider.GetSerializer<T>();
        if (serializer == null) throw new NotImplementedException(); // TODO
        return serializer;
    }
}

