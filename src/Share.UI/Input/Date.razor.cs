using System.Globalization;

namespace Share;

public partial class Date
{
    #region Members
    private const string _dateFormat = "yyyy-MM-dd";
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public DateOnly? Value { get; set; }
    [Parameter] public EventCallback<DateOnly?> ValueChanged { get; set; }
    #endregion

    #region Private    
    private string StringValue
    {
        get { return GetValue(); }
        set { SetValue(value); }
    }

    private string GetValue()
    {
        if (Value != null)
            return Value.Value.ToText();
        return
            string.Empty;
    }

    private void SetValue(string value)
    {
        if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            Value = date;
        else
            Value = DateOnly.FromDateTime(DateTime.Today);

        _ = ValueChanged.InvokeAsync(Value.Value);
    }

    private void OnValueChange(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? DateTime.Today.ToString(_dateFormat, CultureInfo.InvariantCulture);
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