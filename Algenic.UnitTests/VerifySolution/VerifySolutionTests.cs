using System.Linq;
using Algenic.Commands.VerifySolution;
using Algenic.Commons;
using Algenic.Compilation.Outputs;
using Algenic.Compilation.Utilities;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.Compilation;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Xunit;

namespace Algenic.UnitTests.VerifySolution
{ 
    public class VerifySolutionTests : BaseDatabaseTest
    {
        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
            => context;

        [Fact]
        public void SampleVerification_ExpectedCompilationResultInDatabase()
        {
            // Setup DB.
            var givenContest = new Contest
            {
                Name = "", Status = Contest.ContestState.InProgress
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

            ICommandHandler<VerifySolutionCommand> sut = 
                new VerifySolutionCommandHandler(Context, compilationMock);

            var command = VerifySolutionCommand.Create(givenSolution.Id);

            sut.HandleAsync(command).Wait();

            var compilationResult = Context.CompilationResults.Single();
            compilationResult.ExecutionSuccessful.Should().BeTrue();
            compilationResult.CpuTime.Should().Be(givenApiOutput.CpuTime);
            compilationResult.MemoryUsage.Should().Be(givenApiOutput.Memory);
        }
    }
}
