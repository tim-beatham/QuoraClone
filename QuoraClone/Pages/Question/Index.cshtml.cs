using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuoraClone.Models;

namespace QuoraClone.Pages_Question
{
    public class IndexModel : PageModel
    {
        private readonly QuoraCloneDbContext _context;

        public IndexModel(QuoraClone.Models.QuoraCloneDbContext context)
        {
     
            _context = context;
        }

        public IList<Question> QuestionList { get;set; }

        [BindProperty]
        public Question Question { get; set; }

        public async Task OnGetAsync()
        {
            QuestionList = await _context.Questions.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddQuestionAsync()
        {
            if (!ModelState.IsValid)
            {
                QuestionList = await  _context.Questions.ToListAsync();
                return Page();
            }
        
            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
