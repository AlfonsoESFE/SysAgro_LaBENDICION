using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class FacturaDAL
    {
        public static async Task<int> CrearAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                pFactura.FechaFacturacion = DateTime.Now;
                bdContexto.Add(pFactura);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
                Factura.IdUsuario = pFactura.IdUsuario;
                Factura.NumeroFactura = pFactura.NumeroFactura;
                Factura.FormaPago = pFactura.FormaPago;
                Factura.Descuento = pFactura.Descuento;
                Factura.Impuesto = pFactura.Impuesto;
                Factura.FacturaTotal = pFactura.FacturaTotal;
                Factura.FacturaPagado = pFactura.FacturaPagado;
                Factura.FacturaCambio = pFactura.FacturaCambio;
                Factura.NombreCliente = pFactura.NombreCliente;
                Factura.DireccionCliente = pFactura.DireccionCliente;
                Factura.TelefonoCliente = pFactura.TelefonoCliente;
                Factura.DUICliente = pFactura.DUICliente;
                bdContexto.Update(Factura);
                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> EliminarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);

                if (Factura != null)
                {
                    bdContexto.Factura.Remove(Factura);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<Factura> ObtenerPorIdAsync(Factura pFactura)
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
            }
        }

        public static async Task<List<Factura>> ObtenerTodosAsync()
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Factura.Include(f => f.DetallesFactura).ThenInclude(df => df.Producto).ToListAsync();
            }
        }

        private static IQueryable<Factura> QuerySelect(IQueryable<Factura> pQuery, Factura pFactura)
        {
            // Lógica de filtrado
            return pQuery;
        }

        public static async Task<List<Factura>> BuscarAsync(Factura pFactura)
        {
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura);
                return await select.ToListAsync();
            }
        }

        public static async Task<List<Factura>> BuscarIncluirUsuarioAsync(Factura pFactura)
        {
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura).Include(s => s.Usuario).AsQueryable();
                return await select.ToListAsync();
            }
        }
    }
}
