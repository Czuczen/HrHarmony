namespace HrHarmony.Logging;

public static class FileLoggerFactory
{
    private static ILogger? _loggerInstance;
    private static readonly object _lock = new();


    public static ILogger GetLogger()
    {
        if (_loggerInstance == null)
            lock (_lock)
                if (_loggerInstance == null)
                    _loggerInstance = CreateLoggerInstance();

        return _loggerInstance;
    }

    public static void AddFileLogger(this WebApplicationBuilder builder)
    {
        var fileLoggingConfig = builder.Configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        if (fileLoggingConfig.Enabled)
            builder.Logging.AddProvider(new FileLoggerProvider(fileLoggingConfig));
    }

    private static ILogger CreateLoggerInstance()
    {
        var fileLoggingConfig = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json") // Plik główny
             .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true) // Plik środowiskowy
             .Build().GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        return new FileLoggerProvider(fileLoggingConfig).CreateLogger("");
    }
}