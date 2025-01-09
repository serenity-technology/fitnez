namespace Share;

public interface IOption<TValue>
{
    TValue Value { get; set; }
    string Description { get; set; }
}