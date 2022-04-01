using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class CreateLernsetModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;
    private readonly QuizletContext _quizletContext;

    [BindProperty]
    public Lernset Lernset { get; set; }
    
    public CreateLernsetModel(ILogger<ErrorModel> logger, QuizletContext quizletContext)
    {
        _logger = logger;
        _quizletContext = quizletContext;
        Lernset = new Lernset();
    }

    public IActionResult OnGet()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        Console.WriteLine(Lernset.Name);
        Console.WriteLine(ModelState.Count);
        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        _quizletContext.Lernsets.Add(Lernset);
        await _quizletContext.SaveChangesAsync();
        
        return RedirectToPage("./Index");
    }
}