using HrHarmony.Consts;
using HrHarmony.Logging;

namespace HrHarmony.Configuration.Secrets;

public static class SecretsProvider
{
    private static Secrets? Secrets => new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("secrets.json") // Plik główny
        .AddJsonFile($"secrets.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true) // Plik środowiskowy
        .Build().Get<Secrets?>();

    public static string GetConnectionString(string appName, string connectionType)
    {
        var logger = FileLoggerFactory.GetLogger();

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
            logger.LogError(ex, $"An error occurred while retrieving the '{connectionType}' connection from the configuration for the '{appName}' application.");
            throw new InvalidOperationException($"An error occurred while retrieving the '{connectionType}' connection from the configuration for the '{appName}' application.", ex);
        }
    }

    private static string SecretsTemplate(string appName)
    {
        return $@"  
                        {{
                          ""ConnectionStrings"": {{
                            ""{appName}"": {{
                              ""DefaultConnection"": ""Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={appName};Integrated Security=True"",
                              ""TestConnection"": ""Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True""
                            }}
                          }}
                        }}";
    }
}