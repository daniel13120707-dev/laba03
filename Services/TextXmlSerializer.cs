namespace laba03.Services;

using System.Xml.Serialization;
using laba03.Models;

[XmlRoot("Text")]
public class TextDto
{
    [XmlElement("Sentence")]
    public List<SentenceDto> Sentences { get; set; } = new();
}

public class SentenceDto
{
    [XmlElement("Word", typeof(WordDto))]
    [XmlElement("Punctuation", typeof(PunctuationDto))]
    public List<TokenDto> Tokens { get; set; } = new();
}

[XmlInclude(typeof(WordDto))]
[XmlInclude(typeof(PunctuationDto))]
public abstract class TokenDto
{
    [XmlAttribute("value")]
    public string Value { get; set; } = string.Empty;
}

public class WordDto : TokenDto { }

public class PunctuationDto : TokenDto { }

public static class TextXmlSerializer
{
    public static void Serialize(Text text, string path)
    {
        var dto = new TextDto
        {
            Sentences = text.Sentences.Select(s => new SentenceDto
            {
                Tokens = s.Tokens.Select(t => t switch
                {
                    Word w => (TokenDto)new WordDto { Value = w.Value },
                    Punctuation p => new PunctuationDto { Value = p.Value },
                    _ => throw new InvalidOperationException()
                }).ToList()
            }).ToList()
        };

        var serializer = new XmlSerializer(typeof(TextDto));
        using var writer = new StreamWriter(path);
        serializer.Serialize(writer, dto);
    }

    public static Text Deserialize(string path)
    {
        var serializer = new XmlSerializer(typeof(TextDto));
        using var reader = new StreamReader(path);
        var dto = (TextDto)serializer.Deserialize(reader)!;

        var text = new Text();
        foreach (var sentenceDto in dto.Sentences)
        {
            var sentence = new Sentence();
            foreach (var tokenDto in sentenceDto.Tokens)
            {
                Token token = tokenDto switch
                {
                    WordDto w => new Word(w.Value),
                    PunctuationDto p => new Punctuation(p.Value),
                    _ => throw new InvalidOperationException()
                };
                sentence.Add(token);
            }
            text.Add(sentence);
        }
        return text;
    }
}
