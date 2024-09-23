using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationService.AdminService.Queries.GetTrainingRequestById
{
    public class GetSingleTrainingRequestQuery : IRequest<TrainingRequest>
    {
        public int Id { get; set; }

        public GetSingleTrainingRequestQuery(int id)
        {
            Id = id;
        }
    }
    public class GetTrainingRequestByIdHandler : IRequestHandler<GetSingleTrainingRequestQuery, TrainingRequest>
    {
        private readonly AuthDbContext _context;

        public GetTrainingRequestByIdHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingRequest> Handle(GetSingleTrainingRequestQuery request, CancellationToken cancellationToken)
        {
            var trainingRequest = await _context.TrainingRequests
                .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);

            if (trainingRequest == null)
            {
                throw new KeyNotFoundException($"Training request with ID {request.Id} not found.");
            }

            return trainingRequest;
        }
    }
}
