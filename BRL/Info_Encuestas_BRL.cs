using DAL;
using DCL;
using System.Data;

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
