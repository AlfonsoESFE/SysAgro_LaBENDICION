﻿    using Microsoft.EntityFrameworkCore;
    using SysInventarioFacturacionAgro.AccesoADatos;
    using SysInventarioFacturacionAgro.EntidadesDeNegocio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    namespace SysInventarioFacturacionAgro.AccesoADatos
    {
        public class UsuarioDAL
        {
            private static void EncriptarMD5(Usuario pUsuario)
            {
                using (var md5 = MD5.Create())
                {
                    var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.Password));
                    var strEncriptar = "";
                    for (int i = 0; i < result.Length; i++)
                        strEncriptar += result[i].ToString("x2").ToLower();
                    pUsuario.Password = strEncriptar;
                }
            }
            private static async Task<bool> ExisteLogin(Usuario pUsuario, BDContexto pDbContext)
            {
                bool result = false;
                var loginUsuarioExiste = await pDbContext.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login && s.Id != pUsuario.Id);
                if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 && loginUsuarioExiste.Login == pUsuario.Login)
                    result = true;
                return result;
            }
            #region CRUD
            public static async Task<int> CrearAsync(Usuario pUsuario)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                    if (existeLogin == false)
                    {
                        pUsuario.FechaRegistro = DateTime.Now;
                        EncriptarMD5(pUsuario);
                        bdContexto.Add(pUsuario);
                        result = await bdContexto.SaveChangesAsync();
                    }
                    else
                        throw new Exception("Login ya existe");
                }
                return result;
            }
            public static async Task<int> ModificarAsync(Usuario pUsuario)
            {
                int result = 0;

                using (var bdContexto = new BDContexto())
                {
                    bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                    if (existeLogin == false)
                    {
                        var usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                        {
                            usuario.IdRol = pUsuario.IdRol;
                            usuario.Nombre = pUsuario.Nombre;
                            usuario.Apellido = pUsuario.Apellido;
                            usuario.Login = pUsuario.Login;
                            usuario.Estatus = pUsuario.Estatus;
                            EncriptarMD5(pUsuario);
                            usuario.Password = pUsuario.Password;
                            bdContexto.Update(usuario);
                            result = await bdContexto.SaveChangesAsync();
                        }
                    }
                    else
                        throw new Exception("Login ya existe");
                }
                return result;
            }
            public static async Task<int> EliminarAsync(Usuario pUsuario)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                    bdContexto.Usuario.Remove(usuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
            {
                var usuario = new Usuario();
                using (var bdContexto = new BDContexto())
                {
                    usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                }
                return usuario;
            }
            public static async Task<List<Usuario>> ObtenerTodosAsync()
            {
                var usuarios = new List<Usuario>();
                using (var bdContexto = new BDContexto())
                {
                    usuarios = await bdContexto.Usuario.ToListAsync();
                }
                return usuarios;
            }
            internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
            {
                if (pUsuario.Id > 0)
                    pQuery = pQuery.Where(s => s.Id == pUsuario.Id);
                if (pUsuario.IdRol > 0)
                    pQuery = pQuery.Where(s => s.IdRol == pUsuario.IdRol);
                if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                    pQuery = pQuery.Where(s => s.Nombre.Contains(pUsuario.Nombre));
                if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                    pQuery = pQuery.Where(s => s.Apellido.Contains(pUsuario.Apellido));
                if (!string.IsNullOrWhiteSpace(pUsuario.Login))
                    pQuery = pQuery.Where(s => s.Login.Contains(pUsuario.Login));
                if (pUsuario.Estatus > 0)
                    pQuery = pQuery.Where(s => s.Estatus == pUsuario.Estatus);
                if (pUsuario.FechaRegistro.Year > 1000)
                {
                    DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year, pUsuario.FechaRegistro.Month, pUsuario.FechaRegistro.Day, 0, 0, 0);
                    DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                    pQuery = pQuery.Where(s => s.FechaRegistro >= fechaInicial && s.FechaRegistro <= fechaFinal);
                }
                pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
                if (pUsuario.Top_Aux > 0)
                    pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();
                return pQuery;
            }
            public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
            {
                var Usuarios = new List<Usuario>();
                using (var bdContexto = new BDContexto())
                {
                    var select = bdContexto.Usuario.AsQueryable();
                    select = QuerySelect(select, pUsuario);
                    Usuarios = await select.ToListAsync();
                }
                return Usuarios;
            }
            #endregion
            public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
            {
                var usuarios = new List<Usuario>();
                using (var bdContexto = new BDContexto())
                {
                    var select = bdContexto.Usuario.AsQueryable();
                    select = QuerySelect(select, pUsuario).Include(s => s.Rol).AsQueryable();
                    usuarios = await select.ToListAsync();
                }
                return usuarios;
            }
            public static async Task<Usuario> LoginAsync(Usuario pUsuario)
            {
                var usuario = new Usuario();
                using (var bdContexto = new BDContexto())
                {
                    EncriptarMD5(pUsuario);
                    usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login &&
                    s.Password == pUsuario.Password && s.Estatus == (byte)Estatus_Usuario.ACTIVO);
                }
                return usuario;
            }
            public static async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordAnt)
            {
                int result = 0;
                var usuarioPassAnt = new Usuario { Password = pPasswordAnt };
                EncriptarMD5(usuarioPassAnt);
                using (var bdContexto = new BDContexto())
                {
                    var usuario = await bdContexto.Usuario.FirstOrDefaultAsync(s => s.Id == pUsuario.Id);
                    if (usuarioPassAnt.Password == usuario.Password)
                    {
                        EncriptarMD5(pUsuario);
                        usuario.Password = pUsuario.Password;
                        bdContexto.Update(usuario);
                        result = await bdContexto.SaveChangesAsync();
                    }
                    else
                        throw new Exception("El password actual es incorrecto");
                }
                return result;
            }
        }
    }
