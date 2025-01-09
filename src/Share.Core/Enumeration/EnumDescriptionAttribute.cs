namespace Share;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumDescriptionAttribute(string description) : Attribute
{
    #region Members
    private readonly string _description = description;
    #endregion
    
    #region Public
    public string Description => _description;
    #endregion
}