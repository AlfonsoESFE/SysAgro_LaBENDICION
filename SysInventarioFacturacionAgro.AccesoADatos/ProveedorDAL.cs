using Microsoft.EntityFrameworkCore;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using SysInventarioFacturacionAgro.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class ProveedorDAL
    {
        public static async Task<int> CrearAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pProveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.IdProveedor == pProveedor.IdProveedor);
                proveedor.Codigo = pProveedor.Codigo;
                proveedor.Nombre = pProveedor.Nombre;
                proveedor.Telefono = pProveedor.Telefono;
                proveedor.Correo = pProveedor.Correo;
                proveedor.Empresa = pProveedor.Empresa;
                proveedor.DireccionEmpresa = pProveedor.DireccionEmpresa;
                proveedor.TelefonoEmpresa = pProveedor.TelefonoEmpresa;
                bdContexto.Update(proveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Proveedor pProveedor)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.IdProveedor == pProveedor.IdProveedor);
                bdContexto.Proveedor.Remove(proveedor);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Proveedor> ObtenerPorIdProveedorAsync(Proveedor pProveedor)
        {
            var proveedor = new Proveedor();
            using (var bdContexto = new BDContexto())
            {
                proveedor = await bdContexto.Proveedor.FirstOrDefaultAsync(s => s.IdProveedor == pProveedor.IdProveedor);
            }
            return proveedor;
        }
        public static async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            var proveedores = new List<Proveedor>();
            using (var bdContexto = new BDContexto())
            {
                proveedores = await bdContexto.Proveedor.ToListAsync();
            }
            return proveedores;
        }
        internal static IQueryable<Proveedor> QuerySelect(IQueryable<Proveedor> pQuery, Proveedor pProveedor)
        {
            if (pProveedor.IdProveedor > 0)
                pQuery = pQuery.Where(s => s.IdProveedor == pProveedor.IdProveedor);
            if (!string.IsNullOrWhiteSpace(pProveedor.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pProveedor.Nombre));
            if (!string.IsNullOrWhiteSpace(pProveedor.Empresa))
                pQuery = pQuery.Where(s => s.Empresa.Contains(pProveedor.Empresa));
            pQuery = pQuery.OrderByDescending(s => s.IdProveedor).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Proveedor>> BuscarAsync(Proveedor pProveedor)
        {
            var proveedores = new List<Proveedor>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Proveedor.AsQueryable();
                select = QuerySelect(select, pProveedor);
                proveedores = await select.ToListAsync();
            }
            return proveedores;
        }
    }
}
