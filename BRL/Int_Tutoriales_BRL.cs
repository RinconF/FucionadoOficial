using System.Data;
using DAL;
using DCL;

namespace BRL
{
    public class Int_Tutoriales_BRL
    {
        public static Int_Tutoriales Load(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialFactory().Load(_Obj);
        }

        public static Int_TutorialesCollection SelectByParams(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialFactory().SelectByParams(_Obj, Action);
        }

        public static DataTable SelectTable(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialFactory().SelectTable(_Obj, Action);
        }

        public static int InsertOrUpdate(Int_Tutoriales _Obj, int Action)
        {
            return new Int_TutorialFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
