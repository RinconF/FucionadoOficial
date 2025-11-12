using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_Noticias_BRL
    {
        public static Int_Noticias Load(Int_Noticias _Obj, int Action)
        {
            return new Int_NoticiasFactory().Load(_Obj);
        }
        public static Int_NoticiasCollection SelectByParams(Int_Noticias _Obj, int Action)
        {
            return new Int_NoticiasFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(Int_Noticias _Obj, int Action)
        {
            return new Int_NoticiasFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_Noticias _Obj, int Action)
        {
            return new Int_NoticiasFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
