using System.Collections.Generic;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Data;
using Algenic.Data.Models;

namespace Algenic.Commands.CreateScorePolicy
{
    public class CreateScorePolicyCommand
    {
        public string Name { get; }
        public string Description { get; }
        public List<ScoreRule> ScoreRules {get;}
        public readonly ScorePolicy ScorePolicy = new ScorePolicy();

        private CreateScorePolicyCommand(string name, string description, List<ScoreRule> scoreRules)
        {
            ScorePolicy.Name = name;
            ScorePolicy.Description = description;
            ScorePolicy.ScoreRules = scoreRules;
        }

        public static CreateScorePolicyCommand Create(string name, string description, List<ScoreRule> scoreRules)
        {
            Fail.IfNull(name);
            Fail.IfNull(description);
            Fail.IfNull(scoreRules);
            return new CreateScorePolicyCommand(name,description,scoreRules);
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

        public async System.Threading.Tasks.Task HandleAsync(CreateScorePolicyCommand command)
        {
            await _dbContext.ScorePolicies.AddAsync(command.ScorePolicy);
            await _dbContext.SaveChangesAsync();
        }
    }
}
