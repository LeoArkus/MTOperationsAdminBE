using Microsoft.Extensions.Configuration;

namespace CommonStorageCommand
{
    public static class ConfigurationReader
    {
        public static string ReadCommandConnection(IConfiguration configuration, string mode = "Dev")
        {
            try
            {
                return configuration.GetSection("Connections").GetSection("PostgreSqlConnection").Value;
            }
            catch { return string.Empty; }
        }
    }
}