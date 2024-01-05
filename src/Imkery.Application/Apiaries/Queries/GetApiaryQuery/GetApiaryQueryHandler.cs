using ErrorOr;
using Imkery.Domain.Apiaries;
using MediatR;

namespace Imkery.Application.Apiaries.Queries.GetApiaryQuery;

public class GetApiaryQueryHandler(IApiariesRepository _apiariesRepository)
    : IRequestHandler<GetApiaryQuery, ErrorOr<Apiary>>
{
    public async Task<ErrorOr<Apiary>> Handle(GetApiaryQuery request, CancellationToken cancellationToken)
    {
        var apiary = await _apiariesRepository.GetByIdAsync(request.ApiaryId, cancellationToken);

        if (apiary is null)
        {
            return Error.NotFound(description: "Apiary not found");
        }

        return apiary;
    }
}
