using Microsoft.Extensions.Configuration;

namespace OpAdminCommand
{
    public static class ConfigurationReader
    {
        public static string ReadCommandConnection(IConfiguration configuration, string mode = "Dev")
        {
            try
            {
                return configuration.GetSection("Connections").GetSection("PostgreSqlCommandConnection").Value;
            }
            catch { return string.Empty; }
        }
    }
}