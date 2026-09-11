using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferreteria.Models
{
    public class DetVenta
    {

            [Key]
            public int IdDetVent { get; set; }

            public int? IdVenta { get; set; }
            public int? IdProducto { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal Cant { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal PrecUnit { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal? Descuento { get; set; }

            // Relaciones
            [ForeignKey("IdVenta")]
            public virtual Venta? Venta { get; set; }

            [ForeignKey("IdProducto")]
            public virtual Producto? Producto { get; set; }
        }
    }

