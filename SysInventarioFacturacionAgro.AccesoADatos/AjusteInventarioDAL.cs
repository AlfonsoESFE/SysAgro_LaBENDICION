using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class AjusteInventarioDAL
    {
        #region CRUD
        public static async Task<int> CrearAsync(AjusteInventario pAjusteInventario)
        {
            
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    bdContexto.Add(pAjusteInventario);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
        }
        public static async Task<int> ModificarAsync(AjusteInventario pAjusteInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var AjusteInventario = await bdContexto.AjusteInventario.FirstOrDefaultAsync(ai => ai.IdAjusteInventario == pAjusteInventario.IdAjusteInventario);
                AjusteInventario.IdUsuario = pAjusteInventario.IdUsuario;
                AjusteInventario.IdProducto = pAjusteInventario.IdProducto;
                AjusteInventario.IdAjusteInventario = pAjusteInventario.IdAjusteInventario;
                AjusteInventario.TipoAjuste = pAjusteInventario.TipoAjuste;
                AjusteInventario.FechaAjuste = pAjusteInventario.FechaAjuste;
                AjusteInventario.Detalles = pAjusteInventario.Detalles;
                bdContexto.Update(AjusteInventario);
                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> EliminarAsync(AjusteInventario pAjusteInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var AjusteInventario = await bdContexto.AjusteInventario.FirstOrDefaultAsync(ai => ai.IdAjusteInventario == pAjusteInventario.IdAjusteInventario);

                if (AjusteInventario != null)
                {
                    bdContexto.AjusteInventario.Remove(AjusteInventario);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }
        #endregion

        public static async Task<AjusteInventario> ObtenerPorIdAsync(AjusteInventario pAjusteInventario)
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.AjusteInventario.FirstOrDefaultAsync(ai => ai.IdAjusteInventario == pAjusteInventario.IdAjusteInventario);
            }
        }

        public static async Task<List<AjusteInventario>> ObtenerTodosAsync()
        {
            var AjustesInventario = new List<AjusteInventario>();
            using (var bdContexto = new BDContexto())
            {
                AjustesInventario = await bdContexto.AjusteInventario.ToListAsync();
            }
            return AjustesInventario;
        }

        private static IQueryable<AjusteInventario> QuerySelect(IQueryable<AjusteInventario> pQuery, AjusteInventario pAjusteInventario)
        {

            if (pAjusteInventario.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pAjusteInventario.IdUsuario);
            if (pAjusteInventario.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pAjusteInventario.IdProducto);
            if (!string.IsNullOrWhiteSpace(pAjusteInventario.TipoAjuste))
                pQuery = pQuery.Where(s => s.TipoAjuste.Contains(pAjusteInventario.TipoAjuste));
            return pQuery;
        }

        public static async Task<List<AjusteInventario>> BuscarAsync(AjusteInventario pAjusteInventario)
        {
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.AjusteInventario.AsQueryable();
                select = QuerySelect(select, pAjusteInventario);
                return await select.ToListAsync();
            }
        }

        public static async Task<List<AjusteInventario>> BuscarIncluirUsuarioyProductoAsync(AjusteInventario pAjusteInventario)
        {
            var AjustesInventario = new List<AjusteInventario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.AjusteInventario.AsQueryable();
                select = QuerySelect(select, pAjusteInventario).Include(u => u.Usuario).Include(pd => pd.Producto).AsQueryable();
                AjustesInventario = await select.ToListAsync();
            }
            return AjustesInventario;
        }
    }
}