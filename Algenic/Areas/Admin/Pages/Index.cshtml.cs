using System.Collections.Generic;
using System.Threading.Tasks;
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

        public IndexModel(IQueryHandler<AllUsersQuery, AllUsersResult> allUsersQueryHandler)
        {
            _allUsersQueryHandler = allUsersQueryHandler;
        }

        [BindProperty] public IEnumerable<UserDto> Users { get; set; }

        public void OnGet()
        {
            UpdateUsers();
        }

        public async Task<IActionResult> OnPostAccess(string userId)
        {
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
