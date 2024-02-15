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

        [Required(ErrorMessage = "NumeroCompra es obligatorio")]
        public int NumeroCompra { get; set; }

        [Required(ErrorMessage = "FechaCompra es obligatoria")]
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaCompra { get; set; }

        [Required(ErrorMessage = "FormaPago es obligatorio")]
        public byte FormaPago { get; set; }
        [Required(ErrorMessage = "Tota de pago es obligatorio")]
        public decimal TotalPago { get; set; }

        [ForeignKey("Proveedor")]
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "El proveedor es obligatorio")]
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        [ForeignKey("Usuario")]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public List<DetallesCompra>? DetallesCompra { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
