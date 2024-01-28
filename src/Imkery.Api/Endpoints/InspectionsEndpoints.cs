using Imkery.Application.Inspections.Commands.AddItemToInspection;
using Imkery.Application.Inspections.Commands.CreateInspection;
using Imkery.Contracts.Inspections;
using MediatR;

namespace Imkery.Api.Endpoints;
public static class InspectionsEndpoints
{
    public static void MapInspectionEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("api/inspections");

        groupBuilder.MapPost(string.Empty, CreateInspection)
            .WithName(nameof(CreateInspection));
    }

    private static async Task<IResult> CreateInspection(CreateInspectionRequest request, ISender sender)
    {
        var command = new CreateInspectionCommand(request.ApiaryId);
        var result = await sender.Send(command);

        return result.Match<IResult>
        (
            onValue: inspection => TypedResults.Ok(new InspectionResponse(InspectionId: inspection.Id)),
            onError: _ => TypedResults.Problem()
        );
    }

    private static async Task<IResult> AddItemToInspection(AddItemToInspectionRequest request, ISender sender)
    {
        var command = new AddItemToInspectionCommand(request.InspectionId);
        var result = await sender.Send(command);

        return result.Match<IResult>
        (
            onValue: inspection => TypedResults.Ok(new InspectionResponse(InspectionId: inspection.Id)),
            onError: _ => TypedResults.Problem()
        );
    }
}
