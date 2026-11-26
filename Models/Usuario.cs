using System.ComponentModel.DataAnnotations;

namespace Ferreteria.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string PasswordHash { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public int IdRol { get; set; }

        public virtual Rol Rol { get; set; } = null!;

        public int IdEmpleado { get; set; }

        public virtual Empleado Empleado { get; set; } = null!;
    }
}