namespace laba03.Parsers;

using System.Text.RegularExpressions;
using laba03.Models;

public sealed class RegexParser : ITextParser
{
    private static readonly Regex TokenRegex = new(
        @"[A-Za-zА-Яа-яЁё]+|[^\sA-Za-zА-Яа-яЁё]",
        RegexOptions.Compiled);

    public Text Parse(string content)
    {
        ArgumentNullException.ThrowIfNull(content);

        var text = new Text();
        var sentence = new Sentence();

        foreach (Match match in TokenRegex.Matches(content))
        {
            string value = match.Value;

            if (char.IsLetter(value[0]))
            {
                sentence.Add(new Word(value));
            }
            else
            {
                sentence.Add(new Punctuation(value));

                if (value is "." or "!" or "?" or "…")
                {
                    if (sentence.Tokens.Count > 0)
                        text.Add(sentence);
                    sentence = new Sentence();
                }
            }
        }

        if (sentence.Tokens.Count > 0)
            text.Add(sentence);

        return text;
    }
}
