using Imkery.Domain.Common;

namespace Imkery.Domain.Hives;
public class Hive : Entity
{
    public Hive(Guid? id = null)
    : base(id)
    {
    }

    private Hive() { }
}
