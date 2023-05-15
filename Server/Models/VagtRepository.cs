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
    }
}

