using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace Algenic.Commands.CreateContest
{
    public class CreateContestCommandHandler : ICommandHandler<CreateContestCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateContestCommandHandler(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task HandleAsync(CreateContestCommand command)
        {
            var user = await _userManager.GetUserAsync(command.User);
            var contest = new Contest
            {
                Name = command.ContestName,
                IdentityUser = user,
                Status = Contest.ContestState.NotStarted
            };

            await _dbContext.Contests.AddAsync(contest);
            await _dbContext.SaveChangesAsync();
        }
    }
}