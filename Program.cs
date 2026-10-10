using System.Text;
using laba03.Models;
using laba03.Parsers;
using laba03.Services;

Console.OutputEncoding = Encoding.UTF8;


string inputPath = args.Length > 0 ? args[0] : "input.txt";

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Файл '{inputPath}' не найден.");
    Console.WriteLine("Создайте файл input.txt рядом с программой или передайте путь аргументом.");
    Console.WriteLine("Пример: dotnet run -- mytext.txt");
    return;
}

string content = File.ReadAllText(inputPath);


ITextParser parser = new RegexParser();   
Text text = parser.Parse(content);

Console.WriteLine("=== ИСХОДНЫЙ ТЕКСТ ===");
Console.WriteLine(text);
Console.WriteLine();


var analyzer = new TextAnalyzer(text);

analyzer.PrintSentencesByWordCount();
Console.WriteLine();

analyzer.PrintSentencesByLength();
Console.WriteLine();

int targetLength = 5;
analyzer.PrintWordsInQuestions(targetLength);
Console.WriteLine();


analyzer.RemoveWordsStartingWithConsonant(targetLength);
Console.WriteLine();


analyzer.ReplaceWords(targetLength, "*");
Console.WriteLine();


var stopWords = new StopWordProvider();
stopWords.LoadFromFiles("stopwords_ru.txt", "stopwords_en.txt");
analyzer.RemoveStopWords(stopWords.StopWords);
Console.WriteLine();


TextXmlSerializer.Serialize(text, "output.xml");
Console.WriteLine("Текст экспортирован в output.xml");


var restored = TextXmlSerializer.Deserialize("output.xml");
Console.WriteLine();
Console.WriteLine("=== ТЕКСТ ПОСЛЕ ВСЕХ ОПЕРАЦИЙ (из XML) ===");
Console.WriteLine(restored);


Console.WriteLine(" КОНКОРДАНС ");
Console.WriteLine(text.GetConcordanceString());
Console.WriteLine();