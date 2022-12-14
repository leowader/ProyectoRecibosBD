using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ServicioRecibo: ICrud<Recibo>
    {
        List<Recibo> ListaRecibos;
        readonly Datos.RepositorioRecibos RutaRecibos = new Datos.RepositorioRecibos();
        public ServicioRecibo()
        {
            ListaRecibos = RutaRecibos.Leer();
        }
        public void ActualizarLit()
        {
            ListaRecibos = RutaRecibos.Leer();
        }
        public string Guardar(Recibo estudiante)
        {
            try
            {
                var estado = RutaRecibos.Guardar(estudiante);
                return estado ? $"RECIBO REGISTRADO" :
                $"ERROR AL REGISTRAR EL RECIBO";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<Recibo> Mostrar()
        {
            return ListaRecibos;
        }
        public string Eliminar(Recibo recibo)
        {
            ListaRecibos.Remove(recibo);
            var estado = RutaRecibos.Eliminar(recibo);
            ActualizarLit();
            return estado ? "INFORMACION DE RECIBO ELIMINADA" : "ERROR AL ELIMINAR LA INFORMACION DEL RECIBO";
        }
        public Recibo Buscar(string Referencia)
        {
            _ = new Recibo();
            foreach (var item in ListaRecibos)
            {
                if (item.CodigoReferencia.Equals(Referencia))
                {
                    Recibo Reciboestudiante = item;
                    return Reciboestudiante;
                }
            }
            return null;
        }
        public string Actualizar(Recibo recibo)
        {
            RutaRecibos.Actualizar(recibo);
            return $"actualizado";
        }
        public string Actualizar(Recibo recibo, Recibo recibo2)
        {
            Eliminar(recibo);
            Guardar(recibo);
            ActualizarLit();
            return "ok";
        }
        
        public double totalRecibo(Escuela escuela)
        {
            double suma = 0;
            string nombre2=escuela.NombreEscuela;
            foreach (var item in Mostrar())
            {
                string nombre=item.escuela.NombreEscuela;

                if (nombre.Equals(nombre2))
                {
                    suma+=item.Cantidad;
                }
            }
            return suma;
        }
        
    }
}
