using System.Data;
using DAL;
using DCL;

namespace BRL
{
    public class Int_Aplicativos_BRL
    {
        public static Int_Aplicativo Load(Int_Aplicativo _Obj, int Action)
        {
            return new Int_AplicativoFactory().Load(_Obj);
        }

        public static Int_AplicativoCollection SelectByParams(Int_Aplicativo _Obj, int Action)
        {
            return new Int_AplicativoFactory().SelectByParams(_Obj, Action);
        }

        public static DataTable SelectTable(Int_Aplicativo _Obj, int Action)
        {
            return new Int_AplicativoFactory().SelectTable(_Obj, Action);
        }

        public static int InsertOrUpdate(Int_Aplicativo _Obj, int Action)
        {
            return new Int_AplicativoFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
