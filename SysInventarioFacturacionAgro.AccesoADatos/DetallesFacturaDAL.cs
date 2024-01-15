using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class DetallesFacturaDAL
    {
        public static async Task<int> CrearAsync(DetallesFactura pDetallesFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pDetallesFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(DetallesFactura pDetallesFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallesfactura = await bdContexto.DetallesFactura.FirstOrDefaultAsync(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
                detallesfactura.IdFactura = pDetallesFactura.IdFactura;
                detallesfactura.IdProducto = pDetallesFactura.IdProducto;
                detallesfactura.CodigoDetalles = pDetallesFactura.CodigoDetalles;
                detallesfactura.Cantidad = pDetallesFactura.Cantidad;
                detallesfactura.Total = pDetallesFactura.Total;
                bdContexto.Update(detallesfactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(DetallesFactura pDetallesFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallesfactura = await bdContexto.DetallesFactura.FirstOrDefaultAsync(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
                bdContexto.DetallesFactura.Remove(detallesfactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetallesFactura> ObtenerPorIdAsync(DetallesFactura pDetallesFactura)
        {
            var detallesfactura = new DetallesFactura();
            using (var bdContexto = new BDContexto())
            {
                detallesfactura = await bdContexto.DetallesFactura.FirstOrDefaultAsync(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
            }
            return detallesfactura;
        }
        public static async Task<List<DetallesFactura>> ObtenerTodosAsync()
        {
            var DetalleFacturas = new List<DetallesFactura>();
            using (var bdContexto = new BDContexto())
            {
                DetalleFacturas = await bdContexto.DetallesFactura.Include(p => p.Producto).ToListAsync();
            }
            return DetalleFacturas;
        }
        internal static IQueryable<DetallesFactura> QuerySelect(IQueryable<DetallesFactura> pQuery, DetallesFactura pDetallesFactura)
        {
            if (pDetallesFactura.IdDetallesFactura > 0)
                pQuery = pQuery.Where(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
            if (pDetallesFactura.IdFactura > 0)
                pQuery = pQuery.Where(s => s.IdFactura == pDetallesFactura.IdFactura);
            if (pDetallesFactura.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetallesFactura.IdProducto);
            if (pDetallesFactura.IdDetallesFactura > 0)
                pQuery = pQuery.Where(s => s.CodigoDetalles == pDetallesFactura.CodigoDetalles);
            return pQuery;
        }
        public static async Task<List<DetallesFactura>> BuscarAsync(DetallesFactura pDetallesFactura)
        {
            var DetalleFacturas = new List<DetallesFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallesFactura.AsQueryable();
                select = QuerySelect(select, pDetallesFactura);
                DetalleFacturas = await select.ToListAsync();
            }
            return DetalleFacturas;
        }

        public static async Task<List<DetallesFactura>> BuscarIncluirFacturasYProductoAsync(DetallesFactura pDetallesFactura)
        {
            var DetalleFacturas = new List<DetallesFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallesFactura.AsQueryable();
                select = QuerySelect(select, pDetallesFactura).Include(s => s.Factura).Include(s => s.Producto).AsQueryable();
                DetalleFacturas = await select.ToListAsync();
            }
            return DetalleFacturas;
        }
    }
}