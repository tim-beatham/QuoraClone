using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuoraClone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QuoraClone.Infrastructure;

namespace QuoraClone.Pages
{
    public class CreatePageModel : PageModel
    {
        private readonly QuoraCloneDbContext _context;

        public string ReturnUrl { get; set; }

        [BindProperty]
        public User User { get; set; }

        public CreatePageModel(QuoraCloneDbContext context)
        {
            _context = context;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostUser(string returnUrl = "")
        {
            
            // Here we perform server side validation checks.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the user name is already in the database...
            // If the username is already in the database then
            // return an error.

            User userInDb = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Username);

            if (userInDb != null)
            {
                ModelState.AddModelError("Username", "Username taken!");
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            
            // Store the password hash and username in the session state.
            HttpContext.Session.Set<string>("Username", User.Username);
            HttpContext.Session.Set<string>("PasswordHash", User.PasswordHash);


            if (string.IsNullOrEmpty(returnUrl))
            {
                return Redirect("Index");
            }


            return Redirect(returnUrl);
        }

    }
}