using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Codigo es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion es obligatoria")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "PrecioCompra es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "PrecioUnitario es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "PrecioMayorista es obligatorio")]
        [Column(TypeName = "decimal(18, 1)")]
        public decimal PrecioMayorista { get; set; }

        [Required(ErrorMessage = "CantidadStock es obligatoria")]
        public int CantidadStock { get; set; }

        [Required(ErrorMessage = "FechaIngreso es obligatoria")]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "FechaVencimiento es obligatoria")]
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "PaisOrigen es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? PaisOrigen { get; set; }

        [Required(ErrorMessage = "UnidadMedida es obligatoria")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string? UnidadMedida { get; set; }

        //RELACIONES

        [ForeignKey("Proveedor")]
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "IdProveedor es obligatorio")]
        public int IdProveedor { get; set; }

        [ForeignKey("Categoria")]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "IdCategoria es obligatorio")]
        public int IdCategoria { get; set; }

        [ForeignKey("Marca")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "IdMarca es obligatorio")]
        public int IdMarca { get; set; }

        

        public Proveedor? Proveedor { get; set; }
        public Categoria? Categoria { get; set; }
        public Marca? Marca { get; set; }
    }
}
