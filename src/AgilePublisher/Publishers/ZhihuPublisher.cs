using AgilePublisher.Models;
using Microsoft.Playwright;

namespace AgilePublisher.Publishers;

public sealed class ZhihuPublisher : PlaywrightPublisherBase
{
    private readonly IZhihuPublishScript _script;

    public ZhihuPublisher(PlaywrightSettings settings, IZhihuPublishScript script)
        : base(settings)
    {
        _script = script;
    }

    protected override async Task PublishInternalAsync(IPage page, PublishRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _script.ExecuteAsync(page, request.Content, cancellationToken);
    }
}
