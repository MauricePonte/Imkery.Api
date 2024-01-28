using ErrorOr;
using Imkery.Domain.Common;
using Imkery.Domain.Inspections.Events;

namespace Imkery.Domain.Inspections;

public class Inspection : Entity
{
    private readonly List<Guid> _inspectionItems = [];
    public Guid ApiaryId { get; } = Guid.Empty;
    
    public DateTime CreatedDateUtc { get; } = DateTime.MinValue;
    
    public InspectionState State { get; private set; } = InspectionState.Started;
    
    public Inspection(Guid apiaryId, DateTime? createdDateUtc = null, Guid? id = null)
        : base(id)
    {
        ApiaryId = apiaryId;
        CreatedDateUtc = createdDateUtc ?? DateTime.UtcNow;
    }

    public ErrorOr<Success> AddInspectionItem(InspectionItem item)
    {
        _inspectionItems.Add(item.Id);
        
        RaiseDomainEvent(new InspectionItemAddedEvent(item));
        
        return Result.Success;
    }

    private Inspection() { }
}