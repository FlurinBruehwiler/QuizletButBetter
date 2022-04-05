using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class LearnModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;

    public const string SessionKeyName = "_Name";
    public const string SessionKeyAge = "_Age";

    [BindProperty]
    public StudySet StudySet { get; set; } = null!;

    public LearnModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(int id)
    {
        
    }
}