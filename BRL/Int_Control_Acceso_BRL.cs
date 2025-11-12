using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_Control_Acceso_BRL
    {
        public static Int_Control_Acceso Load(Int_Control_Acceso _Obj, int Action)
        {
            return new Int_Control_AccesoFactory().Load(_Obj);
        }
        public static Int_Control_AccesoCollection SelectByParams(Int_Control_Acceso _Obj, int Action)
        {
            return new Int_Control_AccesoFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(Int_Control_Acceso _Obj, int Action)
        {
            return new Int_Control_AccesoFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_Control_Acceso _Obj, int Action)
        {
            return new Int_Control_AccesoFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
