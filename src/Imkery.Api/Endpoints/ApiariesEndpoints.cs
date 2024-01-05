using Imkery.Application.Apiaries.Commands.CreateApiary;
using Imkery.Application.Apiaries.Queries.GetApiaryQuery;
using Imkery.Contracts.Apiaries;
using MediatR;

namespace Imkery.Api.Endpoints;

public static class ApiariesEndpoints
{
    public static void MapApiaryEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("api/apiaries");

        groupBuilder.MapPost(string.Empty, async(CreateApiaryRequest request, ISender sender) => CreateApiary)
            .WithName(nameof(CreateApiary));

        groupBuilder.MapGet("{apiaryId:guid}", GetApiary)
            .WithName(nameof(GetApiary));
    }

    public static async Task<IResult> CreateApiary(CreateApiaryRequest request, ISender sender)
    {
        var command = new CreateApiaryCommand(
            Name: request.Name,
            Latitude: request.Latitude,
            Longitude: request.Longitude);

        var result = await sender.Send(command);

        return result.Match(
            apiary => Results.Ok(new ApiaryResponse(
                Id: apiary.Id,
                Name: request.Name,
                Latitude: request.Latitude,
                Longitude: request.Longitude)),
            error => Results.Problem());
    }

    public static async Task<IResult> GetApiary(Guid apiaryId, ISender sender)
    {
        var query = new GetApiaryQuery(apiaryId);

        var result = await sender.Send(query);

        return result.Match(
            apiary => Results.Ok(new ApiaryResponse(
                Id: apiary.Id,
                Name: apiary.Name,
                Latitude: apiary.Coordinate.Latitude,
                Longitude: apiary.Coordinate.Longitude)),
            error => Results.Problem());
    }
}
