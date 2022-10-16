using System.ComponentModel.DataAnnotations;

namespace AspNetCoreExample.Models.ViewModels
{
    public class UserVM
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!!!")]
        [EmailAddress(ErrorMessage ="Format uygun değil!!!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alan!!!")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Zorunlu alan!!!")]
        [Compare(otherProperty:"Password",ErrorMessage ="Şifreler aynı değil")]
        public string PasswordTekrar { get; set; }
    }
}
