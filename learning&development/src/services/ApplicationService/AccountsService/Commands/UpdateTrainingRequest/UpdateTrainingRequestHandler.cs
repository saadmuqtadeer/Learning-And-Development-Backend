using ApplicationService.AccountsService.Commands.CreateTrainingProgram;
using AuthAPI.Data;
using MediatR;

namespace ApplicationService.AccountsService.Commands.UpdateTrainingRequest
{
    public class UpdateTrainingRequestCommand : IRequest<TrainingResponse>
    {
        public int Id { get; set; }
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

    public class UpdateTrainingRequestHandler : IRequestHandler<UpdateTrainingRequestCommand, TrainingResponse>
    {
        private readonly AuthDbContext _context;

        public UpdateTrainingRequestHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingResponse> Handle(UpdateTrainingRequestCommand request, CancellationToken cancellationToken)
        {
            var trainingRequest = await _context.TrainingRequests.FindAsync(request.Id);

            if (trainingRequest == null)
            {
                throw new KeyNotFoundException("Training request not found.");
            }

            // Update properties
            trainingRequest.RequestorName = request.RequestorName;
            trainingRequest.RequestorEmail = request.RequestorEmail;
            trainingRequest.Department = request.Department;
            trainingRequest.TrainingDetails = request.TrainingDetails;
            trainingRequest.TrainingTitle = request.TrainingTitle;
            trainingRequest.TrainingDescription = request.TrainingDescription;
            trainingRequest.NumberOfEmployees = request.NumberOfEmployees;
            trainingRequest.TechnicalSkillSetRequired = request.TechnicalSkillSetRequired;
            trainingRequest.DurationInDays = request.DurationInDays;
            trainingRequest.PreferredStartDate = request.PreferredStartDate;
            trainingRequest.TrainingLocation = request.TrainingLocation;
            trainingRequest.SpecialRequirements = request.SpecialRequirements;
            trainingRequest.EmployeeId = request.EmployeeId;

            await _context.SaveChangesAsync(cancellationToken);

            return new TrainingResponse { Id = trainingRequest.Id };
        }
    }
}