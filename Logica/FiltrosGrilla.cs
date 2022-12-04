using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
namespace Logica
{
    public class FiltrosGrilla
    {
        List<Recibo> recibos  ;
        RepositorioRecibos RepositorioRecibos = new RepositorioRecibos();
        List<Estudiante> estudies ;
        RepositorioEstudiantes RepositorioEstudiantes=new RepositorioEstudiantes();

        public FiltrosGrilla()
        {
            recibos = RepositorioRecibos.Leer();
            estudies = RepositorioEstudiantes.Leer();
        }
        
        public List<Recibo> FiltroRecibo(string texto)
        {
            var listfiltro=new List<Recibo>();
            foreach (var item in recibos)
            {
                if (item.CodigoReferencia.StartsWith(texto))
                {
                    listfiltro.Add(item);
                }
            }
            return listfiltro;
        }
        public List<Estudiante> Filtrostudent(string texto)
        {
            var liststudent = new List<Estudiante>();
            foreach (var item in estudies)
            {
                if (item.Nombres.StartsWith(texto))
                {
                    liststudent.Add(item);
                }
            }
            return liststudent;
        }
    }
}
