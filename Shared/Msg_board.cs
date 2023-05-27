using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
    public class Msg_board
    {
        public string besked { get; set; }
        public string afsender { get; set; }
        public DateTime tidspunkt { get; set; }

		// Metode der fjerner sekunder fra tidspunktet for beskeder når den kaldes i clienten
		public DateTime FjernSekunderBesked()
		{
			DateTime Tidspunkt = this.tidspunkt;
			DateTime TidUdenSekunder = new DateTime(Tidspunkt.Year, Tidspunkt.Month, Tidspunkt.Day, Tidspunkt.Hour, Tidspunkt.Minute, 0);

			return TidUdenSekunder;
		}

	}
}
