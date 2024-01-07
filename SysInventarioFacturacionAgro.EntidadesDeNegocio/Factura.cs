using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }

        [Required(ErrorMessage = "El campo NumeroFactura es obligatorio")]
        public int NumeroFactura { get; set; }

        [Required(ErrorMessage = "El campo FechaFacturacion es obligatorio")]
        [Display(Name = "Fecha de Facturación")]
        public DateTime FechaFacturacion { get; set; }

        [Required(ErrorMessage = "El campo FormaPago es obligatorio")]
        public byte FormaPago { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Descuento { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Impuesto { get; set; }

        [Required(ErrorMessage = "El campo FacturaTotal es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal FacturaTotal { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? FacturaPagado { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? FacturaCambio { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo NombreCliente")]
        public string NombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo DireccionCliente")]
        public string DireccionCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo TelefonoCliente")]
        public string TelefonoCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo DUICliente")]
        public string DUICliente { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "El campo IdUsuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "La colección DetallesFactura es obligatoria")]
        public ICollection<DetallesFactura> DetallesFactura { get; set; }
    }
}
