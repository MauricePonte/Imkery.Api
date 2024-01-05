using ErrorOr;
using Imkery.Application.Common.Interfaces;
using Imkery.Contracts.Hives;
using Imkery.Domain.Apiaries;
using Imkery.Domain.Hives;
using MediatR;

namespace Imkery.Application.Hives.Commands.CreateHive;

public class CreateHiveCommandHandler(IApiariesRepository _apiariesRepository, IUnitOfWork _unitOfWork)
    : IRequestHandler<CreateHiveCommand, ErrorOr<HiveResponse>>
{
    public async Task<ErrorOr<HiveResponse>> Handle(CreateHiveCommand request, CancellationToken cancellationToken)
    {
        var hive = new Hive();

        var apiary = await _apiariesRepository.GetByIdAsync(request.ApiaryId, cancellationToken);
        if (apiary is null)
        {
            return Error.NotFound(description: "Apiary not found"); // TODO: resource? 
        }

        var addedToHiveResult = apiary.AddHive(hive);
        if (addedToHiveResult.IsError)
        {
            return addedToHiveResult.Errors;
        }

        await _unitOfWork.CommitChangesAsync();
        return new HiveResponse(hive.Id);
    }
}