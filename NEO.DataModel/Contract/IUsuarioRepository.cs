using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEO.DataModel.Contract
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAllUsers();
        Usuario GetUser(int? id);
        void InsertUser(Usuario usuario);
        void DeleteUser(Usuario usuario);
        Usuario Login(string usuario, string password);
        void GuardarDatos();
    }
}
