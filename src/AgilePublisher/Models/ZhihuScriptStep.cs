namespace AgilePublisher.Models;

public sealed record ZhihuScriptStep
{
    public required string Action { get; init; }

    public string? Selector { get; init; }

    public string? Value { get; init; }
}
