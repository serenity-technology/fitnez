namespace Share;

public partial class Responsive : IAsyncDisposable
{
    #region Members
    private readonly IJSRuntime _jsRuntime;
    private Breakpoint _breakpoint = Breakpoint.Small;    
    private IJSObjectReference _responsiveModule = default!;
    #endregion

    #region Constructor
    public Responsive(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    #endregion

    #region Public
    [JSInvokable]
    public void OnResized(BreakpointEventArgs args)
    {
        _breakpoint = args.Breakpoint;
        StateHasChanged();
    }

    public Breakpoint Breakpoint => _breakpoint;
    public bool IsDarkMode { get; set; } = false;
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    #endregion

    #region Override    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _responsiveModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Share.UI/Responsive/Responsive.razor.js");
            if (_responsiveModule is not null)
            {
                var dotnet = DotNetObjectReference.Create(this);
                await _responsiveModule.InvokeVoidAsync("addResize", dotnet);

                IsDarkMode = await _responsiveModule.InvokeAsync<bool>("isDarkMode");
            }
        }
    }
    #endregion

    #region IAsyncDisposable    
    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_responsiveModule is not null)
            {
                await _responsiveModule.InvokeVoidAsync("removeResize");
                await _responsiveModule.DisposeAsync();
            }
        }
        catch (JSDisconnectedException) { }

        GC.SuppressFinalize(this);
    }
    #endregion
}