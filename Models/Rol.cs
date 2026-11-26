using System.ComponentModel.DataAnnotations;

namespace Ferreteria.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreRol { get; set; } = string.Empty;

        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}