using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Update.Main;

public class AbsenceUpdateDto : EntityDto<int>
{
    [Required(ErrorMessage = "Pole jest wymagane!")]
    public int AbsenceTypeId { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane!")]
    public int EmployeeId { get; set; }
}