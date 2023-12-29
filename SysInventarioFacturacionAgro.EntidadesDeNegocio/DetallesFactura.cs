﻿using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class DetallesFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleFactura { get; set; }
        [ForeignKey("Factura")]
        [Required(ErrorMessage = "Factura es obligatorio")]
        [Display(Name = "Factura")]
        public int IdFactura { get; set; }
        [ForeignKey("Producto")]
        [Required(ErrorMessage = "Producto es obligatorio")]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El codigo es obligatorio")]

        public int Codigo { get; set; }

        [Required(ErrorMessage = "la Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "la forma de pago es obligatorio")]


        public byte FormaDePago { get; set; }

        [Required(ErrorMessage = "la fecha de emision obligatorio")]

        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "el valor total es obligatorio")]
        public decimal ValorTotal { get; set; }
        public Factura? Factura { get; set; }


        public Producto? Producto { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

    }

}