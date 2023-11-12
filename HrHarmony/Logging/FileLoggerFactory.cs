namespace HrHarmony.Logging;

public static class FileLoggerFactory
{
    public static void AddFileLogger(this WebApplicationBuilder builder)
    {
        var fileLoggingConfig = builder.Configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        if (fileLoggingConfig.Enabled)
            builder.Logging.AddProvider(new FileLoggerProvider(fileLoggingConfig));
    }

    public static ILogger GetLogger()
    {
        var fileLoggingConfig = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json") // Plik główny
             .AddJsonFile($"appsettings.Development.json", optional: true) // Plik środowiskowy
             .Build().GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        return new FileLoggerProvider(fileLoggingConfig).CreateLogger("");
    }
}