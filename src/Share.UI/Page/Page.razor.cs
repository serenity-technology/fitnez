namespace Share;

public partial class Page
{
    #region Members
    private bool _isBusy = false;
    #endregion

    #region Public
    [Parameter] public string Title { get; set; } = default!;
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public RenderFragment Body { get; set; } = default!;
    [Parameter] public RenderFragment ActionBar { get; set; } = default!;

    public void IsBusy()
    {
        _isBusy = true;
        StateHasChanged();
    }

    public void IsReady()
    {
        _isBusy = false;
        StateHasChanged();
    }
    #endregion    
}