namespace Share;

public partial class Form
{
    #region Members
    private readonly List<IInput> _inputs = [];
    [CascadingParameter(Name = "alert")] private Alert Alert { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;

    public void AddInput(IInput component)
    {
        _inputs.Add(component);
    }

    public void RemoveInput(IInput component)
    {
        _inputs.Remove(component);
    }

    public void ShowErrors(string heading, List<Error> errors)
    {
        foreach (var component in _inputs)
        {
            List<string> messages = errors.Where(w => w.Context.Equals(component.Id, StringComparison.CurrentCultureIgnoreCase)).Select(s => s.Message).ToList();
            if (messages.Count > 0)
                component.ShowErrors(messages);
        }

        errors.RemoveAll(w => _inputs.Any(a => a.Id.Equals(w.Context, StringComparison.CurrentCultureIgnoreCase)));
        Alert?.ShowError(heading, errors);
    }

    public void ClearErrors()
    {
        foreach (var component in _inputs)
            component.ClearErrors();
    }
    #endregion

    #region Private
    private string Style()
    {
        var style = new ElementClass();
        style.Add("p-1 overflow-y-auto scrollbar-background");
        style.Add(Class);

        return style;
    }
    #endregion
}