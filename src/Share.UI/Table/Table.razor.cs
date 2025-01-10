namespace Share;

public partial class Table<TRowData>
{
    #region Members
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference _scrollbarModule = default!;
    private ElementReference _bodyElement = default!;
    #endregion

    #region Constructor
    public Table(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    #endregion

    #region Public
    [Parameter] public RenderFragment Header { get; set; } = default!;
    [Parameter] public RenderFragment<TRowData> Body { get; set; } = default!;
    [Parameter] public IEnumerable<TRowData> Rows { get; set; } = default!;
    [Parameter] public RenderFragment Footer { get; set; } = default!;
    #endregion

    #region Private
    private string ScrollbarWith { get; set; } = "padding-right: 0px";
    #endregion

    #region Protected
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _scrollbarModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Share.UI/Table/Table.razor.js");
            var with = await _scrollbarModule.InvokeAsync<int>("scrollbarWith", _bodyElement);

            ScrollbarWith = $"width: {with}px";
            StateHasChanged();
        }
    }
    #endregion
}