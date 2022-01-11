using System.Text;

namespace Cysharp.Web;

internal static class CollectionHelper
{
    public static string? GetLatestName(StringBuilder sb)
    {
        // foo=
        // ?foo=
        // &foo=
        if (sb.Length != 0 && sb[^1] == '=')
        {
            var lastIndex = -1;
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (sb[i] is '?' or '&')
                {
                    lastIndex = i + 1;
                    break;
                }
                else if (i == 0)
                {
                    lastIndex = i;
                    break;
                }
            }

            if (lastIndex != -1)
            {
                var ok = sb.ToString().Substring(lastIndex, sb.Length - lastIndex - 1);
                return ok + "=";
            }
        }

        return null;
    }
}