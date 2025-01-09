namespace Share;

public partial class AlertMessage
{
    #region Public
    [Parameter] public string Message { get; set; } = default!;
    [Parameter] public required AlertType Type { get; set; }
    [Parameter] public List<Error>? Errors { get; set; }
    #endregion

    #region Private
    [CascadingParameter(Name = "alert")] private Alert Alert { get; set; } = default!;

    private string Style()
    {
        var style = new ElementClass();
        style.Add("pl-4 py-3 pr-2 mb-4 rounded-lg");
        style.Add("text-sm font-medium");

        if (Type == AlertType.Success)
            style.Add("bg-green-900 text-green-300");
        else
            style.Add("bg-red-900 text-red-300");

        style.Add("flex flex-col gap-1");

        return style;
    }

    private string ButtonStyle()
    {
        var style = new ElementClass();
        style.Add("p-1 rounded-lg");

        if (Type == AlertType.Success)
            style.Add("hover:bg-green-950 text-green-500");
        else
            style.Add("hover:bg-red-950 text-red-500");
        return style;
    }

    private void OnClose()
    {
        Alert?.RemoveAlert(Id);
    }
    #endregion
}