using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public class Booking
	{
		public int booking_id { get; set; }
		public Vagt vagt { get; set;}
		public Bruger frivillig { get; set; }
		public Bruger koordinator { get; set; }
		public Boolean er_låst { get; set; }
	}
}

