namespace Share;

public partial class TextArea
{
    #region Members
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public string Value { get; set; } = "";
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    #endregion

    #region Private        
    private void OnValueChange(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? "";
        _ = ValueChanged.InvokeAsync(Value);
    }
    private string LabelText()
    {
        //if (Optional) return $"{Label} - Optional";
        //else return Label;

        return Label;
    }
    #endregion

    #region Protected
    protected override void OnInitialized()
    {
        InputForm?.AddInput(this);
    }
    #endregion
}