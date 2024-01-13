using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class ReporteBL
    {

        public async Task<int> CrearAsync(Reporte pReporte)
        {
            return await ReporteDAL.CrearAsync(pReporte);
        }
        public async Task<int> ModificarAsync(Reporte pReporte)
        {
            return await ReporteDAL.ModificarAsync(pReporte);
        }
        public async Task<int> EliminarAsync(Reporte pReporte)
        {
            return await ReporteDAL.EliminarAsync(pReporte);
        }
        public async Task<Reporte> ObtenerPorIdAsync(Reporte pReporte)
        {
            return await ReporteDAL.ObtenerPorIdAsync(pReporte);
        }
        public async Task<List<Reporte>> ObtenerTodosAsync()
        {
            return await ReporteDAL.ObtenerTodosAsync();
        }
        public async Task<List<Reporte>> BuscarAsync(Reporte pReporte)
        {
            return await ReporteDAL.BuscarAsync(pReporte);
        }

        public async Task<List<Reporte>> BuscarIncluirUsuarioAsync(Reporte pReporte)
        {
            return await ReporteDAL.BuscarIncluirUsuarioAsync(pReporte);
        }

    }
}
