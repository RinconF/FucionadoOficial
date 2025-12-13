using System.Data;
using DAL;
using DCL;

namespace BRL
{
    public class Int_Documentos_BRL
    {
        public static Int_Documentos Load(Int_Documentos _Obj)
        {
            return new Int_DocumentosFactory().Load(_Obj);
        }

        public static Int_DocumentosCollection SelectByParams(Int_Documentos _Obj, int Action)
        {
            return new Int_DocumentosFactory().SelectByParams(_Obj, Action);
        }

        public static DataTable SelectTable(Int_Documentos _Obj, int Action)
        {
            return new Int_DocumentosFactory().SelectTable(_Obj, Action);
        }

        public static int InsertOrUpdate(Int_Documentos _Obj, int Action)
        {
            return new Int_DocumentosFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}