using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.AdminService.Commands.UpdateTrainingRequestStatus
{
    public class UpdateTrainingRequestStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
    public class UpdateTrainingRequestStatusHandler : IRequestHandler<UpdateTrainingRequestStatusCommand, bool>
    {
        private readonly AuthDbContext _context;

        public UpdateTrainingRequestStatusHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateTrainingRequestStatusCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<RequestStatus>(request.Status, true, out var status))
            {
                return false; // Invalid status
            }

            var trainingRequest = await _context.TrainingRequests
                .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);

            if (trainingRequest == null)
            {
                return false; // Request not found
            }

            trainingRequest.Status = status;
            _context.TrainingRequests.Update(trainingRequest);
            await _context.SaveChangesAsync(cancellationToken);

            return true; // Update successful
        }
    }
}
