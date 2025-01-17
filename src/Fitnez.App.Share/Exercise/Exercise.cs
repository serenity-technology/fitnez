namespace Fitnez.Exercise;

public record Exercise
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public ExerciseType Type { get; init; }
    public Guid? EquipmentId { get; init; }
    public string? Note { get; init; }
    public List<BodyPart> BodyParts { get; init; } = default!;
}