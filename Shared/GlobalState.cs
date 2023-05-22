using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
    // Bruges til at definere hvilken bruger der er logget ind
    public class GlobalState
    {
        public string Brugernavn { get; set; }
        public int bruger_id { get; set; }
        public int vagt_id { get; set; }
        public string fulde_navn { get; set; }
        public int telefon_nummer { get; set; }

    }
}
