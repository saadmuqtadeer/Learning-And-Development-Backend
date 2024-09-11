using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;
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
        //public string TrainingDetails { get; set; }
        public string TrainingTitle { get; set; }
        public string TrainingDescription { get; set; }
        public int NumberOfEmployees { get; set; }
        public string TechnicalSkills { get; set; }  // Updated to match the backend model
        public int Duration { get; set; }  // Updated to match the backend model
        public DateTime PreferredStartDate { get; set; }
        public string TrainingLocation { get; set; }
        public string SpecialRequirements { get; set; }
        public int EmployeeId { get; set; }
    }

    public class TrainingResponse
    {
        public int Id { get; set; }
    }
    public class CreateTrainingRequestHandler : IRequestHandler<CreateTrainingRequestCommand, TrainingResponse>
    {
        private readonly AuthDbContext _context;

        public CreateTrainingRequestHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmployeeExists(int employeeId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5000/api/auth/{employeeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TrainingResponse> Handle(CreateTrainingRequestCommand request, CancellationToken cancellationToken)
        {
            // Validate if the employee exists
            //if (!await EmployeeExists(request.EmployeeId))
            //{
            //    throw new Exception("Employee does not exist.");
            //}
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
                    //TrainingDetails = request.TrainingDetails,
                    TrainingTitle = request.TrainingTitle,
                    TrainingDescription = request.TrainingDescription,
                    NumberOfEmployees = request.NumberOfEmployees,
                    TechnicalSkills = request.TechnicalSkills,  // Updated to match the backend model
                    Duration = request.Duration,  // Updated to match the backend model
                    PreferredStartDate = request.PreferredStartDate,
                    TrainingLocation = request.TrainingLocation,
                    SpecialRequirements = request.SpecialRequirements,
                    EmployeeId = request.EmployeeId
                };

                await _context.TrainingRequests.AddAsync(trainingRequest, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

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
