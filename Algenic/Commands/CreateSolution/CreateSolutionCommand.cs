using Algenic.Commons.DesignByContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Algenic.Commands.CreateSolution
{
    public class CreateSolutionCommand
    {
        public string SourceCode { get; }
        public string LanguageCode { get; }
        public int TaskId { get; }
        public ClaimsPrincipal User { get; }

        private CreateSolutionCommand(string sourceCode, string languageCode, int taskId, ClaimsPrincipal user)
        {
            SourceCode = sourceCode;
            LanguageCode = languageCode;
            TaskId = taskId;
            User = user;
        }

        public static CreateSolutionCommand Create(string sourceCode, string languageCode, int taskId, ClaimsPrincipal user)
        {
            Fail.IfNullOrEmpty(sourceCode);
            Fail.IfNullOrEmpty(languageCode);
            Fail.IfNull(taskId);
            Fail.IfNull(user);

            return new CreateSolutionCommand(sourceCode, languageCode, taskId, user);
        }
    }
}
