using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.AccountsService.Queries.GetTrainingRequest
{
    public class GetTrainingRequestsByEmployeeIdQuery : IRequest<List<TrainingRequest>>
    {
        public int EmployeeId { get; set; }
        public RequestStatus? Status { get; set; } // Optional status filter
    }
    public class GetTrainingRequestsByEmployeeIdHandler : IRequestHandler<GetTrainingRequestsByEmployeeIdQuery, List<TrainingRequest>>
    {
        private readonly AuthDbContext _context;

        public GetTrainingRequestsByEmployeeIdHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrainingRequest>> Handle(GetTrainingRequestsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.TrainingRequests.AsQueryable();

            // Filter by EmployeeId
            query = query.Where(tr => tr.EmployeeId == request.EmployeeId);

            // Filter by Status if specified
            if (request.Status.HasValue)
            {
                query = query.Where(tr => tr.Status == request.Status.Value);
            }

            var trainingRequests = await query.ToListAsync(cancellationToken);

            if (trainingRequests == null || !trainingRequests.Any())
            {
                throw new KeyNotFoundException("No training requests found for the specified employee.");
            }

            return trainingRequests;
        }
    }
}
