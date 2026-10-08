namespace laba03.Models;

public sealed class Word : Token
{
    private static readonly char[] RussianConsonants =
        "бвгджзйклмнпрстфхцчшщ".ToCharArray();

    private static readonly char[] EnglishConsonants =
        "bcdfghjklmnpqrstvwxyz".ToCharArray();

    public Word(string value) : base(value) { }

    public int Length => Value.Length;

    public bool StartsWithConsonant()
    {
        if (string.IsNullOrEmpty(Value)) return false;
        char first = char.ToLowerInvariant(Value[0]);
        return RussianConsonants.Contains(first) || EnglishConsonants.Contains(first);
    }

    public bool IsStopWord(ISet<string> stopWords)
        => stopWords.Contains(Value.ToLowerInvariant());
}
