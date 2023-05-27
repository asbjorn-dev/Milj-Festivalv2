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

        // Henter alle beskeder fra databasen
        public async Task<IEnumerable<Msg_board>> HentAlleBeskeder()
        {
            Sql = $"SELECT * FROM msg_board;";

            var BeskedListe = await Context.Connection.QueryAsync<Msg_board>(Sql);
            return BeskedListe.ToList();
        }

        // Metode så koordinator kan tilføje beskeder til databasen
        public async Task TilføjBesked(Msg_board msg)
        {
            Sql = @"INSERT INTO msg_board ( besked, afsender, tidspunkt) 
                    VALUES (@Besked, @Afsender, @Tidspunkt)";
            await Context.Connection.ExecuteAsync(Sql, new
            {
                Besked = msg.besked,
                Afsender = msg.afsender,
                Tidspunkt = msg.tidspunkt
            });
        }
    }
}
