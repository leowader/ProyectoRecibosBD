using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Estudiante : Persona
    {
        public Estudiante()
        {
        }
        public string curso { get; set; }
        public string Grado { get; set; }
        public string PeriodoEstudio { get; set; }
        public string idescuela { get; set; }
        public char TieneRecibo { get; set; }
        public string codigoCurso { get; set; }
        public Escuela Escuela { get; set; }
        public override string ToString()
        {
            return $"{Nombres};{Apellidos};{Sexo};{Id}" +
                $";{curso};{Grado};{PeriodoEstudio};{idescuela};{TieneRecibo}";
        }
    }
}
