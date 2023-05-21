using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
    public class Person
    {
        public int bruger_id { get; set; }
        public string fulde_navn { get; set; }

        public string email { get; set; }

        public string password { get; set; } 

        public int telefon_nummer { get; set; }
    }
}
