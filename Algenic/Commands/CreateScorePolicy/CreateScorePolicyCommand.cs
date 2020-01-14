using System.Collections.Generic;
using System.Linq;
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
        public List<ScoreRuleDto> ScoreRules {get;}
        //public readonly ScorePolicy ScorePolicy = new ScorePolicy();

        private CreateScorePolicyCommand(string name, string description, List<ScoreRuleDto> scoreRules)
        {
            Name = name;
            Description = description;
            ScoreRules = scoreRules;
        }

        public static CreateScorePolicyCommand Create(string name, string description, List<ScoreRuleDto> scoreRules)
        {
            Fail.IfNullOrEmpty(name);
            Fail.IfNullOrEmpty(description);
            Fail.IfNullOrEmpty(scoreRules);
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
            // scoreRules = new List<ScoreRule>();
            var scoreRules = command.ScoreRules.Select(dto => new ScoreRule() { Threshold = dto.Threshold, Score = dto.Score });
            ScorePolicy scorePolicy = new ScorePolicy()
            {
                Name = command.Name,
                Description = command.Description,
                ScoreRules = new List<ScoreRule>(scoreRules)

            };
            await _dbContext.ScorePolicies.AddAsync(scorePolicy);
            await _dbContext.SaveChangesAsync();
        }
    }
}
