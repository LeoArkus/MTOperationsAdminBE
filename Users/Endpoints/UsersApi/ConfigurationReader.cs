using Commons.Commons;
using Microsoft.Extensions.Configuration;

namespace UsersApi
{
    public static class ConfigurationReader
    {
        public static string ReadUsersQueryUrl(IConfiguration configuration, string mode = "Dev")
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

        public static string ReadUsersCommandUrl(IConfiguration configuration, string mode = "Dev")
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

        public static MD5Configuration ReadMd5Configuration(IConfiguration configuration, string mode = "Dev")
        {
            MD5Configuration ReadConfig()
            {
                try
                {
                    return new MD5Configuration()
                    {
                        PasswordHash = configuration.GetSection(mode).GetSection("MD5").GetSection("PasswordHash").Value,
                        SaltKey = configuration.GetSection(mode).GetSection("MD5").GetSection("SaltKey").Value,
                        VIKey = configuration.GetSection(mode).GetSection("MD5").GetSection("VIKey").Value
                    };
                }
                catch
                {
                    return default;
                }
            }

            return ReadConfig();
        }
    }
}