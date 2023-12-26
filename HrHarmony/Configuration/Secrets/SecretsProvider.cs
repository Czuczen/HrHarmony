using HrHarmony.Consts;
using HrHarmony.Logging;

namespace HrHarmony.Configuration.Secrets;

public static class SecretsProvider
{
    private static readonly ILogger Logger = FileLoggerFactory.GetLogger();

    private static Secrets? Secrets
    {
        get
        {
            try
            {
                return new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("secrets.json") // Plik główny
                    .AddJsonFile($"secrets.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true) // Plik środowiskowy
                    .Build().Get<Secrets?>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while reading secrets file.");
                return null;
            }
        }
    }


    public static string GetAccessKey(string accessName)
    {
        try
        {
            if (Secrets == null)
                throw new InvalidOperationException($"No key found for '{accessName}'. "
                                                    + $"\nIf you haven't created the required secrets.json file, please create it according to the following template:\n{SecretsTemplate(null)}\n");

            return Secrets.AccessKeys[accessName];
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error retrieving access key '{accessName}'.");
            throw new InvalidOperationException($"Error retrieving access key '{accessName}'.", ex);
        }
    }

    public static string GetConnectionString(string appName, string connectionType)
    {
        try
        {
            if (Secrets == null)
                throw new InvalidOperationException($"No connection information found for '{connectionType}' in application '{appName}'. "
                                                    + $"\nIf you haven't created the required secrets.json file, please create it according to the following template:\n{SecretsTemplate(appName)}\n");

            var currAppConfig = Secrets.ConnectionStrings[appName];

            switch (connectionType)
            {
                case DbConnectionTypes.DefaultConnection:
                    return currAppConfig.DefaultConnection;
                case DbConnectionTypes.TestConnection:
                    return currAppConfig.TestConnection;
                default:
                    throw new InvalidOperationException($"No connection information found for '{connectionType}' in application '{appName}'. " 
                                                        + $"\nIf you haven't created the required secrets.json file, please create it according to the following template:\n{SecretsTemplate(appName)}\n");
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while retrieving the '{connectionType}' connection from the configuration for the '{appName}' application.");
            throw new InvalidOperationException($"An error occurred while retrieving the '{connectionType}' connection from the configuration for the '{appName}' application.", ex);
        }
    }

    private static string SecretsTemplate(string? appName)
    {
        appName ??= "AppName";

        return $@"
                        {{
                            ""ConnectionStrings"": {{
                                ""{appName}"": {{
                                    ""DefaultConnection"": ""Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={appName};Integrated Security=True"",
                                    ""TestConnection"": ""Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True""
                                }}
                            }},
                            ""AccessKeys"": {{
                                ""Visitors"": ""key"",
                                ""CreateSampleObjects"": ""key"",
                                ""ClearAll"": ""key"",
                                ""Logs"": ""key""
                            }}
                        }}";
    }
}
