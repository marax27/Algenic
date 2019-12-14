using System.Security.Claims;
using Algenic.Commons.DesignByContract;

namespace Algenic.Commands.CreateContest
{
    public class CreateContestCommand
    {
        public string ContestName { get; }
        public ClaimsPrincipal User { get; }

        private CreateContestCommand(string name, ClaimsPrincipal user)
        {
            ContestName = name;
            User = user;
        }

        public static CreateContestCommand Create(string name, ClaimsPrincipal user)
        {
            Fail.IfNullOrEmpty(name);
            Fail.IfNull(user);

            return new CreateContestCommand(name, user);
        }
    }
}
