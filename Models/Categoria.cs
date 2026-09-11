using System.ComponentModel.DataAnnotations;

namespace Ferreteria.Models
{
    public class Categoria
    {

  
            [Key]
            public int IdCat { get; set; }

            [Required]
            [StringLength(50)]
            public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        public string Descr { get; set; } = string.Empty;

            public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
        
    }
}
