namespace Share;

public partial class Confirm<TValue>
{
    #region Members    
    private string _heading = "Please confirm...";
    private string _message = default!;
    private TValue _value = default!;
    private bool _visible = false;
    #endregion

    #region Public
    [Parameter] public bool AllowCancel { get; set; } = false;
    [Parameter] public EventCallback<TValue> OnConfirmYes { get; set; }
    [Parameter] public EventCallback<TValue> OnConfirmNo { get; set; }
    [Parameter] public EventCallback<TValue> OnConfirmCancel { get; set; }

    public void Show(TValue value, string message, string? heading = null)
    {
        _value = value;
        _message = message;

        if (!string.IsNullOrEmpty(heading))
            _heading = heading;

        _visible = true;
    }
    #endregion

    #region Private
    private void OnClickYes()
    {
        _visible = false;
        OnConfirmYes.InvokeAsync(_value);
    }

    private void OnClickNo()
    {
        _visible = false;
        OnConfirmNo.InvokeAsync(_value);
    }

    private void OnClickCancel()
    {
        if (AllowCancel)
        {
            _visible = false;
            OnConfirmCancel.InvokeAsync(_value);
        }
    }

    private string Style()
    {
        var style = new ElementClass();
        style.Add("fixed inset-0 bg-gray-950 bg-opacity-75 transition-opacity");

        return style;
    }
    #endregion
}