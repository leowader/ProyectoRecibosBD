using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class EstadoInscripcion : IEstado<Estudiante>
    {
        bool estadoI=false;
        RepositorioEstudiantes RepositorioEstudiantes =new RepositorioEstudiantes();
        public bool Estado(string id)
        {
            foreach (var item in RepositorioEstudiantes.Leer())
            {
                if (item.Id.Equals(id)&&item.estadoInscripcion.Equals("inscrito"))
                {
                    return estadoI=true;
                }
            }
            return estadoI;
        }
    }
}
