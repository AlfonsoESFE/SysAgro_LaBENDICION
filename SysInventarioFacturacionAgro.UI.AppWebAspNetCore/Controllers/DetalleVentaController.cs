using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysInventarioFacturacionAgro.AccesoADatos;

namespace SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Cajero,SuperAdmin,Admin")]
    public class DetalleVentaController : Controller
    {
        DetalleVentaBL DetalleVentaBL = new DetalleVentaBL();
        VentaBL VentaBL = new VentaBL();
        ProductoBL ProductoBL = new ProductoBL();
        public static int idFac;

        public async Task<IActionResult> Index(DetalleVenta pDetalleVenta = null)
        {
            if (pDetalleVenta == null)
                pDetalleVenta = new DetalleVenta();
            var taskBuscar = DetalleVentaBL.BuscarIncluirVentasYProductoAsync(pDetalleVenta);
            var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var detallesVentas = await taskBuscar;
            ViewBag.Ventas = await taskObtenerTodosVentas;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(detallesVentas);
        }

        public async Task<IActionResult> Details(int IdDetalleVenta)
        {
            var detalleVenta = await DetalleVentaBL.ObtenerPorIdAsync(new DetalleVenta { IdDetalleVenta = IdDetalleVenta });
            detalleVenta.Venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = detalleVenta.IdVenta });
            detalleVenta.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = detalleVenta.IdProducto });
            return View(detalleVenta);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ventas = await VentaBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.CrearAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Ventas = await VentaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleVenta);
            }
        }

        public async Task<IActionResult> Edit(DetalleVenta pDetalleVenta)
        {
            var taskObtenerPorIdDetalleVenta = DetalleVentaBL.ObtenerPorIdAsync(pDetalleVenta);
            var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var detalleVenta = await taskObtenerPorIdDetalleVenta;
            ViewBag.Ventas = await taskObtenerTodosVentas;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(detalleVenta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdDetalleVenta, DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.ModificarAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Ventas = await VentaBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleVenta);
            }
        }

        public async Task<IActionResult> Delete(int IdDetalleVenta)
        {
            var detalleVenta = await DetalleVentaBL.ObtenerPorIdAsync(new DetalleVenta { IdDetalleVenta = IdDetalleVenta });
            detalleVenta.Venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = detalleVenta.IdVenta });
            detalleVenta.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = detalleVenta.IdProducto });
            ViewBag.Error = "";
            return View(detalleVenta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdDetalleVenta, DetalleVenta pDetalleVenta)
        {
            try
            {
                int result = await DetalleVentaBL.EliminarAsync(pDetalleVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var detalleVenta = await DetalleVentaBL.ObtenerPorIdAsync(pDetalleVenta);
                if (detalleVenta == null)
                    detalleVenta = new DetalleVenta();
                if (detalleVenta.IdDetalleVenta > 0)
                    detalleVenta.Venta = await VentaBL.ObtenerPorIdAsync(new Venta { IdVenta = detalleVenta.IdVenta });
                if (detalleVenta.IdDetalleVenta > 0)
                    detalleVenta.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = detalleVenta.IdProducto });
                return View(detalleVenta);
            }
        }
    }
}
