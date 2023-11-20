using System.ComponentModel.DataAnnotations;

namespace HrHarmony.Data.Models.ViewModels.Address;

public class UpdateViewModel
{
    [Display(Name = "Ulica")]
    public string Street { get; set; }

    [Display(Name = "Miasto")]
    public string City { get; set; }

    [Display(Name = "Kod pocztowy")]
    public string PostalCode { get; set; }
}