namespace Fitnez;

public record Measurement
{
    public decimal Height { get; init; }
    public decimal Weight { get; init; }

    public decimal? Neck { get; init; }
    public decimal? Chest { get; init; }
    public decimal? BicepLeft { get; init; }
    public decimal? BicepRight { get; init; }
    public decimal? ForearmLeft { get; init; }
    public decimal? ForearmRight { get; init; }
    public decimal? Waist { get; init; }
    public decimal? Hips { get; init; }
    public decimal? ThighLeft { get; init; }
    public decimal? ThighRight { get; init; }
    public decimal? CalfLeft { get; init; }
    public decimal? CalfRight { get; init; }
}