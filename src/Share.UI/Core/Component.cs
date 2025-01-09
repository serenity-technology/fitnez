namespace Share;

public abstract class Component : ComponentBase
{
    [Parameter] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Parameter] public string Class { get; set; } = default!;
}