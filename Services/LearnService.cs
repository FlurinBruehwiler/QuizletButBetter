using M133.Models;

namespace M133.Services;

public class LearnService
{
    private readonly QuizletContext _quizletContext;

    public LearnService(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }

    public void CreateLearnCards(Models.Learn learn)
    {
        
    }
}