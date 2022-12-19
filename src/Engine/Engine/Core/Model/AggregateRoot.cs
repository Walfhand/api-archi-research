using Engine.Core.Event;

namespace Engine.Core.Model;

public abstract record AggregateRoot : AggregateRoot<Guid>
{

}
public abstract record AggregateRoot<TId> : Audit, IAggregateRoot<TId>
{
    public TId Id { get; set; }
    public long Version { get; set; } = -1;

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public IEvent[] ClearDomainEvents()
    {
        IEvent[] dequeuedEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequeuedEvents;
    }
}
