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
        public int vagt_id { get; set; }
        public Bruger bruger { get; set; }
    }
}
