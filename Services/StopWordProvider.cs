namespace laba03.Services;

public sealed class StopWordProvider
{
    private readonly HashSet<string> _stopWords = new(StringComparer.OrdinalIgnoreCase);

    public ISet<string> StopWords => _stopWords;

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
            return;

        foreach (var line in File.ReadLines(path))
        {
            var word = line.Trim().ToLowerInvariant();
            if (!string.IsNullOrEmpty(word))
                _stopWords.Add(word);
        }
    }

    public void LoadFromFiles(params string[] paths)
    {
        foreach (var path in paths)
            LoadFromFile(path);
    }
}
