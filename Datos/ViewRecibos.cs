using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;

namespace Datos
{
    public class ViewRecibos:Archivos
    {
        OracleConnection Connection;
        OracleCommand Command;
        public List<RecibosView> Leer()
        {
            try
            {
                List<RecibosView> ListRecibos = new List<RecibosView>();
                abrirBD();
                Connection = Miconexion();
                Command = new OracleCommand("select * from recibosgenerados", Connection);
                Command.ExecuteNonQuery();
                var raid = Command.ExecuteReader();
                while (raid.Read())
                {
                    ListRecibos.Add(Mapear(raid));
                }
                cerrarBD();
                return ListRecibos;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public RecibosView Mapear(OracleDataReader Fila)
        {
            var recibo = new RecibosView();
            recibo.nombre = Fila.GetString(0);
            recibo.idEstudiante = int.Parse(Fila.GetString(1));
            recibo.CodigoReferencia = Fila.GetString(2);
            recibo.valor= Fila.GetInt32(3);
            recibo.Concepto = Fila.GetString(4);
            recibo.FechaLimite = DateTime.Parse(Fila.GetString(5));
            recibo.EstadoPago = Fila.GetString(6);
            return recibo;
        }
    }
}
