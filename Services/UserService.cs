using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using M133.Models;

namespace M133.Services;

public class UserService
{
    private readonly QuizletContext _quizletContext;

    public UserService(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }
    
    public int GetUserId(HttpRequest httpRequest)
    {
        return 1;
        
        var jwt = httpRequest.Cookies["X-Access-Token"];

        if (string.IsNullOrEmpty(jwt))
            throw new Exception();
        
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(jwt);
        var tokenS = jsonToken as JwtSecurityToken;
        var idAsString = tokenS?.Claims.First().Value;

        if (int.TryParse(idAsString, out var id))
        {
            return id;
        }

        throw new Exception();
    }

    public User? GetUser(HttpRequest httpRequest)
    {
        var id = GetUserId(httpRequest);
        return _quizletContext.Users.Find(id);
    }
}