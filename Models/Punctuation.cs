namespace laba03.Models;

public sealed class Punctuation : Token
{
    public Punctuation(string value) : base(value) { }

    public bool IsEndOfSentence =>
        Value is "." or "!" or "?" or "…";

    public bool IsQuestionMark => Value == "?";
    public bool IsExclamationMark => Value == "!";
}