using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Queries.TestResults
{
    public class TestResultsQuery
    {
        public int TestId { get; }
        public int SolutionId { get; }

        private TestResultsQuery(int testId, int solutionId)
        {
            TestId = testId;
            SolutionId = solutionId;
        }

        public static TestResultsQuery Create(int testId, int solutionId)
        {
            Fail.If(testId <= 0);
            Fail.If(solutionId <= 0);

            return new TestResultsQuery(testId, solutionId);
        }
    }

    public class TestResultsQueryHandler : IQueryHandler<TestResultsQuery, TestResultsResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public TestResultsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TestResultsResult> HandleAsync(TestResultsQuery query)
        {
            var test = await _dbContext.Tests.FindAsync(query.TestId);
            var compilationResults = _dbContext.CompilationResults
                .Where(cr => cr.TestId == query.TestId && cr.SolutionId == query.SolutionId)
                .SingleOrDefault();

            return new TestResultsResult(test.Name,
                compilationResults.Output,
                compilationResults.CpuTime,
                compilationResults.MemoryUsage,
                compilationResults.ExecutionSuccessful);
        }
    }

    public class TestResultsResult
    {
        public string TestName { get; }
        public string Output { get; }
        public string CpuTime { get; }
        public string MemoryUsage { get; }
        public bool ExecutionSuccessful { get; }

        public TestResultsResult(string testName, string output, string cpuTime, string memoryUsage, bool executionSuccessful)
        {
            TestName = testName;
            Output = output;
            CpuTime = cpuTime;
            MemoryUsage = memoryUsage;
            ExecutionSuccessful = executionSuccessful;
        }
    }
}
