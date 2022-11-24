﻿using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class RepositorioEscuela:Archivos,ICrudDatos<Escuela>
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
                command.Parameters.Add("v_id_escuela",OracleDbType.Varchar2).Value= escuela.NiT;
                command.Parameters.Add("v_nombre_es",OracleDbType.Varchar2).Value=escuela.NombreEscuela;
                command.Parameters.Add("v_direccion",OracleDbType.Varchar2).Value=escuela.Direccion;
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
        public bool Eliminar(List<Escuela> ListEscuela)
        {
            bool estado;
            try
            {
                StreamWriter sw = new StreamWriter(ruta, false);
                foreach (var item in ListEscuela)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.Close();
                return estado = true;
            }
            catch (Exception e)
            {
                estado = false;
                Console.WriteLine(e.Message);
            }
            return estado;
        }
        public Escuela buscarByname(string id)
        {
            foreach (var item in Leer())
            {
                if (item.NiT==(id))
                {
                    return item;
                }
            }
            return null;    
        }
    }
}
