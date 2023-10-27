﻿namespace HrHarmony.Configuration.Logging;

public class FileLoggerConfiguration
{
    public bool Enabled { get; set; }

    public LogLevel LogLevel { get; set; }

    public string LogFilePath { get; set; }

    public long MaxFileSizeBytes { get; set; }

    public int MaxFileCount { get; set; }
}