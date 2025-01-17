namespace Fitnez;

public record WorkoutExercise
{
    public Guid ExerciseId { get; init; }
    public int Sets { get; init; }
    public int Reps { get; init; }
    public int Rest {  get; init; }
}