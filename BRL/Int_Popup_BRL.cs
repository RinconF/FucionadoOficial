using DAL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Obtiene popups activos para un usuario específico
        /// </summary>
        public static Int_PopupCollection ObtenerPopupsParaUsuario(int idUsuario)
        {
            Int_Popup obj = new Int_Popup { Id_Usuario = idUsuario };
            return new Int_PopupFactory().SelectByParams(obj, 0);
        }

        /// <summary>
        /// Registra que un usuario vio un popup
        /// </summary>
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

        /// <summary>
        /// Inserta un nuevo popup con roles
        /// </summary>
        public static int InsertarPopupConRoles(Int_Popup obj)
        {
            return new Int_PopupFactory().InsertOrUpdate(obj, 2);
        }

        /// <summary>
        /// Actualiza un popup existente con roles
        /// </summary>
        public static int ActualizarPopupConRoles(Int_Popup obj)
        {
            return new Int_PopupFactory().InsertOrUpdate(obj, 4);
        }
    }
}