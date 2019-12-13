using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Routing
{
    public class DefaultRedirections
    {
        private PageModel PageModel { get; set; }
        public DefaultRedirections(PageModel pageModel)
        {
            PageModel = pageModel;
        }

        public IActionResult ToAccessDeniedPage(string returnUrl)
        {
            return PageModel.RedirectToPage("/Account/AccessDenied", new { area = "Identity", ReturnUrl = returnUrl });
        }

        public IActionResult ToLoginPage(string returnUrl)
        {
            return PageModel.RedirectToPage("/Account/Login", new { area = "Identity", ReturnUrl = returnUrl });
        }
    }
}
