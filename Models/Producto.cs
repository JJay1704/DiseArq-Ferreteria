using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferreteria.Models
{
    public class Producto
    {

        public int IdProducto { get; set; }

        [StringLength(50)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        public int IdCat { get; set; }
        public int IdMarca { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(50)]
        public string Unidad { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioCosto { get; set; } = 0.00m;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioVenta { get; set; } = 0.00m;

        [Column(TypeName = "decimal(10,2)")]
        public decimal StockActual { get; set; } = 0.00m;

        [Column(TypeName = "decimal(10,2)")]
        public decimal StockMinimo { get; set; } = 0.00m;

        [StringLength(50)]
        public string Ubicacion { get; set; } = string.Empty;

        public byte Estado { get; set; } = 1;

        // Relaciones
        [ForeignKey("IdCat")]
        public virtual Categoria Cat { get; set; } = new Categoria();

        [ForeignKey("IdMarca")]
        public virtual Marca Marc { get; set; } = new Marca();
    }
}
