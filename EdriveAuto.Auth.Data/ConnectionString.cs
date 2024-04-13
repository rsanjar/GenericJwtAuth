using Microsoft.Extensions.Configuration;

namespace EDriveAuto.Auth.Data
{
    public static class ConnectionString
    {
        public const string SqliteDb = nameof(SqliteDb);
        public const string EDriveAutoAuthDb = nameof(EDriveAutoAuthDb);
        private static readonly IConfigurationRoot Root;

        static ConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            
            Root = builder.Build();
        }

        public static string Get => Root.GetConnectionString(IsUseSqlite ? SqliteDb : EDriveAutoAuthDb);
        

        public static bool IsUseSqlite
        {
            get
            {
                bool.TryParse(Root.GetSection("IsUseSqlite").Value, out bool isUseSqlite);

                return isUseSqlite;
            }
        }
    }
}