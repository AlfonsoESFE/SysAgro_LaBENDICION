﻿using Microsoft.AspNetCore.Http;
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
    public class CategoriaController : Controller
    {
        CategoriaBL CategoriaBL = new CategoriaBL();
        // GET: CategoriaController
        public async Task<IActionResult> Index(Categoria? pCategoria = null)
        {
            if (pCategoria == null)
                pCategoria = new Categoria();
            if (pCategoria.Top_Aux == 0)
                pCategoria.Top_Aux = 10;
            else if (pCategoria.Top_Aux == -1)
                pCategoria.Top_Aux = 0;
            var Categoria = await CategoriaBL.BuscarAsync(pCategoria);
            ViewBag.Top = pCategoria.Top_Aux;
            return View(Categoria);
        }

        // GET: CategoriaController/Details/5
        public async Task<IActionResult> Details(int IdCategoria)
        {
            var Categoria = await CategoriaBL.ObtenerPorIdCategoriaAsync(new Categoria { IdCategoria = IdCategoria });
            return View(Categoria);
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
        public async Task<IActionResult> Create(Categoria pCategoria)
        {
            try
            {
                int result = await CategoriaBL.CrearAsync(pCategoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCategoria);
            }
        }

        // GET: CategoriaController/Edit/5
        public async Task<IActionResult> Edit(Categoria pCategoria)
        {
            var Categoria = await CategoriaBL.ObtenerPorIdCategoriaAsync(pCategoria);
            ViewBag.Error = "";
            return View(Categoria);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdCategoria, Categoria pCategoria)
        {
            try
            {
                int result = await CategoriaBL.ModificarAsync(pCategoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCategoria);
            }
        }

        // GET: CategoriaController/Delete/5
        public async Task<IActionResult> Delete(Categoria pCategoria)
        {
            var Categoria = await CategoriaBL.ObtenerPorIdCategoriaAsync(pCategoria);
            return View(Categoria);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdCategoria, Categoria pCategoria)
        {
            try
            {
                int result = await CategoriaBL.EliminarAsync(pCategoria);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCategoria);
            }
        }
    }
}
