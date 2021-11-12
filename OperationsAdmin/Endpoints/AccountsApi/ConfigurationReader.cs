using Commons.Commons;
using Microsoft.Extensions.Configuration;

namespace OpAdminApi
{
    public static class ConfigurationReader
    {
        public static string ReadAccountsQueryUrl(IConfiguration configuration, string mode = "Dev")
        {
            string ReadConfig()
            {
                try
                {
                    return configuration.GetSection(mode).GetSection("AccountQueryUrl").Value;
                }
                catch
                {
                    return string.Empty;
                }
            }

            return ReadConfig();
        }

        public static string ReadAccountsCommandUrl(IConfiguration configuration, string mode = "Dev")
        {
            string ReadConfig()
            {
                try
                {
                    return configuration.GetSection(mode).GetSection("AccountCommandUrl").Value;
                }
                catch
                {
                    return string.Empty;
                }
            }

            return ReadConfig();
        }
        
        public static string ReadCommonQueryUrl(IConfiguration configuration, string mode = "Dev")
        {
            string ReadConfig()
            {
                try
                {
                    return configuration.GetSection(mode).GetSection("CommonQueryUrl").Value;
                }
                catch
                {
                    return string.Empty;
                }
            }

            return ReadConfig();
        }
        
        public static string ReadCommonCommandUrl(IConfiguration configuration, string mode = "Dev")
        {
            string ReadConfig()
            {
                try
                {
                    return configuration.GetSection(mode).GetSection("CommonCommandUrl").Value;
                }
                catch
                {
                    return string.Empty;
                }
            }

            return ReadConfig();
        }
    }
}