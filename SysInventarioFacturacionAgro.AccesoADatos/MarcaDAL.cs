using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class MarcaDAL
    {
        public static async Task<int> CrearAsync(Marca pMarca)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pMarca);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Marca pMarca)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Marca = await bdContexto.Marca.FirstOrDefaultAsync(m => m.IdMarca == pMarca.IdMarca);
                Marca.Nombre = pMarca.Nombre;
                bdContexto.Update(Marca);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Marca pMarca)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Marca = await bdContexto.Marca.FirstOrDefaultAsync(m => m.IdMarca == pMarca.IdMarca);
                bdContexto.Marca.Remove(Marca);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Marca> ObtenerPorIdAsync(Marca pMarca)
        {
            var Marca = new Marca();
            using (var bdContexto = new BDContexto())
            {
                Marca = await bdContexto.Marca.FirstOrDefaultAsync(m => m.IdMarca == pMarca.IdMarca);
            }
            return Marca;
        }
        public static async Task<List<Marca>> ObtenerTodosAsync()
        {
            var Marcas = new List<Marca>();
            using (var bdContexto = new BDContexto())
            {
                Marcas = await bdContexto.Marca.ToListAsync();
            }
            return Marcas;
        }
        internal static IQueryable<Marca> QuerySelect(IQueryable<Marca> pQuery, Marca pMarca)
        {
            if (pMarca.IdMarca > 0)
                pQuery = pQuery.Where(m => m.IdMarca == pMarca.IdMarca);
            if (!string.IsNullOrWhiteSpace(pMarca.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pMarca.Nombre));
            pQuery = pQuery.OrderByDescending(m => m.IdMarca).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Marca>> BuscarAsync(Marca pMarca)
        {
            var Marcas = new List<Marca>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Marca.AsQueryable();
                select = QuerySelect(select, pMarca);
                Marcas = await select.ToListAsync();
            }
            return Marcas;
        }
    }
}
