namespace Share;

public partial class TableRowMenu
{
    #region Members
    private readonly IJSRuntime _jsRuntime;
    private bool _show = false;    
    private IJSObjectReference _jsModule = default!;
    private ElementReference _referenceElement = default!;
    private ElementReference _floatingElement = default!;
    #endregion

    #region Constructor
    public TableRowMenu(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    #endregion

    #region Public
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;

    public void ToggleShow()
    {
        _show = !_show;
    }
    #endregion

    #region Private
    private string Style()
    {
        var style = new ElementClass();
        style.Add("hover:text-orange-400");

        if (_show)
            style.Add("text-orange-400");

        return style;
    }
    #endregion

    #region Protected
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Share.UI/Table/TableRowMenu.razor.js");
        }

        if (_show && _jsModule is not null)
        {
            await _jsModule.InvokeVoidAsync("Show", _referenceElement, _floatingElement);
        }
    }
    #endregion
}