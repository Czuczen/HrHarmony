namespace HrHarmony.Data.Models.Dto.Details.Dictionary;

public class AddressDto : EntityDto<int>
{
    public string Street { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }
}