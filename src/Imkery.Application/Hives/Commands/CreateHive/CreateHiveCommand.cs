using ErrorOr;
using Imkery.Contracts.Hives;
using MediatR;

namespace Imkery.Application.Hives.Commands.CreateHive;
public record CreateHiveCommand(Guid ApiaryId)
    : IRequest<ErrorOr<HiveResponse>>;
