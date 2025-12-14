using System.IO;
using AgilePublisher.Models;
using Microsoft.Playwright;

namespace AgilePublisher.Publishers;

public sealed class ZhihuPublisher : PlaywrightPublisherBase
{
    private readonly ZhihuSelectors _selectors;

    public ZhihuPublisher(PlaywrightSettings settings, ZhihuSelectors? selectors = null)
        : base(settings)
    {
        _selectors = selectors ?? ZhihuSelectors.Default;
    }

    protected override async Task PublishInternalAsync(IPage page, PublishRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await page.GotoAsync(_selectors.EditorUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        await EnsureEditorReadyAsync(page, cancellationToken);

        await FillTitleAsync(page, request.Content.Title, cancellationToken);
        await FillBodyAsync(page, request.Content.Body, cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.CoverImagePath))
        {
            await UploadCoverAsync(page, request.CoverImagePath!, cancellationToken);
        }

        if (request.Tags.Count > 0)
        {
            await ApplyTagsAsync(page, request.Tags, cancellationToken);
        }

        if (request.Publish)
        {
            await PublishArticleAsync(page, cancellationToken);
        }
        else
        {
            await SaveDraftAsync(page, cancellationToken);
        }
    }

    private async Task EnsureEditorReadyAsync(IPage page, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await page.WaitForSelectorAsync(_selectors.TitleInput, new PageWaitForSelectorOptions { Timeout = 15000 });
        await page.WaitForSelectorAsync(_selectors.BodyEditable, new PageWaitForSelectorOptions { Timeout = 15000 });
    }

    private Task FillTitleAsync(IPage page, string title, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return page.FillAsync(_selectors.TitleInput, title, new PageFillOptions { Timeout = 15000 });
    }

    private async Task FillBodyAsync(IPage page, string body, CancellationToken cancellationToken)
    {
        var editor = page.Locator(_selectors.BodyEditable);
        cancellationToken.ThrowIfCancellationRequested();
        await editor.ClickAsync();
        await editor.FillAsync(body);
    }

    private async Task UploadCoverAsync(IPage page, string path, CancellationToken cancellationToken)
    {
        var uploader = page.Locator(_selectors.CoverUploader);
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Cover image not found: {path}");
        }

        cancellationToken.ThrowIfCancellationRequested();
        await uploader.SetInputFilesAsync(new[] { path });
    }

    private async Task ApplyTagsAsync(IPage page, IReadOnlyList<string> tags, CancellationToken cancellationToken)
    {
        var tagInput = page.Locator(_selectors.TagInput);
        foreach (var tag in tags)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await tagInput.FillAsync(tag);
            await page.Keyboard.PressAsync("Enter");
        }
    }

    private async Task PublishArticleAsync(IPage page, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await page.ClickAsync(_selectors.PublishButton);
        await page.ClickAsync(_selectors.ConfirmPublishButton);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    private Task SaveDraftAsync(IPage page, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return page.ClickAsync(_selectors.DraftButton);
    }
}
