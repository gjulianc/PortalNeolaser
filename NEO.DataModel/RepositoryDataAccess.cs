using NEO.DataModel.Contract;
using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEO.DataModel.Data
{
    public class RepositoryDataAccess
    {
        static IUsuarioRepository objUsuario = new UsuarioMap();

        #region Usuarios
        public Usuario Login(string usuario, string password)
        {
            return objUsuario.Login(usuario, password);
        }
        public void DeleteUser(Usuario usuario)
        {
            objUsuario.DeleteUser(usuario);
        }
        public List<Usuario> GetAllUsers()
        {
            return objUsuario.GetAllUsers();
        }
        public Usuario GetUser(int? id)
        {
            return objUsuario.GetUser(id);
        }
        public void GuardarDatos()
        {
            objUsuario.GuardarDatos();
        }
        public void InsertUser(Usuario usuario)
        {
            objUsuario.InsertUser(usuario);
        }
        #endregion


    }
}
