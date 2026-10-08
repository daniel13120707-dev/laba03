namespace laba03.Models;

using System.Text;

public sealed class Sentence
{
    private readonly List<Token> _tokens = new();

    public IReadOnlyList<Token> Tokens => _tokens;

    public IEnumerable<Word> Words => _tokens.OfType<Word>();

    public IEnumerable<Punctuation> Punctuations => _tokens.OfType<Punctuation>();

    public int WordCount => Words.Count();

    public int Length => _tokens.Sum(t => t.Value.Length);

    public bool IsQuestion =>
        Punctuations.Any(p => p.IsQuestionMark);

    public void Add(Token token)
    {
        ArgumentNullException.ThrowIfNull(token);
        _tokens.Add(token);
    }

    public void RemoveAllWords(Func<Word, bool> predicate)
    {
        _tokens.RemoveAll(t => t is Word w && predicate(w));
    }

    public void ReplaceWords(Func<Word, bool> predicate, string replacement)
    {
        for (int i = 0; i < _tokens.Count; i++)
        {
            if (_tokens[i] is Word w && predicate(w))
            {
                _tokens[i] = new Word(replacement);
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var token in _tokens)
        {
            if (token is Punctuation)
            {
                sb.Append(token.Value);
            }
            else
            {
                if (sb.Length > 0 && !char.IsWhiteSpace(sb[^1]))
                    sb.Append(' ');
                sb.Append(token.Value);
            }
        }
        return sb.ToString().Trim();
    }
}