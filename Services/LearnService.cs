using M133.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace M133.Services;

public class LearnService
{
    private readonly QuizletContext _quizletContext;
    private readonly Learn _learn;

    public LearnService(QuizletContext quizletContext, UserService userService, int studySetId, HttpRequest httpRequest)
    {
        _quizletContext = quizletContext;
        _learn = GetLearn(studySetId, userService.GetUserId(httpRequest)) ?? throw new Exception();
    }
    
    public string NextCard(bool? previousResult)
    {
        var currentCard = GetActiveCardsSorted().First();

        if (previousResult == null)
            return JsonConvert.SerializeObject(currentCard);

        MoveCard(currentCard, (bool) previousResult );

        var nextCard = GetActiveCardsSorted().Last();
        
        return JsonConvert.SerializeObject(nextCard);
    }

    private List<LearnCard> GetActiveCardsSorted()
    {
        return GetCardsWithPool(Pool.MultipleChoice, Pool.Schriftlich, Pool.SchriftlichFalse)
            .OrderBy(x => x.LastCardIndex).ToList();
    }

    private void MakeNewCardActive()
    {
        var potentialCards = GetCardsWithPool(Pool.NochNie);
        Random rnd = new Random();
        var newCard = potentialCards[rnd.Next(potentialCards.Count)];
        newCard.Pool = Pool.MultipleChoice;
        newCard.LastCardIndex = NewIndex();
    }

    private int NewIndex()
    {
        var activeCards = GetActiveCardsSorted();
        if (activeCards.Count == 0)
            return 0;
        
        return activeCards.Last().RepeatedFalse + 1;
    }

    private void MoveCard(LearnCard card, bool result)
    {
        Pool poolToMovie = Pool.NochNie;

        switch (card.Pool)
        {
            case Pool.MultipleChoice:
                poolToMovie = result ? Pool.Schriftlich : Pool.MultipleChoice;
                break;
            case Pool.Schriftlich:
                poolToMovie = result ? Pool.Finished : Pool.SchriftlichFalse;
                card.RepeatedFalse += result ? 0 : 1;
                break;
            case Pool.SchriftlichFalse:
                poolToMovie = result ? Pool.Finished : Pool.Schriftlich;
                card.RepeatedFalse += result ? 0 : 1;
                break;
        }

        if (card.RepeatedFalse > 5)
        {
            poolToMovie = Pool.NochNie;
            card.RepeatedFalse = 0;
        }
        
        MoveCardToPool(card, poolToMovie);

        card.LastCardIndex = NewIndex();
    }

    private void MoveCardToPool(LearnCard card, Pool pool)
    {
        card.Pool = pool;

        if (pool == Pool.Finished)
            MakeNewCardActive();
    }

    private Learn? GetLearn(int studySetId, int userId) => _quizletContext.Learns.Where(x => x.UserId == userId && x.StudySetId == studySetId)
            .Include(x => x.LearnCards)
            .ThenInclude(x => x.Card)
            .Include(x => x.LearnCards)
            .ThenInclude(x => x.Pool).FirstOrDefault() ?? CreateLearn(userId, studySetId);


    private List<LearnCard> GetCardsWithPool(params Pool[] pools)
    {
        return _learn.LearnCards.Where(x => pools.Contains(x.Pool)).ToList();
    }

    private Learn? CreateLearn(int userId, int studySetId)
    {
        var studySet = _quizletContext.StudySets.Where(x => x.StudySetId == studySetId)
            .Include(x => x.Cards).FirstOrDefault();

        if (studySet == null)
            return null;
        
        var learn = new Learn
        {
            UserId = userId,
            StudySetId = studySetId,
            LearnCards = studySet.Cards.Select(x => new LearnCard
            {
                Card = x,
                Pool = Pool.NochNie
            }).ToList()
        };

        for (int i = 0; i < 10; i++)
            MakeNewCardActive();

        return learn;
    }
}