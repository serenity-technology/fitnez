namespace Share;

public class EnumItem<TValue>
{
    #region Members
    private readonly TValue _value;
    private readonly string _name;
    private readonly string _description;
    private readonly string _code;
    private readonly Guid _guid;
    #endregion

    #region Constructor    
    public EnumItem(TValue value, string name, string description, string code, Guid guid)
    {
        _value = value;
        _name = name;
        _description = description;
        _code = code;
        _guid = guid;
    }
    #endregion

    #region Public
    public TValue Value => _value;
    public string Name => _name;
    public string Description => _description;
    public string Code => _code;
    public Guid Guid => _guid;
    #endregion
}