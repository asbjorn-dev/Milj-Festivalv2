
using System.ComponentModel.DataAnnotations;

namespace MiljøFestivalv2.Shared
{ 
    //Klasse der bruges når en bruger skal logge ind, så requirements fra brugerklassen ikke giver fejl ved login
    public class Login 
    {
        //Requirements på at der skal indtastes brugernavn og password
        [Required(ErrorMessage = "Brugernavn er påkrævet")]
        public string Brugernavn { get; set; }
        [Required(ErrorMessage = "Password er påkrævet")]
        public string Password { get; set; }
        public int bruger_id { get; set; }
        public string Rolle { get; set; }
    }
}
