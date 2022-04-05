using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ImportStudySetModel : PageModel
{
    private readonly QuizletContext _quizletContext;

    [BindProperty]
    public string Title { get; set; } = null!;

    [BindProperty]
    public string TextArea { get; set; } = null!;

    public ImportStudySetModel(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }

    public IActionResult OnGet()
    {
        return Page();
    }
    
    public IActionResult OnPostAsync()
    {
        return RedirectToPage("./Index");
    }
}