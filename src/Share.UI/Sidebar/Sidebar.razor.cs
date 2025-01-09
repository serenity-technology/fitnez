namespace Share;

public partial class Sidebar
{
    #region Public
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    #endregion

    #region Private
    private string Style()
    {
        var style = new ElementClass();
        style.Add(Class);
        style.Add("p-1 h-full overflow-y-auto table-scrollbar");
        style.Add("bg-gray-800");
        style.Add("text-sm font-medium");
        style.Add("flex flex-col items-center");

        return style;
    }
    #endregion
}