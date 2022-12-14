using Entidades;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.IO;
namespace Datos
{
    public class RepositorioEstudiantes: ConexionBd,ICrudDatos<Estudiante>
    {
        OracleCommand command;
        OracleConnection connection;
        public bool Guardar(Estudiante estudiante)
        {
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("estudiantePackage.insert_estudiante", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id_estudiante", OracleDbType.Varchar2).Value = estudiante.Id;
                command.Parameters.Add("v_nombre", OracleDbType.Varchar2).Value = estudiante.Nombres;
                command.Parameters.Add("v_apellido", OracleDbType.Varchar2).Value = estudiante.Apellidos;
                command.Parameters.Add("v_sexo", OracleDbType.Char).Value = estudiante.Sexo;
                command.Parameters.Add("v_escuela_peri", OracleDbType.Varchar2).Value = estudiante.PeriodoEstudio;
                command.Parameters.Add("v_idescuela", OracleDbType.Varchar2).Value = estudiante.Escuela.NiT.ToString();
                command.Parameters.Add("v_idgrado", OracleDbType.Varchar2).Value = estudiante.codigoCurso;
                command.Parameters.Add("v_estadoinscripcion",OracleDbType.Varchar2).Value=estudiante.estadoInscripcion;
                command.ExecuteNonQuery();
                cerrarBD();
                return true;
            }
            catch (Exception)
            {       
                return false;
            }
        }

        public List<Estudiante> Leer()
        {
            try
            {
                List<Estudiante> listaEstudent = new List<Estudiante>();
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("select * from verestudiantes", connection);
                command.ExecuteNonQuery();
                var raid = command.ExecuteReader();
                while (raid.Read())
                {
                    listaEstudent.Add(Mapear(raid));
                }
                cerrarBD();
                return listaEstudent;
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public string todo()
        {
            try
            {
                List<Estudiante> listaEstudent = new List<Estudiante>();
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("select * from verestudiantes", connection);
                var raid = command.ExecuteReader();
                while (raid.Read())
                {
                    listaEstudent.Add(Mapear(raid));
                }
                cerrarBD();
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        RepositorioEscuela RepositorioEscuela = new RepositorioEscuela();
        public Estudiante Mapear(OracleDataReader linea)
        {
            var estudiante = new Estudiante();
            estudiante.Id = int.Parse(linea.GetString(0));
            estudiante.Nombres = linea.GetString(1);
            estudiante.Apellidos = linea.GetString(2);
            estudiante.Sexo = char.Parse(linea.GetString(3));
            estudiante.curso = linea.GetString(4);
            estudiante.Grado = linea.GetString(5);
            estudiante.PeriodoEstudio = linea.GetString(6);
            estudiante.TieneRecibo= char.Parse(linea.GetString(8));
            estudiante.Escuela = RepositorioEscuela.buscarByNit(linea.GetString(9));
            estudiante.estadoInscripcion= linea.GetString(10);
            return estudiante;
        }
        public bool Eliminar(Estudiante estudiante)
        {
            bool estado;
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("estudiantePackage.eliminar_estudiante", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id",OracleDbType.Varchar2).Value=estudiante.Id;
                command.ExecuteNonQuery();
                cerrarBD();
                return estado=true;
            }
            catch (Exception)
            {
                estado=false;
            }
            return estado;
        }
        public bool Actualizar(Estudiante estudiante)
        {
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("estudiantePackage.atualizar_estudiante", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id", OracleDbType.Varchar2).Value = estudiante.Id;
                command.Parameters.Add("v_grado", OracleDbType.Varchar2).Value = estudiante.codigoCurso;
                command.Parameters.Add("v_periodo", OracleDbType.Varchar2).Value = estudiante.PeriodoEstudio;
                command.ExecuteNonQuery();
                cerrarBD();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool upd(Estudiante estudiante)
        {
            abrirBD();
            connection = Miconexion();
            command = new OracleCommand("estudiantePackage.atualizar_estuTiRecibo", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("v_id", OracleDbType.Varchar2).Value = estudiante.Id;
            command.Parameters.Add("v_tienerecibo", OracleDbType.Char).Value =estudiante.TieneRecibo;
            command.Parameters.Add("v_inscripcion",OracleDbType.Varchar2).Value=estudiante.estadoInscripcion;
            command.ExecuteNonQuery();
            cerrarBD();
            return true;
        }
        public Estudiante BuscarEstudiante(int id)
        {
            var estudiante =new Estudiante();
            foreach (var item in Leer())
            {
                if (item.Id.Equals(id))
                {
                    estudiante = item;
                    return estudiante;
                }
            }
            return null;
        }
    }
}
