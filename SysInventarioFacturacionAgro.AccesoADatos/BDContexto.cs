using Microsoft.EntityFrameworkCore;

using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }


        public DbSet<AjusteInventario> AjusteInventario { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<DetallesCompra> DetallesCompra { get; set; }
        public DbSet<DetallesFactura> DetallesFactura { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Reporte> Reporte { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"workstation id=SysInventarioFacturacion503.mssql.somee.com;packet size=4096;user id=romerooscar_SQLLogin_1;pwd=awaosafn8m;data source=SysInventarioFacturacion503.mssql.somee.com;persist security info=False;initial catalog=SysInventarioFacturacion503;Encrypt=False;TrustServerCertificate=False;");
            optionsBuilder.UseSqlServer(@"workstation id=AgroservicioBD24.mssql.somee.com;packet size=4096;user id=Alphonse53_SQLLogin_1;pwd=cwffkweftj;data source=AgroservicioBD24.mssql.somee.com;persist security info=False;initial catalog=AgroservicioBD24;Encrypt=False;TrustServerCertificate=False;");

            //optionsBuilder.UseSqlServer(@"Data Source=OSCARROMERO\SQLEXPRESS;Initial Catalog=SysInventarioFacturacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Factura>().HasMany(f => f.DetalleFactura).WithOne(df => df.Factura).HasForeignKey(df => df.IdFactura);
        //}
    }
}

