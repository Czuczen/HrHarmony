using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.Dto.Update.Dictionary;

public class AddressUpdateDto : EntityDto<int>
{
    [Required(ErrorMessage = "Pole jest wymagane.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Ulica powinna mieć od 3 do 30 znaków.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Miasto powinno mieć od 3 do 30 znaków.")]
    public string City { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane.")]
    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Błędny kod pocztowy.")]
    public string PostalCode { get; set; }
}