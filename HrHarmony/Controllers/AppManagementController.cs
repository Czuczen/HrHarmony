using HrHarmony.Consts;
using HrHarmony.Data.Database.SeedData;
using HrHarmony.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static HrHarmony.Consts.Enums;
using HrHarmony.Data.Models.ViewModels.AppManagement;
using HrHarmony.Logging;
using System.Text;
using HrHarmony.Configuration.Secrets;

namespace HrHarmony.Controllers;

public class AppManagementController : Controller
{
    private readonly ApplicationDbContext _ctx;
    private readonly IConfiguration _configuration;

    public AppManagementController(ApplicationDbContext context, IConfiguration configuration)
    {
        _ctx = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Uruchamiane z linka - http://localhost:5092/AppManagement/Visitors?accessKey=key
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Visitors(string accessKey)
    {
        var model = new List<VisitorsViewModel>();

        if (accessKey == SecretsProvider.GetAccessKey(AccessNames.Visitors))
        {
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
        }
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return View(model);
    }

    /// <summary>
    /// Uruchamiane z linka - http://localhost:5092/AppManagement/CreateSampleObjects?accessKey=key&sizeLevel=Low
    /// </summary>
    /// <param name="accessKey"></param>
    /// <param name="sizeLevel"></param>
    /// <returns></returns>
    public async Task<IActionResult> CreateSampleObjects(string accessKey, SampleObjectsCreationSizeLevel? sizeLevel)
    {
        if (accessKey == SecretsProvider.GetAccessKey(AccessNames.CreateSampleObjects))
            await RandomDataSeeder.Initialize(_ctx, sizeLevel);
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return Ok("Utworzono");
    }

    /// <summary>
    /// Uruchamiane z linka - http://localhost:5092/AppManagement/ClearAll?accessKey=key
    /// </summary>
    /// <param name="accessKey"></param>
    /// <returns></returns>
    public async Task<IActionResult> ClearAll(string accessKey)
    {
        if (accessKey == SecretsProvider.GetAccessKey(AccessNames.ClearAll))
        {
            var dbSets = _ctx.GetType().GetProperties().Where(p =>
                p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var prop in dbSets)
            {
                var objects = (IEnumerable<object>)prop.GetValue(_ctx)!;
                _ctx.RemoveRange(objects);
            }

            await _ctx.SaveChangesAsync();
        }
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return Ok("Usunięto");
    }

    /// <summary>
    /// Uruchamiane z linka - http://localhost:5092/AppManagement/Logs?accessKey=key
    /// </summary>
    /// <param name="accessKey"></param>
    /// <returns></returns>
    public IActionResult Logs(string accessKey)
    {
        var ret = new LogsViewModel();

        if (accessKey == SecretsProvider.GetAccessKey(AccessNames.Logs))
        {
            var logFilePath = _configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>().LogFilePath;
            var logDirectory = Path.GetDirectoryName(logFilePath);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFilePath);
            var fileExtension = Path.GetExtension(logFilePath);
            var logFiles = Directory.GetFiles(logDirectory, $"{fileNameWithoutExtension}*{fileExtension}")
                .OrderByDescending(System.IO.File.GetLastWriteTime).Reverse();

            foreach (var logFile in logFiles)
            {
                using var fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var sr = new StreamReader(fs, Encoding.Default);
                var buffer = new char[(int)sr.BaseStream.Length];
                sr.Read(buffer, 0, (int)sr.BaseStream.Length);

                var rawLogs = new string(buffer);
                var stringSeparators = new[] { "\r\n" };
                var lines = rawLogs.Split(stringSeparators, StringSplitOptions.None);

                var log = new List<string>();
                foreach (var line in lines)
                {
                    var isNewLog = false;

                    if (line.StartsWith("TRACE"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.TraceLogs.Add(log);
                    }

                    if (line.StartsWith("DEBUG"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.DebugLogs.Add(log);
                    }

                    if (line.StartsWith("INFORMATION"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.InfoLogs.Add(log);
                    }

                    if (line.StartsWith("WARN"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.WarnLogs.Add(log);
                    }

                    if (line.StartsWith("ERROR"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.ErrorLogs.Add(log);
                    }

                    if (line.StartsWith("CRITICAL"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.CriticalLogs.Add(log);
                    }

                    if (line.StartsWith("NONE"))
                    {
                        isNewLog = true;

                        log = new List<string> { line };
                        ret.NoneLogs.Add(log);
                    }

                    if (!isNewLog)
                        log.Add(line);
                }
            }

            ret.TraceLogs.Reverse();
            ret.DebugLogs.Reverse();
            ret.InfoLogs.Reverse();
            ret.WarnLogs.Reverse();
            ret.ErrorLogs.Reverse();
            ret.CriticalLogs.Reverse();
            ret.NoneLogs.Reverse();
        }
        else
            return Unauthorized("Nieprawidłowy klucz dostępu");

        return View(ret);
    }
}
