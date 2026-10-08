namespace laba03.Models;

public sealed class Text
{
    private readonly List<Sentence> _sentences = new();

    public IReadOnlyList<Sentence> Sentences => _sentences;

    public void Add(Sentence sentence)
    {
        ArgumentNullException.ThrowIfNull(sentence);
        _sentences.Add(sentence);
    }

    // 1. Предложения по возрастанию количества слов
    public IEnumerable<Sentence> GetSentencesByWordCount()
        => _sentences.OrderBy(s => s.WordCount);

    // 2. Предложения по возрастанию длины
    public IEnumerable<Sentence> GetSentencesByLength()
        => _sentences.OrderBy(s => s.Length);

    // 3. Слова заданной длины в вопросительных предложениях (без повторов)
    public IEnumerable<string> FindWordsInQuestions(int length)
    {
        return _sentences
            .Where(s => s.IsQuestion)
            .SelectMany(s => s.Words)
            .Where(w => w.Length == length)
            .Select(w => w.Value.ToLowerInvariant())
            .Distinct();
    }

    // 4. Удалить слова заданной длины, начинающиеся с согласной
    public void RemoveWordsStartingWithConsonant(int length)
    {
        foreach (var sentence in _sentences)
        {
            sentence.RemoveAllWords(w =>
                w.Length == length && w.StartsWithConsonant());
        }
    }

    // 5. Заменить слова заданной длины на подстроку
    public void ReplaceWords(int length, string replacement)
    {
        foreach (var sentence in _sentences)
        {
            sentence.ReplaceWords(w => w.Length == length, replacement);
        }
    }

    // 6. Удалить стоп-слова
    public void RemoveStopWords(ISet<string> stopWords)
    {
        foreach (var sentence in _sentences)
        {
            sentence.RemoveAllWords(w => w.IsStopWord(stopWords));
        }
    }

    public override string ToString()
        => string.Join(" ", _sentences.Select(s => s.ToString()));
}