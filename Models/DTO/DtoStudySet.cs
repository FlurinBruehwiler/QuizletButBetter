using System.ComponentModel.DataAnnotations;

namespace M133.Models.DTO;

public class DtoStudySet
{
    [Required, StringLength(60, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [Required]
    public List<DtoCard> Cards { get; set; } = new();
}