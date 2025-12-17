using AgilePublisher.Models;
using Microsoft.Playwright;

namespace AgilePublisher.Publishers;

public interface IZhihuPublishScript
{
    Task ExecuteAsync(IPage page, ArticleContent content, CancellationToken cancellationToken);
}
