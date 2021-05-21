using QuoraClone.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuoraClone.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace QuoraClone.Pages
{
    public class UserModel : PageModel
    {
        
        private readonly QuoraCloneDbContext _context;

        public UserModel(QuoraCloneDbContext context)
        {
            _context = context;
        }

        public List<(UserQuestion, string)> AnsweredQuestions { get; set; }

        public IActionResult OnGet()
        {
            string username = HttpContext.Session.Get<string>("Username");  

            if (username == default)
            {
                return RedirectToPage("Login");
            }

            // Get the current user
            User user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return RedirectToPage("Login");
            }

            // Get the username from the list of answered questions.
            List<UserQuestion> answered = _context.UserQuestions.Where(u => u.Username == username).ToList();

            List<string> questionTitles = new List<string>();

            foreach (var question in answered)
            {
                Models.Question q = _context.Questions.FirstOrDefault(q => q.QuestionID == question.QuestionID);
                questionTitles.Add(q.QuestionTitle);
            }

            var userAnswers = answered.Zip(questionTitles, (answer, title) => (answer, title)).ToList();

            AnsweredQuestions = userAnswers;

            return Page();
        }
    }

}