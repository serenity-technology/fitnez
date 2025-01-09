namespace Share;

public partial class Checkbox
{
    #region Members
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public bool Value { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }
    #endregion

    #region Private
    private void OnValueChange(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? "";
        if (bool.TryParse(value, out var checkedValue))
        {
            Value = checkedValue;
        }
        else
            Value = false;

        _ = ValueChanged.InvokeAsync(Value);
    }

    private void ToggleChecked()
    {
        if (!Disabled)
        {
            Value = !Value;
            _ = ValueChanged.InvokeAsync(Value);
        }
    }
    #endregion

    #region Protected
    protected override void OnInitialized()
    {
        InputForm?.AddInput(this);
    }
    #endregion
}