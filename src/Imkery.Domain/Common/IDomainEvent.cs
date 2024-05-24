using MediatR;

namespace Imkery.Domain.Common;
public interface IDomainEvent : INotification
{
}

public interface ITransactionalDomainEvent : IDomainEvent
{
}

public interface IEventualConsistantDomainEvent : IDomainEvent
{
}