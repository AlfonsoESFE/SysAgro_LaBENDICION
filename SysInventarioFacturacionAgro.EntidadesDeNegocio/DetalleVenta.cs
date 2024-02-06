using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleVenta { get; set; }

        [ForeignKey("Venta")]
        [Required(ErrorMessage = "Venta es obligatorio")]
        [Display(Name = "Venta")]
        public int IdVenta { get; set; }

        [ForeignKey("Producto")]
        [Required(ErrorMessage = "Producto es obligatorio")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        

        [Required(ErrorMessage = "la Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El descuento es obligatorio")]
        public decimal? Descuento { get; set; }


        [Required(ErrorMessage = "el valor total es obligatorio")]
        public decimal ValorTotal { get; set; }

        public Venta? Venta { get; set; }


        public Producto? Producto { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
