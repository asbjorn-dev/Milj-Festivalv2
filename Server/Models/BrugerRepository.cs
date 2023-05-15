using System;
using Dapper;
using System.Linq;
using Npgsql;
using Shared;
using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection; 

namespace Server.Models
{
	public class BrugerRepository : IBrugerRepository
	{
		private string Sql = "";

		private dBContext Context;

		public BrugerRepository(dBContext context)
        {
            this.Context = context;
        }

		public async Task<IEnumerable<Bruger>> HentAlleFrivillige()
		{
			Sql = $"SELECT * FROM bruger WHERE rolle = 'frivillig'";

			var BrugerListe = await Context.Connection.QueryAsync<Bruger>(Sql);
			return BrugerListe.ToList();
		}
	}
}


