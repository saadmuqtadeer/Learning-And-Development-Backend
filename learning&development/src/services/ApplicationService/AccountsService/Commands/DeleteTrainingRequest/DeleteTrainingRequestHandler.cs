using AuthAPI.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationService.AccountsService.Commands.DeleteTrainingRequest
{
    public class DeleteTrainingRequestCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTrainingRequestHandler : IRequestHandler<DeleteTrainingRequestCommand>
    {
        private readonly AuthDbContext _context;

        public DeleteTrainingRequestHandler(AuthDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteTrainingRequestCommand request, CancellationToken cancellationToken)
        {
                var trainingRequest = await _context.TrainingRequests.FindAsync(request.Id);

                if (trainingRequest == null)
                {
                    throw new KeyNotFoundException("Training request not found.");
                }

                _context.TrainingRequests.Remove(trainingRequest);
                await _context.SaveChangesAsync(cancellationToken);

                return;
        }
    }
}
