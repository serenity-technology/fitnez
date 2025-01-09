namespace Share;

public partial class SideMenuItem
{
    #region Members
    private readonly NavigationManager _navigation;

    [CascadingParameter(Name = "responsive")] private Responsive Responsive { get; set; } = default!;
    [CascadingParameter(Name = "sideBar")] private SideMenu SideMenu { get; set; } = default!;
    #endregion

    #region Constructor
    public SideMenuItem(NavigationManager navigation)
    {
        _navigation = navigation;
    }
    #endregion

    #region Public
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public string Uri { get; set; } = default!;
    #endregion

    #region Private
    private void OnOpenSelect()
    {
        _navigation.NavigateTo(Uri);
    }
    #endregion
}