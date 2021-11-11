using Microsoft.Extensions.Configuration;

namespace CommonStorageQuery
{
    public static class ConfigurationReader
    {
        public static string ReadQueryConnection(IConfiguration configuration, string mode = "Dev")
        {
            try
            {
                return configuration.GetSection("Connections").GetSection("PostgreSqlConnection").Value;
            }
            catch { return string.Empty; }
        }
    }
}