namespace Share;

public partial class TableRow<TRowData>
{
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public TRowData Data { get; set; } = default!;
}