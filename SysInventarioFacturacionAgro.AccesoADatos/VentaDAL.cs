using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class VentaDAL
    {
        public static async Task<int> CrearAsync(Venta pVenta)
        {
            int result = 0;

            using (var bdContexto = new BDContexto())
            {
                using (var transaction = bdContexto.Database.BeginTransaction())
                {
                    try
                    {
                        Random random = new Random();
                        int numeroAleatorio;

                        do
                        {
                            // Generar un número aleatorio para la nueva venta
                            numeroAleatorio = random.Next(10000, 99999); // Ajusta el rango según tus necesidades

                        } while (await bdContexto.Venta.AnyAsync(v => v.NumeroFactura == numeroAleatorio));

                        // Asignar el número aleatorio a la nueva venta
                        pVenta.NumeroFactura = numeroAleatorio;

                        // Asignar la fecha actual
                        pVenta.FechaFacturacion = DateTime.Now;

                        // Agregar la venta a la base de datos
                        bdContexto.Add(pVenta);

                        // Guardar los cambios en la base de datos
                        result = await bdContexto.SaveChangesAsync();

                        // Confirmar la transacción
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Manejar cualquier excepción y revertir la transacción en caso de error
                        transaction.Rollback();
                        throw; // Puedes personalizar la lógica de manejo de errores según tus necesidades
                    }
                }
            }

            return result;
        }

        public static async Task<int> ModificarAsync(Venta pVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var venta = await bdContexto.Venta.FirstOrDefaultAsync(v => v.IdVenta == pVenta.IdVenta);
                venta.IdUsuario = pVenta.IdUsuario;
                venta.NumeroFactura = pVenta.NumeroFactura;
                venta.Descuento = pVenta.Descuento;
                venta.Impuesto = pVenta.Impuesto;
                venta.SubTotal = pVenta.SubTotal;
                venta.Total = pVenta.Total;
                venta.NombreCliente = pVenta.NombreCliente;
                venta.DireccionCliente = pVenta.DireccionCliente;
                venta.TelefonoCliente = pVenta.TelefonoCliente;
                venta.DUICliente = pVenta.DUICliente;
                bdContexto.Update(venta);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Venta pVenta)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var venta = await bdContexto.Venta.FirstOrDefaultAsync(v => v.IdVenta == pVenta.IdVenta);

                bdContexto.Venta.Remove(venta);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Venta> ObtenerPorIdAsync(Venta pVenta)
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Venta.FirstOrDefaultAsync(v => v.IdVenta == pVenta.IdVenta);
            }
        }

        public static async Task<List<Venta>> ObtenerTodosAsync()
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Venta.Include(v => v.DetalleVenta).ThenInclude(dv => dv.Producto).ToListAsync();
            }
        }

        private static IQueryable<Venta> QuerySelect(IQueryable<Venta> pQuery, Venta pVenta)
        {
            // Lógica de filtrado
            return pQuery;
        }

        public static async Task<List<Venta>> BuscarAsync(Venta pProducto)
        {
            var Ventas = new List<Venta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Venta.AsQueryable();
                select = QuerySelect(select, pProducto);
                Ventas = await select.ToListAsync();
            }
            return Ventas;
        }

        public static async Task<List<Venta>> BuscarIncluirUsuarioAsync(Venta pVenta)
        {
            var Ventas = new List<Venta>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Venta.AsQueryable();
                select = QuerySelect(select, pVenta).Include(s => s.Usuario).AsQueryable();
                Ventas = await select.ToListAsync();
            }
            return Ventas;
        }
    }
}
