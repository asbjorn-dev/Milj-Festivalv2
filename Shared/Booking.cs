
namespace MiljøFestivalv2.Shared
{
	// booking klassen arver fra vagt 
	public class Booking : Vagt
	{
		// Denne klasse er kun til front-end variabler
		public int booking_id { get; set; }
		public string fulde_navn { get; set; }
		public int telefon_nummer { get; set; }
		public int bruger_id { get; set; }
		public Boolean er_låst { get; set; } = false;
	}
}

