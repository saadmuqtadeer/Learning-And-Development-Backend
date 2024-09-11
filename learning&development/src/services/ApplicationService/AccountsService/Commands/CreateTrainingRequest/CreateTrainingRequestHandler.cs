using AuthAPI.Data;
using AuthAPI.Models;
using MediatR;


namespace ApplicationService.AccountsService.Commands.CreateTrainingProgram
{
    public class CreateTrainingRequestCommand : IRequest<TrainingResponse>
    {
        public string RequestorName { get; set; }
        public string RequestorEmail { get; set; }
        public string Department { get; set; }
        public string TrainingDetails { get; set; }
        public string TrainingTitle { get; set; }
        public string TrainingDescription { get; set; }
        public int NumberOfEmployees { get; set; }
        public string TechnicalSkillSetRequired { get; set; }
        public int DurationInDays { get; set; }
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
            var response = await client.GetAsync($"http://localhost:5000/auth/api/{employeeId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TrainingResponse> Handle(CreateTrainingRequestCommand request, CancellationToken cancellationToken)
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
                TrainingDetails = request.TrainingDetails,
                TrainingTitle = request.TrainingTitle,
                TrainingDescription = request.TrainingDescription,
                NumberOfEmployees = request.NumberOfEmployees,
                TechnicalSkillSetRequired = request.TechnicalSkillSetRequired,
                DurationInDays = request.DurationInDays,
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
    }
}
