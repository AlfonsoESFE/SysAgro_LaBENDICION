using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class DetallesFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdDetallesFactura { get; set; }

        [Required(ErrorMessage = "Codigo es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "FechaFacturacion es obligatoria")]
        [Display(Name = "Fecha de Facturación")]
        public DateTime FechaFacturacion { get; set; }

        [Required(ErrorMessage = "Total es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "IdProducto es obligatorio")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "IdFactura es obligatorio")]
        public int IdFactura { get; set; }

        public Producto Producto { get; set; }
        public Factura Factura { get; set; }
    }
}
