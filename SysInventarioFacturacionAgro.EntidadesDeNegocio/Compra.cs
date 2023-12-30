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

        [Required(ErrorMessage = "Observaciones es obligatorio")]
        [MaxLength]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "IdProveedor es obligatorio")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "IdUsuario es obligatorio")]
        public int IdUsuario { get; set; }

        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<DetallesCompra> DetallesCompra { get; set; }
    }
}
