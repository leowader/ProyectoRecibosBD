using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RecibosView
    {
        public string nombre { get; set; }
        public int idEstudiante { get; set; }
        public string Concepto { get; set; }
        public double valor { get; set; }
        public string CodigoReferencia { get; set; }
        public DateTime FechaLimite { get; set; }
        public string EstadoPago { get; set; }
    }
}
