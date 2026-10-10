namespace laba03.Services;

using laba03.Models;

public sealed class TextAnalyzer
{
    private readonly Text _text;

    public TextAnalyzer(Text text)
    {
        _text = text ?? throw new ArgumentNullException(nameof(text));
    }

   
    public void PrintSentencesByWordCount()
    {
        Console.WriteLine("=== Предложения по возрастанию количества слов ===");
        foreach (var sentence in _text.GetSentencesByWordCount())
            Console.WriteLine($"[{sentence.WordCount} слов] {sentence}");
    }


    public void PrintSentencesByLength()
    {
        Console.WriteLine("=== Предложения по возрастанию длины ===");
        foreach (var sentence in _text.GetSentencesByLength())
            Console.WriteLine($"[{sentence.Length} симв.] {sentence}");
    }

  
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

  
    public void RemoveWordsStartingWithConsonant(int length)
    {
        _text.RemoveWordsStartingWithConsonant(length);
        Console.WriteLine($"Удалены слова длины {length}, начинающиеся с согласной.");
    }

   
    public void ReplaceWords(int length, string replacement)
    {
        _text.ReplaceWords(length, replacement);
        Console.WriteLine($"Слова длины {length} заменены на \"{replacement}\".");
    }

    
    public void RemoveStopWords(ISet<string> stopWords)
    {
        _text.RemoveStopWords(stopWords);
        Console.WriteLine($"Удалено стоп-слов: {stopWords.Count}.");
    }
}
