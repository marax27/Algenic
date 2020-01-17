using System.Linq;
using Algenic.Commands.CreateSolution;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Algenic.UnitTests.Commands
{
    public class CreateSolutionTests : BaseDatabaseTest
    {
        private ICommandHandler<CreateSolutionCommand> Sut { get; }

        private IdentityUser SampleUser { get; set; }
        private Task SampleTask { get; set; }
        private Contest SampleContest { get; set; }

        public CreateSolutionTests()
        {
            Sut = new CreateSolutionCommandHandler(Context);
        }

        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
        {
            SampleUser = new IdentityUser("sample-user");
            SampleTask = new Task { Name = "sample-task" };
            SampleContest = new Contest { Name = "sample-contest", Tasks = new[] { SampleTask } };

            context.Users.Add(SampleUser);
            context.Contests.Add(SampleContest);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void CreateSampleSolution_SolutionPresentInDatabase()
        {
            var givenSourceCode = "{...}";
            var givenTaskId = SampleTask.Id;
            var givenLanguage = "cpp";
            var givenUsername = SampleUser.UserName;
            var command = CreateSolutionCommand.Create(givenSourceCode, givenLanguage, givenTaskId, givenUsername);

            Sut.HandleAsync(command).Wait();
            var solution = Context.Solutions
                .Single(s => s.Task == SampleTask);

            solution.IdentityUser.Should().Be(SampleUser);
            solution.SourceCode.Should().Be(givenSourceCode);
            solution.Language.Should().Be(givenLanguage);
        }
    }
}
