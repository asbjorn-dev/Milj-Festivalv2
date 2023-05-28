using Dapper;
using MiljøFestivalv2.Shared;


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

        //Metode der henter en bruger i databasen der matcher brugernavn og password argumenterne 
        public Bruger HentBrugerMedBrugernavnOgPassword(string Brugernavn, string Password)
        {
            Sql = @"SELECT * FROM bruger WHERE brugernavn = @Brugernavn AND password = @Password";

            var Parametre = new
            {
                Brugernavn = Brugernavn,
                Password = Password

            };
            var Bruger = Context.Connection.QuerySingleOrDefault<Bruger>(Sql, Parametre);
            if(Bruger == null)
            {
                return new Bruger { brugernavn = "FEJL", rolle = "FEJL" };       
            }
            else return Bruger;
        }

        public async Task SkiftAktivStatus(int BrugerId)
        {
            Sql = "UPDATE bruger SET er_aktiv = NOT er_aktiv WHERE bruger_id = @Bruger_id";
            var Parametre = new { Bruger_id = BrugerId };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        public async Task SkiftBlacklistStatus(int BrugerId)
        {
            Sql = "UPDATE bruger SET er_blacklistet = NOT er_blacklistet WHERE bruger_id = @Bruger_id";
            var Parametre = new { Bruger_id = BrugerId };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

		public async Task<Bruger> HentBrugerSingle(int BrugerId)
		{
			Sql = $"SELECT *, decrypt_cpr(cpr_nummer, 'furkan') AS cpr_nummer FROM bruger WHERE bruger_id = {BrugerId}";
            var Parametre = new { Bruger_id = BrugerId };
			var person = await Context.Connection.QuerySingleOrDefaultAsync<Bruger>(Sql, Parametre);
			return person;
		}

		public async Task UpdateBruger(Bruger UpdatedBruger)
		{
			Sql = "UPDATE bruger SET fulde_navn = @FuldeNavn, email = @Email, brugernavn = @Brugernavn, password = @Password, telefon_nummer = @TelefonNummer WHERE bruger_id = @BrugerId";

			var Parametre = new
			{
				FuldeNavn = UpdatedBruger.fulde_navn,
				Email = UpdatedBruger.email,
                Brugernavn = UpdatedBruger.brugernavn,
				Password = UpdatedBruger.password,
				TelefonNummer = UpdatedBruger.telefon_nummer,
				BrugerId = UpdatedBruger.bruger_id,

			};

			await Context.Connection.ExecuteAsync(Sql, Parametre);
		}
	}
}


