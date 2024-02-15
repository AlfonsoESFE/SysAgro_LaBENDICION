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


    public class DetallesCompraController : Controller
    {
        DetallesCompraBL DetalleCompraBL = new DetallesCompraBL();
        ProductoBL ProductoBL = new ProductoBL();
        CompraBL CompraBL = new CompraBL();
        public static int idComp;
        // GET: DetallePedidoController
        public async Task<IActionResult> Index(DetallesCompra pDetalleCompra = null)
        {
            if (pDetalleCompra == null)
                pDetalleCompra = new DetallesCompra();
            if (pDetalleCompra.Top_Aux == 0)
                pDetalleCompra.Top_Aux = 10;
            else if (pDetalleCompra.Top_Aux == -1)
                pDetalleCompra.Top_Aux = 0;
            var taskBuscar = DetalleCompraBL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
            var taskObtenerTodosCompra = CompraBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleCompra = await taskBuscar;
            ViewBag.Top = pDetalleCompra.Top_Aux;
            ViewBag.Compra = await taskObtenerTodosCompra;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(DetalleCompra);
        }

        // GET: DetallePedidoController/Details/5
        public async Task<IActionResult> Details(int IdDetalleCompra)
        {
            var DetalleCompra = await DetalleCompraBL.ObtenerPorIdAsync(new DetallesCompra { IdDetallesCompra = IdDetalleCompra });
            DetalleCompra.Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = DetalleCompra.IdCompra });
            DetalleCompra.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = DetalleCompra.IdProducto });

            return View(DetalleCompra);
        }

        // GET: DetallePedidoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Compra = await CompraBL.ObtenerTodosAsync();
            ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetallesCompra pDetalleCompra)
        {
            try
            {
                int result = await DetalleCompraBL.CrearAsync(pDetalleCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Compra = await CompraBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleCompra);
            }
        }

        // GET: DetallePedidoController/Edit/5
        public async Task<IActionResult> Edit(DetallesCompra pDetalleCompra)
        {
            var taskObtenerPorId = DetalleCompraBL.ObtenerPorIdAsync(pDetalleCompra);
            var taskObtenerTodosCompra = CompraBL.ObtenerTodosAsync();
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            var DetalleCompra = await taskObtenerPorId;
            ViewBag.Compra = await taskObtenerTodosCompra;
            ViewBag.Producto = await taskObtenerTodosProducto;
            ViewBag.Error = "";
            return View(DetalleCompra);
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdDetalleCompra, DetallesCompra pDetalleCompra)
        {
            try
            {
                int result = await DetalleCompraBL.ModificarAsync(pDetalleCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Compra = await CompraBL.ObtenerTodosAsync();
                ViewBag.Producto = await ProductoBL.ObtenerTodosAsync();
                return View(pDetalleCompra);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public async Task<IActionResult> Delete(DetallesCompra pDetalleCompra)
        {
            var DetalleCompra = await DetalleCompraBL.ObtenerPorIdAsync(pDetalleCompra);
            DetalleCompra.Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = DetalleCompra.IdCompra });
            DetalleCompra.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = DetalleCompra.IdProducto });
            ViewBag.Error = "";

            return View(DetalleCompra);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdDetalleCompra, DetallesCompra pDetalleCompra)
        {
            try
            {
                int result = await DetalleCompraBL.EliminarAsync(pDetalleCompra);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var DetalleCompra = await DetalleCompraBL.ObtenerPorIdAsync(pDetalleCompra);
                if (DetalleCompra == null)
                    DetalleCompra = new DetallesCompra();
                if (DetalleCompra.IdDetallesCompra > 0)
                    DetalleCompra.Compra = await CompraBL.ObtenerPorIdAsync(new Compra { IdCompra = DetalleCompra.IdCompra });
                if (DetalleCompra.IdDetallesCompra > 0)
                    DetalleCompra.Producto = await ProductoBL.ObtenerPorIdAsync(new Producto { IdProducto = DetalleCompra.IdProducto });
                return View(DetalleCompra);
            }
        }


        public async Task<IActionResult> Compra(int? campo, DetallesCompra pDetalleCompra = null)
        {
            if (pDetalleCompra == null)
                pDetalleCompra = new DetallesCompra();
            if (pDetalleCompra.Top_Aux == 0)
                pDetalleCompra.Top_Aux = 10;
            else if (pDetalleCompra.Top_Aux == -1)
                pDetalleCompra.Top_Aux = 0;
            List<DetallesCompra>? DetalleCompra;
            var taskBuscar = DetalleCompraBL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
            var taskObtenerTodosCompras = DetalleCompraBL.BuscarIncluirCompraProductoAsync(pDetalleCompra);
            var taskObtenerTodosProducto = ProductoBL.ObtenerTodosAsync();
            DetalleCompra = await taskBuscar;
            ViewBag.Top = pDetalleCompra.Top_Aux;
            ViewBag.Compras = await taskObtenerTodosCompras;
            ViewBag.Producto = await taskObtenerTodosProducto;
            return View(DetalleCompra);
        }

        [HttpPost("ProcesarCompra")]
        public async Task<IActionResult> ProcesarCompra(int IdUsuario, decimal total, decimal totalpagado, byte FormaPago, DateTime FechaCompra, List<DetallesCompra> detalleCompras)
        {
            Random random = new Random();

            Compra objCompra = new();
            objCompra.IdUsuario = IdUsuario;

            objCompra.NumeroCompra = random.Next(100000, 999999);
            objCompra.FormaPago = 1;
            objCompra.TotalPago = totalpagado;
            objCompra.FechaCompra = DateTime.Now;

            await CompraBL.CrearAsync(objCompra);

            foreach (var detalle in detalleCompras)
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = detalle.IdProducto;
                objProducto = await ProductoBL.ObtenerPorIdAsync(objProducto);


                objProducto.CantidadStock = objProducto.CantidadStock + detalle.Cantidad;
                await ProductoBL.ModificarAsync(objProducto);


                idComp = objCompra.IdCompra;
                detalle.IdCompra = objCompra.IdCompra;
                await DetalleCompraBL.CrearAsync(detalle);
            }



            return RedirectToAction("ObtenerCompra");
        }
        [HttpGet("ObtenerCompra")]
        public async Task<IActionResult> ObtenerCompra()
        {

            DetallesCompra objdetalle = new DetallesCompra();
            objdetalle.IdCompra = idComp;
            List<DetallesCompra> ListaDetalle = await DetalleCompraBL.BuscarIncluirCompraProductoAsync(objdetalle);
            ViewBag.Compras = ListaDetalle.FirstOrDefault().Compra;
            ViewBag.ListaDetalle = ListaDetalle;
            return View();
        }

        public async Task<IActionResult> Reportes(DetallesCompra pDetalleFactura, DateTime fInicio, DateTime fFinal, int NumeroCompra)
        {
            List<Compra> compras = await CompraBL.ObtenerTodosAsync();
            List<DetallesCompra> detalleCompras = await DetalleCompraBL.ObtenerTodosAsync();

            if (NumeroCompra != 0)
            {
                ViewBag.Compras = compras.Where(r => r.IdCompra == NumeroCompra).ToList();
            }
            else if (fInicio.Year != 1 && fFinal.Year != 1)
            {
                ViewBag.Compras = compras.Where(r => r.FechaCompra.Date >= fInicio.Date && r.FechaCompra.Date <= fFinal.Date).ToList();
            }
            else
            {
                ViewBag.Compras = compras;
            }

            ViewBag.Detalles = detalleCompras;

            return View();
        }

    }
}