using System.Threading;
using System.Threading.Tasks;
using AgilePublisher.Models;
using AgilePublisher.Publishers;
using Microsoft.Playwright;

public sealed class RecordedZhihuScript : IZhihuPublishScript
{
    public async Task ExecuteAsync(IPage page, ArticleContent content, CancellationToken cancellationToken)
    {
        await page.GotoAsync("https://www.zhihu.com/write", new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

        await page.WaitForSelectorAsync("textarea[placeholder='请输入标题']", new PageWaitForSelectorOptions { Timeout = 15000 });
        await page.FillAsync("textarea[placeholder='请输入标题']", content.Title);

        await page.WaitForSelectorAsync("div.NotionEditor", new PageWaitForSelectorOptions { Timeout = 15000 });
        await page.ClickAsync("div.NotionEditor");
        await page.FillAsync("div.NotionEditor", content.Body);
    }
}
