namespace Share;

public partial class PageAction
{
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
}
