using HrHarmony.Data.Database;
using HrHarmony.Data.Models.Entities;

namespace HrHarmony.Middlewares.VisitorsRecorder;

public class VisitorsRecorderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public VisitorsRecorderMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!IsReviverRequest(context))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await RemoveOldVisitorsData(dbContext);

                var visitorData = new Visitor
                {
                    VisitorId = GetVisitorId(context),
                    Timestamp = DateTime.UtcNow,
                    IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = context.Request.Headers["User-Agent"],
                    Path = context.Request.Path
                };

                dbContext.Visitors.Add(visitorData);
                await dbContext.SaveChangesAsync();

                await _next(context);
            }
        }
    }

    private static string GetVisitorId(HttpContext context)
    {
        if (!context.Request.Cookies.ContainsKey("VisitorId"))
        {
            var visitorId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions{ Expires = DateTimeOffset.MaxValue };
            context.Response.Cookies.Append("VisitorId", visitorId, cookieOptions);

            return visitorId;
        }

        return context.Request.Cookies["VisitorId"];
    }

    private static async Task RemoveOldVisitorsData(ApplicationDbContext dbContext)
    {
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);
        var oldEntries = dbContext.Visitors.Where(entry => entry.Timestamp < oneYearAgo);

        if (oldEntries.Any())
        {
            dbContext.Visitors.RemoveRange(oldEntries);
            await dbContext.SaveChangesAsync();
        }            
    }

    private static bool IsReviverRequest(HttpContext context)
    {
        string? reviverRequest = context.Request.Headers["X-Reviver-Request"];

        if (!string.IsNullOrWhiteSpace(reviverRequest) && reviverRequest.ToLower() == "true")
            return true;

        return false;
    }
}