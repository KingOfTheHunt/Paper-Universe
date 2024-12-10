namespace PaperUniverse.Core.Contexts;

public static class Configuration
{
    public static string PrivateKey { get; set; } = "9cd720ecb38d27140c473dddfbd02f3b8614c2ea";
    public static DatabaseConfiguration Database { get; set; } = new();
    public static SMTPConfiguration Smtp { get; set; } = new();
}

public class DatabaseConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
}

public class SMTPConfiguration
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}