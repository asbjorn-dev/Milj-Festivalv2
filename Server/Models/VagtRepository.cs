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
            // filtre efter priotering med 'høj' kommer først og bagefter filtre den tidligste start_tid
            Sql = $"SELECT * FROM vagt ORDER BY (priotering = 'høj') DESC, start_tid DESC;";

            var VagtListe = await Context.Connection.QueryAsync<Vagt>(Sql);
            return VagtListe.ToList();
        }

        // Sletter en enkelt vagt fra databasen ved hjælp af vagt_id
        public async Task DeleteVagt(int vagt_id)
        {
            Sql = $"DELETE FROM vagt WHERE vagt_id = {vagt_id}";
            await Context.Connection.ExecuteAsync(Sql);
        }
    }
}

