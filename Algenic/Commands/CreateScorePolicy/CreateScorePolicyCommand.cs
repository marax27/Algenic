using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;

namespace Algenic.Commands.CreateScorePolicy
{
    public class CreateScorePolicyCommand
    {
        private CreateScorePolicyCommand() { }

        public static CreateScorePolicyCommand Create()
        {
            // Validation.
            return new CreateScorePolicyCommand();
        }
    }

    public class ScoreRuleDto
    {
        public decimal Threshold { get; }
        public int Score { get; set; }

        public ScoreRuleDto(decimal threshold, int score)
        {
            Threshold = threshold;
            Score = score;
        }
    }

    public class CreateScorePolicyCommandHandler : ICommandHandler<CreateScorePolicyCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateScorePolicyCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task HandleAsync(CreateScorePolicyCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
