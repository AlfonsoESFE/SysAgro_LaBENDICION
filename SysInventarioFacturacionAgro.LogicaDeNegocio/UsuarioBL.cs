﻿using SysInventarioFacturacionAgro.AccesoADatos;
using SysInventarioFacturacionAgro.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInventarioFacturacionAgro.LogicaDeNegocio
{
    public class UsuarioBL
    {
        #region CRUD
        public async Task<int> CrearAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.CrearAsync(pUsuario);
        }
        public async Task<int> ModificarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.ModificarAsync(pUsuario);
        }
        public async Task<int> EliminarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.EliminarAsync(pUsuario);
        }
        public async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.ObtenerPorIdAsync(pUsuario);
        }
        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await UsuarioDAL.ObtenerTodosAsync();
        }
        public async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarAsync(pUsuario);
        }
        #endregion
        public async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.LoginAsync(pUsuario);
        }
        public async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordAnt)
        {
            return await UsuarioDAL.CambiarPasswordAsync(pUsuario, pPasswordAnt);
        }
        public async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarIncluirRolesAsync(pUsuario);
        }
    }
}