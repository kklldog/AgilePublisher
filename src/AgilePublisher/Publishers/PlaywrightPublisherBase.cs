using System.Linq;
using AgilePublisher.Models;
using Microsoft.Playwright;

namespace AgilePublisher.Publishers;

public abstract class PlaywrightPublisherBase : IPublisher
{
    private readonly PlaywrightSettings _settings;

    protected PlaywrightPublisherBase(PlaywrightSettings settings)
    {
        _settings = settings;
    }

    public Task PublishAsync(PublishRequest request, CancellationToken cancellationToken = default)
    {
        return WithPageAsync(page => PublishInternalAsync(page, request, cancellationToken), cancellationToken);
    }

    protected abstract Task PublishInternalAsync(IPage page, PublishRequest request, CancellationToken cancellationToken);

    protected async Task WithPageAsync(Func<IPage, Task> action, CancellationToken cancellationToken)
    {
        using var playwright = await Playwright.CreateAsync();
        IBrowser? browser = null;
        IBrowserContext? context = null;

        if (!string.IsNullOrWhiteSpace(_settings.UserDataDir))
        {
            context = await playwright.Chromium.LaunchPersistentContextAsync(
                _settings.UserDataDir,
                new BrowserTypeLaunchPersistentContextOptions
                {
                    Headless = _settings.Headless,
                    SlowMo = _settings.SlowMo,
                    Timeout = _settings.LaunchTimeout
                });
        }
        else
        {
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = _settings.Headless,
                SlowMo = _settings.SlowMo,
                Timeout = _settings.LaunchTimeout
            });
            context = await browser.NewContextAsync();
        }

        var page = context.Pages.FirstOrDefault() ?? await context.NewPageAsync();
        await action(page);

        await context.CloseAsync();
        if (browser is not null)
        {
            await browser.CloseAsync();
        }
    }
}
