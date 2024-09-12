using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.AccountsService.Commands.CreateTrainingProgram
{
    public class CreateTrainingRequestCommand : IRequest<TrainingResponse>
    {
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string Department { get; set; }
        public string TrainingTitle { get; set; }
        public string TrainingDescription { get; set; }
        public int NumberOfEmployees { get; set; }
        public string TechnicalSkills { get; set; }
        public int Duration { get; set; }
        public DateTime PreferredStartDate { get; set; }
        public string TrainingLocation { get; set; }
        public string SpecialRequirements { get; set; }
        public int EmployeeId { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
    }

    public class TrainingResponse
    {
        public int Id { get; set; }
    }

    public class CreateTrainingRequestHandler : IRequestHandler<CreateTrainingRequestCommand, TrainingResponse>
    {
        private readonly AuthDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public CreateTrainingRequestHandler(AuthDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<bool> EmployeeExists(int employeeId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5000/api/auth/{employeeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TrainingResponse> Handle(CreateTrainingRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var trainingRequest = new TrainingRequest
                {
                    RequestorName = request.RequestorName,
                    RequestorEmail = request.RequestorEmail,
                    Department = request.Department,
                    TrainingTitle = request.TrainingTitle,
                    TrainingDescription = request.TrainingDescription,
                    NumberOfEmployees = request.NumberOfEmployees,
                    TechnicalSkills = request.TechnicalSkills,
                    Duration = request.Duration,
                    PreferredStartDate = request.PreferredStartDate,
                    TrainingLocation = request.TrainingLocation,
                    SpecialRequirements = request.SpecialRequirements,
                    EmployeeId = request.EmployeeId,
                    Status = request.Status
                };

                await _context.TrainingRequests.AddAsync(trainingRequest, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                // Notify admin via SignalR
                var message = $"New training request created: {request.TrainingTitle} by {request.RequestorName}.";
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Admin", message);

                return new TrainingResponse
                {
                    Id = trainingRequest.Id
                };
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.Error.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
