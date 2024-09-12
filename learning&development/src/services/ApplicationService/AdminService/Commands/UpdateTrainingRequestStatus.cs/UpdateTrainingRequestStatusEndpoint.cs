using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ApplicationService.AdminService.Commands.UpdateTrainingRequestStatus;

namespace ApplicationService.AccountsService.Commands.UpdateTrainingRequest
{
    public class UpdateTrainingRequestStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/update-status/{id:int}", async (int id, HttpRequest req, IMediator mediator) =>
            {
                var command = await req.ReadFromJsonAsync<UpdateTrainingRequestStatusCommand>();

                if (command == null)
                {
                    return Results.BadRequest("Invalid request data.");
                }

                command.Id = id;
                var response = await mediator.Send(command);

                return response ? Results.Ok("Updated") : Results.NotFound();
            });
        }
    }
}
