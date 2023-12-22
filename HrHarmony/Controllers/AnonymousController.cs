using HrHarmony.Consts;
using HrHarmony.Data.Database;
using HrHarmony.Data.Database.SeedData;
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
    public IActionResult ClearAll(string accessKey)
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

            _ctx.SaveChanges();
        }
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return Ok("Usunięto");
    }
}
