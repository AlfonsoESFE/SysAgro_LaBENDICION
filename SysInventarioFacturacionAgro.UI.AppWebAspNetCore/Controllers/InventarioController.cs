using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.LogicaDeNegocio;
using SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Models;
using SysInventarioFacturacionAgro.AccesoADatos;


namespace SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class InventarioController : Controller
    {
        InventarioBL inventarioBL = new InventarioBL();
        ProductoBL ProductoBL = new ProductoBL();
        UsuarioBL usuarioBL = new UsuarioBL();

        #region METODOS CRUD DE INVENTARIO

        // GET: DetallePedidoController
        public async Task<IActionResult> Index(Inventario pInventario = null)
        {
            if (pInventario == null)
                pInventario = new Inventario();
            if (pInventario.Top_Aux == 0)
                pInventario.Top_Aux = 10;
            else if (pInventario.Top_Aux == -1)
                pInventario.Top_Aux = 0;
            var taskBuscar = inventarioBL.BuscarIncluirProductoYUsuarioAsync(pInventario);
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var inventario = await taskBuscar;
            ViewBag.Top = pInventario.Top_Aux;
            ViewBag.Usuario = await taskObtenerTodosUsuarios;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(inventario);
        }

        // GET: DetallePedidoController/Details/5
        public async Task<IActionResult> Details(int IdInventario)
        {
            var Inventario = await inventarioBL.ObtenerPorIdAsync(new Inventario { IdInventario = IdInventario });
            Inventario.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = Inventario.IdUsuario });
            Inventario.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = Inventario.IdProducto });

            return View(Inventario);
        }

        // GET: DetallePedidoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventario pInventario)
        {
            try
            {
                int result = await inventarioBL.CrearAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pInventario);
            }
        }

        // GET: DetallePedidoController/Edit/5
        public async Task<IActionResult> Edit(Inventario pInventario)
        {
            var taskObtenerPorId = inventarioBL.ObtenerPorIdAsync(pInventario);
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var Inventario = await taskObtenerPorId;
            ViewBag.Usuario = await taskObtenerTodosUsuarios;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(Inventario);
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdIventario, Inventario pInventario)
        {
            try
            {
                int result = await inventarioBL.ModificarAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pInventario);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public async Task<IActionResult> Delete(Inventario pInventario)
        {
            var Inventario = await inventarioBL.ObtenerPorIdAsync(pInventario);
            Inventario.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = Inventario.IdUsuario });
            Inventario.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = Inventario.IdProducto });
            ViewBag.Error = "";

            return View(Inventario);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdInventario, Inventario pInventario)
        {
            try
            {
                int result = await inventarioBL.EliminarAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var Inventario = await inventarioBL.ObtenerPorIdAsync(pInventario);
                if (Inventario == null)
                    Inventario = new Inventario();
                if (Inventario.IdInventario > 0)
                    Inventario.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = Inventario.IdUsuario });
                if (Inventario.IdInventario > 0)
                    Inventario.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = Inventario.IdProducto });
                return View(Inventario);
            }
        }


#endregion
        public async Task<IActionResult> Ajuste()
        {
            ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajuste(Inventario pInventario, int Diferencia)
        {
            try
            {
                pInventario.IdUsuario = global.idu;

                Producto objProducto = new Producto();
                objProducto.IdProducto = pInventario.IdProducto;
                objProducto = await ProductoBL.ObtenerPorIdAsync(objProducto);

                if (Diferencia == 1)
                {
                    objProducto.CantidadStock = objProducto.CantidadStock + pInventario.Cantidad;
                    await ProductoBL.ModificarAsync(objProducto);
                }
                else if (Diferencia == 2)
                {
                    objProducto.CantidadStock = objProducto.CantidadStock - pInventario.Cantidad;
                    await ProductoBL.ModificarAsync(objProducto);
                }

                int result = await inventarioBL.CrearAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pInventario);
            }
        }
    }
}
