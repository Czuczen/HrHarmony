using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Update.Main;

public class LeaveUpdateDto : EntityDto<int>
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [DataType(DataType.DateTime, ErrorMessage = "Proszę podać poprawną datę.")]
    public DateTime? EndDate { get; set; }
}