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
public class RegisterModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;
    private readonly QuizletContext _quizletContext;
    private readonly AuthService _authService;

    [BindProperty]
    public Register Register { get; set; }
    
    public RegisterModel(ILogger<ErrorModel> logger, QuizletContext quizletContext, AuthService authService)
    {
        _logger = logger;
        _quizletContext = quizletContext;
        _authService = authService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        if (Register.Password != Register.PasswordCheck)
            return Page();

        if (await _quizletContext.Users.AnyAsync(x => x.Username == Register.Username))
            return Page();
        
        _authService.CreatePasswordHash(Register.Password, out byte[] passwordHash, out byte[] passwordSalt);

        _quizletContext.Users.Add(new User
        {
            Username = Register.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        });

        await _quizletContext.SaveChangesAsync();

        var user = _quizletContext.Users.FirstOrDefault(x => x.Username == Register.Username);

        if (user == null)
            return Page();
        
        Response.Cookies.Append("X-Access-Token", _authService.CreateToken(user), new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });
                
        return RedirectToPage("./Index");
    }
}