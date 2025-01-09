namespace Share;

public partial class TableRowButton
{
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
}