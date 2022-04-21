using System.Diagnostics;
using M133.Learn;
using M133.Models;
using M133.Models.DTO;
using M133.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class LearnModel : PageModel
{
    private readonly QuizletContext _quizletContext;
    private readonly UserService _userService;
    public StudySet StudySet { get; set; } = null!;

    public Card Card { get; set; }

    public LearnModel(QuizletContext quizletContext, UserService userService)
    {
        _quizletContext = quizletContext;
        _userService = userService;
    }

    public IActionResult OnGet(int id)
    {
        if(!_quizletContext.StudySets.Any(x => x.StudySetId == id))
            return RedirectToPage("./Index");

        var studySetId = _userService.GetId(Request);
        
        Models.Learn learn = _quizletContext.Learns.Where(x => x.UserId == studySetId && x.StudySetId == id)
                .Include(x => x.LearnCards)
                .ThenInclude(x => x.Card)
                .Include(x => x.LearnCards)
                .ThenInclude(x => x.Pool).FirstOrDefault()
                             ?? CreateLearn(studySetId, id);
        
        Card = learn.NextCard();

        return Page();
    }

    private Models.Learn CreateLearn(int userId, int studySetId)
    {
        CreatePoolsIfNotExist();

        var studySet = _quizletContext.StudySets.Where(x => x.StudySetId == studySetId)
            .Include(x => x.Cards).First();

        var defaultPool = _quizletContext.Pools.First(x => x.Name == "nochNie");
        
        return new Models.Learn
        {
            UserId = userId,
            StudySetId = studySetId,
            LearnCards = studySet.Cards.Select(x => new LearnCard
            {
                Card = x,
                Pool = defaultPool
            }).ToList()
        };
    }

    private void CreatePoolsIfNotExist()
    {
        List<string> poolNames = new()
        {
            "nochNie",
            "multipleChoice",
            "schriftlich",
            "schriftlichFalse",
            "finished"
        };

        var pools = _quizletContext.Pools;
        
        foreach (var poolName in poolNames)
        {
            if (!pools.Any(x => x.Name == poolName))
            {
                _quizletContext.Pools.Add(new Pool
                {
                    Name = poolName
                });
            }
        }

        _quizletContext.SaveChanges();
    }
}