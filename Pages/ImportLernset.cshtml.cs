using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ImportLernsetModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;
    private readonly QuizletContext _quizletContext;

    [BindProperty]
    public string Title { get; set; }
    
    [BindProperty]
    public string TextArea { get; set; }
    
    public ImportLernsetModel(ILogger<ErrorModel> logger, QuizletContext quizletContext)
    {
        _logger = logger;
        _quizletContext = quizletContext;
    }

    public IActionResult OnGet()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        return RedirectToPage("./Index");
    }
}