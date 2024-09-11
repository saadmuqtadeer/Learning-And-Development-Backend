using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ApplicationService.AccountsService.Queries.GetTrainingRequest
{
    public class GetTrainingRequestsByEmployeeIdQuery : IRequest<List<TrainingRequest>>
    {
        public int EmployeeId { get; set; }
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
            var trainingRequests = await _context.TrainingRequests
                .Where(tr => tr.EmployeeId == request.EmployeeId).ToListAsync(cancellationToken);

            if (trainingRequests == null || !trainingRequests.Any())
            {
                throw new KeyNotFoundException("No training requests found for the specified employee.");
            }

            return trainingRequests;
        }
    }
}
