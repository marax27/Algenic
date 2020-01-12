using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algenic.Commands.CreateSolution;
using Algenic.Commons;
using Algenic.Compilation.Utilities;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.Pages.Tasks
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ICommandHandler<CreateSolutionCommand> _createSolutionCommandHandler;

        [BindProperty]
        public Data.Models.Task Task { get; set; }

        [BindProperty]
        public IFormFile SourceCodeFile { get; set; }

        public ViewModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            ICommandHandler<CreateSolutionCommand> createSolutionCommandHandler)
        {
            _context = context;
            _userManager = userManager;
            _createSolutionCommandHandler = createSolutionCommandHandler;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var defaultRedirections = new DefaultRedirections(this);

            if (!User.Identity.IsAuthenticated)
                return defaultRedirections.ToLoginPage(HttpContext.Request.Path);

            Task = await _context.Tasks.FindAsync(id);

            var taskCreatorId = Task.Contest.IdentityUser.Id;
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == taskCreatorId || Task.Contest.Status == Contest.ContestState.NotStarted)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SourceCodeFile == null || SourceCodeFile.Length == 0)
                return RedirectToPage();

            string sourceCode;
            using (var stream = SourceCodeFile.OpenReadStream())
                sourceCode = GetSourceCode(stream);

            var fileName = Path.GetFileName(SourceCodeFile.FileName);
            var extension = Path.GetExtension(fileName).Substring(1);

            ProgrammingLanguage language;
            try
            {
                language = ProgrammingLanguageFactory.Get(extension);
            }
            catch
            {
                return RedirectToPage();
            }

            var command = CreateSolutionCommand.Create(sourceCode, language.LanguageCode, Task.Id, User);
            await _createSolutionCommandHandler.HandleAsync(command);

            return RedirectToPage();
        }

        private string GetSourceCode(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, Convert.ToInt32(stream.Length));
            var sourceCode = Encoding.UTF8.GetString(bytes);

            return sourceCode;
        }
    }
}
