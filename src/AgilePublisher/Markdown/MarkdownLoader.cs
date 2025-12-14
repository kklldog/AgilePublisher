using AgilePublisher.Models;

namespace AgilePublisher.Markdown;

public static class MarkdownLoader
{
    public static ArticleContent Load(string path, string? explicitTitle = null)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Markdown file not found: {path}");
        }

        var lines = File.ReadAllLines(path);
        var title = explicitTitle ?? ExtractTitle(lines) ?? Path.GetFileNameWithoutExtension(path);
        var body = string.Join(Environment.NewLine, lines);
        return new ArticleContent(title, body);
    }

    private static string? ExtractTitle(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith("# ", StringComparison.Ordinal))
            {
                return trimmed[2..].Trim();
            }
        }

        return null;
    }
}
