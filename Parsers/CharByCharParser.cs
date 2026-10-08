namespace laba03.Parsers;

using System.Text;
using laba03.Models;

public sealed class CharByCharParser : ITextParser
{
    private static readonly char[] EndOfSentence = { '.', '!', '?', '…' };

    public Text Parse(string content)
    {
        ArgumentNullException.ThrowIfNull(content);

        var text = new Text();
        var sentence = new Sentence();
        var buffer = new StringBuilder();

        void FlushWord()
        {
            if (buffer.Length > 0)
            {
                sentence.Add(new Word(buffer.ToString()));
                buffer.Clear();
            }
        }

        foreach (char c in content)
        {
            if (char.IsLetter(c))
            {
                buffer.Append(c);
                continue;
            }

            FlushWord();

            if (EndOfSentence.Contains(c))
            {
                sentence.Add(new Punctuation(c.ToString()));
                if (sentence.Tokens.Count > 0)
                    text.Add(sentence);
                sentence = new Sentence();
            }
            else if (!char.IsWhiteSpace(c))
            {
                sentence.Add(new Punctuation(c.ToString()));
            }
        }

        FlushWord();
        if (sentence.Tokens.Count > 0)
            text.Add(sentence);

        return text;
    }
}
