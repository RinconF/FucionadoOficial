using BRL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService_V_Comunicacion : System.Web.Services.WebService
    {
        //vistas
        [WebMethod]
        public List<string[]> cargar_datos_modal_actualizar_noticia(string Id_Noticia)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Noticias obj = new DCL.Int_Noticias();
                obj.Id_Noticia = Convert.ToInt32(Id_Noticia);
                dt = Int_Noticias_BRL.SelectTable(obj, 10);
                if (dt.Rows.Count > 0)
                {
                    string[] array = new string[5];
                    array[0] = dt.Rows[0]["Id_Noticia"].ToString();
                    array[1] = dt.Rows[0]["Titulo"].ToString();
                    array[2] = dt.Rows[0]["Descripcion"].ToString();
                    array[3] = dt.Rows[0]["Imagen"].ToString();
                    array[4] = dt.Rows[0]["Estado"].ToString();

                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[dt.Rows.Count];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        //vistas
        [WebMethod]
        public List<string[]> cargar_datos_modal_actualizar_slidernoticia(string Id_Noticia)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Noticias obj = new DCL.Int_Noticias();
                obj.Id_Noticia = Convert.ToInt32(Id_Noticia);
                dt = Int_Noticias_BRL.SelectTable(obj, 12);
                if (dt.Rows.Count > 0)
                {
                    string[] array = new string[4];
                    array[0] = dt.Rows[0]["Id_SlideShow"].ToString();
                    array[1] = dt.Rows[0]["Descripcion"].ToString();
                    array[2] = dt.Rows[0]["Imagen"].ToString();
                    array[3] = dt.Rows[0]["Visibilidad"].ToString();

                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[dt.Rows.Count];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        #region POPUP
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string[]> cargar_datos_modal_actualizar_Popup(int Id_Popup)
        {
            var list = new List<string[]>();

            try
            {
                var obj = new Int_Popup { Id_Popup = Id_Popup };
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 3); // Action 3

                if (dt.Rows.Count == 0)
                {
                    list.Add(new[] { "0" });
                    return list;
                }

                DataRow row = dt.Rows[0];
                string[] array = new string[12];
                for (int i = 0; i < 12; i++)
                    array[i] = row[i].ToString();

                list.Add(array);
                return list;
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add(new[] { ex.Message });
                return list;
            }
        }

        // Estadísticas (Action 8)
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string[]> Obtener_Estadisticas_Popup(int Id_Popup)
        {
            var list = new List<string[]>();

            try
            {
                var obj = new Int_Popup { Id_Popup = Id_Popup };
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 8);

                foreach (DataRow row in dt.Rows)
                {
                    string[] arr = new string[3];
                    arr[0] = row["Tipo_Interaccion"].ToString();
                    arr[1] = row["Cantidad"].ToString();
                    arr[2] = row["Porcentaje"].ToString();
                    list.Add(arr);
                }

                return list;
            }
            catch (Exception ex)
            {
                list.Clear();
                list.Add(new[] { ex.Message });
                return list;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Registrar_Interaccion_Popup(int Id_Popup, int Id_Usuario, string Interaccion)
        {
            Int_Popup_BRL.RegistrarInteraccion(Id_Popup, Id_Usuario, Interaccion);
        }
        #endregion
    }
}
