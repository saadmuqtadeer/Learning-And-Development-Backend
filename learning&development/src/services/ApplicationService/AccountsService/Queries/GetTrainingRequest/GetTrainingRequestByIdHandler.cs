using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;

namespace ApplicationService.AccountsService.Queries.GetTrainingRequest
{
    public class GetTrainingRequestByIdQuery : IRequest<TrainingRequest>
    {
        public int Id { get; set; }
    }

    public class GetTrainingRequestByIdHandler : IRequestHandler<GetTrainingRequestByIdQuery, TrainingRequest>
    {
        private readonly AuthDbContext _context;

        public GetTrainingRequestByIdHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingRequest> Handle(GetTrainingRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var trainingRequest = await _context.TrainingRequests.FindAsync(request.Id);

            if (trainingRequest == null)
            {
                throw new KeyNotFoundException("Training request not found.");
            }

            return trainingRequest;
        }
    }
}