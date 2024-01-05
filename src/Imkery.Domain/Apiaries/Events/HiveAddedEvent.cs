using Imkery.Domain.Common;
using Imkery.Domain.Hives;

namespace Imkery.Domain.Apiaries.Events;

public record HiveAddedEvent(Hive Hive) : IDomainEvent;