using Dapper;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public class VagtRepository : IVagtRepository
    {
        // Definerer en SQL-streng, så den ikke behøver at blive oprettet igen i hver metode
        private string Sql = "";

        // Bruger dBContext fra klassen og opretter en variabel, der kan genbruges i metoderne
        private dBContext Context;

        //Denne constructor-metode tager en parameter af typen "dBContext" og tildeler værdien af denne parameter 
        //til den private variabel "Context" i klassen. Hvilket muliggør brugen af databaseforbindelsen i metoderne i vores repository
        public VagtRepository(dBContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Vagt>> HentAlleVagter()
        {
            // filtre efter priotering med 'høj' kommer først og bagefter filtre den tidligste start_tid
            Sql = $"SELECT * FROM vagt ORDER BY (priotering = 'høj') DESC, start_tid DESC;";

            var VagtListe = await Context.Connection.QueryAsync<Vagt>(Sql);
            return VagtListe.ToList();
        }

        public async Task DeleteVagt(int VagtId)
        {
            Sql = $"DELETE FROM vagt WHERE vagt_id = {VagtId}";
            await Context.Connection.ExecuteAsync(Sql);
        }

        public async Task TilføjVagt(Vagt vagt)
        {
            Sql = @"INSERT INTO vagt ( område, start_tid, slut_tid, beskrivelse, priotering, antal_personer, point) 
             VALUES (@Område, @StartTid, @SlutTid, @Beskrivelse, @Priotering, @AntalPersoner, @Point)";
            await Context.Connection.ExecuteAsync(Sql, new
            {
                Område = vagt.område,
                StartTid = vagt.start_tid,
                SlutTid = vagt.slut_tid,
                Beskrivelse = vagt.beskrivelse,
                Priotering = vagt.priotering,
                AntalPersoner = vagt.antal_personer,
                Point = vagt.point
            });
        }

		public async Task<Vagt> HentVagtSingle(int VagtId)
		{
			Sql = $"SELECT * FROM vagt WHERE vagt_id = {VagtId}";
			var Parametre = new { Vagt_id = VagtId };
			var Vagt = await Context.Connection.QuerySingleOrDefaultAsync<Vagt>(Sql, Parametre);
			return Vagt;
		}

		public async Task OpdaterVagt(Vagt OpdateretVagt)
        {
            Sql = "UPDATE vagt SET område = @Område, start_tid = @StartTid, slut_tid = @SlutTid, beskrivelse = @Beskrivelse, priotering = @Priotering, antal_personer = @AntalPersoner, point = @Point WHERE vagt_id = @VagtId";

            var Parametre = new
            {
                Område = OpdateretVagt.område,
                StartTid = OpdateretVagt.start_tid,
                SlutTid = OpdateretVagt.slut_tid,
                Beskrivelse = OpdateretVagt.beskrivelse,
                Priotering = OpdateretVagt.priotering,
                AntalPersoner = OpdateretVagt.antal_personer,
                VagtId = OpdateretVagt.vagt_id,
                Point = OpdateretVagt.point
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }


    }
}

