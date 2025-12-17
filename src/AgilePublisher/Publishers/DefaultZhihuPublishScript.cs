using AgilePublisher.Models;
using Microsoft.Playwright;

namespace AgilePublisher.Publishers;

public sealed class DefaultZhihuPublishScript : IZhihuPublishScript
{
    private readonly ZhihuSelectors _selectors;

    public DefaultZhihuPublishScript(ZhihuSelectors selectors)
    {
        _selectors = selectors;
    }

    public async Task ExecuteAsync(IPage page, ArticleContent content, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await page.GotoAsync(_selectors.EditorUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

        await page.Locator(_selectors.TitleInput).ClickAsync();
        await page.FillAsync(_selectors.TitleInput, content.Title);

        await page.Locator(_selectors.BodyEditable).ClickAsync();
        await page.FillAsync(_selectors.BodyEditable, content.Body);
    }
}
