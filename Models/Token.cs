namespace laba03.Models;

public abstract class Token
{
    public string Value { get; }

    protected Token(string value)
    {
        Value = value ?? throw new System.ArgumentNullException(nameof(value));
    }

    public override string ToString() => Value;
}
