using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiljøFestivalv2.Shared
{
    public class BookingSql
    {
        public int bruger_id { get; set; }
        public int vagt_id { get; set; }
        public Boolean er_låst { get; set; }    

    }
}
