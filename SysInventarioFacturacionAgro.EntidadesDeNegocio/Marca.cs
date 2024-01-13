using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdMarca { get; set; }

        [Required(ErrorMessage = "Nombre de Marca es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? Nombre { get; set; }

        [NotMapped]
        public ICollection<Producto>? Producto { get; set; }

    }
}
