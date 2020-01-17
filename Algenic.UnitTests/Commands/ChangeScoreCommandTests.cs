using Algenic.Data;
using Algenic.UnitTests.Setup;
using Xunit;
using Algenic.Commands.ChangeScore;
using Algenic.Commons;
using Algenic.Data.Models;
using Algenic.Compilation.Outputs;
using Algenic.Compilation.Utilities;
using Algenic.Queries.Compilation;
using FluentAssertions;

namespace Algenic.UnitTests.Commands
{
    public class ChangeScoreCommandTests : BaseDatabaseTest
    {
        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
            => context;
        [Fact]
        public void Test1()
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
            Context.Solutions.Add(givenSolution);
            Context.SaveChanges();

            var givenApiOutput = new JDoodleOutput
            {
                CpuTime = "1",
                Memory = "2",
                Output = "expected-output",
                StatusCode = "200"
            };
            var givenQuery = CompilationQuery.Create("given-source-code", "given-input", ProgrammingLanguage.Java());
            var givenResult = new CompilationQueryResult
            {
                ExecutionSuccessful = true,
                Output = givenApiOutput
            };

            IQueryHandler<CompilationQuery, CompilationQueryResult> compilationMock =
                new CompilationMockQueryHandler()
                    .On(givenQuery).Returns(givenResult);

            ICommandHandler<ChangeScoreCommand> sut =
                new ChangeScoreCommandHandler(Context);

            int newScore = 2;
            var command = ChangeScoreCommand.Create(givenSolution.Id,newScore);

            sut.HandleAsync(command).Wait();
            var solution = Context.Solutions.Find(givenSolution.Id);
            solution.PointCount.Should().Be(newScore);
        }
    }
}
