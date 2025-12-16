using AgilePublisher.Models;
using Microsoft.Playwright;

namespace AgilePublisher.Publishers;

public sealed class ZhihuPublisher : PlaywrightPublisherBase
{
    private readonly ZhihuScriptExecutor _scriptExecutor;

    public ZhihuPublisher(PlaywrightSettings settings, ZhihuSelectors? selectors = null, ZhihuScriptExecutor? scriptExecutor = null)
        : base(settings)
    {
        _scriptExecutor = scriptExecutor ?? new ZhihuScriptExecutor(Path.Combine(AppContext.BaseDirectory, "scripts", "zhihu-script.json"));
    }

    protected override async Task PublishInternalAsync(IPage page, PublishRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _scriptExecutor.ExecuteAsync(page, request.Content, cancellationToken);
    }
}
