using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Address;

public class UpdateViewModel
{
    [Display(Name = "Ulica")]
    public string Street { get; set; }

    [Display(Name = "Miasto")]
    public string City { get; set; }

    //[RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Błędny kod pocztowy!")] // nie działa
    [Display(Name = "Kod pocztowy")]
    public string PostalCode { get; set; }
}