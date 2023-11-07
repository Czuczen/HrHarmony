namespace HrHarmony.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly FileLoggerConfiguration _configuration;

    public FileLoggerProvider(FileLoggerConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ILogger CreateLogger(string categoryName) => new FileLogger(_configuration);

    public void Dispose()
    {
    }
}