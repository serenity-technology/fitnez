using System.Globalization;

namespace Share;

public static class DecimalExtension
{
    public static string ToText(this decimal input, int decimals)
    {
        var formatInfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = ".",
            NumberGroupSeparator = " ",
            NumberDecimalDigits = decimals
        };

        return input.ToString("N", formatInfo);
    }
}