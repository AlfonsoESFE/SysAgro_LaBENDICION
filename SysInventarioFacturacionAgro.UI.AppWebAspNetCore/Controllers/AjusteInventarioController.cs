using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/********************************/
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class AjusteInventarioController : Controller
    {
        AjusteInventarioBL AjusteInventarioBL = new AjusteInventarioBL();
        UsuarioBL UsuarioBL = new UsuarioBL();
        ProductoBL ProductoBL = new ProductoBL();

        // GET: AjusteInventarioController
        public async Task<IActionResult> Index(AjusteInventario? pAjusteInventario = null)
        {
            if (pAjusteInventario == null)
                pAjusteInventario = new AjusteInventario();
            // Puedes seguir el patrón similar a tu controlador original, ajustando nombres según sea necesario
            var taskBuscar = AjusteInventarioBL.BuscarIncluirUsuarioyProductoAsync(pAjusteInventario);
            var taskObtenerTodosUsuario = UsuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var AjustesInventario = await taskBuscar;
            ViewBag.Usuario = await taskObtenerTodosUsuario;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(AjustesInventario);
        }

        // GET: AjusteInventarioController/Details/5
        public async Task<IActionResult> Details(int IdAjusteInventario)
        {
            var ajusteInventario = await AjusteInventarioBL.ObtenerPorIdAsync(new AjusteInventario { IdAjusteInventario = IdAjusteInventario });
            return View(ajusteInventario);
        }

        // GET: AjusteInventarioController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: AjusteInventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AjusteInventario pAjusteInventario)
        {
            try
            {
                int result = await AjusteInventarioBL.CrearAsync(pAjusteInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pAjusteInventario);
            }
        }

        // GET: AjusteInventarioController/Edit/5
        public async Task<IActionResult> Edit(AjusteInventario pAjusteInventario)
        {
            var taskObtenerPorId = AjusteInventarioBL.ObtenerPorIdAsync(pAjusteInventario);
            var taskObtenerTodosUsuario = UsuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var ajusteInventario = await taskObtenerPorId;
            ViewBag.Usuario = await taskObtenerTodosUsuario;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(ajusteInventario);
        }

        // POST: AjusteInventarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdAjusteInventario, AjusteInventario pAjusteInventario)
        {
            try
            {
                int result = await AjusteInventarioBL.ModificarAsync(pAjusteInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pAjusteInventario);
            }
        }

        // GET: AjusteInventarioController/Delete/5
        public async Task<IActionResult> Delete(AjusteInventario pAjusteInventario)
        {
            var ajusteInventario = await AjusteInventarioBL.ObtenerPorIdAsync(pAjusteInventario);
            return View(ajusteInventario);
        }

        // POST: AjusteInventarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdAjusteInventario, AjusteInventario pAjusteInventario)
        {
            try
            {
                int result = await AjusteInventarioBL.EliminarAsync(pAjusteInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var ajusteInventario = await AjusteInventarioBL.ObtenerPorIdAsync(pAjusteInventario);
                if (ajusteInventario == null)
                    ajusteInventario = new AjusteInventario();
                return View(ajusteInventario);
            }
        }
    }
}
