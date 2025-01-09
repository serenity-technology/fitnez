using System.Reflection;

namespace Share;

public static class Enums
{
    public static string Description(this Enum enumValue)
    {
        string desc = "";

        try
        {
            var descAtt = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<EnumDescriptionAttribute>()?
                .Description;

            desc = descAtt ?? enumValue.ToString().ToSplitCamelCase();
        }
        catch { }

        return desc;
    }

    public static string Code(this Enum enumValue)
    {
        string desc = "";

        try
        {
            var code = enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<EnumCodeAttribute>()?
                        .Code;

            desc = code ?? string.Empty;
        }
        catch { }

        return desc;
    }

    public static Guid Guid(this Enum enumValue)
    {
        Guid guid = System.Guid.Empty;

        try
        {
            var guidValue = enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<EnumGuidAttribute>()?
                    .Guid;

            if (guidValue != null)
            {
                guid = guidValue.Value;
            }
        }
        catch { }

        return guid;
    }

    public static T ParseFromCode<T>(string code) where T : Enum
    {
        var enumType = typeof(T);
        var enums = Enum.GetValues(enumType)
                .Cast<int>()
                .Select(e => Enum.ToObject(enumType, e))
                .Select(s => (Enum)s)
                .Where(w => w.Code().Equals(code, StringComparison.CurrentCultureIgnoreCase))
                .Select(s => s.ToString())
                .ToList();

        if (enums.Count > 1)
            throw new Exception("Could not parse enum, to many matching Codes");

        if (enums.Count == 0)
            throw new Exception("Could not parse enum from Code");

        var enumName = enums.First();
        return (T)Enum.Parse(enumType, enumName);
    }

    public static T ParseFromGuid<T>(Guid guid) where T : Enum
    {
        var enumType = typeof(T);
        var enums = Enum.GetValues(enumType)
                .Cast<int>()
                .Select(e => Enum.ToObject(enumType, e))
                .Select(s => (Enum)s)
                .Where(w => w.Guid() == guid)
                .Select(s => s.ToString())
                .ToList();

        if (enums.Count > 1)
            throw new Exception("Could not parse enum, to many matching Guids");

        if (enums.Count == 0)
            throw new Exception("Could not parse enum from Guid");

        var enumName = enums.First();
        return (T)Enum.Parse(enumType, enumName);
    }

    public static T ParseFromDescription<T>(string description) where T : Enum
    {
        var enumType = typeof(T);
        var enums = Enum.GetValues(enumType)
                .Cast<int>()
                .Select(e => Enum.ToObject(enumType, e))
                .Select(s => (Enum)s)
                .Where(w => w.Description() == description)
                .Select(s => s.ToString())
                .ToList();

        if (enums.Count > 1)
            throw new Exception("Could not parse enum, to many matching Descriptions");

        if (enums.Count == 0)
            throw new Exception("Could not parse enum from Description");

        var enumName = enums.First();
        return (T)Enum.Parse(enumType, enumName);
    }

    public static IReadOnlyList<EnumItem<T>> ToList<T>()
    {
        var tmpEnum = default(T)!;
        if (tmpEnum != null && tmpEnum.GetType().IsEnum)
        {
            var enumType = typeof(T);

            return [.. Enum.GetValues(enumType)
                .Cast<int>()
                .Select(e => new { value = Enum.ToObject(enumType, e) })
                .Select(s => (Enum)s.value)
                .Select(s => new EnumItem<T>((T)(object)s, s.ToString(), s.Description(), s.Code(), s.Guid()))
                .OrderBy(o => o.Value)];
        }
        else
        {
            return [];
        }
    }
}