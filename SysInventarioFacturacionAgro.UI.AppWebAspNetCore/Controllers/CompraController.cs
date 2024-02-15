using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.LogicaDeNegocio;

using System.Data;

namespace SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class CompraController : Controller
    {
        ProveedorBL ProveedorBL = new ProveedorBL();
        CompraBL CompraBL = new CompraBL();
        UsuarioBL UsuarioBL = new UsuarioBL();
        // GET: FacturaController
        public async Task<IActionResult> Index(Compra pCompra = null)
        {
            if (pCompra == null)
                pCompra = new Compra();
            if (pCompra.Top_Aux == 0)
                pCompra.Top_Aux = 10;
            else if (pCompra.Top_Aux == -1)
                pCompra.Top_Aux = 0;
            var taskBuscar = CompraBL.BuscarIncluirProveedoryUsuarioAsync(pCompra);
            var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var listaCompras = await taskBuscar;
            ViewBag.Top = pCompra.Top_Aux;
            ViewBag.Proveedores = await taskObtenerTodosProveedor;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(listaCompras);
        }

        // GET: FacturaController/Details/5
        public async Task<IActionResult> Details(int IdCompra)
        {
            var Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = IdCompra });
            Compra.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Compra.IdProveedor });
            Compra.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Compra.IdUsuario });
            return View(Compra);
        }

        // GET: FacturaController/Create

        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Error = "";
                return View();
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera apropiada, como registrar el error
                // o redirigir a una página de error
                return RedirectToAction("Error");
            }
        }

        // POST: FacturaController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra pCompra)
        {
            try
            {
                int result = await CompraBL.CrearAsync(pCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pCompra);
            }
        }

        // GET: FacturaController/Edit/5
        public async Task<IActionResult> Edit(Compra pCompra)
        {
            var taskObtenerPorId = CompraBL.ObtenerPorIdAsync(pCompra);
            var taskObtenerTodosProveedor = ProveedorBL.ObtenerTodosAsync();
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var Compra = await taskObtenerPorId;
            ViewBag.Proveedor = await taskObtenerTodosProveedor;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(Compra);
        }

        // POST: FacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdCompra, Compra pCompra)
        {
            try
            {
                int result = await CompraBL.ModificarAsync(pCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Proveedor = await ProveedorBL.ObtenerTodosAsync();
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                return View(pCompra);
            }
        }

        // GET: FacturaController/Delete/5
        public async Task<IActionResult> Delete(Compra pCompra)
        {
            var Compra = await CompraBL.ObtenerPorIdAsync(pCompra);
            Compra.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Compra.IdProveedor });
            Compra.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Compra.IdUsuario });
            ViewBag.Error = "";

            return View(Compra);
        }

        // POST: FacturaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdCompra, Compra pCompra)
        {
            try
            {
                int result = await CompraBL.EliminarAsync(pCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var Compra = await CompraBL.ObtenerPorIdAsync(pCompra);
                if (Compra == null)
                    Compra = new Compra();
                if (Compra.IdCompra > 0)
                    Compra.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Compra.IdProveedor });
                if (Compra.IdCompra > 0)
                    Compra.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Compra.IdUsuario });
                return View(Compra);
            }
        }

    }
}
