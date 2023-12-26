namespace HrHarmony.Configuration.Secrets;

public class Secrets
{
    public Dictionary<string, AppConnectionStrings> ConnectionStrings { get; set; }

    public Dictionary<string, string> AccessKeys { get; set; }
}
