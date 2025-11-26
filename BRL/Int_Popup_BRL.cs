using DAL;
using DCL;
using System.Data;

namespace BRL
{
    public class Int_Popup_BRL
    {
        public static Int_Popup Load(Int_Popup _Obj)
        {
            return new Int_PopupFactory().Load(_Obj);
        }

        public static Int_PopupCollection SelectByParams(Int_Popup _Obj, int Action)
        {
            return new Int_PopupFactory().SelectByParams(_Obj, Action);
        }

        public static DataTable SelectTable(Int_Popup _Obj, int Action)
        {
            return new Int_PopupFactory().SelectTable(_Obj, Action);
        }

        public static int InsertOrUpdate(Int_Popup _Obj, int Action)
        {
            return new Int_PopupFactory().InsertOrUpdate(_Obj, Action);
        }

        // Action 0: popups activos para un usuario
        public static Int_PopupCollection ObtenerPopupsParaUsuario(int idUsuario)
        {
            Int_Popup obj = new Int_Popup { Id_Usuario = idUsuario };
            return new Int_PopupFactory().SelectByParams(obj, 0);
        }

        // Action 7: registrar interacción (visto, clic, etc.)
        public static bool RegistrarInteraccion(int idPopup, int idUsuario, string interaccion)
        {
            Int_Popup obj = new Int_Popup
            {
                Id_Popup = idPopup,
                Id_Usuario = idUsuario,
                Interaccion = interaccion
            };

            int result = new Int_PopupFactory().InsertOrUpdate(obj, 7);
            return result > 0;
        }

        // Action 2: insertar popup con roles
        public static int InsertarPopupConRoles(Int_Popup obj)
        {
            return new Int_PopupFactory().InsertOrUpdate(obj, 2);
        }

        // Action 4: actualizar popup con roles
        public static int ActualizarPopupConRoles(Int_Popup obj)
        {
            return new Int_PopupFactory().InsertOrUpdate(obj, 4);
        }
    }
}
