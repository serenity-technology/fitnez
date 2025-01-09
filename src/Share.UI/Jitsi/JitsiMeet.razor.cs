namespace Share;

public partial class JitsiMeet
{
    #region Members
    private IJSObjectReference _module = default!;
    private ElementReference _bodyElement = default!;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    #endregion

    #region Override    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Share.UI/Jitsi/JitsiMeet.razor.js");
            if (_module is not null)
            {
                await _module.InvokeVoidAsync("setup", _bodyElement);
            }
        }
    }
    #endregion
}