using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class CreateStudySetModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;
    private readonly QuizletContext _quizletContext;

    [BindProperty]
    public StudySet StudySet { get; set; }
    
    public CreateStudySetModel(ILogger<ErrorModel> logger, QuizletContext quizletContext)
    {
        _logger = logger;
        _quizletContext = quizletContext;
        StudySet = new StudySet();
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

        if (StudySet.Cards == null)
        {
            Console.WriteLine("No Cards");
            return Page();
        }

        foreach (var card in StudySet.Cards)
        {
            if (string.IsNullOrWhiteSpace(card.Definition) || string.IsNullOrWhiteSpace(card.Term))
            {
                StudySet.Cards.Remove(card);
            }
        }

        if (StudySet.Cards.Count == 0)
        {
            Console.WriteLine("No Cards");
            return Page();
        }   

        Console.WriteLine($"Creating Lernset with name {StudySet.Name} and the following cards");
        //Lernset.Cards?.ForEach(x => Console.WriteLine($"Definition: {x.Definition}, Begriff: {x.Begriff}"));

        _quizletContext.StudySets.Add(StudySet);
        await _quizletContext.SaveChangesAsync();
        
        return RedirectToPage("./Index");
    }
}