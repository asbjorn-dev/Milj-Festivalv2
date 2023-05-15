using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
	public class Booking : Vagt
	{
		public int booking_id { get; set; }
		public Bruger frivllig_id { get; set; }
		public string fulde_navn { get; set; }
		public string telefon_nummer { get; set; }
		public Bruger koordinator { get; set; }
		public Boolean er_låst { get; set; }
	}
}

