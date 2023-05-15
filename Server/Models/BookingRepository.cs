using System;
using Dapper;
using System.Linq;
using Npgsql;
using Shared;
using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Models
{
    public class BookingRepository: IBookingRepository
    {
        private string Sql = "";

        private dBContext Context;

        public BookingRepository(dBContext context)
        {
            this.Context = context;
        }

        public async Task<IEnumerable<Booking>> HentAlleBookinger()
        {
            Sql = $"SELECT * FROM booking";

            var BookingList = await Context.Connection.QueryAsync<Booking>(Sql);
            return BookingList.ToList();
        }
    }
}


