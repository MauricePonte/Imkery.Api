using Imkery.Domain.Common;

namespace Imkery.Domain.Inspections.Events;

public record InspectionItemAddedEvent(InspectionItem InspectionItem) : IDomainEvent;