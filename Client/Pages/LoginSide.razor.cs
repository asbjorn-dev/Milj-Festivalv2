using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Pages
{

    public class LoginModel
    {
        [Required(ErrorMessage = "Email er påkrævet")]
        [EmailAddress(ErrorMessage = "Ugyldig email-adresse")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password er påkrævet")]
        public string password { get; set; }
    }

}
