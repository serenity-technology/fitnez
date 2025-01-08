namespace Share;

public interface IRule
{
    Task EvaluateAsync(CancellationToken cancellationToken = default);
    bool IsValid { get; }
    Error? Error { get; set; }
}