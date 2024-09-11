using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApplicationService.AccountsService.Commands.CreateTrainingProgram
{
    public class CreateTrainingRequestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/training-request", async (HttpRequest req, IMediator mediator) =>
            {
                var command = await req.ReadFromJsonAsync<CreateTrainingRequestCommand>();

                if (command == null)
                {
                    return Results.BadRequest("Invalid request data.");
                }

                var response = await mediator.Send(command);

                return Results.Created($"/training/{response.Id}", response);
            });
        }
    }
}
