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
    public class VagtRepository : IVagtRepository
    {
        private string Sql = "";

        private dBContext Context;

        public VagtRepository(dBContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Vagt>> HentAlleVagter()
        {
            Sql = $"SELECT * FROM vagt";

            var VagtListe = await Context.Connection.QueryAsync<Vagt>(Sql);
            return VagtListe.ToList();
        }

        // Sletter en enkelt vagt fra databasen ved hjælp af vagt_id
        public async Task DeleteVagt(int vagt_id)
        {
            Sql = $"DELETE FROM vagt WHERE vagt_id = {vagt_id}";
            await Context.Connection.ExecuteAsync(Sql);
        }

        public async Task TilføjVagt(Vagt vagt)
        {
            Sql = @"INSERT INTO vagt (vagt_id, område, start_tid, slut_tid, beskrivelse, priotering, antal_personer) 
             VALUES (@VagtId, @Område, @StartTid, @SlutTid, @Beskrivelse, @Priotering, @AntalPersoner)";
            await Context.Connection.ExecuteAsync(Sql, new
            {
                VagtId = vagt.vagt_id,
                Område = vagt.område,
                StartTid = vagt.start_tid,
                SlutTid = vagt.slut_tid,
                Beskrivelse = vagt.beskrivelse,
                Priotering = vagt.priotering,
                AntalPersoner = vagt.antal_personer
            });
        }


    }
}

