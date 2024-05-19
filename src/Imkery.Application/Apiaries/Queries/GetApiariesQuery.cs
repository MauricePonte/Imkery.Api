using ErrorOr;
using Imkery.Domain.Apiaries;
using MediatR;

namespace Imkery.Application.Apiaries.Queries;
public record GetApiariesQuery()
    : IRequest<ErrorOr<ICollection<Apiary>>>;

public class GetApiariesQueryHandler(IApiariesRepository _apiariesRepository)
    : IRequestHandler<GetApiariesQuery, ErrorOr<ICollection<Apiary>>>
{
    public async Task<ErrorOr<ICollection<Apiary>>> Handle(GetApiariesQuery request, CancellationToken cancellationToken)
    {
        var apiaries = await _apiariesRepository.GetCollectionAsync(cancellationToken);

        if (apiaries is null)
        {
            return Error.NotFound(description: "Something went really wrong");
        }

        return apiaries.ToList();
    }
}