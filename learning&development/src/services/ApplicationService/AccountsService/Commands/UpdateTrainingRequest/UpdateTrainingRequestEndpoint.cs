using Carter;
using MediatR;

namespace ApplicationService.AccountsService.Commands.UpdateTrainingRequest
{
    public class UpdateTrainingRequestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/training/{id:int}", async (int id, HttpRequest req, IMediator mediator) =>
            {
                var command = await req.ReadFromJsonAsync<UpdateTrainingRequestCommand>();

                if (command == null)
                {
                    return Results.BadRequest("Invalid request data.");
                }

                command.Id = id;
                var response = await mediator.Send(command);

                return Results.Ok(response);
            });
        }
    }
}
