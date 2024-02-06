using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class VentaBL
    {
        public async Task<int> CrearAsync(Venta pVenta)
        {
            return await VentaDAL.CrearAsync(pVenta);
        }
        public async Task<int> ModificarAsync(Venta pVenta)
        {
            return await VentaDAL.ModificarAsync(pVenta);
        }
        public async Task<int> EliminarAsync(Venta pVenta)
        {
            return await VentaDAL.EliminarAsync(pVenta);
        }
        public async Task<Venta> ObtenerPorIdAsync(Venta pVenta)
        {
            return await VentaDAL.ObtenerPorIdAsync(pVenta);
        }
        public async Task<List<Venta>> ObtenerTodosAsync()
        {
            return await VentaDAL.ObtenerTodosAsync();
        }
        public async Task<List<Venta>> BuscarAsync(Venta pVenta)
        {
            return await VentaDAL.BuscarAsync(pVenta);
        }

        public async Task<List<Venta>> BuscarIncluirUsuarioAsync(Venta pVenta)
        {
            return await VentaDAL.BuscarIncluirUsuarioAsync(pVenta);
        }

    }
}
