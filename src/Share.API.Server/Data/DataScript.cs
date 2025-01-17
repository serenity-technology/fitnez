namespace Share;

public record DataScript
{
    public required int No {  get; init; }
    public required string Name { get; init; }
    public required string Sql { get; init; }
}