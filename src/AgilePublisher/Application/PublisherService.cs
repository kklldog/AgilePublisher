using AgilePublisher.Models;
using AgilePublisher.Publishers;

namespace AgilePublisher.Application;

public class PublisherService
{
    private readonly Dictionary<string, IPublisher> _publishers;

    public PublisherService(PlaywrightSettings settings, ZhihuSelectors? zhihuSelectors = null, string? zhihuScriptPath = null)
    {
        _publishers = new(StringComparer.OrdinalIgnoreCase)
        {
            ["zhihu"] = new ZhihuPublisher(settings, zhihuSelectors, zhihuScriptPath is null ? null : new ZhihuScriptExecutor(zhihuScriptPath))
        };
    }

    public Task PublishAsync(string platform, PublishRequest request, CancellationToken cancellationToken = default)
    {
        if (!_publishers.TryGetValue(platform, out var publisher))
        {
            throw new InvalidOperationException($"Unsupported platform: {platform}");
        }

        return publisher.PublishAsync(request, cancellationToken);
    }
}
