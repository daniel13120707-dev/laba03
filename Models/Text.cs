namespace laba03.Models;

using System.Text;

public sealed class Text
{
    private readonly List<Sentence> _sentences = new();

    public IReadOnlyList<Sentence> Sentences => _sentences;

    public void Add(Sentence sentence)
    {
        ArgumentNullException.ThrowIfNull(sentence);
        _sentences.Add(sentence);
    }


    public IEnumerable<Sentence> GetSentencesByWordCount()
        => _sentences.OrderBy(s => s.WordCount);


    public IEnumerable<Sentence> GetSentencesByLength()
        => _sentences.OrderBy(s => s.Length);


    public IEnumerable<string> FindWordsInQuestions(int length)
    {
        return _sentences
            .Where(s => s.IsQuestion)
            .SelectMany(s => s.Words)
            .Where(w => w.Length == length)
            .Select(w => w.Value.ToLowerInvariant())
            .Distinct();
    }


    public void RemoveWordsStartingWithConsonant(int length)
    {
        foreach (var sentence in _sentences)
        {
            sentence.RemoveAllWords(w =>
                w.Length == length && w.StartsWithConsonant());
        }
    }


    public void ReplaceWords(int length, string replacement)
    {
        foreach (var sentence in _sentences)
        {
            sentence.ReplaceWords(w => w.Length == length, replacement);
        }
    }


    public void RemoveStopWords(ISet<string> stopWords)
    {
        foreach (var sentence in _sentences)
        {
            sentence.RemoveAllWords(w => w.IsStopWord(stopWords));
        }
    }

    public override string ToString()
        => string.Join(" ", _sentences.Select(s => s.ToString()));


  
    public IReadOnlyList<ConcordanceEntry> BuildConcordance()
    {
        var map = new Dictionary<string, (int Count, SortedSet<int> Lines)>(
            StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < _sentences.Count; i++)
        {
            int lineNumber = i + 1;

            foreach (var word in _sentences[i].Words)
            {
                string key = word.Value.ToLowerInvariant();

                if (!map.TryGetValue(key, out var data))
                {
                    data = (0, new SortedSet<int>());
                }

                data.Count++;
                data.Lines.Add(lineNumber);
                map[key] = data;
            }
        }

        return map
            .OrderBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
            .Select(kv => new ConcordanceEntry(
                kv.Key,
                kv.Value.Count,
                kv.Value.Lines.ToList()))
            .ToList();
    }

    public string GetConcordanceString()
    {
        var sb = new StringBuilder();
        foreach (var entry in BuildConcordance())
        {
            sb.Append(entry.ToString());
            sb.Append(' ');
        }
        return sb.ToString().TrimEnd();
    }
}