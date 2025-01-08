namespace Share;

public class Result<TValue>
{
    #region Constructors
    public Result()
    { }

    public Result(TValue value, Error error)
    {
        Value = value;
        Error = error;
    }

    public Result(TValue value)
    {
        Value = value;
    }

    public Result(Error error)
    {
        Error = error;
    }

    public Result(string message, string context, string? contextKey = null)
    {
        Error = new Error { Context = context, Message = message, ContextKey = contextKey };
    }
    #endregion

    #region Public
    public TValue? Value { get; private set; } = default!;
    public Error? Error { get; private set; } = default!;
    public bool Successful => Error == null;
    #endregion
}