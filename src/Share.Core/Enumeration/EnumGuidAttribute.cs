namespace Share;

[AttributeUsage(AttributeTargets.All)]
public sealed class EnumGuidAttribute : Attribute
{
    #region Members
    private readonly Guid _guid;
    #endregion

    #region Constructor    
    public EnumGuidAttribute(string input)
    {
        if (Guid.TryParse(input, out var guid))
            _guid = guid;
        else
            _guid = Guid.Empty;
    }
    #endregion

    #region Public    
    public Guid Guid => _guid;
    #endregion
}