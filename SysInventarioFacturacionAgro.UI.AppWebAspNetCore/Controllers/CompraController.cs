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
            var taskBuscar = CompraBL.BuscarIncluirUsuarioyProveedorAsync(pCompra);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosProveedores = ProveedorBL.ObtenerTodosAsync();

            var Compra = await taskBuscar;
            ViewBag.Top = pCompra.Top_Aux;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Proveedores = await taskObtenerTodosProveedores;

            return View(Compra);
        }

        // GET: FacturaController/Details/5
        public async Task<IActionResult> Details(int IdCompra)
        {
            var Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = IdCompra });
            Compra.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Compra.IdUsuario });
            Compra.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Compra.IdProveedor });

            return View(Compra);
        }

        // GET: FacturaController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
            ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();

            ViewBag.Error = "";
            return View();
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
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();

                ViewBag.Error = ex.Message;
                return View(pCompra);
            }
        }

        // GET: FacturaController/Edit/5
        public async Task<IActionResult> Edit(Compra pCompra)
        {
            var taskObtenerPorId = CompraBL.ObtenerPorIdAsync(pCompra);
            var taskObtenerTodosUsuarios = UsuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosProveedores = ProveedorBL.ObtenerTodosAsync();

            var Compra = await taskObtenerPorId;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Proveedores = await taskObtenerTodosProveedores;

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
                ViewBag.Usuarios = await UsuarioBL.ObtenerTodosAsync();
                ViewBag.Proveedores = await ProveedorBL.ObtenerTodosAsync();

                return View(pCompra);
            }
        }

        // GET: FacturaController/Delete/5
        public async Task<IActionResult> Delete(Compra pCompra)
        {
            var Compra = await CompraBL.ObtenerPorIdAsync(pCompra);
            Compra.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Compra.IdUsuario });
            Compra.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Compra.IdProveedor });

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
                    Compra.Usuario = await UsuarioBL.ObtenerPorIdAsync(new Usuario { Id = Compra.IdUsuario });
                if (Compra.IdCompra > 0)
                    Compra.Proveedor = await ProveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = Compra.IdProveedor });
                return View(Compra);
            }
        }
    }
}
