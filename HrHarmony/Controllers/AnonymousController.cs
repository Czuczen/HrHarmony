using HrHarmony.Consts;
using HrHarmony.Data.Database;
using HrHarmony.Data.Database.SeedData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HrHarmony.Data.Models.Entities.Enums;

namespace HrHarmony.Controllers;

[AllowAnonymous]
public class AnonymousController : Controller
{
    private readonly ApplicationDbContext _ctx;

    public AnonymousController(ApplicationDbContext context)
    {
        _ctx = context;
    }

    public IActionResult CreateSampleObjects(string accessKey, SampleObjectsCreationSizeLevel sizeLevel)
    {
        if (accessKey == AccessKeys.CreateSampleObjectsKey)
            RandomDataSeeder.Initialize(_ctx, sizeLevel);
        else
            return Unauthorized();

        return Ok();
    }
}