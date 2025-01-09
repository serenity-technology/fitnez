using System.Globalization;

namespace Share;

public partial class Number
{
    #region Members
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public int Value { get; set; }
    [Parameter] public EventCallback<int> ValueChanged { get; set; }
    #endregion

    #region Private    
    private string StringValue
    {
        get { return GetValue(); }
        set { SetValue(value); }
    }

    private string GetValue()
    {
        if (Value != 0)
            return Value.ToString(CultureInfo.InvariantCulture);
        else
            return string.Empty;
    }

    private void SetValue(string value)
    {
        var amountText = value.Replace(" ", "").Trim();
        if (int.TryParse(amountText, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            Value = number;
        else
            Value = 0;

        _ = ValueChanged.InvokeAsync(Value);
    }

    private void OnValueChange(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? "";
        SetValue(value);
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