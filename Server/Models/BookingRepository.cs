using Dapper;
using MiljøFestivalv2.Shared;


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

        public async Task<IEnumerable<Booking>> HentAlleBookinger()
        {
            // laver en join mellem booking, bruger og vagt og laver en order by start tiden fra vagt tabellen
            Sql = $"SELECT booking.booking_id, booking.er_låst, bruger.fulde_navn, bruger.telefon_nummer, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id ORDER BY vagt.start_tid;";

            var BookingList = await Context.Connection.QueryAsync<Booking>(Sql);
            return BookingList.ToList();
        }

		public async Task<IEnumerable<Booking>> HentBookingerForBruger(int BrugerId)
		{
			Sql = $"SELECT booking.booking_id, bruger.fulde_navn, bruger.telefon_nummer, booking.er_låst, vagt.* FROM booking JOIN bruger ON booking.bruger_id = bruger.bruger_id JOIN vagt ON booking.vagt_id = vagt.vagt_id WHERE booking.bruger_id = @BrugerId";
			var Parametre = new { BrugerId = BrugerId };
			var BookingListe = await Context.Connection.QueryAsync<Booking>(Sql, Parametre);
			return BookingListe.ToList();
		}

        
        public async Task SletBooking(int BookingId)
        {
            var Booking = await HentBookingSingle(BookingId);
            await FjernBooking(BookingId);
            await OpdaterVagtPlus(Booking.vagt_id);
            await FjernPoint(Booking.vagt_id, Booking.bruger_id);
        }

        public async Task<Booking> HentBookingSingle(int BookingId)
        {
            Sql = $"SELECT * FROM booking WHERE booking_id = @BookingId";
            var Parametre = new { BookingId = BookingId };

            var booking = await Context.Connection.QueryFirstOrDefaultAsync<Booking>(Sql, Parametre);

            return booking;
        }

        private async Task FjernBooking(int BookingId)
        {
            Sql = $"DELETE FROM booking WHERE booking_id = @BookingId";
            var Parametre = new
            {
                BookingId = BookingId
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

            var Vagt = await Context.Connection.QueryFirstOrDefaultAsync<Vagt>(Sql, Parametre);

            Sql = "UPDATE bruger SET dine_point = dine_point - @Point WHERE bruger_id = @BrugerId;";

            var Parametre2 = new
            {
                BrugerId = BrugerId,
                Point = Vagt.point
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre2);
        }

        public async Task OpretBooking(BookingSql Booking)
        {
            await LavBooking(Booking);
            await OpdaterVagt(Booking.vagt_id);
            await TilføjPoint(Booking.vagt_id, Booking.bruger_id);

		}

        private async Task LavBooking(BookingSql Booking)
        {
            Sql = "INSERT INTO booking (bruger_id, vagt_id, er_låst) VALUES (@BrugerId, @VagtId, @ErLåst);";

            var Parametre = new
            {
                BrugerId = Booking.bruger_id,
                VagtId = Booking.vagt_id,
                ErLåst = Booking.er_låst
            };

            await Context.Connection.ExecuteAsync(Sql, Parametre);
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


		private async Task TilføjPoint(int VagtId, int BrugerId)
		{
            Sql = "SELECT * FROM vagt WHERE vagt_id = @VagtId";
			var Parametre = new
			{
			    VagtId = VagtId,
			};

            var Vagt = await Context.Connection.QueryFirstOrDefaultAsync<Vagt>(Sql, Parametre);

            Sql = "UPDATE bruger SET dine_point = dine_point + @Point WHERE bruger_id = @BrugerId;";

            var Parametre2 = new
            {
                BrugerId = BrugerId,
                Point = Vagt.point
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


