using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class InventarioDAL
    {
        public static async Task<int> CrearAsync(Inventario pInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {

                bdContexto.Add(pInventario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Inventario pInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var inventario = await bdContexto.Inventario.FirstOrDefaultAsync(s => s.IdInventario == pInventario.IdInventario);
                inventario.IdProducto = pInventario.IdProducto;
                inventario.IdUsuario = pInventario.IdUsuario;
                inventario.Cantidad = pInventario.Cantidad;
                inventario.Diferencia = pInventario.Diferencia;
                inventario.Detalles = pInventario.Detalles;
                bdContexto.Update(inventario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Inventario pInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var inventario = await bdContexto.Inventario.FirstOrDefaultAsync(s => s.IdInventario == pInventario.IdInventario);
                bdContexto.Inventario.Remove(inventario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Inventario> ObtenerPorIdAsync(Inventario pInventario)
        {
            var inventario = new Inventario();
            using (var bdContexto = new BDContexto())
            {
                inventario = await bdContexto.Inventario.FirstOrDefaultAsync(s => s.IdInventario == pInventario.IdInventario);
            }
            return inventario;
        }
        public static async Task<List<Inventario>> ObtenerTodosAsync()
        {
            var inventario = new List<Inventario>();
            using (var bdContexto = new BDContexto())
            {
                inventario = await bdContexto.Inventario.Include(p => p.Producto).ToListAsync();
            }
            return inventario;
        }
        internal static IQueryable<Inventario> QuerySelect(IQueryable<Inventario> pQuery, Inventario pInventario)
        {
            if (pInventario.IdInventario > 0)
                pQuery = pQuery.Where(s => s.IdInventario == pInventario.IdInventario);
            if (pInventario.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pInventario.IdUsuario);
            if (pInventario.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pInventario.IdProducto);



            pQuery = pQuery.OrderByDescending(s => s.IdInventario).AsQueryable();
            if (pInventario.Top_Aux > 0)
                pQuery = pQuery.Take(pInventario.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Inventario>> BuscarAsync(Inventario pInventario)
        {
            var Inventarios = new List<Inventario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Inventario.AsQueryable();
                select = QuerySelect(select, pInventario);
                Inventarios = await select.ToListAsync();
            }
            return Inventarios;
        }

        public static async Task<List<Inventario>> BuscarIncluirProductoYUsuarioAsync(Inventario pInventario)
        {
            var Inventarios = new List<Inventario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Inventario.AsQueryable();
                select = QuerySelect(select, pInventario).Include(s => s.Usuario).Include(s => s.Producto).AsQueryable();
                Inventarios = await select.ToListAsync();
            }
            return Inventarios;
        }
    }
}
