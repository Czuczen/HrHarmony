namespace HrHarmony.Logging;

public class FileLoggerConfiguration
{
    public bool Enabled { get; set; }

    public LogLevel ToFileLogLevel { get; set; }

    public string LogFilePath { get; set; }

    public long MaxFileSizeBytes { get; set; }

    public int MaxFileCount { get; set; }
}