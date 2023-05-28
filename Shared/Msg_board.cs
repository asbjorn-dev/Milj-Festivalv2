
namespace MiljøFestivalv2.Shared
{
    public class Msg_board
    {
        public string Besked { get; set; }
        public string Afsender { get; set; }
        public DateTime Tidspunkt { get; set; }

		// Metode der fjerner sekunder fra Tidspunktet for Beskeder når den kaldes i clienten
		public DateTime FjernSekunderBesked()
		{
			DateTime Tidspunkt = this.Tidspunkt;
			DateTime TidUdenSekunder = new DateTime(Tidspunkt.Year, Tidspunkt.Month, Tidspunkt.Day, Tidspunkt.Hour, Tidspunkt.Minute, 0);

			return TidUdenSekunder;
		}

	}
}
