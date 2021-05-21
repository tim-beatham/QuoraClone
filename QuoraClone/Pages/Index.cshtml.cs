using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using QuoraClone.Infrastructure;

namespace QuoraClone.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }


        // Logs the user out of there current session
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Set<string>("Username", default);
            HttpContext.Session.Set<string>("PasswordHash", default);
            return Page();
        }

    }
}
