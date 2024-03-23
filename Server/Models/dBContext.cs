using Npgsql;
namespace Server.Models
{
    public class dBContext
    {
        public NpgsqlConnection Connection { get; }

        public dBContext()
        {
            // Opretter en forbindelsesstreng til PostgreSQL-databasen
            string ConnString = "User ID=asbjorn;Password=Whoistheknife112;Host=festival-db.postgres.database.azure.com;Port=5432;Database=postgres;";

            // Opretter en ny NpgsqlConnection-objekt ved at bruge forbindelsesstrengen
            this.Connection = new NpgsqlConnection(ConnString);
        }
    }
}
