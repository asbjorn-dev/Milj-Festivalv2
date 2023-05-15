using System;
using Dapper;
using System.Linq;
using Npgsql;
using MiljøFestivalv2.Shared;
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
			// Query til at hente alle frivillige fra Bruger tabbellen og decryptere cpr_nummer med nøglen "furkan"
			Sql = $"SELECT bruger_id, fulde_navn, email, telefon_nummer, fødselsdag, brugernavn, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE rolle = 'frivillig'";

			var BrugerListe = await Context.Connection.QueryAsync<Bruger>(Sql);
			return BrugerListe.ToList();
		}
	}
}


