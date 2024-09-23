using Carter;
using MediatR;

namespace ApplicationService.AdminService.Queries.GetTrainingRequestById
{
    public class GetTrainingRequestByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/training-requests/{id:int}", async (int id, IMediator mediator) =>
            {
                var query = new GetSingleTrainingRequestQuery(id); 
                try
                {
                    var trainingRequest = await mediator.Send(query);
                    return Results.Ok(trainingRequest);
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(new { message = ex.Message });
                }
            });
        }
    }
}
