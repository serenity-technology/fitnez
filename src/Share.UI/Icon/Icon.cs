namespace Share;

public abstract class Icon : ComponentBase
{
    [Parameter] public string Class { get; set; } = default!;
}