using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_Nucleo_Familiar_BRL
    {
        public static Int_Nucleo_Familiar Load(Int_Nucleo_Familiar _Obj, int Action)
        {
            return new Int_Nucleo_FamiliarFactory().Load(_Obj);
        }
        public static Int_Nucleo_FamiliarCollection SelectByParams(Int_Nucleo_Familiar _Obj, int Action)
        {
            return new Int_Nucleo_FamiliarFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(Int_Nucleo_Familiar _Obj, int Action)
        {
            return new Int_Nucleo_FamiliarFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_Nucleo_Familiar _Obj, int Action)
        {
            return new Int_Nucleo_FamiliarFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
