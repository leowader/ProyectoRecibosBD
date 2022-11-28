using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Datos
{
    public class RepositorioEscuela : Archivos, ICrudDatos<Escuela>
    {
        OracleConnection connection;
        OracleCommand command;
        public bool Guardar(Escuela escuela)
        {
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("insert_escuela", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id_escuela", OracleDbType.Varchar2).Value = escuela.NiT;
                command.Parameters.Add("v_nombre_es", OracleDbType.Varchar2).Value = escuela.NombreEscuela;
                command.Parameters.Add("v_direccion", OracleDbType.Varchar2).Value = escuela.Direccion;
                command.Parameters.Add("v_telefono", OracleDbType.Varchar2).Value = escuela.Telefono;
                command.Parameters.Add("v_correo", OracleDbType.Varchar2).Value = escuela.Correo;
                command.ExecuteNonQuery();
                cerrarBD();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Escuela> Leer()
        {
            try
            {
                List<Escuela> ListEscuela = new List<Escuela>();
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("select * from escuela", connection);
                var raid = command.ExecuteReader();
                while (raid.Read())
                {
                    ListEscuela.Add(Mapear(raid));
                }

                cerrarBD();
                return ListEscuela;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public double totalcobro(Escuela escuela)
        {
            abrirBD();
            command = new OracleCommand("totalescuela", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("v_id_escuela", OracleDbType.Varchar2).Value = escuela.NiT;
            command.ExecuteNonQuery();
            var total = command.ExecuteScalar();
            cerrarBD();
            return Convert.ToDouble(total);
        }
        public Escuela Mapear(OracleDataReader linea)
        {
            var escuela = new Escuela
            {
                NombreEscuela = linea.GetString(1),
                Direccion = linea.GetString(2),
                NiT = linea.GetString(0),
                Telefono = linea.GetString(3),
                Correo = linea.GetString(4),
            };
            return escuela;
        }
        public bool Eliminar(Escuela escuela)
        {
            bool estado;
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("eliminar_escuela", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id", OracleDbType.Varchar2).Value = escuela.NiT;
                command.ExecuteNonQuery();
                cerrarBD();
                return estado = true;
            }
            catch (Exception)
            {
                estado = false;
            }
            return estado;
        }
        public Escuela buscarByname(string id)
        {
            foreach (var item in Leer())
            {
                if (item.NiT == (id))
                {
                    return item;
                }
            }
            return null;
        }
        public bool Actualizar(Escuela escuela)
        {
            abrirBD();
            connection = Miconexion();
            command = new OracleCommand("update_escuela",connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("v_id",OracleDbType.Varchar2).Value=escuela.NiT;
            command.Parameters.Add("v_telefono", OracleDbType.Varchar2).Value = escuela.Telefono;
            command.Parameters.Add("v_correo", OracleDbType.Varchar2).Value = escuela.Correo;
            command.ExecuteNonQuery();
            cerrarBD();
            return true;
        }
    }
}
