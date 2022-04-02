using System.ComponentModel.DataAnnotations;

namespace M133.Models;

public class Pool
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}