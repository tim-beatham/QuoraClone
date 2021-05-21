using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuoraClone.Models;
using Microsoft.AspNetCore.Http;

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

        public IActionResult OnGet(int? questionID)
        {
            if (questionID == null)
            {
                return NotFound();
            }

            Question = _context.Questions
                                .FirstOrDefault(q => q.QuestionID == questionID);
            if (Question == null)
            {
                return NotFound();
            }

            Responses = _context.UserQuestions
                            .Where(ua => ua.QuestionID == questionID)
                            .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddQuestion()
        {
            // TODO: Preferably check that the user is valid etc...
            _context.UserQuestions.Add(UserQuestion);
            await _context.SaveChangesAsync();
            return RedirectToPage("ViewQuestion", new { questionID = UserQuestion.QuestionID });            
        }
    }
}