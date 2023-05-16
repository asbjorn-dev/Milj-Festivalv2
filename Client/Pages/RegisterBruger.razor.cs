using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages
{
    public class RegisterModel
    {
        
       
        public string fulde_navn { get; set; }


        [Required(ErrorMessage = "Email er påkrævet")]
        [EmailAddress(ErrorMessage = "Ugyldig email-adresse")]
        public string email { get; set; }
        public int telefon_nummer { get; set; }
        public DateTime fødselsdag { get; set; }
        public string cpr_nummer { get; set; }
        public string brugernavn { get; set; }
        public string password { get; set; }
            
     

    }
}