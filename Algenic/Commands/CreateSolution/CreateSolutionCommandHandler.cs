﻿using Algenic.Data;
using Algenic.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Commands.CreateSolution
{
    public class CreateSolutionCommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
    
        public CreateSolutionCommandHandler(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task HandleAsync(CreateSolutionCommand command)
        {
            var user = await _userManager.GetUserAsync(command.User);
            var solution = new Solution()
            {
                SourceCode = command.SourceCode,
                Language = command.LanguageCode,
                TaskId = command.TaskId,
                TimeStamp = DateTime.Now,
                IdentityUser = user
            };

            await _dbContext.Solutions.AddAsync(solution);
            await _dbContext.SaveChangesAsync();
        }
    }
}