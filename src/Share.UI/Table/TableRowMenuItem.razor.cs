namespace Share;

public partial class TableRowMenuItem
{
    #region Members
    [CascadingParameter(Name = "tableRowMenu")] private TableRowMenu Menu { get; set; } = default!;
    #endregion

    #region Public
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    #endregion

    #region Private
    private void OnItemClick()
    {
        Menu.ToggleShow();
        OnClick.InvokeAsync();
    }
    #endregion
}