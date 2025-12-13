using System.Data;
using DAL;
using DCL;

namespace BRL
{
    public class Int_Tutoriales_BRL
    {
        public static Int_Tutoriales Load(Int_Tutoriales _Obj)
        {
            return new Int_TutorialesFactory().Load(_Obj);
        }

        public static Int_TutorialesCollection SelectByParams(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialesFactory().SelectByParams(_Obj, Action);
        }

        public static DataTable SelectTable(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialesFactory().SelectTable(_Obj, Action);
        }

        public static int InsertOrUpdate(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialesFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}