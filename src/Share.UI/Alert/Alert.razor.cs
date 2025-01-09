using System.Timers;
using Timer = System.Timers.Timer;

namespace Share;

public partial class Alert : IDisposable
{
    #region Members
    private readonly List<AlertItem> _alerts = [];
    private readonly Timer _timer = new() { Interval = 800 };
    private bool _disposed = false;
    #endregion

    #region Constructor
    public Alert()
    {
        _timer.Elapsed += TimerElapsed!;
    }
    #endregion

    #region Public
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;

    public void ShowSuccess(string message)
    {
        var alertItem = new AlertItem
        {
            Id = Guid.NewGuid().ToString(),
            Type = AlertType.Success,
            Message = message,
            CloseBy = DateTime.Now.AddSeconds(2)
        };

        _alerts.Add(alertItem);
        StateHasChanged();
    }

    public void ShowError(string message, List<Error>? errors = null)
    {
        var alertItem = new AlertItem
        {
            Id = Guid.NewGuid().ToString(),
            Type = AlertType.Error,
            Message = message,
            CloseBy = DateTime.Now.AddDays(1),
            Errors = errors
        };

        _alerts.Add(alertItem);
        StateHasChanged();
    }

    public void RemoveAlert(string id)
    {
        var deleted = _alerts.RemoveAll(w => w.Id.Equals(id));
        if (deleted > 0)
            InvokeAsync(() => { StateHasChanged(); });
    }
    #endregion

    #region Private
    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        var deleted = _alerts.RemoveAll(w => w.CloseBy < DateTime.Now);
        if (deleted > 0)
            InvokeAsync(() => { StateHasChanged(); });
    }
    #endregion

    #region IDisposable            
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _timer.Stop();
            _timer.Elapsed -= TimerElapsed!;
            _timer.Dispose();
        }

        _disposed = true;
    }
    #endregion

    #region Override
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            _timer.Start();
    }
    #endregion
}
