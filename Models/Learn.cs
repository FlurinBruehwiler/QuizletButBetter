using System.ComponentModel.DataAnnotations;

namespace M133.Models;

public class Learn
{
    [Key]
    public int Id { get; set; }
    public Lernset Lernset { get; set; }
}