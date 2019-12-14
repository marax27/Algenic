using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Algenic.Queries.ContestOwner
{
    public class ContestOwnerQueryHandler : IQueryHandler<ContestOwnerQuery, ContestOwnerResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ContestOwnerQueryHandler(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ContestOwnerResult> HandleAsync(ContestOwnerQuery query)
        {
            var contest = await _dbContext.Contests.SingleAsync(c => c.Id == query.ContestId);
            var ownerId = contest.IdentityUser.Id;
            var userId = _userManager.GetUserId(query.User);
            return new ContestOwnerResult(ownerId == userId);
        }
    }
}
