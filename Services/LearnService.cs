using M133.Models;
using M133.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace M133.Services;

public class LearnService
{
    private readonly QuizletContext _quizletContext;
    private Learn _learn = null!;

    public LearnService(QuizletContext quizletContext, UserService userService, int studySetId, HttpRequest httpRequest)
    {
        _quizletContext = quizletContext;
        GetLearn(studySetId, userService.GetUserId(httpRequest));
    }
    
    public DtoLearnCard NextCard(bool? previousResult)
    {
        var currentCard = GetActiveCardsSorted().First();

        if (previousResult == null)
            return new DtoLearnCard(currentCard, GetMultipleChoiceChoices(currentCard)); 

        MoveCard(currentCard, (bool) previousResult );

        var nextCard = GetActiveCardsSorted().FirstOrDefault();

        DtoLearnCard result;
        
        if (nextCard == null)
        {
            _quizletContext.Remove(_learn);
            result = new DtoLearnCard();
        }
        else
        {
            result = new DtoLearnCard(nextCard, GetMultipleChoiceChoices(nextCard));
        }

        _quizletContext.SaveChanges();
        return result; 
    }

    private string[] GetMultipleChoiceChoices(LearnCard cardNotToInclude)
    {
        List<string> output = new();
        while (output.Count < 3 && output.Count < _learn.LearnCards.Count - 1)
        {   
            Random r = new();
            var potentialCard = _learn.LearnCards[r.Next(0, _learn.LearnCards.Count)];
            if(potentialCard == cardNotToInclude)
                continue;
            if(output.Contains(potentialCard.Card.Definition))
                continue;
            output.Add(potentialCard.Card.Definition);
        }

        return output.ToArray();
    }

    private List<LearnCard> GetActiveCardsSorted()
    {
        return GetCardsWithPool(Pool.MultipleChoice, Pool.Schriftlich, Pool.SchriftlichFalse)
            .OrderBy(x => x.LastCardIndex).ToList();
    }

    private void MakeNewCardActive()
    {
        var potentialCards = GetCardsWithPool(Pool.NochNie);
        if (potentialCards.Count == 0)
            return;
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
                poolToMovie = result ? Pool.Schriftlich : Pool.SchriftlichFalse;
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

    private void GetLearn(int studySetId, int userId)
    {
        var learn = _quizletContext.Learns.Where(x => x.UserId == userId && x.StudySetId == studySetId)
            .Include(x => x.LearnCards)
            .ThenInclude(x => x.Card)
            .FirstOrDefault();

        if (learn == null)
        {
            CreateLearn(userId, studySetId);
            return;
        }

        _learn = learn;
    } 


    private List<LearnCard> GetCardsWithPool(params Pool[] pools)
    {
        return _learn.LearnCards.Where(x => pools.Contains(x.Pool)).ToList();
    }

    private void CreateLearn(int userId, int studySetId)
    {
        var studySet = _quizletContext.StudySets.Where(x => x.StudySetId == studySetId)
            .Include(x => x.Cards).FirstOrDefault();
        
        _learn = new Learn
        {
            UserId = userId,
            StudySetId = studySetId,
            LearnCards = new()
        };
        
        if(studySet == null)
            return;
        
        foreach (var card in studySet.Cards)
        {
            _learn.LearnCards.Add(new LearnCard
            {
                Card = card,
                Pool = Pool.NochNie
            });
        }
        
        for (int i = 0; i < 10; i++)
            MakeNewCardActive();

        _quizletContext.Learns.Add(_learn);
    }
}