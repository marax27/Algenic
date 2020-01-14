using Algenic.Commons;
using Algenic.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Commands.ChangeScore
{
    public class ChangeScoreCommandHandler : ICommandHandler<ChangeScoreCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public ChangeScoreCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(ChangeScoreCommand command)
        {
            var solution = await _dbContext.Solutions.SingleAsync(s => s.Id == command.SolutionId);
            solution.PointCount = command.Score;
            await _dbContext.SaveChangesAsync();
        }
    }
}
