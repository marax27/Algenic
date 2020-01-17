using System.Collections.Generic;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Queries.AllScorePolicies;
using Algenic.Data.Models;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Xunit;

namespace Algenic.UnitTests.Queries
{
    public class AllScorePoliciesQueryTests : BaseDatabaseTest
    {
        private ScorePolicyDto ScorePolicyDto { get; set; }

        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
        {
            foreach (var entity in context.ScorePolicies)
            {
                context.ScorePolicies.Remove(entity);
            }
            context.SaveChanges();
            return context;
        }
      
        [Fact]
        public void NoScorePolicyInDatabase_NoPolicyReturned()
        {
            var expectedScorePolicyIds = new List<ScorePolicyDto>();
            var givenQuery = AllScorePoliciesQuery.Create();
            IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult> sut =
                            new AllScorePoliciesQueryHandler(Context);
            var result = sut.HandleAsync(givenQuery).Result;
            var actualScorePolicy = result.ScorePolicies;
            actualScorePolicy.Should().BeEquivalentTo(expectedScorePolicyIds);

        }
        [Fact]
        public void TwoScorePolicyInDatabase_TwoPolicyReturned()
        {
            ScorePolicy firstScorePolicy = new ScorePolicy {
                Id = 1,
                Name = "test1",
                Description = "FirstScorePolicyTest"
            };
            ScorePolicy secondScorePolicy = new ScorePolicy
            {
                Id = 2,
                Name = "test2",
                Description = "SecondScorePolicyTest"
            };
            ScorePolicyDto firstScorePolicyDto = new ScorePolicyDto(1, "test1", "FirstScorePolicyTest");
            ScorePolicyDto secondScorePolicyDto = new ScorePolicyDto(2, "test2", "SecondScorePolicyTest");
            var expectedScorePolicyIds = new List<ScorePolicyDto>
            {
                firstScorePolicyDto,
                secondScorePolicyDto
            };
            var givenQuery = AllScorePoliciesQuery.Create();
            IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult> sut =
                            new AllScorePoliciesQueryHandler(Context);
            Context.ScorePolicies.AddRange(firstScorePolicy, secondScorePolicy);
            Context.SaveChanges();
            var result = sut.HandleAsync(givenQuery).Result;
            var actualScorePolicy = result.ScorePolicies;
            actualScorePolicy.Should().BeEquivalentTo(expectedScorePolicyIds).And.HaveCount(2);
        }
    }
}
