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
        public void SampleVerification_ExpectedNewScorePolicyInDatabase()
        {
            var firstscoreRule = new ScoreRuleDto(1, 1);
            var secondscoreRule = new ScoreRuleDto(2, 2);

            var givenScoreRulesDtos = new List<ScoreRuleDto>();
            givenScoreRulesDtos.Add(firstscoreRule);
            givenScoreRulesDtos.Add(secondscoreRule);

            ICommandHandler<CreateScorePolicyCommand> sut =
                new CreateScorePolicyCommandHandler(Context);

            string givenName = "ScorePolicy1";
            string givenDescription = "description for ScorePolicy1";
            
            var command = CreateScorePolicyCommand.Create(givenName, givenDescription, givenScoreRulesDtos);

            sut.HandleAsync(command).Wait();
            var scorePolicy = Context.ScorePolicies.Single(p => p.Name == givenName);

            scorePolicy.Name.Should().Be(givenName);
            scorePolicy.Description.Should().Be(givenDescription);
            scorePolicy.ScoreRules.Should().HaveCount(2)
                .And.Contain(rule =>
                    rule.Threshold == firstscoreRule.Threshold && rule.Score == firstscoreRule.Score)
                .And.Contain(rule =>
                    rule.Threshold == secondscoreRule.Threshold && rule.Score == secondscoreRule.Score);
        }
    }
}
