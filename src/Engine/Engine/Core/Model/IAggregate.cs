using Engine.Core.Event;

namespace Engine.Core.Model;
public interface IAggregateRoot : IAudit
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IEvent[] ClearDomainEvents();
    long Version { get; set; }
}

public interface IAggregateRoot<out TId> : IAggregateRoot
{
    TId Id { get; }
}
