using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.AdminService.Queries.GetAllTrainingRequests
{
    public class GetAllTrainingRequestsQuery : IRequest<List<TrainingRequest>> { }

    public class GetAllTrainingRequestsHandler : IRequestHandler<GetAllTrainingRequestsQuery, List<TrainingRequest>>
    {
        private readonly AuthDbContext _context;

        public GetAllTrainingRequestsHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingRequest>> Handle(GetAllTrainingRequestsQuery request, CancellationToken cancellationToken)
        {
            // Fetch all training requests from the database
            var trainingRequests = await _context.TrainingRequests.ToListAsync(cancellationToken);

            if (!trainingRequests.Any())
            {
                throw new KeyNotFoundException("No training requests found.");
            }

            return trainingRequests;
        }
    }

}
