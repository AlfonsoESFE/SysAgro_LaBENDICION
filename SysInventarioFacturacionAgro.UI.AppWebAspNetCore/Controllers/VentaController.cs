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
using SysInventarioFacturacionAgro.AccesoADatos;

namespace SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Controllers
{
    
    public class VentaController : Controller
    {
        VentaBL VentaBL = new VentaBL();
        UsuarioBL UsuarioBL = new UsuarioBL();
        // GET: FacturaController
        public async Task<IActionResult> Index(Venta pVenta = null)
        {
            if (pVenta == null)
                pVenta = new Venta();
            if (pVenta.Top_Aux == 0)
                pVenta.Top_Aux = 10;
            else if (pVenta.Top_Aux == -1)
                pVenta.Top_Aux = 0;
            var taskBuscar = VentaBL.BuscarIncluirUsuarioAsync(pVenta);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var Ventas = await taskBuscar;
            ViewBag.Top = pVenta.Top_Aux;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(Ventas);
        }

        // GET: FacturaController/Details/5
        public async Task<IActionResult> Details(int IdVenta)
        {
            var venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = IdVenta });
            venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = venta.IdUsuario });
            return View(venta);
        }

        // GET: FacturaController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        //POST: FacturaController/Create

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



        // GET: FacturaController/Edit/5
        public async Task<IActionResult> Edit(Venta pVenta)
        {
            var taskObtenerPorId = VentaBL.ObtenerPorIdAsync(pVenta);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var venta = await taskObtenerPorId;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(venta);
        }

        // POST: FacturaController/Edit/5
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

        // GET: FacturaController/Delete/5
        public async Task<IActionResult> Delete(Venta pVenta)
        {
            var Venta = await VentaBL.ObtenerPorIdAsync(pVenta);
            Venta.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Venta.IdUsuario });
            ViewBag.Error = "";

            return View(Venta);
        }

        // POST: FacturaController/Delete/5
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
