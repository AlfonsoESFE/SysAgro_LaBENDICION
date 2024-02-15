using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class DetallesCompraBL
    {
        public async Task<int> CrearAsync(DetallesCompra pDetalleCompra)
        {
            return await DetallesCompraDAL.CrearAsync(pDetalleCompra);
        }
        public async Task<int> ModificarAsync(DetallesCompra pDetalleCompra)
        {
            return await DetallesCompraDAL.ModificarAsync(pDetalleCompra);
        }
        public async Task<int> EliminarAsync(DetallesCompra pDetalleCompra)
        {
            return await DetallesCompraDAL.EliminarAsync(pDetalleCompra);
        }
        public async Task<DetallesCompra> ObtenerPorIdAsync(DetallesCompra pDetalleCompra)
        {
            return await DetallesCompraDAL.ObtenerPorIdAsync(pDetalleCompra);
        }
        public async Task<List<DetallesCompra>> ObtenerTodosAsync()
        {
            return await DetallesCompraDAL.ObtenerTodosAsync();
        }
        public async Task<List<DetallesCompra>> BuscarAsync(DetallesCompra pDetalleCompra)
        {
            return await DetallesCompraDAL.BuscarAsync(pDetalleCompra);
        }

        public async Task<List<DetallesCompra>> BuscarIncluirCompraProductoAsync(DetallesCompra pDetalleCompra)
        {
            return await DetallesCompraDAL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
        }
    }
}