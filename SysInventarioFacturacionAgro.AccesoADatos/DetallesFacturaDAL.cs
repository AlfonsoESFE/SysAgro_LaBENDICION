using System;
using System.Collections.Generic;
using System.Linq;
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
                var detalleFactura = await bdContexto.DetallesFactura.FirstOrDefaultAsync(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
                detalleFactura.IdFactura = pDetallesFactura.IdFactura;
                detalleFactura.IdProducto = pDetallesFactura.IdProducto;
                detalleFactura.CodigoDetalles = pDetallesFactura.CodigoDetalles;
                detalleFactura.Cantidad = pDetallesFactura.Cantidad;
                detalleFactura.Total = pDetallesFactura.Total;
                bdContexto.Update(detalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(DetallesFactura pDetallesFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detalleFactura = await bdContexto.DetallesFactura.FirstOrDefaultAsync(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
                bdContexto.DetallesFactura.Remove(detalleFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<DetallesFactura> ObtenerPorIdAsync(DetallesFactura pDetallesFactura)
        {
            var detalleFactura = new DetallesFactura();
            using (var bdContexto = new BDContexto())
            {
                detalleFactura = await bdContexto.DetallesFactura.FirstOrDefaultAsync(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
            }
            return detalleFactura;
        }

        public static async Task<List<DetallesFactura>> ObtenerTodosAsync()
        {
            var DetallesFacturas = new List<DetallesFactura>();
            using (var bdContexto = new BDContexto())
            {
                DetallesFacturas = await bdContexto.DetallesFactura.Include(p => p.Producto).ToListAsync();
            }
            return DetallesFacturas;
        }

        internal static IQueryable<DetallesFactura> QuerySelect(IQueryable<DetallesFactura> pQuery, DetallesFactura pDetallesFactura)
        {
            if (pDetallesFactura.IdDetallesFactura > 0)
                pQuery = pQuery.Where(s => s.IdDetallesFactura == pDetallesFactura.IdDetallesFactura);
            if (pDetallesFactura.IdFactura > 0)
                pQuery = pQuery.Where(s => s.IdFactura == pDetallesFactura.IdFactura);
            if (pDetallesFactura.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetallesFactura.IdProducto);
            if (pDetallesFactura.CodigoDetalles > 0)
                pQuery = pQuery.Where(s => s.CodigoDetalles == pDetallesFactura.CodigoDetalles);
            if (pDetallesFactura.Cantidad > 0)
                pQuery = pQuery.Where(s => s.Cantidad == pDetallesFactura.Cantidad);
            if (pDetallesFactura.Total > 0)
                pQuery = pQuery.Where(s => s.Total == pDetallesFactura.Total);

            pQuery = pQuery.OrderByDescending(s => s.IdDetallesFactura).AsQueryable();

            return pQuery;
        }

        public static async Task<List<DetallesFactura>> BuscarAsync(DetallesFactura pDetallesFactura)
        {
            var DetallesFacturas = new List<DetallesFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallesFactura.AsQueryable();
                select = QuerySelect(select, pDetallesFactura);
                DetallesFacturas = await select.ToListAsync();
            }
            return DetallesFacturas;
        }

        public static async Task<List<DetallesFactura>> BuscarIncluirFacturasYProductoAsync(DetallesFactura pDetallesFactura)
        {
            var DetallesFacturas = new List<DetallesFactura>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallesFactura.AsQueryable();
                select = QuerySelect(select, pDetallesFactura).Include(s => s.Factura).Include(s => s.Producto).AsQueryable();
                DetallesFacturas = await select.ToListAsync();
            }
            return DetallesFacturas;
        }
    }
}
