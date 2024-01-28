using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Apiaries.Events;
using Imkery.Domain.Hives;
using MediatR;

namespace Imkery.Application.Hives.Events;
public class HiveAddedEventHandler(IHivesRepository hivesRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<HiveAddedEvent>
{
    public async Task Handle(HiveAddedEvent @event, CancellationToken cancellationToken)
    {
        await hivesRepository.CreateAsync(@event.Hive, cancellationToken);
        await unitOfWork.CommitChangesAsync();
    }
}
