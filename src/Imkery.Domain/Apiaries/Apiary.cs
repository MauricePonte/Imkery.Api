using ErrorOr;
using Imkery.Domain.Apiaries.Events;
using Imkery.Domain.Common;
using Imkery.Domain.Hives;
using Imkery.Domain.Locations;

namespace Imkery.Domain.Apiaries;
public class Apiary : Entity
{
    private readonly List<Guid> _hiveIds = [];

    public string Name { get; private set; } = string.Empty;
    public Coordinates Coordinate { get; private set; } = null!;
    
    public Apiary(string name, Coordinates location, Guid? id = null) 
        : base(id)
    {
        Name = name;
        Coordinate = location;
    }

    public ErrorOr<Success> AddHive(Hive hive)
    {
        _hiveIds.Add(hive.Id);

        RaiseDomainEvent(new HiveAddedEvent(hive));

        return Result.Success;
    }

    private Apiary() { }
}
