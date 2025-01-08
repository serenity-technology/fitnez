namespace Share;

public record Error
{
    public required string Message { get; init; }
    public required string Context { get; init; }
    public string? ContextKey { get; init; }
}