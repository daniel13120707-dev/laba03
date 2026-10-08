using System.Text;
using laba03.Models;
using laba03.Parsers;
using laba03.Services;

Console.OutputEncoding = Encoding.UTF8;

// ---------- 1. Загрузка текста ----------
string inputPath = args.Length > 0 ? args[0] : "input.txt";

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Файл '{inputPath}' не найден.");
    Console.WriteLine("Создайте файл input.txt рядом с программой или передайте путь аргументом.");
    Console.WriteLine("Пример: dotnet run -- mytext.txt");
    return;
}

string content = File.ReadAllText(inputPath);

// ---------- 2. Парсинг ----------
ITextParser parser = new RegexParser();   // можно заменить на CharByCharParser
Text text = parser.Parse(content);

Console.WriteLine("=== ИСХОДНЫЙ ТЕКСТ ===");
Console.WriteLine(text);
Console.WriteLine();

// ---------- 3. Анализ ----------
var analyzer = new TextAnalyzer(text);

analyzer.PrintSentencesByWordCount();
Console.WriteLine();

analyzer.PrintSentencesByLength();
Console.WriteLine();

int targetLength = 5;
analyzer.PrintWordsInQuestions(targetLength);
Console.WriteLine();

// ---------- 4. Удаление слов ----------
analyzer.RemoveWordsStartingWithConsonant(targetLength);
Console.WriteLine();

// ---------- 5. Замена слов ----------
analyzer.ReplaceWords(targetLength, "*");
Console.WriteLine();

// ---------- 6. Удаление стоп-слов ----------
var stopWords = new StopWordProvider();
stopWords.LoadFromFiles("stopwords_ru.txt", "stopwords_en.txt");
analyzer.RemoveStopWords(stopWords.StopWords);
Console.WriteLine();

// ---------- 7. Экспорт в XML ----------
TextXmlSerializer.Serialize(text, "output.xml");
Console.WriteLine("Текст экспортирован в output.xml");

// ---------- 8. Проверка десериализации ----------
var restored = TextXmlSerializer.Deserialize("output.xml");
Console.WriteLine();
Console.WriteLine("=== ТЕКСТ ПОСЛЕ ВСЕХ ОПЕРАЦИЙ (из XML) ===");
Console.WriteLine(restored);
