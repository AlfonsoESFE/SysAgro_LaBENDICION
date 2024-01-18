using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El campo NumeroFactura es obligatorio")]
        public int NumeroFactura { get; set; }

        [Required(ErrorMessage = "El campo FechaFacturacion es obligatorio")]
        [Display(Name = "Fecha de Facturación")]
        public DateTime FechaFacturacion { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Descuento { get; set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Impuesto { get; set; }

        [Required(ErrorMessage = "El campo SubTotal es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal SubTotal { get; set; }

        [Required(ErrorMessage = " El campo Total es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Total { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo NombreCliente")]
        public string? NombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo DireccionCliente")]
        public string? DireccionCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo TelefonoCliente")]
        public string? TelefonoCliente { get; set; }

        [StringLength(50, ErrorMessage = "Máximo 50 caracteres para el campo DUICliente")]
        public string? DUICliente { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "El campo IdUsuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "La colección DetalleVenta es obligatoria")]
        public ICollection<DetalleVenta>? DetalleVenta { get; set; }
    }
}
