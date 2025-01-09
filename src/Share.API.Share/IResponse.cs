namespace Share;

public interface IResponse
{
    List<Error> Errors { get; }
    void AddError(Error error);
    void AddError(Exception exception, string context);
    void AddErrorRange(List<Error> errors);
}