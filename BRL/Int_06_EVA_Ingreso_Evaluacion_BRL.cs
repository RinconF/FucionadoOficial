using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_06_EVA_Ingreso_Evaluacion_BRL
    {
        //public static  Int_06_EVA_Ingreso_Evaluacion Load( Int_06_EVA_Ingreso_Evaluacion _Obj, int Action)
        //{
        //    return new  Int_06_EVA_Ingreso_EvaluacionFactory().Load(_Obj);
        //}
        //public static  Int_06_EVA_Ingreso_EvaluacionCollection SelectByParams( Int_06_EVA_Ingreso_Evaluacion _Obj, int Action)
        //{
        //    return new  Int_06_EVA_Ingreso_EvaluacionFactory().SelectByParams(_Obj, Action);
        //}
        public static DataTable SelectTable(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_06_EVA_Ingreso_EvaluacionFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_06_EVA_Ingreso_EvaluacionFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
