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
using SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Models;

namespace SysInventarioFacturacionAgro.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Cajero,SuperAdmin,Admin")]
    public class DetalleFacturaController : Controller
    {
        DetallesFacturaBL detallesFacturaBL = new DetallesFacturaBL();
        FacturaBL facturaBL = new FacturaBL();
        //ProductoBL productoBL = new ProductoBL();
        public static int idFac;

        // GET: DetalleFactura
        public async Task<IActionResult> Index(DetallesFactura pDetallesFactura = null)
        {
            if (pDetallesFactura == null)
                pDetallesFactura = new DetallesFactura();
            var taskBuscar = detallesFacturaBL.BuscarIncluirFacturasYProductoAsync(pDetallesFactura);
            var taskObtenerTodosFacturas = facturaBL.ObtenerTodosAsync();
            //var taskObtenerTodosProducto = productoBL.ObtenerTodosAsync();
            var detallesFacturas = await taskBuscar;
            ViewBag.Facturas = await taskObtenerTodosFacturas;
            //ViewBag.Producto = await taskObtenerTodosProducto;
            return View(detallesFacturas);
        }

        // GET: DetalleFactura/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var detalleFactura = await detallesFacturaBL.ObtenerPorIdAsync(new DetallesFactura { IdDetallesFactura = id });
            detalleFactura.Factura = await facturaBL.ObtenerPorIdAsync(new Factura { IdFactura = detalleFactura.IdFactura });
            //detalleFactura.Producto = await productoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = detalleFactura.IdProducto });
            return View(detalleFactura);
        }

        // GET: DetalleFactura/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Facturas = await facturaBL.ObtenerTodosAsync();
            //ViewBag.Producto = await productoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetalleFactura/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetallesFactura pDetallesFactura)
        {
            try
            {
                int result = await detallesFacturaBL.CrearAsync(pDetallesFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Facturas = await facturaBL.ObtenerTodosAsync();
                //ViewBag.Producto = await productoBL.ObtenerTodosAsync();
                return View(pDetallesFactura);
            }
        }

        // GET: DetalleFactura/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var taskObtenerPorId = detallesFacturaBL.ObtenerPorIdAsync(new DetallesFactura { IdDetallesFactura = id });
            var taskObtenerTodosFacturas = facturaBL.ObtenerTodosAsync();
            //var taskObtenerTodosProducto = productoBL.ObtenerTodosAsync();
            var detalleFactura = await taskObtenerPorId;
            ViewBag.Facturas = await taskObtenerTodosFacturas;
            //ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(detalleFactura);
        }

        // POST: DetalleFactura/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DetallesFactura pDetallesFactura)
        {
            try
            {
                int result = await detallesFacturaBL.ModificarAsync(pDetallesFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Facturas = await facturaBL.ObtenerTodosAsync();
                //ViewBag.Producto = await productoBL.ObtenerTodosAsync();
                return View(pDetallesFactura);
            }
        }

        // GET: DetalleFactura/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var detallesFactura = await detallesFacturaBL.ObtenerPorIdAsync(new DetallesFactura { IdDetallesFactura = id });
            detallesFactura.Factura = await facturaBL.ObtenerPorIdAsync(new Factura { IdFactura = detallesFactura.IdFactura });
            //detallesFactura.Producto = await productoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = detallesFactura.IdProducto });
            ViewBag.Error = "";
            return View(detallesFactura);
        }

        // POST: DetalleFactura/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DetallesFactura pDetallesFactura)
        {
            try
            {
                int result = await detallesFacturaBL.EliminarAsync(pDetallesFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var detallesFactura = await detallesFacturaBL.ObtenerPorIdAsync(pDetallesFactura);
                if (detallesFactura == null)
                    detallesFactura = new DetallesFactura();
                if (detallesFactura.IdDetallesFactura > 0)
                    detallesFactura.Factura = await facturaBL.ObtenerPorIdAsync(new Factura { IdFactura = detallesFactura.IdFactura });
                //if (detallesFactura.IdDetallesFactura > 0)
                //    detallesFactura.Producto = await productoBL.ObtenerPorIdProductoAsync(new Producto { IdProducto = detallesFactura.IdProducto });
                return View(detallesFactura);
            }
        }

        //List<DetallesFactura>? DetallesFacturas;
        //public async Task<IActionResult> Venta(int? campo, DetallesFactura pDetallesFactura = null)
        //{
        //    if (pDetallesFactura == null)
        //        pDetallesFactura = new DetallesFactura();
        //    var taskBuscar = detallesFacturaBL.BuscarIncluirFacturasYProductoAsync(pDetallesFactura);
        //    var taskObtenerTodosFacturas = facturaBL.ObtenerTodosAsync();
        //    var taskObtenerTodosProducto = productoBL.ObtenerTodosAsync();
        //    DetallesFacturas = await taskBuscar;
        //    ViewBag.Facturas = await taskObtenerTodosFacturas;

        //    if (campo > 0)
        //    {
        //        Producto producto = new Producto();
        //        producto.Codigo = campo.Value;
        //        List<Producto> ProductoBuscado = await productoBL.BuscarAsync(producto);
        //        ViewBag.Producto = ProductoBuscado;
        //    }
        //    else
        //    {
        //        ViewBag.Producto = await taskObtenerTodosProducto;
        //    }
        //    return View(DetallesFacturas);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetalleVenta(DetallesFactura pDetallesFactura)
        {
            try
            {
                int result = await detallesFacturaBL.CrearAsync(pDetallesFactura);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Facturas = await facturaBL.ObtenerTodosAsync();
                //ViewBag.Producto = await productoBL.ObtenerTodosAsync();
                return View(pDetallesFactura);
            }
        }

        // GET: DetalleFactura/DetalleVenta
        public async Task<IActionResult> DetalleVenta()
        {
            ViewBag.Facturas = await facturaBL.ObtenerTodosAsync();
            //ViewBag.Producto = await productoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        //[HttpPost("ProcesarFactura")]
        //public async Task<IActionResult> ProcesarFactura(string Descripcion, string Direccion, string Correo, string Telefono, decimal total, decimal descuento, decimal impuesto, decimal totalpagado, int cantidad, int codigo, byte FormaDePago, DateTime FechaEmision, decimal ValorTotal, List<DetallesFactura> detallesFacturas)
        //{
        //    Random random = new Random();

        //    Factura objFactura = new Factura();
        //    objFactura.IdUsuario = global.idu;
        //    objFactura.NumeroFactura = random.Next(100000, 999999);
        //    objFactura.Descripcion = Descripcion ?? "N/A";
        //    objFactura.Direccion = Direccion ?? "N/A";
        //    objFactura.Correo = Correo ?? "N/A";
        //    objFactura.Total = total;
        //    objFactura.Descuento = descuento;
        //    objFactura.Impuesto = impuesto;
        //    objFactura.TotalPagado = totalpagado;
        //    objFactura.Telefono = Telefono ?? "N/A";
        //    objFactura.FechaFacturacion = DateTime.Now;

        //    await facturaBL.CrearAsync(objFactura);

        //    foreach (var detalle in detallesFacturas)
        //    {
        //        Producto objProducto = new Producto();
        //        objProducto.IdProducto = detalle.IdProducto;
        //        objProducto = await productoBL.ObtenerPorIdProductoAsync(objProducto);

        //        objProducto.Cantidad = objProducto.Cantidad - detalle.Cantidad;
        //        await productoBL.ModificarAsync(objProducto);

        //        idFac = objFactura.IdFactura;
        //        detalle.IdFactura = objFactura.IdFactura;
        //        await detallesFacturaBL.CrearAsync(detalle);
        //    }

        //    var venta = new Venta
        //    {
        //        ObjFactura = objFactura,
        //        DetallesFacturas = detallesFacturas
        //    };

        //    return RedirectToAction("ObtenerFactura");
        //}

        //[HttpGet("ObtenerFactura")]
        //public async Task<IActionResult> ObtenerFactura()
        //{
        //    DetallesFactura objdetalle = new DetallesFactura();
        //    objdetalle.IdFactura = idFac;
        //    List<DetallesFactura> ListaDetalles = await detallesFacturaBL.BuscarIncluirFacturasYProductoAsync(objdetalle);
        //    ViewBag.Facturas = ListaDetalles.FirstOrDefault().Factura;
        //    ViewBag.ListaDetalles = ListaDetalles;
        //    return View();
        //}

        //public async Task<IActionResult> Reportes(DetallesFactura pDetallesFactura, DateTime fInicio, DateTime fFinal, int NumeroFactura)
        //{
        //    List<Factura> facturas = await facturaBL.ObtenerTodosAsync();
        //    List<DetallesFactura> detallesFacturas = await detallesFacturaBL.ObtenerTodosAsync();

        //    if (NumeroFactura != 0)
        //    {
        //        ViewBag.Facturas = facturas.Where(r => r.NumeroFactura == NumeroFactura).ToList();
        //    }
        //    else if (fInicio.Year != 1 && fFinal.Year != 1)
        //    {
        //        ViewBag.Facturas = facturas.Where(r => r.FechaFacturacion.Date >= fInicio.Date && r.FechaFacturacion.Date <= fFinal.Date).ToList();
        //    }
        //    else
        //    {
        //        ViewBag.Facturas = facturas;
        //    }

        //    ViewBag.Detalles = detallesFacturas;

        //    return View();
        //}
    }
}
