using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;

namespace Datos
{
    public class RepositorioRecibos:ConexionBd,ICrudDatos<Recibo>
    {
        bool Estado;
        OracleCommand Command;
        OracleConnection Connection;
        public bool Guardar(Recibo recibo)
        {
            try
            {
                abrirBD();
                Connection = Miconexion();
                Command = new OracleCommand("matriculapackage.insert_matricula", Connection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.Add("v_idmatricula", OracleDbType.Int32).Value = 0;
                Command.Parameters.Add("v_valor", OracleDbType.Varchar2).Value = recibo.Cantidad;
                Command.Parameters.Add("fechamatricula", OracleDbType.Date).Value = DateTime.Now;
                Command.Parameters.Add("v_idestudiante", OracleDbType.Varchar2).Value = recibo.estudiante.Id;
                Command.ExecuteNonQuery();
                Command = new OracleCommand("reciboPackage.insert_recibo", Connection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.Add("v_idrecibo", OracleDbType.Varchar2).Value = recibo.CodigoReferencia;
                Command.Parameters.Add("v_impression", OracleDbType.Date).Value = DateTime.Now;
                Command.Parameters.Add("v_observacion", OracleDbType.Varchar2).Value = recibo.Observaciones;
                Command.Parameters.Add("v_fechalimite", OracleDbType.Date).Value = recibo.FechaLimite;
                Command.Parameters.Add("v_fechaextra", OracleDbType.Date).Value = recibo.FechaExtraordinaria;
                Command.Parameters.Add("v_banco", OracleDbType.Varchar2).Value = recibo.Banco;
                Command.Parameters.Add("v_concepto", OracleDbType.Varchar2).Value = recibo.Concepto;
                Command.Parameters.Add("v_estado", OracleDbType.Varchar2).Value = recibo.EstadoPago;
                Command.ExecuteNonQuery();
                cerrarBD();
                Estado = true;
            }
            catch (Exception)
            {
                Estado = false;
            }
            return Estado;
        }
        public List<Recibo> Leer()
        {
            try
            {
                List<Recibo> ListRecibos = new List<Recibo>();
                abrirBD();
                Connection = Miconexion();
                Command = new OracleCommand("select * from recibosgenerados", Connection);
                Command.ExecuteNonQuery();
                var raid = Command.ExecuteReader();
                while (raid.Read())
                {
                    if (raid!=null)
                    {
                        ListRecibos.Add(Mapear(raid));
                    }
                }
                cerrarBD();
                return ListRecibos;
            }
            catch (Exception )
            {
                return null;
            }
        }
        RepositorioEstudiantes RepositorioEstudiantes = new RepositorioEstudiantes();
        RepositorioEscuela repositorioEscuela = new RepositorioEscuela();

        public Recibo Mapear(OracleDataReader Fila)
        {
            var recibo = new Recibo();
            recibo.escuela =repositorioEscuela.buscarByNit(Fila.GetString(0));
            recibo.estudiante =RepositorioEstudiantes.BuscarEstudiante( int.Parse(Fila.GetString(1)));
            recibo.CodigoReferencia = Fila.GetString(2);
            recibo.Cantidad = Fila.GetInt32(3);
            recibo.Concepto=Fila.GetString(4);
            recibo.FechaLimite = DateTime.Parse(Fila.GetString(5));
            recibo.EstadoPago= Fila.GetString(6);
            recibo.Banco = Fila.GetString(7);
            recibo.Observaciones=Fila.GetString(8);
            return recibo;
        }
        public bool Eliminar(Recibo recibo)
        {
            bool estado;
            try
            {
                abrirBD();
                Connection = Miconexion();
                Command = new OracleCommand("reciboPackage.eliminar_recibo", Connection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.Add("v_id",OracleDbType.Varchar2).Value=recibo.CodigoReferencia;
                Command.ExecuteNonQuery();
                cerrarBD();
                estado = true;
            }
            catch (Exception e)
            {
                estado = false;

                Console.WriteLine(e.Message);
            }
            return estado;
        }
        public bool Actualizar(Recibo recibo)
        {
            abrirBD();
            Connection = Miconexion();
            Command = new OracleCommand("recibopackage.upd_estado", Connection);
            Command.CommandType = System.Data.CommandType.StoredProcedure;
            Command.Parameters.Add("v_id", OracleDbType.Varchar2).Value = recibo.CodigoReferencia;
            Command.Parameters.Add("v_estado", OracleDbType.Varchar2).Value = recibo.EstadoPago;
            Command.ExecuteNonQuery();
            cerrarBD();
            return true;
        }
    }
}
