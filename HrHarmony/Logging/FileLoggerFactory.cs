namespace HrHarmony.Logging;

public static class FileLoggerFactory
{
    public static void AddFileLogger(this WebApplicationBuilder builder)
    {
        var fileLoggingConfig = builder.Configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>();

        if (fileLoggingConfig.Enabled)
            builder.Logging.AddProvider(new FileLoggerProvider(fileLoggingConfig));
    }
}