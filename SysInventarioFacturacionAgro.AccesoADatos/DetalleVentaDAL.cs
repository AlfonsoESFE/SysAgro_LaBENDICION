using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class DetalleVentaDAL
    {
        public static async Task<int> CrearAsync(DetalleVenta pDetalleVenta)
        {
            int result = 0;

            // Asigna 0 al descuento si es nulo
            pDetalleVenta.Descuento ??= 0;

            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pDetalleVenta);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }

        public static async Task<int> ModificarAsync(DetalleVenta pDetalleVenta)
        {
            int result = 0;

            // Asigna 0 al descuento si es nulo
            pDetalleVenta.Descuento ??= 0;

            using (var bdContexto = new BDContexto())
            {
                var detalleventa = await bdContexto.DetalleVenta.FirstOrDefaultAsync(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
                detalleventa.IdProducto = pDetalleVenta.IdProducto;
                detalleventa.IdVenta = pDetalleVenta.IdVenta;
                detalleventa.Cantidad = pDetalleVenta.Cantidad;
                detalleventa.ValorTotal = pDetalleVenta.ValorTotal;
                detalleventa.Descuento = pDetalleVenta.Descuento;
                bdContexto.Update(detalleventa);
                result = await bdContexto.SaveChangesAsync();
            }

            return result;
        }

        public static async Task<int> EliminarAsync(DetalleVenta pDetalleVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detalleventa = await bdContexto.DetalleVenta.FirstOrDefaultAsync(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
                bdContexto.DetalleVenta.Remove(detalleventa);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetalleVenta> ObtenerPorIdAsync(DetalleVenta pDetalleVenta)
        {
            var detalleventa = new DetalleVenta();
            using (var bdContexto = new BDContexto())
            {
                detalleventa = await bdContexto.DetalleVenta.FirstOrDefaultAsync(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
            }
            return detalleventa;
        }
        public static async Task<List<DetalleVenta>> ObtenerTodosAsync()
        {
            var DetalleVenta = new List<DetalleVenta>();
            using (var bdContexto = new BDContexto())
            {
                DetalleVenta = await bdContexto.DetalleVenta.Include(p => p.Producto).ToListAsync();
            }
            return DetalleVenta;
        }
        internal static IQueryable<DetalleVenta> QuerySelect(IQueryable<DetalleVenta> pQuery, DetalleVenta pDetalleVenta)
        {
            if (pDetalleVenta.IdDetalleVenta > 0)
                pQuery = pQuery.Where(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
            if (pDetalleVenta.IdVenta > 0)
                pQuery = pQuery.Where(s => s.IdVenta == pDetalleVenta.IdVenta);
            if (pDetalleVenta.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetalleVenta.IdProducto);

          

            pQuery = pQuery.OrderByDescending(s => s.IdDetalleVenta).AsQueryable();
            if (pDetalleVenta.Top_Aux > 0)
                pQuery = pQuery.Take(pDetalleVenta.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<DetalleVenta>> BuscarAsync(DetalleVenta pDetalleVenta)
        {
            var DetalleVentas = new List<DetalleVenta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleVenta.AsQueryable();
                select = QuerySelect(select, pDetalleVenta);
                DetalleVentas = await select.ToListAsync();
            }
            return DetalleVentas;
        }

        public static async Task<List<DetalleVenta>> BuscarIncluirVentaProductoAsync(DetalleVenta pDetalleVenta)
        {
            var DetalleVentas = new List<DetalleVenta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleVenta.AsQueryable();
                select = QuerySelect(select, pDetalleVenta).Include(s => s.Venta).Include(s => s.Producto).AsQueryable();
                DetalleVentas = await select.ToListAsync();
            }
            return DetalleVentas;
        }
    }

}
