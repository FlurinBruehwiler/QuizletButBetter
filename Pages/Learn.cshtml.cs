using M133.Models;
using M133.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class LearnModel : PageModel
{
    private readonly QuizletContext _quizletContext;

    public Card Card { get; set; }

    public LearnModel(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }

    public IActionResult OnGet(int id)
    {
        if(!_quizletContext.StudySets.Any(x => x.StudySetId == id))
            return RedirectToPage("./Index");

        return Page();
    }
}