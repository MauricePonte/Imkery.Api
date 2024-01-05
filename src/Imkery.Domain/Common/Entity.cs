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

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected Entity() { }
}
