﻿using System.Runtime.InteropServices;

namespace Imkery.Domain.Common;
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = []; // TODO Make type of IDomainEvent

    public Guid Id { get; private set; }

    protected Entity(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public List<IDomainEvent> PopTransactionalDomainEvents() 
    {
        var transactionalDomainEvents = _domainEvents.Where(domainEvent => domainEvent is ITransactionalDomainEvent).ToList();
        _domainEvents.RemoveAll(domainEvent => domainEvent is ITransactionalDomainEvent);
        return transactionalDomainEvents;
    }

    public List<IDomainEvent> PopEventualConsistantDomainEvents()
    {
        var eventualConsistantDomainEvents = _domainEvents.ToList();
        _domainEvents.Clear();
        return eventualConsistantDomainEvents;
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected Entity() { }
}
