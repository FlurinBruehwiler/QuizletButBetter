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
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Error");

            return Page();
        }

        if (Lernset.Cards == null)
        {
            Console.WriteLine("No Cards");
            return Page();
        }

        foreach (var card in Lernset.Cards)
        {
            if (string.IsNullOrWhiteSpace(card.Definition) || string.IsNullOrWhiteSpace(card.Begriff))
            {
                Lernset.Cards.Remove(card);
            }
        }

        if (Lernset.Cards.Count == 0)
        {
            Console.WriteLine("No Cards");
            return Page();
        }

        Console.WriteLine($"Creating Lernset with name {Lernset.Name} and the following cards");
        Lernset.Cards?.ForEach(x => Console.WriteLine($"Definition: {x.Definition}, Begriff: {x.Begriff}"));

        _quizletContext.Lernsets.Add(Lernset);
        await _quizletContext.SaveChangesAsync();
        
        return RedirectToPage("./Index");
    }
}