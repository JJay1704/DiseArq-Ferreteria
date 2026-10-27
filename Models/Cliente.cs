using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferreteria.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [StringLength(100)]
        public string Codigo { get; set; } = string.Empty;

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

        public int TpeCliente { get; set; }

        [StringLength(100)]
        public string NumCuenta { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Saldo { get; set; } = 0.00m;
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalConsum { get; set; } = 0.00m;

        // Relaciones
        [ForeignKey("TpeCliente")]
        public virtual TipoCliente TipoClienteNavegacion { get; set; } = new TipoCliente();
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
