using Imkery.Application.Hives.Commands.CreateHive;
using Imkery.Contracts.Hives;
using MediatR;

namespace Imkery.Api.Endpoints;
public static class HivesEndpoints
{
    public static void MapHivesEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("api/hives");

        groupBuilder.MapPost(string.Empty, CreateHive)
            .WithName(nameof(CreateHive));
    }

    public static async Task<IResult> CreateHive(CreateHiveRequest request, ISender sender)
    {
        var command = new CreateHiveCommand(request.ApiaryId);

        var result = await sender.Send(command);

        return result.Match<IResult>(
            hive => TypedResults.Ok(
                new HiveResponse(HiveId: hive.HiveId)),
            error => TypedResults.Problem());
    }
}
