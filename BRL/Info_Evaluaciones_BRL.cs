using DAL;
using DCL;
using System.Data;

namespace BRL
{
    public class Info_Evaluaciones_BRL
    {
        public static DataTable SelectTable(Info_Evaluaciones _Obj, int Action)
        {
            return new Info_EvaluacionesFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Info_Evaluaciones _Obj, int Action)
        {
            return new Info_EvaluacionesFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
