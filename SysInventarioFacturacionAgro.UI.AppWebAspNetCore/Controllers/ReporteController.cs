using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/********************************/
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.LogicaDeNegocio;

namespace SysAgroservicio.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Cajero,SuperAdmin")]

    public class ReporteController : Controller
    {
        ReporteBL ReporteBL = new ReporteBL();
        UsuarioBL UsuarioBL = new UsuarioBL();
        // GET: FacturaController
        public async Task<IActionResult> Index(Reporte pReporte = null)
        {
            if (pReporte == null)
                pReporte = new Reporte();
            var taskBuscar = ReporteBL.BuscarIncluirUsuarioAsync(pReporte);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var Reportes = await taskBuscar;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(Reportes);
        }

        // GET: FacturaController/Details/5
        public async Task<IActionResult> Details(int IdReporte)
        {
            var reporte = await ReporteBL.ObtenerPorIdAsync(new Reporte { IdReporte = IdReporte });
            reporte.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = reporte.IdUsuario });
            return View(reporte);
        }

        // GET: FacturaController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: FacturaController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reporte pReporte)
        {
            try
            {
                int result = await ReporteBL.CrearAsync(pReporte);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pReporte);
            }
        }

        // GET: FacturaController/Edit/5
        public async Task<IActionResult> Edit(Reporte pReporte)
        {
            var taskObtenerPorId = ReporteBL.ObtenerPorIdAsync(pReporte);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var reporte = await taskObtenerPorId;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(reporte);
        }

        // POST: FacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdReporte, Reporte pReporte)
        {
            try
            {
                int result = await ReporteBL.ModificarAsync(pReporte);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pReporte);
            }
        }

        // GET: FacturaController/Delete/5
        public async Task<IActionResult> Delete(Reporte pReporte)
        {
            var Reporte = await ReporteBL.ObtenerPorIdAsync(pReporte);
            Reporte.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Reporte.IdUsuario });
            ViewBag.Error = "";

            return View(Reporte);
        }

        // POST: FacturaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdReporte, Reporte pReporte)
        {
            try
            {
                int result = await ReporteBL.EliminarAsync(pReporte);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var reporte = await ReporteBL.ObtenerPorIdAsync(pReporte);
                if (reporte == null)
                    reporte = new Reporte();
                if (reporte.IdReporte > 0)
                    reporte.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = reporte.IdUsuario });
                return View(reporte);
            }
        }
    }
}
