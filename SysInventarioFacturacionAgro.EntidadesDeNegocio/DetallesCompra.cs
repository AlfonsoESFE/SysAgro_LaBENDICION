using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class DetallesCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetallesCompra { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "PrecioUnitario es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "Descuento es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal Descuento { get; set; }

        [Required(ErrorMessage = "IdCompra es obligatorio")]
        public int IdCompra { get; set; }

        public Compra Compra { get; set; }
    }
}
