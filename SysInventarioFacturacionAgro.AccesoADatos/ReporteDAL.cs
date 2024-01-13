using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.AccesoADatos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class ReporteDAL
    {
        public static async Task<int> CrearAsync(Reporte pReporte)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pReporte);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Reporte pReporte)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var reporte = await bdContexto.Reporte.FirstOrDefaultAsync(s => s.IdReporte == pReporte.IdReporte);
                reporte.IdUsuario = pReporte.IdUsuario;
                reporte.TipoReporte = pReporte.TipoReporte;
                reporte.FechaReporte = pReporte.FechaReporte;
                reporte.Detalles = pReporte.Detalles;
                bdContexto.Update(reporte);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Reporte pReporte)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var reporte = await bdContexto.Reporte.FirstOrDefaultAsync(s => s.IdReporte == pReporte.IdReporte);
                bdContexto.Reporte.Remove(reporte);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Reporte> ObtenerPorIdAsync(Reporte pReporte)
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Reporte.FirstOrDefaultAsync(s => s.IdReporte == pReporte.IdReporte);
            }
        }

        public static async Task<List<Reporte>> ObtenerTodosAsync()
        {
            var reportes = new List<Reporte>();
            using (var bdContexto = new BDContexto())
            {
                reportes = await bdContexto.Reporte.ToListAsync();
            }
            return reportes;
        }

        internal static IQueryable<Reporte> QuerySelect(IQueryable<Reporte> pQuery, Reporte pReporte)
        {
            if (pReporte.IdReporte > 0)
                pQuery = pQuery.Where(s => s.IdReporte == pReporte.IdReporte);
            if (pReporte.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pReporte.IdUsuario);
            pQuery = pQuery.OrderByDescending(s => s.IdReporte).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Reporte>> BuscarAsync(Reporte pReporte)
        {
            var reportes = new List<Reporte>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Reporte.AsQueryable();
                select = QuerySelect(select, pReporte);
                reportes = await select.ToListAsync();
            }
            return reportes;
        }

        public static async Task<List<Reporte>> BuscarIncluirUsuarioAsync(Reporte pReporte)
        {
            var reportes = new List<Reporte>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Reporte.AsQueryable();
                select = QuerySelect(select, pReporte).Include(s => s.Usuario).AsQueryable();
                reportes = await select.ToListAsync();
            }
            return reportes;
        }
    }
}
