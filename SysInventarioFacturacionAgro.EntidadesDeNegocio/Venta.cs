using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVenta { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Numero venta es obligatorio")]
        public int NumeroVenta { get; set; }


        [Required(ErrorMessage = "Fecha venta es obligatorio")]
        public DateTime FechaVenta { get; set; }

        [Required(ErrorMessage = "NombreCliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? NombreCliente { get; set; }


        [Required(ErrorMessage = "DireccionCliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string? DireccionCliente { get; set; }


        [Required(ErrorMessage = "TelefonoCliente es obligatorio")]
        [StringLength(15, ErrorMessage = "Maximo 15 caracteres")]
        public string? TelefonoCliente { get; set; }


        [Required(ErrorMessage = "DUICliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 9 caracteres")]
        public string? DUICliente { get; set; }


        [Required(ErrorMessage = "FormaDePago es obligatorio")]
        public byte? FormaPago { get; set; }


        [Display(Name = "Total")]
        [Required(ErrorMessage = "El campo Total es obligatorio.")]
        public decimal? Total { get; set; }


        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "El campo Descuento es obligatorio.")]
        [Range(0.00, 10000, ErrorMessage = "El campo Precio debe estar entre 0.01 y 10,000.")]
        public decimal? Descuento { get; set; }


        [Display(Name = "Impuesto")]
        [Required(ErrorMessage = "El campo Impuesto es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El campo Precio debe estar entre 0.01 y 10,000.")]
        public decimal? Impuesto { get; set; }


        [Display(Name = "Total Pagado")]
        [Required(ErrorMessage = "El campo Total Pagado es obligatorio.")]
        [Range(0.01, 10000, ErrorMessage = "El campo Precio debe estar entre 0.01 y 10,000.")]
        public decimal? TotalPagado { get; set; }

        public Usuario? Usuario { get; set; }

        public List<DetalleVenta>? DetalleVenta { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

    }
}
