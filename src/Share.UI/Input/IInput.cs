namespace Share;

public interface IInput
{
    string Id { get; set; }
    bool Busy { get; set; }
    bool Disabled { get; set; }
    void ShowErrors(List<string> errors);
    void ClearErrors();
}