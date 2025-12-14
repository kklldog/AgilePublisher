namespace AgilePublisher.Models;

public record PublishRequest(
    ArticleContent Content,
    IReadOnlyList<string> Tags,
    string? CoverImagePath,
    bool Publish); 
