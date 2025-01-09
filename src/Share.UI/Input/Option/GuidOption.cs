namespace Share;

public class GuidOption : IOption<Guid>
{
    #region IOption
    public Guid Value { get; set; }
    public string Description { get; set; } = default!;
    #endregion
}