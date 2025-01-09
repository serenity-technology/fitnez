namespace Share;

public abstract class Input : Component, IInput, IDisposable
{
    #region Members
    private readonly List<string> _errors = [];
    private bool _disposed = false;
    #endregion

    #region Public
    [Parameter] public string Label { get; set; } = default!;
    #endregion

    #region IInput
    [Parameter] public bool Busy { get; set; }
    [Parameter] public bool Disabled { get; set; }

    public void ShowErrors(List<string> errors)
    {
        _errors.Clear();
        _errors.AddRange(errors);
    }

    public void ClearErrors()
    {
        _errors.Clear();
    }
    #endregion

    #region Protected
    [CascadingParameter(Name = "form")] protected Form Form { get; set; } = default!;
    protected bool HasErrors => _errors.Count != 0;
    protected List<string> Errors => _errors;

    protected string ErrorMessage()
    {
        if (HasErrors)
            return Errors[0];
        else
            return string.Empty;
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            Form?.RemoveInput(this);

        _disposed = true;
    }
    #endregion
}