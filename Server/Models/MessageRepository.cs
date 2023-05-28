using Dapper;
using MiljøFestivalv2.Shared;


namespace Server.Models
{
    public class MessageRepository : IMessageRepository
    {
        // Definerer en SQL-streng, så den ikke behøver at blive oprettet igen i hver metode
        private string Sql = "";

        // Bruger dBContext fra klassen og opretter en variabel, der kan genbruges i metoderne
        private dBContext Context;

        //Denne constructor-metode tager en parameter af typen "dBContext" og tildeler værdien af denne parameter 
        //til den private variabel "Context" i klassen. Hvilket muliggør brugen af databaseforbindelsen i metoderne i vores repository
        public MessageRepository(dBContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Msg_board>> HentAlleBeskeder()
        {
            Sql = $"SELECT * FROM msg_board;";

            var BeskedListe = await Context.Connection.QueryAsync<Msg_board>(Sql);
            return BeskedListe.ToList();
        }

        // Metode så koordinator kan tilføje Beskeder til databasen
        public async Task TilføjBesked(Msg_board Msg)
        {
            Sql = @"INSERT INTO msg_board ( Besked, Afsender, Tidspunkt) 
                    VALUES (@Besked, @Afsender, @Tidspunkt)";
            await Context.Connection.ExecuteAsync(Sql, new
            {
                Besked = Msg.Besked,
                Afsender = Msg.Afsender,
                Tidspunkt = Msg.Tidspunkt
            });
        }
    }
}
