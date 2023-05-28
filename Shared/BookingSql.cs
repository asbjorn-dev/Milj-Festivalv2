
namespace MiljøFestivalv2.Shared
{
    //Klasse der bruges når der skal oprettes en booking, da bookingklassen arver fra vagt så ikke matcher databasen
    public class BookingSql
    {
        public int bruger_id { get; set; }
        public int vagt_id { get; set; }
        public Boolean er_låst { get; set; }    

    }
}
