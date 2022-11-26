using Entidades;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.IO;
namespace Datos
{
    public class RepositorioEstudiantes: Archivos,ICrudDatos<Estudiante>
    {
        OracleCommand command;
        OracleConnection connection;
        public bool Guardar(Estudiante estudiante)
        {
            try
            {
                abrirBD();
                connection = Miconexion();
                command = new OracleCommand("insert_estudiante", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("v_id_estudiante", OracleDbType.Varchar2).Value = estudiante.Id;
                command.Parameters.Add("v_nombre", OracleDbType.Varchar2).Value = estudiante.Nombres;
                command.Parameters.Add("v_apellido", OracleDbType.Varchar2).Value = estudiante.Apellidos;
                command.Parameters.Add("v_sexo", OracleDbType.Char).Value = estudiante.Sexo;
                command.Parameters.Add("v_escuela_peri", OracleDbType.Varchar2).Value = estudiante.PeriodoEstudio;
                command.Parameters.Add("v_idescuela", OracleDbType.Varchar2).Value = estudiante.idescuela;
                //command.Parameters.Add("v_idescuela", OracleDbType.Varchar2).Value = estudiante.Escuela.NiT;
                command.Parameters.Add("v_idgrado", OracleDbType.Varchar2).Value = estudiante.codigoCurso;
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
                command = new OracleCommand("select * from estudiante", connection);
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
        //public string todo()
        //{
        //    try
        //    {
        //        List<Estudiante> listaEstudent = new List<Estudiante>();
        //        abrirBD();
        //        connection = Miconexion();
        //        command = new OracleCommand("select * from estudiante", connection);
        //        var raid = command.ExecuteReader();
        //        while (raid.Read())
        //        {
        //            listaEstudent.Add(Mapear(raid));
        //        }
        //        cerrarBD();
        //        return "ok";
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}
        public Estudiante Mapear(OracleDataReader linea)
        {
            var estudiante = new Estudiante();
            estudiante.Id = int.Parse(linea.GetString(0));
            estudiante.Nombres = linea.GetString(1);
            estudiante.Apellidos = linea.GetString(2);
            estudiante.Sexo = char.Parse(linea.GetString(3));
            estudiante.PeriodoEstudio = linea.GetString(4);
            estudiante.idescuela = linea.GetString(5);
            estudiante.codigoCurso = linea.GetString(6);
            return estudiante;
        }
        public bool Eliminar(List<Estudiante>estudiantes)
        {
            bool estado;
            try
            {
                StreamWriter sw = new StreamWriter(ruta, false);
                foreach (var item in estudiantes)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.Close();
                return estado=true;
            }
            catch (Exception e)
            {
                estado=false;
                Console.WriteLine(e.Message);
            }
            return estado;
        }


    }
}
