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
			//Sql = $"SELECT bruger_id, fulde_navn, email, telefon_nummer, fødselsdag, brugernavn, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE rolle = 'frivillig'";
            Sql = $"SELECT bruger_id, fulde_navn, email, rolle, telefon_nummer, fødselsdag, brugernavn, cpr_nummer FROM bruger WHERE rolle = 'frivillig'";
            var BrugerListe = await Context.Connection.QueryAsync<Bruger>(Sql);
			return BrugerListe.ToList();
		}

        public async Task TilføjFrivillig(Bruger bruger)
        {
           // Sql = "INSERT INTO bruger(fulde_navn, email, telefon_nummer, fødselsdag, brugernavn, password, cpr_nummer, rolle, er_aktiv, er_blacklistet) VALUES(@fulde_navn, @email, @telefon_nummer, @fødselsdag, @brugernavn, @password, encrypt_cpr(@cpr_nummer, 'furkan'), @rolle, @er_aktiv, @er_blacklistet)";
            Sql = "INSERT INTO bruger(fulde_navn, email, telefon_nummer, fødselsdag, brugernavn, password, cpr_nummer, rolle, er_aktiv, er_blacklistet) VALUES(@fulde_navn, @email, @telefon_nummer, @fødselsdag, @brugernavn, @password, @cpr_nummer, @rolle, @er_aktiv, @er_blacklistet)";
            var parametre = new
            {
                fulde_navn = bruger.fulde_navn,
                email = bruger.email,
                telefon_nummer = bruger.telefon_nummer,
                fødselsdag = bruger.fødselsdag,
                brugernavn = bruger.brugernavn,
                password = bruger.password,
                cpr_nummer = bruger.cpr_nummer,
                rolle = "frivillig", // Default value
                er_aktiv = true, // Default value
                er_blacklistet = false // Default value
            };

            await Context.Connection.ExecuteAsync(Sql, parametre);
        }


    }
}


