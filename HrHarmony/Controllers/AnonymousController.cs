using HrHarmony.Consts;
using HrHarmony.Data.Database;
using HrHarmony.Data.Database.SeedData;
using HrHarmony.Data.Models.ViewModels.Anonymous;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HrHarmony.Data.Models.Shared.Enums;

namespace HrHarmony.Controllers;

[AllowAnonymous]
public class AnonymousController : Controller
{
    private readonly ApplicationDbContext _ctx;

    public AnonymousController(ApplicationDbContext context)
    {
        _ctx = context;
    }

    public async Task<IActionResult> Visitors()
    {
        var model = new List<VisitorsViewModel>();
        var visitors = await _ctx.Visitors.ToListAsync();
        var groupedVisitorsById = visitors.GroupBy(x => x.VisitorId);

        foreach (var visitorGroup in groupedVisitorsById)
        {
            model.Add(new VisitorsViewModel
            {
                VisitorDataById = visitorGroup.OrderBy(x => x.Timestamp).ToList(),
                VisitorOthersId = visitorGroup.GroupBy(x => x.IpAddress).SelectMany(ipGroup =>
                    visitors.Where(x => x.IpAddress == ipGroup.Key && x.VisitorId != visitorGroup.Key)).GroupBy(x =>
                    x.VisitorId).Select(group => group.OrderBy(x => x.Timestamp).ToList())
            });
        }

        return View(model);
    }

    /// <summary>
    /// Uruchamiane z linka - http://localhost:5092/Anonymous/CreateSampleObjects?accessKey=klucz&sizeLevel=Low
    /// </summary>
    /// <param name="accessKey"></param>
    /// <param name="sizeLevel"></param>
    /// <returns></returns>
    public IActionResult CreateSampleObjects(string accessKey, SampleObjectsCreationSizeLevel? sizeLevel)
    {
        if (accessKey == AccessKeys.CreateSampleObjectsKey)
            RandomDataSeeder.Initialize(_ctx, sizeLevel);
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return Ok("Utworzono");
    }

    /// <summary>
    /// Uruchamiane z linka - http://localhost:5092/Anonymous/ClearAll?accessKey=klucz
    /// </summary>
    /// <param name="accessKey"></param>
    /// <returns></returns>
    public async Task<IActionResult> ClearAll(string accessKey)
    {
        if (accessKey == AccessKeys.CreateSampleObjectsKey)
        {
            var dbSets = _ctx.GetType().GetProperties().Where(p =>
                p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var prop in dbSets)
            {
                var objects = (IEnumerable<object>) prop.GetValue(_ctx)!;
                _ctx.RemoveRange(objects);
            }

            await _ctx.SaveChangesAsync();
        }
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return Ok("Usunięto");
    }
}
