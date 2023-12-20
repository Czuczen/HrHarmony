namespace HrHarmony.Data.Models.Shared;

public class CustomEntity<TObject>
{
    public string EntityName { get; set; }

    public TObject Item { get; set; }
}