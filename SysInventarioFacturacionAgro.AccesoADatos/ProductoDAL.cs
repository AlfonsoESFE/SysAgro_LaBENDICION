using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class ProductoDAL
    {
        public static async Task<int> CrearAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pProducto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        //public static async Task<int> ModificarExistenciasAsync(Producto pProducto)
        //{
        //    int result = 0;
        //    using (var bdContexto = new BDContexto())
        //    {
        //        var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);

        //        producto.Cantidad = pProducto.Cantidad;

        //        bdContexto.Update(producto);
        //        result = await bdContexto.SaveChangesAsync();
        //    }
        //    return result;
        //}
        public static async Task<int> ModificarAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
                producto.IdProveedor = pProducto.IdProveedor;
                producto.IdCategoria = pProducto.IdCategoria;
                producto.IdMarca = pProducto.IdMarca;
                producto.Codigo = pProducto.Codigo;
                producto.Nombre = pProducto.Nombre;
                producto.Descripcion = pProducto.Descripcion;
                producto.PrecioCompra = pProducto.PrecioCompra;
                producto.PrecioUnitario = pProducto.PrecioUnitario;
                producto.PrecioMayorista = pProducto.PrecioMayorista;
                producto.CantidadStock = pProducto.CantidadStock;
                producto.FechaIngreso = pProducto.FechaIngreso;
                producto.FechaVencimiento = pProducto.FechaVencimiento;
                producto.PaisOrigen = pProducto.PaisOrigen;
                producto.UnidadMedida = pProducto.UnidadMedida;
                bdContexto.Update(producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Producto pProducto)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
                bdContexto.Producto.Remove(producto);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Producto> ObtenerPorIdAsync(Producto pProducto)
        {
            var producto = new Producto();
            using (var bdContexto = new BDContexto())
            {
                producto = await bdContexto.Producto.FirstOrDefaultAsync(s => s.IdProducto == pProducto.IdProducto);
            }
            return producto;
        }
        public static async Task<List<Producto>> ObtenerTodosAsync()
        {
            var Productos = new List<Producto>();
            using (var bdContexto = new BDContexto())
            {
                Productos = await bdContexto.Producto.ToListAsync();
            }
            return Productos;
        }

        //SIRVE PARA BUSCAR POR FILTRO
        internal static IQueryable<Producto> QuerySelect(IQueryable<Producto> pQuery, Producto pProducto)
        {

            if (pProducto.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdProducto == pProducto.IdProducto);

            if (pProducto.IdProveedor > 0)
                pQuery = pQuery.Where(s => s.IdProveedor == pProducto.IdProveedor);

            if (pProducto.IdProducto > 0)
                pQuery = pQuery.Where(s => s.IdCategoria == pProducto.IdCategoria);

            if (pProducto.IdMarca > 0)
                pQuery = pQuery.Where(s => s.IdMarca == pProducto.IdMarca);

            if (!string.IsNullOrWhiteSpace(pProducto.Codigo))
                pQuery = pQuery.Where(s => s.Codigo.Contains(pProducto.Codigo));

            if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pProducto.Nombre));
          
            return pQuery;
        }
        public static async Task<List<Producto>> BuscarAsync(Producto pProducto)
        {
            var Productos = new List<Producto>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Producto.AsQueryable();
                select = QuerySelect(select, pProducto);
                Productos = await select.ToListAsync();
            }
            return Productos;
        }
        public static async Task<List<Producto>> BuscarIncluirProveedoryCategoriayMarcaAsync(Producto pProducto)
        {
            var Productos = new List<Producto>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Producto.AsQueryable();
                select = QuerySelect(select, pProducto).Include(s => s.Proveedor).Include(s => s.Categoria).Include(s => s.Marca).AsQueryable();
                Productos = await select.ToListAsync();
            }
            return Productos;
        }
    }
}
