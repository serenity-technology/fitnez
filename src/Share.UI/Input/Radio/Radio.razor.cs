namespace Share;

public partial class Radio<TValue> : Input
{
    #region Members
    [CascadingParameter(Name = "form")] private Form InputForm { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public IEnumerable<IOption<TValue>> Options { get; set; } = default!;

    public TValue CurrentValue
    {
        get => Value;
        set
        {
            var hasChanged = !EqualityComparer<TValue>.Default.Equals(value, Value);
            if (hasChanged)
            {
                Value = value;
                _ = ValueChanged.InvokeAsync(value);
            }
        }
    }
    #endregion

    #region Private
    private string InternalLabelClass
    {
        get
        {
            var css = new ElementClass();

            if (Disabled)
                css.Add("text-sm font-medium text-gray-700");
            else
                css.Add("text-sm font-medium text-gray-400");

            return css;
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