using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using System;
using System.Linq;

namespace Algenic.Commands.CreateSolution
{
    public class CreateSolutionCommandHandler : ICommandHandler<CreateSolutionCommand>
    {
        private readonly ApplicationDbContext _dbContext;
    
        public CreateSolutionCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task HandleAsync(CreateSolutionCommand command)
        {
            var user = _dbContext.Users.Single(u => u.UserName == command.Username);
            var solution = new Solution
            {
                SourceCode = command.SourceCode,
                Language = command.LanguageCode,
                TaskId = command.TaskId,
                TimeStamp = DateTime.Now,
                IdentityUser = user
            };

            await _dbContext.Solutions.AddAsync(solution);

            var previousSolutions = _dbContext.Solutions
                .Where(s => s.TaskId == command.TaskId && s.IdentityUser.Id == user.Id && s.Id != solution.Id);

            _dbContext.Solutions.RemoveRange(previousSolutions);

            await _dbContext.SaveChangesAsync();
        }
    }
}
