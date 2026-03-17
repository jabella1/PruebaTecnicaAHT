namespace PruebaTecnicaAHT.Domain.Options;

public class AppSettings
{
    public const string SectionKey = "CONNECTIONSTRING_PRUEBAT";
    public string ConnectionString { get; set; } = string.Empty;
}
