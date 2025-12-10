using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class ENC_Ingreso_Encuesta_BRL
    {
        public static ENC_Ingreso_Encuesta Load(ENC_Ingreso_Encuesta _Obj, int Action)
        {
            return new ENC_Ingreso_EncuestaFactory().Load(_Obj);
        }
        public static ENC_Ingreso_EncuestaCollection SelectByParams(ENC_Ingreso_Encuesta _Obj, int Action)
        {
            return new ENC_Ingreso_EncuestaFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(ENC_Ingreso_Encuesta _Obj, int Action)
        {
            return new ENC_Ingreso_EncuestaFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(ENC_Ingreso_Encuesta _Obj, int Action)
        {
            return new ENC_Ingreso_EncuestaFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
