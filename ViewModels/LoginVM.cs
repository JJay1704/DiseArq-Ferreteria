using System.ComponentModel.DataAnnotations;

namespace Ferreteria.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Ingrese su correo electrónico")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [StringLength(150)]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese su contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}