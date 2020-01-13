using System;
using System.Collections.Generic;
using System.Linq;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.NewestSolutions;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Algenic.UnitTests.Queries
{
    public class NewestSolutionsTests : BaseDatabaseTest
    {
        private IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> Sut { get; }

        private Contest GivenContest { get; set; }
        private Task GivenFirstTask { get; set; }
        private Task GivenSecondTask { get; set; }
        private IdentityUser GivenFirstUser { get; set; }
        private IdentityUser GivenSecondUser { get; set; }

        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
        {
            GivenContest = new Contest { Name = "C1", Status = Contest.ContestState.InProgress };
            context.Contests.Add(GivenContest);

            GivenFirstTask = new Task {Contest = GivenContest, Description = "T1"};
            GivenSecondTask = new Task {Contest = GivenContest, Description = "T2"};
            context.Tasks.AddRange(GivenFirstTask, GivenSecondTask);

            GivenFirstUser = new IdentityUser("test-user");
            GivenSecondUser = new IdentityUser("another-user");
            context.Users.AddRange(GivenFirstUser, GivenSecondUser);

            context.SaveChanges();
            return context;
        }

        public NewestSolutionsTests()
        {
            Sut = new NewestSolutionsQueryHandler(Context);
        }

        [Fact]
        public void NoSolutionsInDatabase_NoSolutionsReturned()
        {
            var expectedSolutionIds = new List<int>();

            var givenQuery = NewestSolutionsQuery.Create(GivenContest.Id);
            var result = Sut.HandleAsync(givenQuery).Result;

            var actualSolutionIds = result.SolutionIds;

            actualSolutionIds.Should().BeEquivalentTo(expectedSolutionIds);
        }

        [Fact]
        public void TwoSolutionsForDifferentTasks_BothSolutionsReturned()
        {
            var givenFirstSolution = new Solution {IdentityUser = GivenFirstUser, Task = GivenFirstTask};
            var givenSecondSolution = new Solution {IdentityUser = GivenFirstUser, Task = GivenSecondTask};
            var givenSolutions = new[] {givenFirstSolution, givenSecondSolution};
            Context.Solutions.AddRange(givenSolutions);
            Context.SaveChanges();

            var expectedSolutionIds = givenSolutions.Select(s => s.Id);

            var givenQuery = NewestSolutionsQuery.Create(GivenContest.Id);
            var result = Sut.HandleAsync(givenQuery).Result;
            var actualSolutionIds = result.SolutionIds;

            actualSolutionIds.Should().BeEquivalentTo(expectedSolutionIds);
        }

        [Fact]
        public void TwoSolutionsFromDifferentUsersForSingleTask_BothSolutionsReturned()
        { 
            var givenFirstSolution = new Solution { IdentityUser = GivenFirstUser, Task = GivenFirstTask };
            var givenSecondSolution = new Solution { IdentityUser = GivenSecondUser, Task = GivenFirstTask};
            var givenSolutions = new[] { givenFirstSolution, givenSecondSolution };
            Context.Solutions.AddRange(givenSolutions);
            Context.SaveChanges();

            var expectedSolutionIds = givenSolutions.Select(s => s.Id);

            var givenQuery = NewestSolutionsQuery.Create(GivenContest.Id);
            var result = Sut.HandleAsync(givenQuery).Result;
            var actualSolutionIds = result.SolutionIds;

            actualSolutionIds.Should().BeEquivalentTo(expectedSolutionIds);
        }

        [Fact]
        public void TwoSolutionsFromSingleUserForSingleTask_NewerSolutionReturned()
        {
            var givenEarlierDate = new DateTime(1999, 5, 5);
            var givenLaterDate = new DateTime(1999, 5, 6);

            var givenEarlierSolution = new Solution
                {IdentityUser = GivenFirstUser, Task = GivenFirstTask, TimeStamp = givenEarlierDate};
            var givenLaterSolution = new Solution
                { IdentityUser = GivenFirstUser, Task = GivenFirstTask, TimeStamp = givenLaterDate };


            Context.Solutions.AddRange(givenEarlierSolution, givenLaterSolution);
            Context.SaveChanges();

            var expectedSolutionIds = new[] {givenLaterSolution.Id};

            var givenQuery = NewestSolutionsQuery.Create(GivenContest.Id);
            var result = Sut.HandleAsync(givenQuery).Result;
            var actualSolutionIds = result.SolutionIds;

            actualSolutionIds.Should().BeEquivalentTo(expectedSolutionIds);
        }
    }
}
