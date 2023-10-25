namespace HrHarmony.Models.Dto.Update.Dictionary;

public class AddressUpdateDto : EntityDto<int>
{
    public string Street { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }
}