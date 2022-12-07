using Entidades;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
namespace Datos
{
    public class RepositorioLogin:ConexionBd
    {
        OracleCommand command;
        OracleConnection connection;
        public bool Guardar(Login usuario)
        {
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("crear_user", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id", OracleDbType.Varchar2).Value = usuario.IdUser;
                command.Parameters.Add("v_nombre", OracleDbType.Varchar2).Value = usuario.Usuario;
                command.Parameters.Add("v_clave", OracleDbType.Varchar2).Value = usuario.Contraseña;
                command.Parameters.Add("v_correo", OracleDbType.Varchar2).Value=usuario.CorreoElectronico;
                command.ExecuteNonQuery();
                cerrarBD();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Login> Leer()
        {
            try
            {
                List<Login> ListUsuario = new List<Login>();
                
                return ListUsuario;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }
        public string verificarUsuario(Login user)
        {
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("SELECT * FROM LOGIN WHERE USUARIO_NAME =:usuario_name AND CLAVE_USER =: contra", connection);
                command.Parameters.Add(":usuario_name", user.Usuario);
                command.Parameters.Add(":contra", user.Contraseña);
                OracleDataReader raided = command.ExecuteReader();
                if (raided.Read())
                {
                    cerrarBD();
                    return "SI";
                }
                else
                {
                    return "NO";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
