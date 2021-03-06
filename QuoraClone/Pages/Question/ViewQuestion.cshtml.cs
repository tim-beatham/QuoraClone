using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuoraClone.Models;

namespace QuoraClone.Pages_Question
{
    public class ViewQuestionModel : PageModel
    {
        private readonly QuoraCloneDbContext _context;


        public ViewQuestionModel(QuoraCloneDbContext context)
        {
            _context = context;
        }

        public Question Question { get; set; }

        public IEnumerable<UserQuestion> Responses { get; set; }

        [BindProperty]
        public UserQuestion UserQuestion { get; set; }

        public async Task<IActionResult> OnGet(int? questionID)
        {
            if (questionID == null)
            {
                return NotFound();
            }

            var questions = await _context.GetQuestionsAsync();

            Question = questions.FirstOrDefault(q => q.QuestionID == questionID);
            
            if (Question == null)
            {
                return NotFound();
            }

            var responses = await _context.GetUserQuestionsAsync();

            Responses = (IEnumerable<UserQuestion>) responses.Where(ua => ua.QuestionID == questionID)
                                                    .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddQuestion()
        {
            // TODO: Preferably check that the user is valid etc...
            if (!ModelState.IsValid) 
            {
                await OnGet(UserQuestion.QuestionID);
                return Page();
            }

            _context.UserQuestions.Add(UserQuestion);
            await _context.SaveChangesAsync();
            return RedirectToPage("ViewQuestion", new { questionID = UserQuestion.QuestionID });            
        }
    }
}