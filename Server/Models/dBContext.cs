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
			string connString = "User ID=hrcbomrh;Password=czku8YaHYA3BBb7nRQgvRxrPZb2LoZGC;Host=dumbo.db.elephantsql.com;Port=5432;Database=hrcbomrh;";
			this.Connection = new NpgsqlConnection(connString);
		}
	}
}

