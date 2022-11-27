using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace Datos
{
    public interface ICrudDatos<Tipo>
    {
        bool Guardar(Tipo tipo);
        List<Tipo> Leer();
        bool Actualizar(Tipo tipo);
        Tipo Mapear(OracleDataReader linea);
        bool Eliminar(Tipo tipo);
    }
}
