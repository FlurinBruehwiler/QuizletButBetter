using System.ComponentModel.DataAnnotations;

namespace M133.Models;

public class LearnCard
{
    [Key]
    public int Id { get; set; }
    public Learn Learn { get; set; }
    public Card Card { get; set; }
    public Pool Pool { get; set; }
    public User User { get; set; }
    public int RepeatedFalse { get; set; }
}