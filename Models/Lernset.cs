using System.ComponentModel.DataAnnotations;

namespace M133.Models;

public class Lernset
{
    [Key]
    public int Id { get; set; }
    
    public List<Card>? Cards { get; set; }
    
    [Required, StringLength(60, MinimumLength = 3)]
    public string Name { get; set; }
    public User? Ersteller { get; set; }
}