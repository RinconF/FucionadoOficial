using DAL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRL
{
    public class Int_04_EVA_Respuestas_BRL
    {
        //public static Int_06_EVA_Ingreso_Evaluacion Load(Int_01_EVA_Evaluaciones _Obj, int Action)
        //{
        //    return new Int_04_EVA_RespuestasFactory().Load(_Obj);
        //}
        //public static Int_01_EVA_EvaluacionesCollection SelectByParams(Int_01_EVA_Evaluaciones _Obj, int Action)
        //{
        //    return new Int_04_EVA_RespuestasFactory().SelectByParams(_Obj, Action);
        //}
        public static DataTable SelectTable(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_04_EVA_RespuestasFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_04_EVA_RespuestasFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}