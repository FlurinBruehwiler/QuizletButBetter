namespace M133.Models.DTO;

public class DtoCard
{
    public DtoCard(string term, string definition)
    {
        Term = term;
        Definition = definition;
    }

    public DtoCard()
    {
        
    }
    
    public string Term { get; set; }
    public string Definition { get; set; }
}