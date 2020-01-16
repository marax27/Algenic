using System.Linq;
using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;

namespace Algenic.Commands.ExaminerRole
{
    public class ExaminerRoleCommand
    {
        public string UserId { get; }

        private ExaminerRoleCommand(string userId)
            => UserId = userId;

        public static ExaminerRoleCommand Create(string userId)
        {
            Fail.IfNullOrEmpty(userId);
            return new ExaminerRoleCommand(userId);
        }
    }

    public class ExaminerRoleCommandHandler : ICommandHandler<ExaminerRoleCommand>
    {
        private ApplicationDbContext _dbContext;
        private UserManager<IdentityUser> _userManager;

        public ExaminerRoleCommandHandler(UserManager<IdentityUser> userManager, 
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task HandleAsync(ExaminerRoleCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            var isExaminer = await _userManager.IsInRoleAsync(user, RoleName);

            if (isExaminer)
            {
                var contests = _dbContext.Contests.Where(c => c.IdentityUser.Id == user.Id);
                foreach (var contest in contests)
                {
                    contest.IdentityUser = GetAdministrator();
                }
                await _dbContext.SaveChangesAsync();
                await _userManager.RemoveFromRoleAsync(user, RoleName);
            }
            else
                await _userManager.AddToRoleAsync(user, RoleName);
        }

        private IdentityUser GetAdministrator()
        {
            return _userManager.GetUsersInRoleAsync("Admin").Result.Single();
        }

        public const string RoleName = "Examiner";
    }
}
