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
        // Definerer en SQL-streng, så den ikke behøver at blive oprettet igen i hver metode
        private string Sql = "";

        // Bruger dBContext fra klassen og opretter en variabel, der kan genbruges i metoderne
        private dBContext Context;

        //Denne constructor-metode tager en parameter af typen "dBContext" og tildeler værdien af denne parameter 
        //til den private variabel "Context" i klassen. Hvilket muliggør brugen af databaseforbindelsen i metoderne i vores repository
        public BookingRepository(dBContext context)
        {
            this.Context = context;
        }

        // Henter alle bookinger fra databasen, joiner den med bruger og vagt tabellen så tilhørende frivillig og vagt kan vises sammen med bookingen
        public async Task<IEnumerable<Booking>> HentAlleBookinger()
        {
            // laver en join mellem booking, bruger og vagt og laver en order by start tiden fra vagt tabellen
            Sql = $"SELECT booking.booking_id, booking.er_låst, bruger.fulde_navn, bruger.telefon_nummer, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id ORDER BY vagt.start_tid;";

            var BookingList = await Context.Connection.QueryAsync<Booking>(Sql);
            return BookingList.ToList();
        }

        //Henter bookinger for en enkelt bruger via bruger_id
		public async Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId)
		{
			Sql = $"SELECT booking.booking_id, bruger.fulde_navn, bruger.telefon_nummer, booking.er_låst, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id WHERE booking.bruger_id = @BrugerId";
			var parametre = new { BrugerId = brugerId };
			var bookingListe = await Context.Connection.QueryAsync<Booking>(Sql, parametre);
			Console.WriteLine(brugerId + "hej brugerid");
			return bookingListe.ToList();
		}

        //Metode til at slette booking, der også kalder metoderne til at opdatere den tilknyttede data
        public async Task SletBooking(int BookingId)
        {
            var booking = await HentBookingSingle(BookingId);
            await FjernBooking(BookingId);
            await OpdaterVagtPlus(booking.vagt_id);
            await FjernPoint(booking.vagt_id, booking.bruger_id);
        }

        // Henter en enkelt booking ud fra bookingId, bruges til at slette en booking
        public async Task<Booking> HentBookingSingle(int BookingId)
        {
            Sql = $"SELECT * FROM booking WHERE booking_id = @BookingId";
            var Parametre = new { BookingId = BookingId };

            var booking = await Context.Connection.QueryFirstOrDefaultAsync<Booking>(Sql, Parametre);

            return booking;
        }

        // Metode til at fjerne en booking ud fra bookingid
        private async Task FjernBooking(int bookingId)
        {
            Sql = $"DELETE FROM booking WHERE booking_id = @BookingId";
            var Parametre = new
            {
                BookingId = bookingId
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        //Metode til at opdatere antallet af frivillige der mangler på en vagt, når en booking slettes
        private async Task OpdaterVagtPlus(int VagtId)
        {
            Sql = "UPDATE vagt SET antal_personer = antal_personer + 1 WHERE vagt_id = @vagt_id;";
            var Parametre = new
            {
                vagt_id = VagtId,
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }

        //Metode der fjerner de indtjente point fra en bruger, når bookingen slettes
        private async Task FjernPoint(int VagtId, int BrugerId)
        {
            Sql = "SELECT * FROM vagt WHERE vagt_id = @VagtId";
            var Parametre = new
            {
                VagtId = VagtId,
            };

            var vagt = await Context.Connection.QueryFirstOrDefaultAsync<Vagt>(Sql, Parametre);

            Sql = "UPDATE bruger SET dine_point = dine_point - @Point WHERE bruger_id = @BrugerId;";

            var Parametre2 = new
            {
                BrugerId = BrugerId,
                Point = vagt.point
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre2);
        }

        //Metode til at oprette en booking, der også kalder metoderne til at opdatere den tilknyttede data
        public async Task OpretBooking(BookingSql booking)
        {
            await LavBooking(booking);
            await OpdaterVagt(booking.vagt_id);
            await TilføjPoint(booking.vagt_id, booking.bruger_id);

		}

        //Metoden der opretter en ny booking
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

        //Metode til at opdatere antallet af frivillige vagten mangler, når en vagt bliver booket
        private async Task OpdaterVagt(int VagtId) 
        {
            Sql = "UPDATE vagt SET antal_personer = antal_personer - 1 WHERE vagt_id = @vagt_id;";
            var Parametre = new 
            { 
                vagt_id = VagtId,
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }


		// Tilføjer point til brugeren når de har booket en vagt
		private async Task TilføjPoint(int VagtId, int BrugerId)
		{
            Sql = "SELECT * FROM vagt WHERE vagt_id = @VagtId";
			var Parametre = new
			{
			    VagtId = VagtId,
			};

            var vagt = await Context.Connection.QueryFirstOrDefaultAsync<Vagt>(Sql, Parametre);

            Sql = "UPDATE bruger SET dine_point = dine_point + @Point WHERE bruger_id = @BrugerId;";

            var Parametre2 = new
            {
                BrugerId = BrugerId,
                Point = vagt.point
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre2);
		}

        //Metode der gør det muligt for koordinatoren at låse en booking, så den ikke kan slettes af den frivillige 
        public async Task SkiftLåsStatus(int BookingId)
        {
            Sql = "UPDATE booking SET er_låst = NOT er_låst WHERE booking_id = @booking_id;";
            var Parametre = new { booking_id = BookingId };
            await Context.Connection.ExecuteAsync(Sql, Parametre);
        }
    }
}


