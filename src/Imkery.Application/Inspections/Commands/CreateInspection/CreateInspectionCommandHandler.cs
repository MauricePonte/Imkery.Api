using ErrorOr;
using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Inspections;
using Imkery.Domain.Inspections.Repositories;
using MediatR;

namespace Imkery.Application.Inspections.Commands.CreateInspection;
public class CreateInspectionCommandHandler(IInspectionsRepository inspectionsRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateInspectionCommand, ErrorOr<Inspection>>
{
    public async Task<ErrorOr<Inspection>> Handle(CreateInspectionCommand request, CancellationToken cancellationToken)
    {
        var inspection = new Inspection
        (
            apiaryId: request.ApiaryId 
        );

        await inspectionsRepository.CreateAsync(inspection, cancellationToken);
        await unitOfWork.CommitChangesAsync();
        return inspection;
    }
}