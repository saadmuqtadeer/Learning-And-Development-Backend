using Carter;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationService.AdminService.Queries.GetAllTrainingRequests; // Ensure this namespace is correct

namespace ApplicationService.AccountsService.Queries.GetTrainingRequest
{
    public class GetAllTrainingRequestEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/training-requests", async (IMediator mediator) =>
            {
                var query = new GetAllTrainingRequestsQuery(); // Use the query, not the handler
                var trainingRequests = await mediator.Send(query);

                return trainingRequests.Any() ? Results.Ok(trainingRequests) : Results.NotFound();
            });
        }
    }
}
