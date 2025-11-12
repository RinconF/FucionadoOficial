using System.Data;
using DCL;
using DAL;
namespace BRL{
    public class Int_Usuarios_BRL{
        public static Int_Usuarios Load(Int_Usuarios _Obj, int Action){
            return new Int_UsuariosFactory().Load(_Obj);
        }
        public static Int_UsuariosCollection SelectByParams(Int_Usuarios _Obj, int Action){
            return new Int_UsuariosFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(Int_Usuarios _Obj, int Action){
            return new Int_UsuariosFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_Usuarios _Obj, int Action){
            return new Int_UsuariosFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}