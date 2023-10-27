namespace HrHarmony.Configuration.Logging;

public class FileLogger : ILogger
{
    private readonly FileLoggerConfiguration _configuration;
    private readonly object _lockObject = new();

    public FileLogger(FileLoggerConfiguration configuration)
    {
        _configuration = configuration;

        var logDirectory = Path.GetDirectoryName(configuration.LogFilePath);

        if (!Directory.Exists(logDirectory))
            Directory.CreateDirectory(logDirectory);
    }

    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _configuration.LogLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
   
        var logFilePath = _configuration.LogFilePath;

        lock (_lockObject)
        {
            // Sprawdź rozmiar pliku
            if (File.Exists(logFilePath) && new FileInfo(logFilePath).Length > _configuration.MaxFileSizeBytes)
                RotateLogFiles(logFilePath);

            // Zapisz nowy wpis do pliku
            using var writer = File.AppendText(logFilePath);
            var logMessage = formatter(state, exception);
            logMessage += Environment.NewLine + exception;
            writer.WriteLine(logMessage);
        }
    }

    private void RotateLogFiles(string logFilePath)
    {
        var logDirectory = Path.GetDirectoryName(logFilePath);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFilePath);
        var fileExtension = Path.GetExtension(logFilePath);

        // Znajdź wszystkie pliki dziennika w katalogu
        var logFiles = Directory.GetFiles(logDirectory, $"{fileNameWithoutExtension}*{fileExtension}")
            .OrderByDescending(File.GetLastWriteTime)
            .ToList();

        // Usuń nadmiarowe pliki, jeśli przekroczono limit plików
        while (logFiles.Count >= _configuration.MaxFileCount)
        {
            var fileToRemove = logFiles.Last();
            File.Delete(fileToRemove);
            logFiles.Remove(fileToRemove);
        }

        // Zmień nazwę istniejących plików w kolejności malejącej, aby uniknąć konfliktów
        for (int i = logFiles.Count - 1; i >= 0; i--)
        {
            var newFileName = $"{fileNameWithoutExtension}_{i + 1}{fileExtension}";
            var newFilePath = Path.Combine(logDirectory, newFileName);

            // Użyj blokady (lock), aby zapewnić, że tylko jeden wątek naraz zmienia nazwę pliku
            lock (_lockObject)
                File.Move(logFiles[i], newFilePath);
        }

        // Użyj blokady (lock) do zmiany nazwy pliku
        lock (_lockObject)
            using (File.Create(logFilePath)) { }
    }
}