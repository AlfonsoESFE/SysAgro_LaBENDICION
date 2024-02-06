using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class DetalleVentaBL
    {
        public async Task<int> CrearAsync(DetalleVenta pDetalleVenta)
        {
            return await DetalleVentaDAL.CrearAsync(pDetalleVenta);
        }
        public async Task<int> ModificarAsync(DetalleVenta pDetalleVenta)
        {
            return await DetalleVentaDAL.ModificarAsync(pDetalleVenta);
        }
        public async Task<int> EliminarAsync(DetalleVenta pDetalleVenta)
        {
            return await DetalleVentaDAL.EliminarAsync(pDetalleVenta);
        }
        public async Task<DetalleVenta> ObtenerPorIdAsync(DetalleVenta pDetalleVenta)
        {
            return await DetalleVentaDAL.ObtenerPorIdAsync(pDetalleVenta);
        }
        public async Task<List<DetalleVenta>> ObtenerTodosAsync()
        {
            return await DetalleVentaDAL.ObtenerTodosAsync();
        }
        public async Task<List<DetalleVenta>> BuscarAsync(DetalleVenta pDetalleVenta)
        {
            return await DetalleVentaDAL.BuscarAsync(pDetalleVenta);
        }

        public async Task<List<DetalleVenta>> BuscarIncluirVentaProductoAsync(DetalleVenta pDetalleVenta)
        {
            return await DetalleVentaDAL.BuscarIncluirVentaProductoAsync(pDetalleVenta);
        }
    }
}
