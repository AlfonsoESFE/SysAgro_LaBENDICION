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
    //[Authorize(Roles = "Cajero,SuperAdmin,Admin")]
    public class DetalleVentaController : Controller
    {
        DetalleVentaBL DetalleVentaBL = new DetalleVentaBL();
        VentaBL VentaBL = new VentaBL();
        ProductoBL ProductoBL = new ProductoBL();
        public static int idFac;

        #region CRUD Y METODOS PARA INCLUIR FACTURAS Y LO RELACIONADO A LA FUNCIONALIDAD DE UN CRUD

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

        #endregion

        #region METODOS PARA PROCESOS DE FACTURACION - VENTAS Y DETALLES DE VENTAS

        //METODO PARA VISTA DE VENTAS

     

        List<DetalleVenta> DetalleVentas;

        public async Task<IActionResult> Ventas(int campo, DetalleVenta pDetalleVenta = null)
        {
            if (pDetalleVenta == null)
                pDetalleVenta = new DetalleVenta();
            var taskBuscar = DetalleVentaBL.BuscarIncluirVentasYProductoAsync(pDetalleVenta);
            var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            DetalleVentas = await taskBuscar;
            ViewBag.Ventas = await taskObtenerTodosVentas;

            if (campo > 0)
            {
                Producto producto = new Producto();
                producto.Codigo = campo.ToString();
                List<Producto> ProductoBuscado = await ProductoBL.BuscarAsync(producto);
                ViewBag.Producto = ProductoBuscado;
            }
            else
            {
                ViewBag.Producto = await taskObtenerTodosProducto;
            }
            return View(DetalleVentas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetalleVenta(DetalleVenta pDetalleVenta)
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
                // Reutiliza la lista de productos que ya obtuviste en el método Venta
                ViewBag.Producto = ViewBag.Producto;
                return View("Venta", DetalleVentas); // Retorna a la vista Venta con el modelo DetalleVentas
            }
        }

        //List<DetalleVenta>? DetalleVentas;
        //public async Task<IActionResult> Venta(int campo, DetalleVenta pDetalleVenta = null)
        //{
        //    if (pDetalleVenta == null)
        //        pDetalleVenta = new DetalleVenta();

        //    var taskBuscar = DetalleVentaBL.BuscarIncluirVentasYProductoAsync(pDetalleVenta);
        //    var taskObtenerTodosVentas = VentaBL.ObtenerTodosAsync();
        //    var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
        //    DetalleVentas = await taskBuscar;
        //    ViewBag.Ventas = await taskObtenerTodosVentas;

        //    if (campo > 0)
        //    {
        //        Producto producto = new Producto();
        //        producto.Codigo = campo.ToString();
        //        List<Producto> ProductoBuscado = await ProductoBL.BuscarAsync(producto);
        //        ViewBag.Producto = ProductoBuscado;
        //    }
        //    else
        //    {
        //        ViewBag.Producto = await taskObtenerTodosProducto;
        //    }
        //    return View(DetalleVentas);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DetalleVenta(DetalleVenta pDetalleVenta)
        //{
        //    try
        //    {
        //        int result = await DetalleVentaBL.CrearAsync(pDetalleVenta);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        ViewBag.Ventas = await VentaBL.ObtenerTodosAsync();
        //        ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
        //        return View(pDetalleVenta);
        //    }
        //}


        //[HttpPost("ProcesarFactura")]
        //public async Task<IActionResult> ProcesarFactura(string Descripcion, string Direccion, string Correo, string Telefono, decimal total, decimal descuento, decimal impuesto, decimal totalpagado, int cantidad, int codigo, byte FormadaDePago, DateTime FechaEmision, decimal ValorTotal, List<DetalleFactura> detalleFacturas)
        //{
        //    Random random = new Random();

        //    Factura objFactura = new();
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

        //    //FacturaBL.CrearAsync(objFactura);
        //    await FacturaBL.CrearAsync(objFactura);


        //    DetalleFactura DetalleFacturas = new();
        //    DetalleFacturas.Codigo = random.Next(100000, 999999);
        //    DetalleFacturas.FechaEmision = DateTime.Now;
        //    DetalleFacturas.FormaDePago = 1;

        //    foreach (var detalle in detalleFacturas)
        //    {
        //        Producto objProducto = new Producto();
        //        objProducto.IdProducto = detalle.IdProducto;
        //        objProducto = await ProductoBL.ObtenerPorIdProductoAsync(objProducto);


        //        objProducto.Cantidad = objProducto.Cantidad - detalle.Cantidad;
        //        await ProductoBL.ModificarAsync(objProducto);


        //        idFac = objFactura.IdFactura;
        //        detalle.IdFactura = objFactura.IdFactura;
        //        await detalle_facturaBL.CrearAsync(detalle);
        //    }

        //    var venta = new Venta
        //    {
        //        ObjFactura = objFactura,
        //        DetalleFacturas = detalleFacturas
        //    };

        //    return RedirectToAction("ObtenerFactura");
        //}

        //[HttpGet("ObtenerFactura")]

        //public async Task<IActionResult> ObtenerFactura()
        //{

        //    DetalleFactura objdetalle = new DetalleFactura();
        //    objdetalle.IdFactura = idFac;
        //    List<DetalleFactura> ListaDetalle = await detalle_facturaBL.BuscarIncluirFacturasYProductoAsync(objdetalle);
        //    ViewBag.Facturas = ListaDetalle.FirstOrDefault().Factura;
        //    ViewBag.ListaDetalle = ListaDetalle;
        //    return View();
        //}

        //public async Task<IActionResult> Reportes(DetalleFactura pDetalleFactura, DateTime fInicio, DateTime fFinal, int NumeroFactura)
        //{
        //    List<Factura> facturas = await FacturaBL.ObtenerTodosAsync();
        //    List<DetalleFactura> detalleFacturas = await detalle_facturaBL.ObtenerTodosAsync();

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

        //    ViewBag.Detalles = detalleFacturas;

        //    return View();
        //}



        #endregion

    }
}
