using Microsoft.Data.Sqlite;


namespace MajorMud.Database
{
    public sealed class Database
    {
        private static SqliteConnection _connection;

        public void Bootstrap()
        {
            MaybeCreateClasses();
        }

        internal void EnsureConnected()
        {
            string settingsFile = Settings.Default.DatabaseFile;
            if (_connection == null)
            {
                _connection = new SqliteConnection($"Data Source={settingsFile}");
            }

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        internal void MaybeCreateClasses()
        {
            const string createClassesQuery = @"
                CREATE TABLE 
                IF NOT EXISTS 
                Class (
                    Id INT,
                    Name CHAR,
                    ExperiencePercentage INT,
                    Combat INT,
                    HitpointPerLevelMinimum INT,
                    HitpointPerLevelMaximum INT,
                    WeaponType INT,
                    ArmorType INT,
                    MagicLevel INT,
                    MagicType INT
                );
            ";

            EnsureConnected();

            using (SqliteCommand command = _connection.CreateCommand())
            {
                command.CommandText = createClassesQuery;
                command.CommandTimeout = Settings.Default.DatabaseCommandTimeoutSeconds;
                command.ExecuteNonQuery();
            }
        }
    }
}
