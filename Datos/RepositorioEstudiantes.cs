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
                StreamReader sr = new StreamReader(ruta);
                while (!sr.EndOfStream)
                {
                    listaEstudent.Add(Mapear(sr.ReadLine()));
                }
                sr.Close();
                return listaEstudent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            return null;
        }


        public Estudiante Mapear(string linea)
        {
            var estudiante = new Estudiante
            {
                //Nombres = linea.Trim().Split(';')[0],
                //Apellidos = linea.Trim().Split(';')[1],
                //Sexo = char.Parse(linea.Trim().Split(';')[2]),
                //Id = int.Parse(linea.Trim().Split(';')[3]),
                //curso = linea.Trim().Split(';')[4],
                //Grado = linea.Trim().Split(';')[5],
                //PeriodoEstudio = linea.Trim().Split(';')[6],
                //idescuela = linea.Trim().Split(';')[7],
                //TieneRecibo = bool.Parse(linea.Trim().Split(';')[8]),
            };
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
