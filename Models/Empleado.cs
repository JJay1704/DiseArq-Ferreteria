using System.ComponentModel.DataAnnotations;

namespace Ferreteria.Models
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombres { get; set; } = string.Empty;

        [StringLength(50)]
        public string ApeMat { get; set; } = string.Empty;

        [StringLength(50)]
        public string ApePat { get; set; } = string.Empty;

        [StringLength(5)]
        public string TipoDoc { get; set; } = string.Empty;

        [StringLength(12)]
        public string NumDoc { get; set; } = string.Empty;

        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
