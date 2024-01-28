using ErrorOr;
using Imkery.Domain.Inspections;
using MediatR;

namespace Imkery.Application.Inspections.Commands.AddItemToInspection;
public record AddItemToInspectionCommand(Guid InspectionId) 
    : IRequest<ErrorOr<Inspection>>;