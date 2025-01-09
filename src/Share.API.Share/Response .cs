namespace Share;

public abstract class Response : IResponse
{
    #region Members
    private List<Error> _errors = [];
    #endregion

    #region Public
    public bool Successful => _errors.Count == 0;
    #endregion

    #region IResponse
    public List<Error> Errors
    {
        get
        {
            return _errors;
        }

        set
        {
            if (value is not null)
                _errors = value;
        }
    }

    public void AddError(Error error)
    {
        if (!_errors.Any(w =>
            CleanContextKey(w.ContextKey).Equals(CleanContextKey(error.ContextKey), StringComparison.CurrentCultureIgnoreCase) &&
            w.Context.Equals(error.Context, StringComparison.CurrentCultureIgnoreCase) &&
            w.Message.Equals(error.Message, StringComparison.CurrentCultureIgnoreCase)))
        {
            _errors.Add(error);
        }
    }

    public void AddError(Exception exception, string context)
    {
        var errors = new List<Error>();
        Exception? internalException = exception;

        while (internalException != null)
        {
            errors.Add(new Error { Message = exception.Message, Context = context });
            internalException = internalException.InnerException;
        }

        AddErrorRange(errors);
    }

    public void AddErrorRange(List<Error> errors)
    {
        foreach (var error in errors)
        {
            AddError(error);
        }
    }
    #endregion

    #region Private
    private static string CleanContextKey(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return "";
        else
            return input;
    }
    #endregion
}