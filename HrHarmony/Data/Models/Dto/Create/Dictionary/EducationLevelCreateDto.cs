using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Create.Dictionary;

public class EducationLevelCreateDto
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Nazwa poziomu powinna mieć od 3 do 30 znaków.")]
    public string LevelName { get; set; }
}