using MediatR;
using ErrorOr;
using Imkery.Domain.Inspections;

namespace Imkery.Application.Inspections.Commands.CreateInspection;
public record CreateInspectionCommand(Guid ApiaryId)
    : IRequest<ErrorOr<Inspection>>;