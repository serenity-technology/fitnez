namespace Share;

public abstract class Rule
{
    public bool IsValid { get; protected set; }
    public Error? Error { get; set; }
}