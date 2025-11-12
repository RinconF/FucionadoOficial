using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class ENC_Ingreso_Encuesta_Pertenece_BRL_2
    {
        public static ENC_Ingreso_Encuesta_Pertenece_2 Load(ENC_Ingreso_Encuesta_Pertenece_2 _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory_2().Load(_Obj);
        }
        public static ENC_Ingreso_EncuestaPerteneceCollection_2 SelectByParams(ENC_Ingreso_Encuesta_Pertenece_2 _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory_2().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(ENC_Ingreso_Encuesta_Pertenece_2 _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory_2().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(ENC_Ingreso_Encuesta_Pertenece_2 _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory_2().InsertOrUpdate(_Obj, Action);
        }
    }
}
