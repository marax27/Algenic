using System;
using System.Collections.Generic;
using System.Text;
using Algenic.Commands.ExaminerRole;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.ContestScoreQuery;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Xunit;

namespace Algenic.UnitTests.Queries
{
    public class ContestScoreTests : BaseDatabaseTest
    {
        private IQueryHandler<ContestScoreQuery, ContestScoreQueryResult> Sut { get; }
        private Contest GivenContest { get; set; }

        public ContestScoreTests()
        {
            Sut = new ContestScoreQueryHandler(Context);
        }

        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
        {
            GivenContest = new Contest {Name = "given-contest"};
            context.Contests.Add(GivenContest);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void ExecuteHandle_UserIdAndContestIdAreTheSameInQueryAndResult()
        {
            var givenUserId = "user-id";
            var givenContestId = GivenContest.Id;

            var query = ContestScoreQuery.Create(givenUserId, givenContestId);
            var result = Sut.HandleAsync(query).Result;

            result.UserId.Should().Be(givenUserId);
            result.ContestId.Should().Be(givenContestId);
        }

        [Fact]
        public void QueryContestWithNoTasks_ReturnZeroPoints()
        {
            var givenUserId = "user-id";
            var givenContestId = GivenContest.Id;

            var query = ContestScoreQuery.Create(givenUserId, givenContestId);
            var result = Sut.HandleAsync(query).Result;

            result.Score.Should().Be(0);
        }
    }
}
