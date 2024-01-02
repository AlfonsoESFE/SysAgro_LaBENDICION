using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class DetallesFacturaBL
    {
        public async Task<int> CrearAsync(DetallesFactura pDetallesFactura)
        {
            return await DetallesFacturaDAL.CrearAsync(pDetallesFactura);
        }

        public async Task<int> ModificarAsync(DetallesFactura pDetallesFactura)
        {
            return await DetallesFacturaDAL.ModificarAsync(pDetallesFactura);
        }

        public async Task<int> EliminarAsync(DetallesFactura pDetallesFactura)
        {
            return await DetallesFacturaDAL.EliminarAsync(pDetallesFactura);
        }

        public async Task<DetallesFactura> ObtenerPorIdAsync(DetallesFactura pDetallesFactura)
        {
            return await DetallesFacturaDAL.ObtenerPorIdAsync(pDetallesFactura);
        }

        public async Task<List<DetallesFactura>> ObtenerTodosAsync()
        {
            return await DetallesFacturaDAL.ObtenerTodosAsync();
        }

        public async Task<List<DetallesFactura>> BuscarAsync(DetallesFactura pDetallesFactura)
        {
            return await DetallesFacturaDAL.BuscarAsync(pDetallesFactura);
        }

        public async Task<List<DetallesFactura>> BuscarIncluirFacturasYProductoAsync(DetallesFactura pDetallesFactura)
        {
            return await DetallesFacturaDAL.BuscarIncluirFacturasYProductoAsync(pDetallesFactura);
        }
    }
}
