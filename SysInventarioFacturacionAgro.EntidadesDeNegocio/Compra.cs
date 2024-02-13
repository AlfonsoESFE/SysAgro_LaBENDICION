using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "Numero de Compra es obligatorio")]
        public int NumeroCompra { get; set; }

        [Display(Name = "Fecha Compra")]
        public DateTime FechaCompra { get; set; }

        [Required(ErrorMessage = "Forma de Pago es obligatorio")]
        public byte FormaPago { get; set; }

        [Required(ErrorMessage = "Total de pago es obligatorio")]
        public decimal TotalPago { get; set; }

        [Required(ErrorMessage = "Total es obligatorio")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Observaciones es obligatorio")]
        public string? Observaciones { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Proveedor")]
        [Required(ErrorMessage = "Proveedor es obligatorio")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        public Usuario? Usuario { get; set; }

        public Proveedor? Proveedor { get; set; }

        public List<DetallesCompra>? DetallesCompra { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
