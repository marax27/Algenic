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

    public class TestResultsQueryHandler : IQueryHandler<TestResultsQuery, TestResultsQueryResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public TestResultsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TestResultsQueryResult> HandleAsync(TestResultsQuery query)
        {
            var test = await _dbContext.Tests.FindAsync(query.TestId);
            var compilationResults = _dbContext.CompilationResults
                .Where(cr => cr.TestId == query.TestId && cr.SolutionId == query.SolutionId)
                .SingleOrDefault();
            var logs = _dbContext.Logs
                ?.Where(l => l.TestId == query.TestId && l.SolutionId == query.SolutionId)
                .SingleOrDefault();

            return new TestResultsQueryResult(query.TestId, 
                query.SolutionId,
                test.Name,
                test.Input,
                compilationResults.Output,
                compilationResults.CpuTime,
                compilationResults.MemoryUsage,
                compilationResults.ExecutionSuccessful,
                logs?.StatusCode,
                logs?.ErrorMessage);
        }
    }

    public class TestResultsQueryResult
    {
        public int TestId { get; }
        public int SolutionId { get; }
        public string TestName { get; }
        public string Input { get; }
        public string Output { get; }
        public string CpuTime { get; }
        public string MemoryUsage { get; }
        public bool ExecutionSuccessful { get; }
        public string StatusCode { get; }
        public string ErrorMessage { get; }

        public TestResultsQueryResult(int testId, int solutionId, string testName, 
            string input, string output, string cpuTime, string memoryUsage, bool executionSuccessful,
            string statusCode, string errorMessage)
        {
            TestId = testId;
            SolutionId = solutionId;
            TestName = testName;
            Input = input;
            Output = output ?? "null";
            CpuTime = cpuTime ?? "null";
            MemoryUsage = memoryUsage ?? "null";
            ExecutionSuccessful = executionSuccessful;
            StatusCode = statusCode ?? "null";
            ErrorMessage = errorMessage ?? "null";
        }
    }
}
