using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class FacturaDAL
    {
        public static async Task<int> CrearAsync(Factura pFactura)
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
                            // Generar un número aleatorio para la nueva factura
                            numeroAleatorio = random.Next(10000, 99999); // Ajusta el rango según tus necesidades

                        } while (await bdContexto.Factura.AnyAsync(f => f.NumeroFactura == numeroAleatorio));

                        // Asignar el número aleatorio a la nueva factura
                        pFactura.NumeroFactura = numeroAleatorio;

                        // Asignar la fecha actual
                        pFactura.FechaFacturacion = DateTime.Now;

                        // Agregar la factura a la base de datos
                        bdContexto.Add(pFactura);

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




        public static async Task<int> ModificarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
                Factura.IdUsuario = pFactura.IdUsuario;
                Factura.NumeroFactura = pFactura.NumeroFactura;
                Factura.FormaPago = pFactura.FormaPago;
                Factura.Descuento = pFactura.Descuento;
                Factura.Impuesto = pFactura.Impuesto;
                Factura.FacturaTotal = pFactura.FacturaTotal;
                Factura.FacturaPagado = pFactura.FacturaPagado;
                Factura.FacturaCambio = pFactura.FacturaCambio;
                Factura.NombreCliente = pFactura.NombreCliente;
                Factura.DireccionCliente = pFactura.DireccionCliente;
                Factura.TelefonoCliente = pFactura.TelefonoCliente;
                Factura.DUICliente = pFactura.DUICliente;
                bdContexto.Update(Factura);
                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> EliminarAsync(Factura pFactura)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var Factura = await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);

                if (Factura != null)
                {
                    bdContexto.Factura.Remove(Factura);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<Factura> ObtenerPorIdAsync(Factura pFactura)
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Factura.FirstOrDefaultAsync(s => s.IdFactura == pFactura.IdFactura);
            }
        }

        public static async Task<List<Factura>> ObtenerTodosAsync()
        {
            using (var bdContexto = new BDContexto())
            {
                return await bdContexto.Factura.Include(f => f.DetallesFactura).ThenInclude(df => df.Producto).ToListAsync();
            }
        }

        private static IQueryable<Factura> QuerySelect(IQueryable<Factura> pQuery, Factura pFactura)
        {
            // Lógica de filtrado
            return pQuery;
        }

        public static async Task<List<Factura>> BuscarAsync(Factura pFactura)
        {
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura);
                return await select.ToListAsync();
            }
        }

        public static async Task<List<Factura>> BuscarIncluirUsuarioAsync(Factura pFactura)
        {
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Factura.AsQueryable();
                select = QuerySelect(select, pFactura).Include(s => s.Usuario).AsQueryable();
                return await select.ToListAsync();
            }
        }
    }
}
