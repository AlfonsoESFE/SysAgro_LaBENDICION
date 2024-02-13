using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInventario { get; set; }
        //Hola
        [Required(ErrorMessage = "Detalles es obligatorio")]
        [StringLength(50, ErrorMessage = "Detalles 50 caracteres")]
        public string? Detalles { get; set; }

        [Required(ErrorMessage = "Diferencia es obligatorio")]
        public byte? Diferencia { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Producto")]
        [Required(ErrorMessage = "Producto es obligatorio")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public Usuario? Usuario { get; set; }


        public Producto? Producto { get; set; }

    }
}
