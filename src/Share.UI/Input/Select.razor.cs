using System.Security.Cryptography;

namespace Share;

public partial class Select<TValue> : Input
{
    #region Members
    private readonly IJSRuntime _jsRuntime;
    private bool _showDropdown = false;
    private IJSObjectReference _jsModule = default!;
    private ElementReference _reference = default!;
    private ElementReference _floating = default!;
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Constructor
    public Select(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    #endregion

    #region Public
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public IEnumerable<IOption<TValue>> Options { get; set; } = default!;
    #endregion

    #region Private
    private string StringValue
    {
        //get { return GetValue(); }
        //set { SetValue(value); }
        get; set;
    }

    private string GetValue()
    {
        if (Value != null)
        {
            return Value.ToString()!;
        }
        else
        {
            return string.Empty;
        }
    }

    private void SetValue(string value)
    {
        var underlyingType = Nullable.GetUnderlyingType(typeof(TValue));
        if (underlyingType != null && value != null)
        {
            Value = default!;
        }
        else
        {
            switch (typeof(TValue))
            {
                case var _ when typeof(TValue) == typeof(string):
                    Value = (TValue)(object)value!;
                    break;
                case var _ when typeof(TValue).IsEnum:
                    if (Enum.TryParse(typeof(TValue), value, out var enumValue))
                        Value = (TValue)(object)enumValue!;
                    else
                        Value = default!;
                    break;
                case var _ when typeof(TValue) == typeof(int):
                    if (int.TryParse(value, out var number))
                        Value = (TValue)(object)number;
                    else
                        Value = default!;
                    break;
                case var _ when typeof(TValue) == typeof(Guid):
                    if (Guid.TryParse(value, out var guid))
                        Value = (TValue)(object)guid;
                    else
                        Value = default!;
                    break;
                default:
                    Value = default!;
                    break;
            }
        }

        _ = ValueChanged.InvokeAsync(Value);
    }

    private void OnValueChange(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? "";
        //SetValue(value);
    }

    private string LabelText()
    {
        return Label;
    }

    private void ToggleShowDropdown()
    {
        _showDropdown = !_showDropdown;
    }

    private void SelectOption(IOption<TValue> option)
    {
        _showDropdown = false;
        StringValue = option.Description;
        Value = option.Value;
    }

    private void OnFocusLeft()
    {
        _showDropdown = false;
    }

    private void OnClick()
    {
        _showDropdown = true;        
    }

    private void OnEnterKey(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
            _showDropdown = true;
    }
    #endregion

    #region Protected
    protected override void OnInitialized()
    {
        InputForm?.AddInput(this);
    }
    #endregion

    #region Protected
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Fitnez.UI/Input/Select.razor.js");
        }

        if (_showDropdown && _jsModule is not null)
        {
            await _jsModule.InvokeVoidAsync("Show", _reference, _floating);
        }

        if (_showDropdown)
            await _reference.FocusAsync();
    }
    #endregion
}