namespace Share;

public static class DateOnlyExtension
{
    public static string ToText(this DateOnly input)
    {
        return input.ToString("dd-MM-yyyy");
    }
}