namespace Share;

public class EnumOption<TValue> : IOption<TValue> where TValue : Enum
{
    #region IOption
    public TValue Value { get; set; } = default!;
    public string Description { get; set; } = default!;
    #endregion
}