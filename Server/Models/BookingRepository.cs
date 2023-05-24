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
using System.Drawing;

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
            // laver en join mellem booking, bruger og vagt og laver en order by start tiden fra vagt tabellen
            Sql = $"SELECT booking.booking_id, booking.er_låst, bruger.fulde_navn, bruger.telefon_nummer, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id ORDER BY vagt.start_tid;";

            var BookingList = await Context.Connection.QueryAsync<Booking>(Sql);
            return BookingList.ToList();
        }

		public async Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId)
		{
			Sql = $"SELECT booking.booking_id, bruger.fulde_navn, bruger.telefon_nummer, booking.er_låst, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id WHERE booking.bruger_id = @BrugerId";
			var parametre = new { BrugerId = brugerId };
			var bookingListe = await Context.Connection.QueryAsync<Booking>(Sql, parametre);
			Console.WriteLine(brugerId + "hej brugerid");
			return bookingListe.ToList();
		}

        public async Task<Booking> HentBookingSingle(int BookingId)
        {
            Sql = $"SELECT * FROM booking WHERE booking_id = @BookingId";
            var Parametre = new { BookingId = BookingId };

            var booking = await Context.Connection.QueryFirstOrDefaultAsync<Booking>(Sql, Parametre);

            return booking;
        }

        public async Task SletBooking(int BookingId)
        {
            var booking = await HentBookingSingle(BookingId);
            await FjernBooking(BookingId);
            await OpdaterVagtPlus(booking.vagt_id);
        }

        private async Task FjernBooking(int bookingId)
        {
            Sql = $"DELETE FROM booking WHERE booking_id = @BookingId";
            var Parametre = new
            {
                BookingId = bookingId
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        private async Task OpdaterVagtPlus(int VagtId)
        {
            Sql = "UPDATE vagt SET antal_personer = antal_personer + 1 WHERE vagt_id = @vagt_id;";
            var Parametre = new
            {
                vagt_id = VagtId,
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        public async Task OpretBooking(BookingSql booking)
        {
            await LavBooking(booking);
            await OpdaterVagt(booking.vagt_id);
            await TilføjPoint(booking.vagt_id, booking.point);

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


        public async Task SkiftLåsStatus(int BookingId)
        {
            Sql = "UPDATE booking SET er_låst = NOT er_låst WHERE booking_id = @booking_id";
            var Parametre = new { booking_id = BookingId };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

		// Tilføjer point til brugeren når de har booket en vagt
		private async Task TilføjPoint(int VagtId, int Point)
		{
			Sql = $"UPDATE bruger SET dine_point = dine_point + @Point WHERE vagt_id = @VagtId";
			var Parametre = new
			{
				vagt_id = VagtId,
				dine_point = Point
			};
			await Context.Connection.ExecuteAsync(Sql, Parametre);
		}
	}
}


