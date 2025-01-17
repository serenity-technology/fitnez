namespace Fitnez;

public record Equipment
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
}