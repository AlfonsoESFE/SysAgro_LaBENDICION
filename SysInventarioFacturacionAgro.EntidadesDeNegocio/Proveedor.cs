using System.ComponentModel.DataAnnotations;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "Codigo es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Telefono es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Correo es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Empresa es obligatoria")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "DireccionEmpresa es obligatoria")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string DireccionEmpresa { get; set; }

        [Required(ErrorMessage = "TelefonoEmpresa es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string TelefonoEmpresa { get; set; }
    }
}
