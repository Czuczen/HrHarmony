namespace HrHarmony.Data.Models.ViewModels.AppManagement;

public class LogsViewModel
{
    public List<List<string>> TraceLogs { get; } = new();

    public List<List<string>> DebugLogs { get; } = new();

    public List<List<string>> InfoLogs { get; } = new();

    public List<List<string>> WarnLogs { get; } = new();

    public List<List<string>> ErrorLogs { get; } = new();

    public List<List<string>> CriticalLogs { get; } = new();

    public List<List<string>> NoneLogs { get; } = new();
}