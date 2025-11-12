using System.Data;
using DCL;
using DAL;

namespace BRL
{
    public class Int_Notificaciones_BRL
    {
        public static Int_Notificaciones Load(Int_Notificaciones _Obj, int Action)
        {
            return new Int_NotificacionesFactory().Load(_Obj);
        }
        public static Int_NotificacionesCollection SelectByParams(Int_Notificaciones _Obj, int Action)
        {
            return new Int_NotificacionesFactory().SelectByParams(_Obj, Action);
        }
        public static DataTable SelectTable(Int_Notificaciones _Obj, int Action)
        {
            return new Int_NotificacionesFactory().SelectTable(_Obj, Action);
        }
        public static int InsertOrUpdate(Int_Notificaciones _Obj, int Action)
        {
            return new Int_NotificacionesFactory().InsertOrUpdate(_Obj, Action);
        }
    }
}
