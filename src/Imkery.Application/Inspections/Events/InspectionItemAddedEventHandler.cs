using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Inspections;
using Imkery.Domain.Inspections.Events;
using MediatR;

namespace Imkery.Application.Inspections.Events;
public class InspectionItemAddedEventHandler(IInspectionItemsRepository inspectionItemsRepository, IUnitOfWork unitOfWork) 
    : INotificationHandler<InspectionItemAddedEvent>
{
    public async Task Handle(InspectionItemAddedEvent @event, CancellationToken cancellationToken)
    {
        await inspectionItemsRepository.CreateAsync(@event.InspectionItem, cancellationToken);
        await unitOfWork.CommitChangesAsync();
    }
}