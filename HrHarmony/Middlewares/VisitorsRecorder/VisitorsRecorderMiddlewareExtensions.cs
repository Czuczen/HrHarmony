namespace HrHarmony.Middlewares.VisitorsRecorder;

public static class VisitorsRecorderMiddlewareExtensions
{
    public static IApplicationBuilder UseVisitorsRecorder(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<VisitorsRecorderMiddleware>();
    }
}