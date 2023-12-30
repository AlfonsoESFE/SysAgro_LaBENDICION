using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class AjusteInventario
    {
        [Key]
        public int IdAjusteInventario { get; set; }

        [Required(ErrorMessage = "TipoAjuste es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string TipoAjuste { get; set; }

        [Required(ErrorMessage = "FechaAjuste es obligatorio")]
        [Display(Name = "Fecha de Ajuste")]
        public DateTime FechaAjuste { get; set; }

        [Required(ErrorMessage = "Detalles es obligatorio")]
        [MaxLength]
        public string Detalles { get; set; }

        [Required(ErrorMessage = "IdUsuario es obligatorio")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "IdProducto es obligatorio")]
        public int IdProducto { get; set; }

        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
    }
}
