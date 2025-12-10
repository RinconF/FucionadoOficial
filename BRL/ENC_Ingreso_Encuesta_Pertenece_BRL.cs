using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class ENC_Ingreso_Encuesta_Pertenece_BRL
    {
        public static ENC_Ingreso_Encuesta_Pertenece Load(ENC_Ingreso_Encuesta_Pertenece _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory().Load(_Obj);
        }
        public static ENC_Ingreso_EncuestaPerteneceCollection SelectByParams(ENC_Ingreso_Encuesta_Pertenece _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(ENC_Ingreso_Encuesta_Pertenece _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(ENC_Ingreso_Encuesta_Pertenece _Obj, int Action)
        {
            return new ENC_Ingreso_Encuesta_PerteneceFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
