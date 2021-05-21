using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using QuoraClone.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using QuoraClone.Infrastructure;

namespace QuoraClone.Pages
{
    public class LoginModel : PageModel
    {
        private readonly QuoraCloneDbContext _context;

        public LoginModel(QuoraCloneDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        // Represents the url to return to...
        public string ReturnUrl { get; set; }


        // On Get do nothing...
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        // On Post we need to check if the Username exists
        // in the database.
        public IActionResult OnPostLogin(string returnurl = "")
        {   
            // If the model that the User provided is not valid
            // then we need to make the User aware.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Find the User in the database
            User actualUser = _context.Users.FirstOrDefault(u => User.Username == u.Username);

            // Check whether or not the Username has been defined.
            if (actualUser == null)
            {
                ModelState.AddModelError("Username", "User does not exist!");
                return Page();
            }

            // Check if the password hash matches.
            // If they do not mathc then we have a validation error and thus
            // throw an error.
            if (actualUser.PasswordHash != User.PasswordHash)
            {
                ModelState.AddModelError("Password", "Password is incorrect!");
                return Page();
            }

            // Store the password hash and Username in the session state.
            HttpContext.Session.Set<string>("Username", User.Username);
            HttpContext.Session.Set<string>("PasswordHash", User.PasswordHash);

            if (string.IsNullOrEmpty(returnurl))
            {
                return Redirect("Index");
            }
            else
            {
                return Redirect(returnurl);
            }

        }
    }
}