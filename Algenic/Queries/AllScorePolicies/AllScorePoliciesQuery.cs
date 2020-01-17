using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;

namespace Algenic.Queries.AllScorePolicies
{
    public class AllScorePoliciesQuery
    {
        private AllScorePoliciesQuery() { }

        public static AllScorePoliciesQuery Create() => new AllScorePoliciesQuery();
    }

    public class AllScorePoliciesResult
    {
        public IEnumerable<ScorePolicyDto> ScorePolicies { get; }

        public AllScorePoliciesResult(IEnumerable<ScorePolicyDto> scorePolicies)
            => ScorePolicies = scorePolicies;
    }

    public class AllScorePoliciesQueryHandler : IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public AllScorePoliciesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AllScorePoliciesResult> HandleAsync(AllScorePoliciesQuery query)
        {
            var dtos = _dbContext.ScorePolicies
                .Where(policy => policy.ScoreRules.Count > 0)
                .Select(policy => new ScorePolicyDto(policy.Id, policy.Name, policy.Description))
                .AsEnumerable();
            return new AllScorePoliciesResult(dtos);
        }
    }
}
