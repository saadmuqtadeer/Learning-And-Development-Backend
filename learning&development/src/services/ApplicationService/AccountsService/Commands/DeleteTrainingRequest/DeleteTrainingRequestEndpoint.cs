using Carter;
using MediatR;

namespace ApplicationService.AccountsService.Commands.DeleteTrainingRequest
{
    public class DeleteTrainingRequestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/training/{id:int}", async (int id, IMediator mediator) =>
            {
                var command = new DeleteTrainingRequestCommand { Id = id };
                await mediator.Send(command);

                return Results.Ok("Deleted Successfully");
            });
        }
    }
}
