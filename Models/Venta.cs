using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferreteria.Models
{
    public class Venta
    {

            [Key]
            public int IdVenta { get; set; }

            public DateTime Fecha { get; set; } = DateTime.Now;

            public int IdCliente { get; set; }
            public int IdEmpleado { get; set; }
            public int IdMetodoPago { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal Total { get; set; }

            public int Estado { get; set; }

            // Relaciones
            [ForeignKey("IdCliente")]
            public virtual Cliente Cliente { get; set; } = new Cliente();

        [ForeignKey("IdEmpleado")]
        public virtual Empleado Empleado { get; set; } = new Empleado();

        [ForeignKey("IdMetodoPago")]
        public virtual MetodoPago MetodoPago { get; set; } = new MetodoPago();

            public virtual ICollection<DetVenta> Detalles { get; set; } = new List<DetVenta>();
        }
    }
