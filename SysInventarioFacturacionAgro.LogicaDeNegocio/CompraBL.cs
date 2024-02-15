using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class CompraBL
    {
        public async Task<int> CrearAsync(Compra pCompra)
        {
            return await CompraDAL.CrearAsync(pCompra);
        }
        public async Task<int> ModificarAsync(Compra pCompra)
        {
            return await CompraDAL.ModificarAsync(pCompra);
        }
        public async Task<int> EliminarAsync(Compra pCompra)
        {
            return await CompraDAL.EliminarAsync(pCompra);
        }
        public async Task<Compra> ObtenerPorIdAsync(Compra pCompra)
        {
            return await CompraDAL.ObtenerPorIdAsync(pCompra);
        }
        public async Task<List<Compra>> ObtenerTodosAsync()
        {
            return await CompraDAL.ObtenerTodosAsync();
        }
        public async Task<List<Compra>> BuscarAsync(Compra pCompra)
        {
            return await CompraDAL.BuscarAsync(pCompra);
        }

        public async Task<List<Compra>> BuscarIncluirProveedoryUsuarioAsync(Compra pCompra)
        {
            return await CompraDAL.BuscarIncluirProveedoryUsuarioAsync(pCompra);
        }

    }
}
