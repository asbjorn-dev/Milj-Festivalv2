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
            Sql = $"SELECT *, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE rolle = 'frivillig' ORDER BY fulde_navn ASC";
            var BrugerListe = await Context.Connection.QueryAsync<Bruger>(Sql);
            return BrugerListe.ToList();
        }

        public async Task TilføjFrivillig(Bruger bruger)
        {
            Sql = "INSERT INTO bruger(fulde_navn, email, telefon_nummer, fødselsdag, brugernavn, password, cpr_nummer, rolle, er_aktiv, er_blacklistet) VALUES(@fulde_navn, @email, @telefon_nummer, @fødselsdag, @brugernavn, @password, encrypt_cpr_nummer(@cpr_nummer), @rolle, @er_aktiv, @er_blacklistet)";

            var Parametre = new
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

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        //login funktion
        public Login HentBrugerMedBrugernavnOgPassword(string brugernavn, string password)
        {
            var sql = @"SELECT * FROM bruger WHERE brugernavn = @Brugernavn AND password = @Password";

            var parametre = new
            {
                Brugernavn = brugernavn,
                Password = password

            };
            var bruger = Context.Connection.QuerySingleOrDefault<Login>(sql, parametre);
            if(bruger == null)
            {
                return new Login { Brugernavn = "FEJL", Rolle = "FEJL" };
                
            }
            else return bruger;
        }
        public async Task SkiftAktivStatus(int bruger_id)
        {
            Sql = "UPDATE bruger SET er_aktiv = NOT er_aktiv WHERE bruger_id = @Bruger_id";
            var Parametre = new { Bruger_id = bruger_id };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        public async Task SkiftBlacklistStatus(int bruger_id)
        {
            Sql = "UPDATE bruger SET er_blacklistet = NOT er_blacklistet WHERE bruger_id = @Bruger_id";
            var Parametre = new { Bruger_id = bruger_id };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

		public async Task<Bruger> HentBrugerSingle(int bruger_id)
		{
			Sql = $"SELECT *, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE bruger_id = {bruger_id}";
            var Parametre = new { Bruger_id = bruger_id };
			var person = await Context.Connection.QuerySingleOrDefaultAsync<Bruger>(Sql, Parametre);
			return person;
		}

		public async Task UpdateBruger(Bruger updatedBruger)
		{
			Sql = "UPDATE bruger SET fulde_navn = @FuldeNavn, email = @Email, brugernavn = @Brugernavn, password = @Password, telefon_nummer = @TelefonNummer WHERE bruger_id = @BrugerId";

			var Parametre = new
			{
				FuldeNavn = updatedBruger.fulde_navn,
				Email = updatedBruger.email,
                Brugernavn = updatedBruger.brugernavn,
				Password = updatedBruger.password,
				TelefonNummer = updatedBruger.telefon_nummer,
				BrugerId = updatedBruger.bruger_id,

			};

			await Context.Connection.ExecuteAsync(Sql, Parametre);
		}
	}
}


