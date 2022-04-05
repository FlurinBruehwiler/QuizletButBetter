namespace M133.Models;

public class Card
{
    public Card(string definition, string term, StudySet studySet)
    {
        Definition = definition;
        Term = term;
        StudySet = studySet;
    }

    public Card()
    {
        
    }

    public int CardId { get; set; }
    public string Definition { get; set; } = null!;
    public string Term { get; set; } = null!;

    public int StudySetId { get; set; }
    public StudySet StudySet { get; set; } = null!;

    public List<LearnCard>? LearnCards { get; set; }
}