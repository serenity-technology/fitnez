namespace Fitnez;

public record PersonRead : Person
{
    public PersonStatus Status { get; init; }
}