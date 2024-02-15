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

        [Required(ErrorMessage = "Valor Total es obligatorio")]
        public decimal ValorTotal { get; set; }

        [ForeignKey("Compra")]
        [Display(Name = "Compra")]
        [Required(ErrorMessage = "IdCompra es obligatorio")]
        public int IdCompra { get; set; }

        [ForeignKey("Producto")]
        [Display(Name = "Producto")]
        [Required(ErrorMessage = "IdProducto es obligatorio")]
        public int IdProducto { get; set; }

        public Compra? Compra { get; set; }
        public Producto? Producto { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

    }
}