using Carter;
using MediatR;

namespace ApplicationService.AccountsService.Queries.GetTrainingRequest
{
    public class GetTrainingRequestByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/training/{id:int}", async (int id, IMediator mediator) =>
            {
                var query = new GetTrainingRequestByIdQuery { Id = id };
                var trainingRequest = await mediator.Send(query);

                return trainingRequest != null ? Results.Ok(trainingRequest) : Results.NotFound();
            });
        }
    }
}
