using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "Codigo es obligatorio")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion es obligatoria")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Descripcion { get; set; }
    }
}
