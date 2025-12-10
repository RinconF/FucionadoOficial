using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DCL;

namespace BRL
{
    public class Info_Encuestas_BRL
    {
        public static DataTable ObtenerDatosEncuesta(int action, int idEncuesta)
        {
            return new Info_EncuestasFactory().EjecutarProcedimientoEncuesta(action, idEncuesta);
        }

        public static DataTable SelectTable(Info_Encuestas obj, int action)
        {
            return new Info_EncuestasFactory().EjecutarProcedimientoEncuesta(action, obj.IdEncuesta);
        }

    }
}
