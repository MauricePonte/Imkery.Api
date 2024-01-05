using ErrorOr;
using Imkery.Domain.Apiaries;
using MediatR;

namespace Imkery.Application.Apiaries.Queries.GetApiaryQuery;
public record GetApiaryQuery(Guid ApiaryId)
    : IRequest<ErrorOr<Apiary>>;
