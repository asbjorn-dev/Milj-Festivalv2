using System;
using Dapper;
using System.Linq;
using MiljøFestivalv2.Shared;
using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Models
{
    public class PersonRepository : IPersonRepository
    {
        private string Sql = "";

        private dBContext Context;

        public PersonRepository(dBContext context)
        {
            this.Context = context;
        }

        public async Task<Person> GetPerson(int brugerId)
        {
            Sql = $"SELECT * FROM bruger WHERE bruger_id = {brugerId}";

            var person = await Context.Connection.QuerySingleOrDefaultAsync<Person>(Sql);
            return person;
        }

        public async Task UpdatePerson(Person person)
        {
           
            var updateSql = "UPDATE bruger SET fulde_navn = @FuldeNavn, email = @Email, password = @Password, telefon_nummer = @TelefonNummer WHERE bruger_id = @BrugerId";

            await Context.Connection.ExecuteAsync(updateSql, new { person.fulde_navn, person.email, person.password, person.telefon_nummer, person.bruger_id });
        }
    }
}