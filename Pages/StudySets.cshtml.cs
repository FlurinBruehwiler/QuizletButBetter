using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class StudySetsModel : PageModel
{
    private readonly QuizletContext _quizletContext;

    public List<StudySet> StudySets { get; set; } = null!;

    public StudySetsModel(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }

    public void OnGet()
    {
        StudySets = _quizletContext.StudySets.ToList();
    }
}