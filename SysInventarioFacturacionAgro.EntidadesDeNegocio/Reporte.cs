using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Reporte
    {
        [Key]
        public int IdReporte { get; set; }

        [Required(ErrorMessage = "TipoReporte es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string TipoReporte { get; set; }

        [Required(ErrorMessage = "FechaReporte es obligatoria")]
        [Display(Name = "Fecha del Reporte")]
        public DateTime FechaReporte { get; set; }

        [Required(ErrorMessage = "Detalles es obligatorio")]
        [StringLength(int.MaxValue, ErrorMessage = "Detalles puede ser de longitud ilimitada")]
        public string Detalles { get; set; }

        [Required(ErrorMessage = "IdUsuario es obligatorio")]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
    }
}
