using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
	public class Vagt
	{
		// Metoder der fjerner sekunderne fra start- og sluttid på en vagt, bruges i clienten
		public DateTime FjernSekunderStart()
		{
			DateTime Start = this.start_tid;
			DateTime StartUdenSekunder = new DateTime(Start.Year, Start.Month, Start.Day, Start.Hour, Start.Minute, 0);

			return (StartUdenSekunder);
		}

		public DateTime FjernSekunderSlut() 
		{
			DateTime Slut = this.slut_tid;
			DateTime SlutUdenSekunder = new DateTime(Slut.Year, Slut.Month, Slut.Day, Slut.Hour, Slut.Minute, 0);

			return SlutUdenSekunder;
		}

		public int vagt_id { get; set; }
		public string område { get; set; }
		public DateTime start_tid { get; set; }
		public DateTime slut_tid { get; set; }
		public string beskrivelse { get; set; }
		public string priotering { get; set; }
		public int antal_personer { get; set; }
		public int point { get; set; }
	}
}

