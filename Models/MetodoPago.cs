using System.ComponentModel.DataAnnotations;

namespace Ferreteria.Models
{
    public class MetodoPago
    {
        [Key]
        public int IdMetPago { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;


        public int Comision { get; set; } = 0;

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
