using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_04_ENC_Respuestas_BRL
    {
        //public static Int_01_ENC_Encuestas Load(Int_01_ENC_Encuestas _Obj, int Action)
        //{
        //    return new Int_01_ENC_EncuestasFactory().Load(_Obj);
        //}
        //public static ENC_Ingreso_EncuestaCollection SelectByParams(ENC_Ingreso_Encuesta _Obj, int Action)
        //{
        //    return new ENC_Ingreso_EncuestaFactory().SelectByParams(_Obj, Action);
        //}
        public static DataTable SelectTable(Int_01_ENC_Encuestas _Obj, int Action)
        {
            return new Int_04_ENC_RespuestasFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_01_ENC_Encuestas _Obj, int Action)
        {
            return new Int_04_ENC_RespuestasFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}