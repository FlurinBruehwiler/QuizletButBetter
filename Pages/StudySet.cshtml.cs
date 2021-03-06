using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class StudySetModel : PageModel
{
    private readonly QuizletContext _quizletContext;

    [BindProperty]
    public StudySet StudySet { get; set; } = null!;

    public StudySetModel(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }

    public void OnGet(int id)
    {
        StudySet = _quizletContext.StudySets.Include(x => x.Cards).First(x => x.StudySetId == id);
        Console.WriteLine(StudySet.StudySetId);
    }
}