using System.Collections.Generic;
using System.Threading.Tasks;
using Algenic.Commands.ExaminerRole;
using Algenic.Commons;
using Algenic.Queries.AllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.Admin.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IQueryHandler<AllUsersQuery, AllUsersResult> _allUsersQueryHandler;
        private ICommandHandler<ExaminerRoleCommand> _examinerRoleCommandHandler;

        public IndexModel(IQueryHandler<AllUsersQuery, AllUsersResult> allUsersQueryHandler,
            ICommandHandler<ExaminerRoleCommand> examinerRoleCommandHandler)
        {
            _allUsersQueryHandler = allUsersQueryHandler;
            _examinerRoleCommandHandler = examinerRoleCommandHandler;
        }

        [BindProperty] public IEnumerable<UserDto> Users { get; set; }

        public void OnGet()
        {
            UpdateUsers();
        }

        public async Task<IActionResult> OnPostAccess(string userId)
        {
            var command = ExaminerRoleCommand.Create(userId);
            await _examinerRoleCommandHandler.HandleAsync(command);
            return RedirectToPage();
        }

        private void UpdateUsers()
        {
            var query = AllUsersQuery.Create();
            var result = _allUsersQueryHandler.HandleAsync(query).Result;
            Users = result.UserDtos;
        }
    }
}
