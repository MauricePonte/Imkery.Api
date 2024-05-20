using ErrorOr;
using Imkery.Application.Common.Interfaces;
using Imkery.Domain.Apiaries;
using Imkery.Domain.Locations;
using MediatR;

namespace Imkery.Application.Apiaries.Commands.CreateApiary;

public class CreateApiaryCommandHandler(IApiariesRepository _apiariesRepository, IUnitOfWork _unitOfWork)
    : IRequestHandler<CreateApiaryCommand, ErrorOr<Apiary>>
{
    public async Task<ErrorOr<Apiary>> Handle(CreateApiaryCommand request, CancellationToken cancellationToken)
    {
        var apiary = new Apiary
        (
            name: request.Name,
            location: new Coordinates(request.Latitude, request.Longitude)
        );

        await _apiariesRepository.CreateAsync(apiary, cancellationToken);
        await _unitOfWork.CommitChangesAsync();
        return apiary;
    }
}
