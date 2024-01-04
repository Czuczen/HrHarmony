using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Update.Dictionary;

public class ExperienceUpdateDto : EntityDto<int>
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Nazwa doświadczenia powinna mieć od 3 do 30 znaków.")]
    public string ExperienceName { get; set; }
}