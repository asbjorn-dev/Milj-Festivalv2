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

		public async Task<Vagt> HentVagtSingle(int vagt_id)
		{
			Sql = $"SELECT * FROM vagt WHERE vagt_id = {vagt_id}";
			var Parametre = new { Vagt_id = vagt_id };
			var Vagt = await Context.Connection.QuerySingleOrDefaultAsync<Vagt>(Sql, Parametre);
			return Vagt;
		}

		public async Task OpdaterVagt(Vagt OpdateretVagt)
        {
            Sql = "UPDATE vagt SET område = @Område, start_tid = @StartTid, slut_tid = @SlutTid, beskrivelse = @Beskrivelse, priotering = @Priotering, antal_personer = @AntalPersoner WHERE vagt_id = @VagtId";

            var Parametre = new
            {
                Område = OpdateretVagt.område,
                StartTid = OpdateretVagt.start_tid,
                SlutTid = OpdateretVagt.slut_tid,
                Beskrivelse = OpdateretVagt.beskrivelse,
                Priotering = OpdateretVagt.priotering,
                AntalPersoner = OpdateretVagt.antal_personer,
                VagtId = OpdateretVagt.vagt_id
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

    }
}

