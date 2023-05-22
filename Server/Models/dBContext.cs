using System;
using Npgsql;
namespace Server.Models

	//DBContext contructor henter connectionstring fra appsettings og bruger den til at skabe en ny npgsql connection.
{
	public class dBContext
	{
		public NpgsqlConnection Connection { get; }
		public dBContext()
		{
			//string connString = "User ID=hrcbomrh;Password=czku8YaHYA3BBb7nRQgvRxrPZb2LoZGC;Host=dumbo.db.elephantsql.com;Port=5432;Database=hrcbomrh;";

			string connString = "User ID=postgres;Password=Whoistheknife112 ;Host=miljofestivalpggruppe2.postgres.database.azure.com;Port=5432;Database=postgres;";
            this.Connection = new NpgsqlConnection(connString);
        }
	}
}

