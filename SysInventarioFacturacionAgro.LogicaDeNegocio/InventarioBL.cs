using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class InventarioBL
    {
        public async Task<int> CrearAsync(Inventario pInventario)
        {
            return await InventarioDAL.CrearAsync(pInventario);
        }
        public async Task<int> ModificarAsync(Inventario pInventario)
        {
            return await InventarioDAL.ModificarAsync(pInventario);
        }
        public async Task<int> EliminarAsync(Inventario pInventario)
        {
            return await InventarioDAL.EliminarAsync(pInventario);
        }
        public async Task<Inventario> ObtenerPorIdAsync(Inventario pInventario)
        {
            return await InventarioDAL.ObtenerPorIdAsync(pInventario);
        }
        public async Task<List<Inventario>> ObtenerTodosAsync()
        {
            return await InventarioDAL.ObtenerTodosAsync();
        }
        public async Task<List<Inventario>> BuscarAsync(Inventario pInventario)
        {
            return await InventarioDAL.BuscarAsync(pInventario);
        }

        public async Task<List<Inventario>> BuscarIncluirProductoYUsuarioAsync(Inventario pInventario)
        {
            return await InventarioDAL.BuscarIncluirProductoYUsuarioAsync(pInventario);
        }
    }
}

