namespace laba03.Services;

using laba03.Models;

public sealed class TextAnalyzer
{
    private readonly Text _text;

    public TextAnalyzer(Text text)
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
    }

    // 1. Предложения по возрастанию количества слов
    public void PrintSentencesByWordCount()
    {
        Console.WriteLine("=== Предложения по возрастанию количества слов ===");
        foreach (var sentence in _text.GetSentencesByWordCount())
            Console.WriteLine($"[{sentence.WordCount} слов] {sentence}");
    }

    // 2. Предложения по возрастанию длины
    public void PrintSentencesByLength()
    {
        Console.WriteLine("=== Предложения по возрастанию длины ===");
        foreach (var sentence in _text.GetSentencesByLength())
            Console.WriteLine($"[{sentence.Length} симв.] {sentence}");
    }

    // 3. Слова заданной длины в вопросительных предложениях
    public void PrintWordsInQuestions(int length)
    {
        Console.WriteLine($"=== Слова длины {length} в вопросительных предложениях ===");
        var words = _text.FindWordsInQuestions(length).ToList();
        if (words.Count == 0)
        {
            Console.WriteLine("(не найдено)");
            return;
        }
        foreach (var w in words)
            Console.WriteLine(w);
    }

    // 4. Удаление слов заданной длины, начинающихся с согласной
    public void RemoveWordsStartingWithConsonant(int length)
    {
        _text.RemoveWordsStartingWithConsonant(length);
        Console.WriteLine($"Удалены слова длины {length}, начинающиеся с согласной.");
    }

    // 5. Замена слов заданной длины на подстроку
    public void ReplaceWords(int length, string replacement)
    {
        _text.ReplaceWords(length, replacement);
        Console.WriteLine($"Слова длины {length} заменены на \"{replacement}\".");
    }

    // 6. Удаление стоп-слов
    public void RemoveStopWords(ISet<string> stopWords)
    {
        _text.RemoveStopWords(stopWords);
        Console.WriteLine($"Удалено стоп-слов: {stopWords.Count}.");
    }
}
