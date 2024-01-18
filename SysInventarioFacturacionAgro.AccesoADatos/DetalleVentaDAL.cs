using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class DetalleVentaDAL
    {

        #region CRUD Y METODOS BASICOS PARA UN CRUD
        public static async Task<int> CrearAsync(DetalleVenta pDetalleVenta)
        {
            int result = 0;
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
            using (var bdContexto = new BDContexto())
            {
                var detalleVenta = await bdContexto.DetalleVenta.FirstOrDefaultAsync(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
                if (detalleVenta != null)
                {
                    detalleVenta.IdVenta = pDetalleVenta.IdVenta;
                    detalleVenta.IdProducto = pDetalleVenta.IdProducto;
                    detalleVenta.CodigoDetalles = pDetalleVenta.CodigoDetalles;
                    detalleVenta.Cantidad = pDetalleVenta.Cantidad;
                    detalleVenta.Total = pDetalleVenta.Total;
                    bdContexto.Update(detalleVenta);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<int> EliminarAsync(DetalleVenta pDetalleVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detalleVenta = await bdContexto.DetalleVenta.FirstOrDefaultAsync(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
                if (detalleVenta != null)
                {
                    bdContexto.DetalleVenta.Remove(detalleVenta);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<DetalleVenta> ObtenerPorIdAsync(DetalleVenta pDetalleVenta)
        {
            var detalleVenta = new DetalleVenta();
            using (var bdContexto = new BDContexto())
            {
                detalleVenta = await bdContexto.DetalleVenta.FirstOrDefaultAsync(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
            }
            return detalleVenta;
        }

        public static async Task<List<DetalleVenta>> ObtenerTodosAsync()
        {
            var detallesVenta = new List<DetalleVenta>();
            using (var bdContexto = new BDContexto())
            {
                detallesVenta = await bdContexto.DetalleVenta.Include(p => p.Producto).ToListAsync();
            }
            return detallesVenta;
        }

        internal static IQueryable<DetalleVenta> QuerySelect(IQueryable<DetalleVenta> pQuery, DetalleVenta pDetalleVenta)
        {
            if (pDetalleVenta.IdDetalleVenta > 0)
                pQuery = pQuery.Where(s => s.IdDetalleVenta == pDetalleVenta.IdDetalleVenta);
            if (pDetalleVenta.IdVenta > 0)
                pQuery = pQuery.Where(s => s.IdVenta == pDetalleVenta.IdVenta);
            if (pDetalleVenta.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetalleVenta.IdProducto);
            if (pDetalleVenta.IdDetalleVenta > 0)
                pQuery = pQuery.Where(s => s.CodigoDetalles == pDetalleVenta.CodigoDetalles);
            return pQuery;
        }

        public static async Task<List<DetalleVenta>> BuscarAsync(DetalleVenta pDetalleVenta)
        {
            var detallesVenta = new List<DetalleVenta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleVenta.AsQueryable();
                select = QuerySelect(select, pDetalleVenta);
                detallesVenta = await select.ToListAsync();
            }
            return detallesVenta;
        }

        public static async Task<List<DetalleVenta>> BuscarIncluirVentasYProductoAsync(DetalleVenta pDetalleVenta)
        {
            var detallesVenta = new List<DetalleVenta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetalleVenta.AsQueryable();
                select = QuerySelect(select, pDetalleVenta).Include(s => s.Venta).Include(s => s.Producto).AsQueryable();
                detallesVenta = await select.ToListAsync();
            }
            return detallesVenta;
        }
    }
}

#endregion

