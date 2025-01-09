namespace Share;

public record AlertItem
{
    public required string Id { get; init; }
    public required AlertType Type { get; init; }
    public required string Message { get; init; }
    public required DateTime CloseBy { get; init; }
    public List<Error>? Errors { get; init; }
}