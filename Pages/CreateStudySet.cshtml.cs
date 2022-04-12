using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using M133.Models;
using M133.Models.DTO;
using M133.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class CreateStudySetModel : PageModel
{
    private readonly QuizletContext _quizletContext;
    private readonly UserService _userService;

    [BindProperty]
    public DtoStudySet StudySet { get; set; }
    
    public CreateStudySetModel(QuizletContext quizletContext, UserService userService)
    {
        _quizletContext = quizletContext;
        _userService = userService;
        StudySet = new DtoStudySet();
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

        _quizletContext.StudySets.Add(new StudySet
        {
            Name = StudySet.Name,
            Cards = StudySet.Cards.Select(x => new Card
            {
                Term = x.Term,
                Definition = x.Definition
            }).ToList(),
            User = _userService.GetUser(Request) ?? throw new InvalidOperationException()
        });
        await _quizletContext.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}