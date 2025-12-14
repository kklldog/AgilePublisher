using AgilePublisher.Models;

namespace AgilePublisher.Publishers;

public interface IPublisher
{
    Task PublishAsync(PublishRequest request, CancellationToken cancellationToken = default);
}
