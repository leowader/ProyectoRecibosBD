using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ServicioLogin
    {
        readonly Datos.RepositorioLogin Rutalogin = new Datos.RepositorioLogin();
        List<Login> ListUsuarios;
        public ServicioLogin()
        {
            ListUsuarios = Rutalogin.Leer();
        }
        public void ActualizarList()
        {
            ListUsuarios = Rutalogin.Leer();
        }
        public string Guardar(Login usuario)
        {
            try
            {
                var estado = Rutalogin.Guardar(usuario);
                return estado ? "USUARIO REGISTRADO" : "ERROR AL REGISTRAR EL USUARIO";
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }
        }
        public bool verificarUsuario(Login user)
        {
            try
            {
                var estado = Rutalogin.verificarUsuario(user);
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        public List<Login> Mostrar()
        {
            try
            {
                ActualizarList();
                return ListUsuarios;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

    }
}
