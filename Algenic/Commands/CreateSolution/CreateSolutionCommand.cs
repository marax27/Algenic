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
        public string Username { get; }

        private CreateSolutionCommand(string sourceCode, string languageCode, int taskId, string username)
        {
            SourceCode = sourceCode;
            LanguageCode = languageCode;
            TaskId = taskId;
            Username = username;
        }

        public static CreateSolutionCommand Create(string sourceCode, string languageCode, int taskId, string username)
        {
            Fail.IfNullOrEmpty(sourceCode);
            Fail.IfNullOrEmpty(languageCode);
            Fail.IfNull(taskId);
            Fail.IfNull(username);

            return new CreateSolutionCommand(sourceCode, languageCode, taskId, username);
        }
    }
}
