using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.AdminService.Commands.UpdateTrainingRequestStatus
{
    public class UpdateTrainingRequestStatusCommand : IRequest<UpdateResponse>
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string AdminFeedback { get; set; } = string.Empty;
        // New optional field
    }

    public class UpdateResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class UpdateTrainingRequestStatusHandler : IRequestHandler<UpdateTrainingRequestStatusCommand, UpdateResponse>
    {
        private readonly AuthDbContext _context;

        public UpdateTrainingRequestStatusHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateResponse> Handle(UpdateTrainingRequestStatusCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<RequestStatus>(request.Status, true, out var status))
            {
                return new UpdateResponse
                {
                    Success = false,
                    Message = "Invalid status provided."
                };
            }

            var trainingRequest = await _context.TrainingRequests
                .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);

            if (trainingRequest == null)
            {
                return new UpdateResponse
                {
                    Success = false,
                    Message = "Training request not found."
                };
            }

            trainingRequest.Status = status;
            trainingRequest.AdminFeedback = string.IsNullOrEmpty(request.AdminFeedback) ? string.Empty : request.AdminFeedback;
            //trainingRequest.AdminFeedback = request.AdminFeedback; // Set feedback if provided

            _context.TrainingRequests.Update(trainingRequest);
            await _context.SaveChangesAsync(cancellationToken);

            string message = status switch
            {
                RequestStatus.Accepted => "Training request has been accepted.",
                RequestStatus.Rejected => "Training request has been rejected.",
                _ => "Training request status has been updated."
            };

            return new UpdateResponse
            {
                Success = true,
                Message = message
            };
        }
    }
}
