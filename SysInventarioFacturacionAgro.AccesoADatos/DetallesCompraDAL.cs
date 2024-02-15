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
    public class DetallesCompraDAL

    {
        public static async Task<int> CrearAsync(DetallesCompra pDetalleCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {

                bdContexto.Add(pDetalleCompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(DetallesCompra pDetalleCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallecompra = await bdContexto.DetallesCompra.FirstOrDefaultAsync(s => s.IdDetallesCompra == pDetalleCompra.IdDetallesCompra);
                detallecompra.IdProducto = pDetalleCompra.IdProducto;
                detallecompra.IdCompra = pDetalleCompra.IdCompra;
                detallecompra.Cantidad = pDetalleCompra.Cantidad;
                bdContexto.Update(detallecompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(DetallesCompra pDetalleCompra)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var detallecompra = await bdContexto.DetallesCompra.FirstOrDefaultAsync(s => s.IdDetallesCompra == pDetalleCompra.IdDetallesCompra);
                bdContexto.DetallesCompra.Remove(detallecompra);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<DetallesCompra> ObtenerPorIdAsync(DetallesCompra pDetalleCompra)
        {
            var detallecompra = new DetallesCompra();
            using (var bdContexto = new BDContexto())
            {
                detallecompra = await bdContexto.DetallesCompra.FirstOrDefaultAsync(s => s.IdDetallesCompra == pDetalleCompra.IdDetallesCompra);
            }
            return detallecompra;
        }
        public static async Task<List<DetallesCompra>> ObtenerTodosAsync()
        {
            var DetalleCompra = new List<DetallesCompra>();
            using (var bdContexto = new BDContexto())
            {
                DetalleCompra = await bdContexto.DetallesCompra.Include(p => p.Producto).ToListAsync();
            }
            return DetalleCompra;
        }
        internal static IQueryable<DetallesCompra> QuerySelect(IQueryable<DetallesCompra> pQuery, DetallesCompra pDetalleCompra)
        {
            if (pDetalleCompra.IdDetallesCompra > 0)
                pQuery = pQuery.Where(s => s.IdDetallesCompra == pDetalleCompra.IdDetallesCompra);
            if (pDetalleCompra.IdCompra > 0)
                pQuery = pQuery.Where(s => s.IdCompra == pDetalleCompra.IdCompra);
            if (pDetalleCompra.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pDetalleCompra.IdProducto);
            //if (pDetalleFactura.Codigo > 0)
            //    pQuery = pQuery.Where(s => s.Codigo == pDetalleFactura.Codigo);
            //if (pDetalleFactura.Cantidad > 0)
            //    pQuery = pQuery.Where(s => s.Cantidad == pDetalleFactura.Cantidad);          
            //if (!string.IsNullOrWhiteSpace(pDetalleFactura.FormaDePago))
            //    pQuery = pQuery.Where(s => s.FormaDePago.Contains(pDetalleFactura.FormaDePago));


            //if (!string.IsNullOrWhiteSpace(pDetalleFactura.FechaEmision.ToString()))
            //    pQuery = pQuery.Where(s => s.FechaEmision.ToString().Contains(pDetalleFactura.FechaEmision.ToString()));
            //if (pDetalleFactura.ValorTotal > 0)
            //    pQuery = pQuery.Where(s => s.ValorTotal == pDetalleFactura.ValorTotal);



            pQuery = pQuery.OrderByDescending(s => s.IdDetallesCompra).AsQueryable();
            if (pDetalleCompra.Top_Aux > 0)
                pQuery = pQuery.Take(pDetalleCompra.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<DetallesCompra>> BuscarAsync(DetallesCompra pDetalleCompra)
        {
            var DetalleCompras = new List<DetallesCompra>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallesCompra.AsQueryable();
                select = QuerySelect(select, pDetalleCompra);
                DetalleCompras = await select.ToListAsync();
            }
            return DetalleCompras;
        }
        public static async Task<List<DetallesCompra>> BuscarIncluirCompraProductoAsync(DetallesCompra pDetalleCompra)
        {
            var DetalleCompra = new List<DetallesCompra>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.DetallesCompra.AsQueryable();
                select = QuerySelect(select, pDetalleCompra).Include(s => s.Compra).Include(s => s.Producto).AsQueryable();
                DetalleCompra = await select.ToListAsync();
            }
            return DetalleCompra;
        }
    }
}