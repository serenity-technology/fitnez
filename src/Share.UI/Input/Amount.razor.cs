using System.Globalization;

namespace Share;

public partial class Amount
{
    #region Members
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;

    #endregion

    #region Public
    [Parameter] public int DecimalPlaces { get; set; } = 2;
    [Parameter] public decimal Value { get; set; }
    [Parameter] public EventCallback<decimal> ValueChanged { get; set; }
    #endregion

    #region Private    
    private string StringValue
    {
        get { return GetValue(); }
        set { SetValue(value); }
    }

    private string GetValue()
    {
        if (Value != 0m)
            return Value.ToText(DecimalPlaces);
        else
            return string.Empty;
    }

    private void SetValue(string value)
    {
        var amountText = value.Replace(" ", "").Trim();
        if (decimal.TryParse(amountText, NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
            Value = amount;
        else
            Value = 0m;

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

    private string PlaceHolder()
    {
        var value = 0m;
        return value.ToText(DecimalPlaces);
    }
    #endregion

    #region Protected
    protected override void OnInitialized()
    {
        InputForm?.AddInput(this);
    }
    #endregion
}