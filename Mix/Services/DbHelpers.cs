using System.Configuration;

namespace Mix.Services
{
    public static class DbHelpers
    {
        public static string GetConnectionString()
        {
            var dbName = "MixDB";
            var appSettings = ConfigurationManager.AppSettings;
            string host = appSettings["RDS_HOSTNAME"];

            if (string.IsNullOrEmpty(host))
            {
                return ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
            }

            string username = appSettings["RDS_USERNAME"];
            string password = appSettings["RDS_PASSWORD"];
            string port = appSettings["RDS_PORT"];

            return "Data Source=" + host + ";Initial Catalog=" + dbName + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}