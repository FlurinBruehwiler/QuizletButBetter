using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using M133.Services;
using Microsoft.EntityFrameworkCore;


namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class StudySetsModel : PageModel
{
    private readonly QuizletContext _quizletContext;
    private readonly UserService _userService;
    private readonly HttpRequest _httpRequest;

    public List<StudySet> StudySets { get; set; } = null!;

    public StudySetsModel(QuizletContext quizletContext, UserService userService)
    {
        _quizletContext = quizletContext;
        _userService = userService;
    }

    public void OnGet()
    {
        StudySets = _quizletContext.StudySets.Where(x => x.UserId == _userService.GetUserId(Request)).ToList();
    }
}