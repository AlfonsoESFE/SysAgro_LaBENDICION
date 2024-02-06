using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.EntidadesDeNegocio
{
    public class Ventas
    {
        public Venta ObjVenta { get; set; }
        public List<DetalleVenta> DetalleVentas { get; set; }
    }
}
