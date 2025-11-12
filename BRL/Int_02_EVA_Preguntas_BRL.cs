using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_02_EVA_Preguntas_BRL
    {
        //public static Int_01_EVA_Evaluaciones Load(Int_01_EVA_Evaluaciones _Obj, int Action)
        //{
        //    return new Int_02_EVA_PreguntasFactory().Load(_Obj);
        //}
        //public static Int_02_EVA_PreguntasCollection SelectByParams(Int_01_EVA_Evaluaciones _Obj, int Action)
        //{
        //    return new Int_02_EVA_PreguntasFactory().SelectByParams(_Obj, Action);
        //}
        public static DataTable SelectTable(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_02_EVA_PreguntasFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_01_EVA_Evaluaciones _Obj, int Action)
        {
            return new Int_02_EVA_PreguntasFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}