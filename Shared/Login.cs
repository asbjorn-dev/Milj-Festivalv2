using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
    public class Login
    {
       public string Brugernavn { get; set; }
       public string Password { get; set; }
       public int bruger_id { get; set; }
        // Rolle har tom værdi fordi når en bruger prøver at login SKAL sql have både brugernavn,
        //password og rolle med. Rollerne bliver assignet/tjekket på klienten
        public string Rolle { get; set; } = "";
    }
}
