using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class VentaDAL
    {
        public static async Task<int> CrearAsync(Venta pVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                pVenta.FechaVenta = DateTime.Now;
                bdContexto.Add(pVenta);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Venta pVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Venta = await bdContexto.Venta.FirstOrDefaultAsync(s => s.IdVenta == pVenta.IdVenta);
                Venta.IdUsuario = pVenta.IdUsuario;
                Venta.NumeroVenta = pVenta.NumeroVenta;
                Venta.NombreCliente = pVenta.NombreCliente;
                Venta.DireccionCliente = pVenta.DireccionCliente;
                Venta.TelefonoCliente = pVenta.TelefonoCliente;
                Venta.DUICliente = pVenta.DUICliente;
                Venta.FormaPago = pVenta.FormaPago;
                Venta.Total = pVenta.Total;
                Venta.Descuento = pVenta.Descuento;
                Venta.Impuesto = pVenta.Impuesto;
                Venta.TotalPagado = pVenta.TotalPagado;
                bdContexto.Update(Venta);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Venta pVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Venta = await bdContexto.Venta.FirstOrDefaultAsync(s => s.IdVenta == pVenta.IdVenta);
                bdContexto.Venta.Remove(Venta);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Venta> ObtenerPorIdAsync(Venta pVenta)
        {
            var Venta = new Venta();
            using (var bdContexto = new BDContexto())
            {
                Venta = await bdContexto.Venta.FirstOrDefaultAsync(s => s.IdVenta == pVenta.IdVenta);
            }
            return Venta;
        }
        public static async Task<List<Venta>> ObtenerTodosAsync()
        {
            var Ventas = new List<Venta>();
            using (var bdContexto = new BDContexto())
            {
                Ventas = await bdContexto.Venta.Include(v => v.DetalleVenta).ThenInclude(p => p.Producto).ToListAsync();
            }
            return Ventas;
        }
        internal static IQueryable<Venta> QuerySelect(IQueryable<Venta> pQuery, Venta pVenta)
        {
            //Para enteros y decimales
            if (pVenta.IdVenta > 0)
                pQuery = pQuery.Where(s => s.IdVenta == pVenta.IdVenta);
            //if (pFactura.IdUsuario > 0)
            //    pQuery = pQuery.Where(s => s.IdUsuario == pFactura.IdUsuario);
            //if (pFactura.NumeroFactura > 0)
            //    pQuery = pQuery.Where(s => s.NumeroFactura == pFactura.NumeroFactura);
            //Para fecha
            if (pVenta.FechaVenta.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pVenta.FechaVenta.Year, pVenta.FechaVenta.Month, pVenta.FechaVenta.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaVenta >= fechaInicial && s.FechaVenta <= fechaFinal);
            }
            //if (!string.IsNullOrWhiteSpace(pFactura.FechaFacturacion.ToString()))
            //    pQuery = pQuery.Where(s => s.FechaFacturacion.ToString().Contains(pFactura.FechaFacturacion.ToString()));

            //if (pFactura.Cantidad > 0)
            //    pQuery = pQuery.Where(s => s.Cantidad == pFactura.Cantidad);
            //if (!string.IsNullOrWhiteSpace(pFactura.Descripcion))
            //    pQuery = pQuery.Where(s => s.Descripcion.Contains(pFactura.Descripcion));
            //if (!string.IsNullOrWhiteSpace(pFactura.Direccion))
            //    pQuery = pQuery.Where(s => s.Direccion.Contains(pFactura.Direccion));
            //if (!string.IsNullOrWhiteSpace(pFactura.Telefono))
            //    pQuery = pQuery.Where(s => s.Telefono.Contains(pFactura.Telefono));
            //if (!string.IsNullOrWhiteSpace(pFactura.Correo))
            //    pQuery = pQuery.Where(s => s.Correo.Contains(pFactura.Correo));

            //if (pFactura.Total > 0)
            //    pQuery = pQuery.Where(s => s.Total == pFactura.Total);
            //if (pFactura.Descuento > 0)
            //    pQuery = pQuery.Where(s => s.Descuento == pFactura.Descuento);
            //if (pFactura.Impuesto > 0)
            //    pQuery = pQuery.Where(s => s.Impuesto == pFactura.Impuesto);
            //if (pFactura.TotalPagado > 0)
            //    pQuery = pQuery.Where(s => s.TotalPagado == pFactura.TotalPagado);

            pQuery = pQuery.OrderByDescending(s => s.IdVenta).AsQueryable();
            if (pVenta.Top_Aux > 0)
                pQuery = pQuery.Take(pVenta.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Venta>> BuscarAsync(Venta pVenta)
        {
            var Ventas = new List<Venta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Venta.AsQueryable();
                select = QuerySelect(select, pVenta);
                Ventas = await select.ToListAsync();
            }
            return Ventas;
        }

        public static async Task<List<Venta>> BuscarIncluirUsuarioAsync(Venta pVenta)
        {
            var Ventas = new List<Venta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Venta.AsQueryable();
                select = QuerySelect(select, pVenta).Include(s => s.Usuario).AsQueryable();
                Ventas = await select.ToListAsync();
            }
            return Ventas;
        }
    }
}
