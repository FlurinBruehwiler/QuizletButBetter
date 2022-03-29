namespace M133.Models;

public class Lernset
{
    public int Id { get; set; }
    public List<Card> Cards { get; set; }
    public string Name { get; set; }
    public User Ersteller { get; set; }
}