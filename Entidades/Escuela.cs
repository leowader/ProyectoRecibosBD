using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Escuela
    {
        public Escuela()
        {
        }
        public Escuela(string nombreEscuela, string direccion, string niT, string telefono, string correo)
        {
            NombreEscuela = nombreEscuela;
            Direccion = direccion;
            NiT = niT;
            Telefono = telefono;
            Correo = correo;
        }
        public string NombreEscuela { get; set; }
        public string Direccion { get; set; }
        public string NiT { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public double totalCobro { get; set; }

        public Persona EscuelaPersona
        {
            get => default;
            set
            {
            }
        }
        public override string ToString()
        {
            return $"{NiT}";
        }
    }
}
