using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ServicioView
    {
        List<RecibosView> ListaRecibos;
        readonly Datos.ViewRecibos RutaRecibos = new Datos.ViewRecibos();
        public ServicioView()
        {
            ListaRecibos = RutaRecibos.Leer();
        }
        public void ActualizarLit()
        {
            ListaRecibos = RutaRecibos.Leer();
        }
        public List<RecibosView> Mostrar()
        {
            return ListaRecibos;
        }
    }
}
