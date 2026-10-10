namespace laba03.Models;


public sealed class ConcordanceEntry
{
    public string Word { get; }
    public int Frequency { get; }
    public IReadOnlyList<int> LineNumbers { get; }

    public ConcordanceEntry(string word, int frequency, IReadOnlyList<int> lineNumbers)
    {
        Word = word;
        Frequency = frequency;
        LineNumbers = lineNumbers;
    }

    public override string ToString()
        => $"{Word}. {Frequency}:{string.Concat(LineNumbers)}";
}