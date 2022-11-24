using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace Datos
{
    public class Archivos
    {
        public string ruta;
        public string stringConexion= "DATA SOURCE= LOCALHOST:1521/XEPDB1 ; PASSWORD = recibovol1 ; USER ID = recibo";

        OracleConnection conexion;
        public Archivos()
        {
            conexion = new OracleConnection(stringConexion);
        }
        public string abrirBD()
        {
            try
            {
                conexion.Open();
                return "BD ABIERTA";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }
        public void cerrarBD()
        {
            if (ConnectionState.Open==conexion.State)
            {
                conexion.Close();
            }
        }
        public OracleConnection Miconexion()
        {
            return conexion;
        }
    }
}
