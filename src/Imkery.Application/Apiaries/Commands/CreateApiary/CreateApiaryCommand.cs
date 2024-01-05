using ErrorOr;
using Imkery.Domain.Apiaries;
using MediatR;

namespace Imkery.Application.Apiaries.Commands.CreateApiary;
public record CreateApiaryCommand(string Name, decimal Latitude, decimal Longitude) 
    : IRequest<ErrorOr<Apiary>>;
