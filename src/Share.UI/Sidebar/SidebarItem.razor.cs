namespace Share;

public partial class SidebarItem
{
    #region Public
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public string Uri { get; set; } = default!;
    #endregion

    #region Private
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    [CascadingParameter(Name = "responsive")] private Responsive Responsive { get; set; } = default!;
    [CascadingParameter(Name = "sideBar")] private Sidebar SideBar { get; set; } = default!;

    private void OnOpenSelect()
    {
        //SideBar.ToggleMenu();
        Navigation.NavigateTo(Uri);
    }

    private string Style()
    {
        var style = new ElementClass();
        style.Add(Class);
        style.Add("p-2 w-full");
        style.Add("text-gray-400 hover:text-gray-300");
        style.Add("hover:bg-gray-900");
        style.Add("rounded-md cursor-pointer");

        //if (Responsive.Breakpoint == Breakpoint.Small)
        //    style.Add("flex items-center");
        //else
            style.Add("flex flex-col items-center");

        return style;
    }
    #endregion
}