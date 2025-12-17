using AgilePublisher.Models;
using AgilePublisher.Publishers;

namespace AgilePublisher.Application;

public class PublisherService
{
    private readonly Dictionary<string, IPublisher> _publishers;

    public PublisherService(PlaywrightSettings settings, ZhihuSelectors? zhihuSelectors = null, string? zhihuScriptPath = null)
    {
        var selectors = zhihuSelectors ?? ZhihuSelectors.Default;
        var script = zhihuScriptPath is null
            ? new DefaultZhihuPublishScript(selectors)
            : new ZhihuScriptLoader(zhihuScriptPath).Load();

        _publishers = new(StringComparer.OrdinalIgnoreCase)
        {
            ["zhihu"] = new ZhihuPublisher(settings, script)
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
