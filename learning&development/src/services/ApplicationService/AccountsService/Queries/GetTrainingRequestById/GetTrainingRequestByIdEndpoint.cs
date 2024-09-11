using Carter;
using MediatR;
using System.Collections.Generic;

namespace ApplicationService.AccountsService.Queries.GetTrainingRequest
{
    public class GetTrainingRequestsByEmployeeIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/training-requests/employee/{employeeId:int}", async (int employeeId, IMediator mediator) =>
            {
                var query = new GetTrainingRequestsByEmployeeIdQuery { EmployeeId = employeeId };
                var trainingRequests = await mediator.Send(query);

                return trainingRequests.Any() ? Results.Ok(trainingRequests) : Results.NotFound();
            });
        }
    }
}
