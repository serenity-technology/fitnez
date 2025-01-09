namespace Share;

public partial class RadioOption<TValue> : Component
{
    #region Public
    [Parameter] public IOption<TValue> Option { get; set; } = default!;
    [Parameter] public bool Checked { get; set; } = false;
    [Parameter] public bool Disabled { get; set; } = false;
    #endregion

    #region Private
    [CascadingParameter(Name = "radioGroup")] private Radio<TValue> RadioGroup { get; set; } = default!;

    private void OnItemSelect()
    {
        RadioGroup.CurrentValue = Option.Value;
    }

    private string InternalInputClass
    {
        get
        {
            var css = new ElementClass();

            if (Disabled)
                css.Add("h-4 w-4 text-gray-400 border-gray-300");
            else
                css.Add("focus:ring-indigo-500 h-4 w-4 text-indigo-600 border-gray-300");

            return css;
        }
    }

    private string InternalLabelClass
    {
        get
        {
            var css = new ElementClass();

            if (Disabled)
                css.Add("ml-3 block text-sm font-medium text-gray-400");
            else
                css.Add("ml-3 block text-sm font-medium text-gray-500");

            return css;
        }
    }
    #endregion
}