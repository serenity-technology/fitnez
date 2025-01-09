namespace Share;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumCodeAttribute(string code) : Attribute
{
    #region Members
    private readonly string _code = code;
    #endregion
    
    #region Public
    public string Code => _code;
    #endregion
}