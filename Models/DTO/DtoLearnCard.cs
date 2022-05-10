namespace M133.Models.DTO;

public class DtoLearnCard
{
    public DtoLearnCard(LearnCard card, string[] multipleChoice)
    {
        this.multipleChoice = multipleChoice;
        Card = new DtoCard(card.Card.Definition, card.Card.Term);
        Pool = card.Pool;
    }

    public DtoLearnCard()
    {
    }

    public DtoCard Card { get; set; }
    public Pool Pool { get; set; }
    public string[] multipleChoice { get; set; }
}