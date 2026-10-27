using System.ComponentModel.DataAnnotations;

namespace Ferreteria.Models
{
    public class TipoCliente
    {
        [Key]
        public int IdTpeCliente { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(100)]
        public string Descr { get; set; } = string.Empty;

        // Propiedad de navegación (Uno a Muchos)
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
