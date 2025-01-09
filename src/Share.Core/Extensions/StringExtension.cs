using System.Net;
using System.Security;

namespace Share;

public static class StringExtension
{
    public static SecureString ToSecure(this string input)
    {
        var credential = new NetworkCredential("", input);
        return credential.SecurePassword;
    }

    public static string ToUnsecured(this SecureString secureInput)
    {
        var credential = new NetworkCredential("", secureInput);
        return credential.Password;
    }

    public static string ToSplitCamelCase(this string input)
    {
        if (input.Length > 0)
        {
            var result = new List<char>();
            var array = input.ToCharArray();
            var uppercaseCount = array.Count(w => char.IsUpper(w));

            if (array.Length != uppercaseCount)
            {
                foreach (var item in array)
                {
                    if (uppercaseCount == 0 && result.Count == 0)
                        result.Add(char.ToUpper(item));
                    else
                    {
                        if (char.IsUpper(item))
                            result.Add(' ');

                        result.Add(item);
                    }
                }

                return new string(result.ToArray()).Trim();
            }
        }

        return input;
    }
}