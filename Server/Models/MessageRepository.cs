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
        private string Sql = "";

        private dBContext Context;

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
