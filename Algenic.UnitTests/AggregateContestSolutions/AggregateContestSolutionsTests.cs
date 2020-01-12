using System.Linq;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.AggregateContestSolutions;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Algenic.UnitTests.AggregateContestSolutions
{
    public class AggregateContestSolutionsTests : BaseDatabaseTest
    {
        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
            => context;

        [Fact]
        public void SampleContest_ReturnExpectedAggregate()
        {
            // Setup DB.
            var givenUser = new IdentityUser("USER");
            Context.Users.Add(givenUser);
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
                Name = "task-name",
            };
            Context.Tasks.Add(givenTask);
            var givenSolution = new Solution
            {
                SourceCode = "given-source-code",
                Language = "java",
                Task = givenTask,
                IdentityUser = givenUser
            };
            Context.Solutions.Add(givenSolution);
            Context.SaveChanges();

            IQueryHandler<AggregateContestSolutionsQuery, AggregateContestSolutionsResult> sut =
                new AggregateContestSolutionsQueryHandler(Context);

            var givenQuery = AggregateContestSolutionsQuery.Create(givenContest.Id);

            var aggregate = sut.HandleAsync(givenQuery).Result.Aggregate;

            var actualKey = aggregate.Users.Keys.Single();
            var actualValue = aggregate.Users.Values.Single();

            actualKey.Should().Be(givenUser.Id);
            actualValue.Tasks.Should().HaveCount(1)
                .And.ContainSingle(dto => dto.Name == givenTask.Name);
        }
    }
}
