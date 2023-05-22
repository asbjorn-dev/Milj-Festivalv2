using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
	// booking arver fra vagt til frivilligsiden 
	public class Booking : Vagt
	{
		public int booking_id { get; set; }
		public string bruger_navn { get; set; }
		public int telefon_nummer { get; set; }
		public int bruger_id { get; set; }
		public Boolean er_låst { get; set; } = false;
	}
}

