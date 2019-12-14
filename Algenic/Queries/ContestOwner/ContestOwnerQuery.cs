using System.Security.Claims;
using Algenic.Commons.DesignByContract;

namespace Algenic.Queries.ContestOwner
{
    public class ContestOwnerQuery
    {
        public int ContestId { get; }
        public ClaimsPrincipal User { get; }

        private ContestOwnerQuery(int contestId, ClaimsPrincipal user)
        {
            ContestId = contestId;
            User = user;
        }

        public static ContestOwnerQuery Create(int contestId, ClaimsPrincipal user)
        {
            Fail.IfNull(contestId);
            Fail.IfNull(user);

            return new ContestOwnerQuery(contestId, user);
        }
    }
}
