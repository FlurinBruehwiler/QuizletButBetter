using System.Diagnostics;
using M133.Models;
using M133.Models.DTO;
using M133.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class LoginModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;
    private readonly QuizletContext _quizletContext;
    private readonly AuthService _authService;

    [BindProperty]
    public DtoLogin DtoLogin { get; set; } = null!;

    public LoginModel(ILogger<ErrorModel> logger, QuizletContext quizletContext, AuthService authService)
    {
        _logger = logger;
        _quizletContext = quizletContext;
        _authService = authService;
    }

    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var user = _quizletContext.Users.FirstOrDefault(x => x.Username == DtoLogin.Username);

        if (user == null)
            return Page();

        if (!_authService.VerifyPasswordHash(DtoLogin.Password, user.PasswordHash, user.PasswordSalt))
            return Page();
        
        Response.Cookies.Append("X-Access-Token", _authService.CreateToken(user), new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });
                
        return RedirectToPage("./Index");
    }
}