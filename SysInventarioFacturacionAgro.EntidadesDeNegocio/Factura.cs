using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdFactura { get; set; }

        [Required(ErrorMessage = "NumeroFactura es obligatorio")]
        public int NumeroFactura { get; set; }

        [Required(ErrorMessage = "FechaFacturacion es obligatoria")]
        [Display(Name = "Fecha de Facturación")]
        public DateTime FechaFacturacion { get; set; }

        [Required(ErrorMessage = "FormaPago es obligatorio")]
        public byte FormaPago { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Descuento { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Impuesto { get; set; }

        [Required(ErrorMessage = "TotalPagado es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal TotalPagado { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string NombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string DireccionCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string TelefonoCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string DUICliente { get; set; }

        [Required(ErrorMessage = "IdUsuario es obligatorio")]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
    }
}
