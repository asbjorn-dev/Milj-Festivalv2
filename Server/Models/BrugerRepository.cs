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
        // Definerer en SQL-streng, så den ikke behøver at blive oprettet igen i hver metode
        private string Sql = "";

        // Bruger dBContext fra klassen og opretter en variabel, der kan genbruges i metoderne
        private dBContext Context;

        //Denne constructor-metode tager en parameter af typen "dBContext" og tildeler værdien af denne parameter 
        //til den private variabel "Context" i klassen. Hvilket muliggør brugen af databaseforbindelsen i metoderne i vores repository
        public BrugerRepository(dBContext context)
        {
            this.Context = context;
        }

        //Metode til at hente alle frivillige fra databasen
        public async Task<IEnumerable<Bruger>> HentAlleFrivillige()
        {
            // Query til at hente alle frivillige fra Bruger tabbellen og decryptere cpr_nummer med nøglen "furkan"
            Sql = $"SELECT *, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE rolle = 'frivillig' ORDER BY fulde_navn ASC";
            var BrugerListe = await Context.Connection.QueryAsync<Bruger>(Sql);
            return BrugerListe.ToList();
        }

        // Metode til at tilføje en frivillig til databasen
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

        //Metode der henter en bruger i databasen der matcher brugernavn og password argumenterne 
        public Bruger HentBrugerMedBrugernavnOgPassword(string brugernavn, string password)
        {
            var sql = @"SELECT * FROM bruger WHERE brugernavn = @Brugernavn AND password = @Password";

            var parametre = new
            {
                Brugernavn = brugernavn,
                Password = password

            };
            var bruger = Context.Connection.QuerySingleOrDefault<Bruger>(sql, parametre);
            if(bruger == null)
            {
                return new Bruger { brugernavn = "FEJL", rolle = "FEJL" };       
            }
            else return bruger;
        }

        //Metode til at skifte aktiv status for en bruger i databasen
        public async Task SkiftAktivStatus(int bruger_id)
        {
            Sql = "UPDATE bruger SET er_aktiv = NOT er_aktiv WHERE bruger_id = @Bruger_id";
            var Parametre = new { Bruger_id = bruger_id };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        //Metode til at skifte blacklist status for en bruger i databasen
        public async Task SkiftBlacklistStatus(int bruger_id)
        {
            Sql = "UPDATE bruger SET er_blacklistet = NOT er_blacklistet WHERE bruger_id = @Bruger_id";
            var Parametre = new { Bruger_id = bruger_id };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        // Metode til at hente en enkelt frivillig, her køres decrypt også på CPR-nummer ved hjælp af decrypt key
		public async Task<Bruger> HentBrugerSingle(int bruger_id)
		{
			Sql = $"SELECT *, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE bruger_id = {bruger_id}";
            var Parametre = new { Bruger_id = bruger_id };
			var person = await Context.Connection.QuerySingleOrDefaultAsync<Bruger>(Sql, Parametre);
			return person;
		}

        //Metode til at opdaterer brugerens oplysninger
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


