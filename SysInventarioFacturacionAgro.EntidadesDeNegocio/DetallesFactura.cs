using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class DetallesFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetallesFactura { get; set; }

        [Required(ErrorMessage = "CodigoDetalles es obligatorio")]
        public int CodigoDetalles { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Total es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal Total { get; set; }

        // RELACIONES CON OTRAS TABLAS FK
        [ForeignKey("Producto")]
        [Display(Name = "Producto")]
        [Required(ErrorMessage = "IdProducto es obligatorio")]
        public int IdProducto { get; set; }


        [ForeignKey("Factura")]
        [Display(Name = "Factura")]
        [Required(ErrorMessage = "IdFactura es obligatorio")]
        public int IdFactura { get; set; }

        public Factura? Factura { get; set; }
        public Producto? Producto { get; set; }

    }
}
