using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class LernsetsModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;
    private readonly QuizletContext _quizletContext;

    public List<Lernset> Lernsets { get; set; }
    
    public LernsetsModel(ILogger<ErrorModel> logger, QuizletContext quizletContext)
    {
        _logger = logger;
        _quizletContext = quizletContext;
    }

    public void OnGet()
    {
        Lernsets = _quizletContext.Lernsets.Include(x => x.Cards).ToList();
    }
}