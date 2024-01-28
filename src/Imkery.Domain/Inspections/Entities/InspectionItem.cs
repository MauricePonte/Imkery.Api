using Imkery.Domain.Common;
using System.Drawing;

namespace Imkery.Domain.Inspections;

public class InspectionItem : Entity
{
    private Guid SubjectId { get; }
    
    public InspectionItem(Guid subjectId, Guid? id = null) 
        : base(id)
    {
        SubjectId = subjectId;
    }

    private InspectionItem() { }
}