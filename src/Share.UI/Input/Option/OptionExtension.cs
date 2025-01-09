namespace Share;

public static class OptionExtension
{
    public static List<EnumOption<T>> ToEnumOptions<T>(this IReadOnlyList<EnumItem<T>> items) where T : Enum
    {
        var options = new List<EnumOption<T>>();

        foreach (var item in items)
        {
            options.Add(new EnumOption<T> { Value = item.Value, Description = item.Description! });
        }

        return options;
    }
}