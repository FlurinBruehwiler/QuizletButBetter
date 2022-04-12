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
    
    public int GetId(HttpRequest httpRequest)
    {
        var jwt = httpRequest.Cookies["X-Access-Token"];
        
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(jwt);
        var tokenS = jsonToken as JwtSecurityToken;
        var idAsString = tokenS?.Claims.First().Value;

        if (int.TryParse(idAsString, out var id))
        {
            return id;
        }

        return -1;
    }

    public User? GetUser(HttpRequest httpRequest)
    {
        var id = GetId(httpRequest);
        return _quizletContext.Users.Find(id);
    }
}