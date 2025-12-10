using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_11_EVA_Resultados_BRL
    {
        //public static Int_01_ENC_Encuestas Load(Int_01_EVA_Evaluaciones _Obj, int Action)
        //{
        //    return new Int_01_EVA_EvaluacionesFactory().Load(_Obj);
        //}
        //public static Int_01_EVA_EvaluacionesCollection SelectByParams(Int_01_EVA_Evaluaciones _Obj, int Action)
        //{
        //    return new Int_01_EVA_EvaluacionesCollection().SelectByParams(_Obj, Action);
        //}
        public static DataTable SelectTable(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_11_EVA_ResultadosFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_11_EVA_ResultadosFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}