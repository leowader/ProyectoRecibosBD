using Entidades;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using Datos;
namespace Logica
{
    public class ServicioEscuela: ICrud<Escuela>
    {
        readonly Datos.RepositorioEscuela RutaEscuela=new Datos.RepositorioEscuela();
        List<Escuela> ListEscuelas;
        readonly RepositorioEscuela repositorioEscuela = new RepositorioEscuela();
        public ServicioEscuela()
        {
            ListEscuelas=RutaEscuela.Leer();
        }
        public void ActualizarList()
        {
            ListEscuelas = RutaEscuela.Leer();
        }
        public string Guardar(Escuela escuela)
        {
            try
            {
                var estado= RutaEscuela.Guardar(escuela);
                return estado ? "ESCUELA REGISTRADA" : "ERROR AL REGISTRAR LA ESCUELA";
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }
        }
        public string VerificarNit(Escuela escuela)
        {
            string estado = "No";
            try
            {
                foreach (var item in ListEscuelas)
                {
                    if (escuela.NiT.Equals(item.NiT))
                    {
                        estado = "Si";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return estado;
        }
        public List<Escuela> Mostrar()
        {
            try
            {
                ActualizarList();
                return ListEscuelas;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public string Eliminar(Escuela escuela)
        {
            try
            {
                var Nit = BuscarEscuela(escuela.NiT);
                ListEscuelas.Remove(Nit);
                var estado = RutaEscuela.Eliminar(escuela);
                ActualizarList();
                return estado ? $"ESCUELA ELIMINADA: {escuela.NombreEscuela}" : $"ERROR AL ELIMINAR LA ESCUELA: {escuela.NombreEscuela}";
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }
        }
        public string Actualizar(Escuela EscuelaActualizada)
        {
            try
            {
                ActualizarList();
                RutaEscuela.Actualizar(EscuelaActualizada);
                return "ESCUELA ACTUALIZADA";
            }
            catch (Exception e)
            {

                return e.Message + e.StackTrace;
            }
        }
        public Escuela BuscarEscuela(string nit)
        {
            return repositorioEscuela.buscarByNit(nit);
        }
        public double total(Escuela escuela)
        {
            RutaEscuela.totalcobro(escuela);
            return 0;
        }

        public Escuela BuscarNombre(string Nombre)
        {
            try
            {
                _ = new Escuela();
                foreach (var item in ListEscuelas)
                {
                    if (item.NombreEscuela.Equals(Nombre))
                    {
                        Escuela escuela = item;
                        return escuela;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
