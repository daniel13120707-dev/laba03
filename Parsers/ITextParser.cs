namespace laba03.Parsers;

using laba03.Models;

public interface ITextParser
{
    Text Parse(string content);
}