using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/********************************/
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SysInventarioFacturacion.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "SuperAdmin")]
    public class MarcaController : Controller
    {
        MarcaBL MarcaBL = new MarcaBL();
        // GET: CategoriaController
        public async Task<IActionResult> Index(Marca? pMarca = null)
        {
            if (pMarca == null)
                pMarca = new Marca();
            var Marca = await MarcaBL.BuscarAsync(pMarca);
            return View(Marca);
        }

        // GET: CategoriaController/Details/5
        public async Task<IActionResult> Details(int IdMarca)
        {
            var Marca = await MarcaBL.ObtenerPorIdAsync(new Marca { IdMarca = IdMarca });
            return View(Marca);
        }

        // GET: CategoriaController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Marca pMarca)
        {
            try
            {
                int result = await MarcaBL.CrearAsync(pMarca);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMarca);
            }
        }

        // GET: CategoriaController/Edit/5
        public async Task<IActionResult> Edit(Marca pMarca)
        {
            var Marca = await MarcaBL.ObtenerPorIdAsync(pMarca);
            ViewBag.Error = "";
            return View(Marca);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdMarca, Marca pMarca)
        {
            try
            {
                int result = await MarcaBL.ModificarAsync(pMarca);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMarca);
            }
        }

        // GET: CategoriaController/Delete/5
        public async Task<IActionResult> Delete(Marca pMarca)
        {
            var Marca = await MarcaBL.ObtenerPorIdAsync(pMarca);
            return View(Marca);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdMarca, Marca pMarca)
        {
            try
            {
                int result = await MarcaBL.EliminarAsync(pMarca);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMarca);
            }
        }
    }
}

