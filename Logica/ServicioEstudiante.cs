using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
namespace Logica
{
    public class ServicioEstudiante:ICrud<Estudiante>
    {
        List<Estudiante> ListaEstudiantes ;
        readonly Datos.RepositorioEstudiantes RutaEstudiantes = new Datos.RepositorioEstudiantes();
        readonly RepositorioEstudiantes repositorioEstudiantes = new RepositorioEstudiantes();
        public ServicioEstudiante()
        {
            ListaEstudiantes = RutaEstudiantes.Leer();
        }


        public void ActualizarLit()
        {
            ListaEstudiantes = RutaEstudiantes.Leer();
        }


        public List<Estudiante> Mostrar()
        {
            return repositorioEstudiantes.Leer();
        }

        public string Guardar(Estudiante estudiante)
        {
            try
            {

                var estado = RutaEstudiantes.Guardar(estudiante);
                if (ListaEstudiantes != null)
                {
                    VerificarId(estudiante);
                }
                
                return estado ? $"ESTUDIANTE GUARDADO CON NOMBRE: {estudiante.Nombres}" :
                $"ERROR AL GUARDAR EL ESTDIANTE :{estudiante.Nombres.ToUpper()}";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public string VerificarId(Estudiante estudiante)
        {
            string estado = "No";
            try
            {
                
                foreach (var item in ListaEstudiantes)
                {
                    if (estudiante.Id.Equals(item.Id))
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


        public string Actualizar(Estudiante estudiante)
        {
            repositorioEstudiantes.actualizar(estudiante);
            ActualizarLit();
            return $"ESTUDIANTE ACTUALIZADO:{estudiante.Nombres}";
        }


        public string Eliminar(Estudiante estudiante)
        {
            //var estudianteEncon = Buscar(estudiante.Id);
            ListaEstudiantes.Remove(estudiante);
            var estado = RutaEstudiantes.Eliminar(estudiante);
            ActualizarLit();
            return estado ?$"ESTUDIANTE ELIMINADO{estudiante.Nombres}":"ERROR AL ELIMINAR EL ETUDIANTE";
        }


        public Estudiante Buscar(int id)
        {
            _ = new Estudiante();
            foreach (var item in ListaEstudiantes)
            {
                if (item.Id.Equals(id))
                {
                    Estudiante estudiante = item;
                    return estudiante;
                }
            }
            return null;
        }
        
    }
}
