using Algenic.Data;
using Algenic.UnitTests.Setup;
using Xunit;
using Algenic.Commands.CreateScorePolicy;
using Algenic.Commons;
using Algenic.Data.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;


namespace Algenic.UnitTests.Commands
{
    public class CreateScorePolicyTests : BaseDatabaseTest
    {
        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
            => context;
        [Fact]
        public void SampleVerification_ExpectedNewScirePolicyInDatabase()
        {
            var givenContest = new Contest
            {
                Name = "",
                Status = Contest.ContestState.InProgress
            };
            Context.Contests.Add(givenContest);
            var givenTask = new Task
            {
                Contest = givenContest,
                Description = "",
                Name = "",
            };
            Context.Tasks.Add(givenTask);
            var givenTest = new Test
            {
                ExpectedOutput = "expected-output",
                Input = "given-input",
                Task = givenTask
            };
            Context.Tests.Add(givenTest);
            var givenSolution = new Solution
            {
                SourceCode = "given-source-code",
                Language = "java",
                Task = givenTask
            };

            var firstscoreRule = new ScoreRuleDto(1, 1);
            var secondscoreRule = new ScoreRuleDto(2, 2);

            var scoreRulesDto = new List<ScoreRuleDto>();
            scoreRulesDto.Add(firstscoreRule);
            scoreRulesDto.Add(secondscoreRule);

            Context.SaveChanges();

            ICommandHandler<CreateScorePolicyCommand> sut =
                new CreateScorePolicyCommandHandler(Context);

            string name = "ScorePolicy1";
            string description = "description for ScorePolicy1";
            
            var command = CreateScorePolicyCommand.Create(name, description,scoreRulesDto);

            sut.HandleAsync(command).Wait();
            var scorePolicy = Context.ScorePolicies.Find(name);

            scorePolicy.Name.Should().Be(name);
            scorePolicy.Description.Should().Be(description);
            var scoreRules = scoreRulesDto.Select(dto => new ScoreRule() { Threshold = dto.Threshold, Score = dto.Score });
            scorePolicy.ScoreRules.Should().HaveCount(scoreRulesDto.Count()).And.BeEquivalentTo(scoreRules);
        }
    }
}
