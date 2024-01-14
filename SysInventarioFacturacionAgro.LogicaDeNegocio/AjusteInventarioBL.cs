using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class AjusteInventarioBL
    {

        public async Task<int> CrearAsync(AjusteInventario pAjusteInventario)
        {
            return await AjusteInventarioDAL.CrearAsync(pAjusteInventario);
        }
        public async Task<int> ModificarAsync(AjusteInventario pAjusteInventario)
        {
            return await AjusteInventarioDAL.ModificarAsync(pAjusteInventario);
        }
        public async Task<int> EliminarAsync(AjusteInventario pAjusteInventario)
        {
            return await AjusteInventarioDAL.EliminarAsync(pAjusteInventario);
        }
        public async Task<AjusteInventario> ObtenerPorIdAsync(AjusteInventario pAjusteInventario)
        {
            return await AjusteInventarioDAL.ObtenerPorIdAsync(pAjusteInventario);
        }
        public async Task<List<AjusteInventario>> ObtenerTodosAsync()
        {
            return await AjusteInventarioDAL.ObtenerTodosAsync();
        }
        public async Task<List<AjusteInventario>> BuscarAsync(AjusteInventario pAjusteInventario)
        {
            return await AjusteInventarioDAL.BuscarAsync(pAjusteInventario);
        }

        public async Task<List<AjusteInventario>> BuscarIncluirUsuarioyProductoAsync(AjusteInventario pAjusteInventario)
        {
            return await AjusteInventarioDAL.BuscarIncluirUsuarioyProductoAsync(pAjusteInventario);
        }

    }
}
