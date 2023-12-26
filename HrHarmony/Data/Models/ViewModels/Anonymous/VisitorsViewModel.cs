using HrHarmony.Data.Models.Entities;

namespace HrHarmony.Data.Models.ViewModels.Anonymous;

public class VisitorsViewModel
{
    public IEnumerable<Visitor> VisitorDataById { get; set; } = new List<Visitor>();

    public IEnumerable<List<Visitor>> VisitorOthersId { get; set; } = new List<List<Visitor>>();
}
