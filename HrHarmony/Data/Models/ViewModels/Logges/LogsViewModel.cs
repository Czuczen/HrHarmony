namespace HrHarmony.Data.Models.ViewModels.Logges;

public class LogsViewModel
{
    public bool HasPermission => string.IsNullOrWhiteSpace(PermissionInfo);

    public string PermissionInfo { get; set; }

    public List<List<string>> TraceLogs { get; } = new();

    public List<List<string>> DebugLogs { get; } = new();

    public List<List<string>> InfoLogs { get; } = new();
        
    public List<List<string>> WarnLogs { get; } = new();

    public List<List<string>> ErrorLogs { get; } = new();

    public List<List<string>> CriticalLogs { get; } = new();

    public List<List<string>> NoneLogs { get; } = new();
}