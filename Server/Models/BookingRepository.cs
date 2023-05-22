using System;
using Dapper;
using System.Linq;
using Npgsql;
using MiljøFestivalv2.Shared;
using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;

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
            Sql = $"SELECT booking.booking_id, bruger.fulde_navn, bruger.telefon_nummer, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id;";
     
            var BookingList = await Context.Connection.QueryAsync<Booking>(Sql);
            return BookingList.ToList();
        }

		public async Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId)
		{
			Sql = $"SELECT booking.booking_id, bruger.fulde_navn, bruger.telefon_nummer, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id WHERE booking.bruger_id = @BrugerId";
			var parametre = new { BrugerId = brugerId };
			var bookingListe = await Context.Connection.QueryAsync<Booking>(Sql, parametre);
			Console.WriteLine(brugerId + "hej brugerid");
			return bookingListe.ToList();
		}

        public async Task OpretBooking(BookingSql booking)
        {
            await LavBooking(booking);
            await OpdaterVagt(booking.vagt_id);
        }

        private async Task LavBooking(BookingSql booking)
        {
            Sql = "INSERT INTO booking (bruger_id, vagt_id, er_låst) VALUES (@BrugerId, @VagtId, @ErLåst);";

            var parametre = new
            {
                BrugerId = booking.bruger_id,
                VagtId = booking.vagt_id,
                ErLåst = booking.er_låst
            };

            await Context.Connection.ExecuteAsync(Sql, parametre);
        }

        private async Task OpdaterVagt(int VagtId) 
        {
            Sql = "UPDATE vagt SET antal_personer = antal_personer - 1 WHERE vagt_id = @vagt_id;";
            var Parametre = new 
            { 
                vagt_id = VagtId,
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }
    }
}


