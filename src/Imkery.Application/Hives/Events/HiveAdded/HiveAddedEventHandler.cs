using Imkery.Domain.Apiaries.Events;
using Imkery.Domain.Hives;
using MediatR;

namespace Imkery.Application.Hives.Events.HiveAdded;
public class HiveAddedEventHandler(IHivesRepository _hivesRepository)
    : INotificationHandler<HiveAddedEvent>
{
    public async Task Handle(HiveAddedEvent @event, CancellationToken cancellationToken)
    {
        await _hivesRepository.CreateAsync(@event.Hive, cancellationToken);
    }
}
