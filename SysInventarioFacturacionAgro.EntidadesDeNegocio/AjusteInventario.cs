using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class AjusteInventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdAjusteInventario { get; set; }

        [Required(ErrorMessage = "TipoAjuste es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? TipoAjuste { get; set; }


        [Required(ErrorMessage = "FechaAjuste es obligatorio")]
        [Display(Name = "Fecha de Ajuste")]
        public DateTime FechaAjuste { get; set; }


        [Required(ErrorMessage = "Detalles es obligatorio")]
        [MaxLength]
        public string? Detalles { get; set; }


        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "El campo IdUsuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }


        [ForeignKey("Producto")]
        [Required(ErrorMessage = "El campo IdProducto es obligatorio")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        public Usuario? Usuario { get; set; }
        public Producto? Producto { get; set; }
    }
}
