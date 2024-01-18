using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.LogicaDeNegocio;

namespace SysAgroservicio.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "Cajero,SuperAdmin")]

    public class VentaController : Controller
    {
        VentaBL VentaBL = new VentaBL();
        UsuarioBL UsuarioBL = new UsuarioBL();

        // GET: VentaController
        public async Task<IActionResult> Index(Venta pVenta = null)
        {
            if (pVenta == null)
                pVenta = new Venta();
            var taskBuscar = VentaBL.BuscarIncluirUsuarioAsync(pVenta);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var Ventas = await taskBuscar;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(Ventas);
        }

        // GET: VentaController/Details/5
        public async Task<IActionResult> Details(int IdVenta)
        {
            var venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = IdVenta });
            venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = venta.IdUsuario });
            return View(venta);
        }

        // GET: VentaController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: VentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta pVenta)
        {
            try
            {
                int result = await VentaBL.CrearAsync(pVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pVenta);
            }
        }

        // GET: VentaController/Edit/5
        public async Task<IActionResult> Edit(Venta pVenta)
        {
            var taskObtenerPorId = VentaBL.ObtenerPorIdAsync(pVenta);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var venta = await taskObtenerPorId;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(venta);
        }

        // POST: VentaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdVenta, Venta pVenta)
        {
            try
            {
                int result = await VentaBL.ModificarAsync(pVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pVenta);
            }
        }

        // GET: VentaController/Delete/5
        //public async Task<IActionResult> Delete(Venta pVenta)
        //{
        //    var Venta = await VentaBL.ObtenerPorIdAsync(pVenta);
        //    Venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Venta.IdUsuario });
        //    ViewBag.Error = "";

        //    return View(Venta);
        //}

        // POST: VentaController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int IdVenta, Venta pVenta)
        //{
        //    try
        //    {
        //        int result = await VentaBL.EliminarAsync(pVenta);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        var venta = await VentaBL.ObtenerPorIdAsync(pVenta);
        //        if (venta == null)
        //            venta = new Venta();
        //        if (venta.IdVenta > 0)
        //            venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = venta.IdUsuario });
        //        return View(venta);
        //    }
        //}

        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(Venta pVenta)
        {
            var Venta = await VentaBL.ObtenerPorIdAsync(pVenta);
            Venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Venta.IdUsuario });
            ViewBag.Error = "";
            return View(Venta);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdVenta, Venta pVenta)
        {
            try
            {
                int result = await VentaBL.EliminarAsync(pVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var venta = await VentaBL.ObtenerPorIdAsync(pVenta);
                if (venta == null)
                    venta = new Venta();
                if (venta.IdVenta > 0)
                    venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = venta.IdUsuario });
                
                return View(venta);
            }
        }
    }
}
