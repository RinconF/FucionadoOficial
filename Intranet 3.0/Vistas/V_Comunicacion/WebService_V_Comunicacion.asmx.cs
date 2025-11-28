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

        // Lista para la tabla (Action 1)
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string[]> Obtener_Popups_Para_Grid()
        {
            var list = new List<string[]>();

            try
            {
                var obj = new Int_Popup();
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 1);

                foreach (DataRow row in dt.Rows)
                {
                    // ajusta el tamaño si en el front ocupas más/menos columnas
                    string[] arr = new string[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                        arr[i] = row[i].ToString();

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


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<object> Obtener_Popups_Usuario(int Id_Usuario)
        {
            var respuesta = new List<object>();
            try
            {
                var obj = new Int_Popup { Id_Usuario = Id_Usuario };
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 0); // Action 0: popups para usuario

                foreach (DataRow row in dt.Rows)
                {
                    string imagen = row.Table.Columns.Contains("Imagen") ? row["Imagen"].ToString() : null;
                    string video = row.Table.Columns.Contains("Video") ? row["Video"].ToString() : null;

                    string rutaPublica = !string.IsNullOrWhiteSpace(video)
                        ? ResolverRutaPublicaPopup(video)
                        : ResolverRutaPublicaPopup(imagen);

                    respuesta.Add(new
                    {
                        Id_Popup = row["Id_Popup"],
                        Titulo = row.Table.Columns.Contains("Titulo") ? row["Titulo"] : null,
                        Descripcion = row.Table.Columns.Contains("Descripcion") ? row["Descripcion"] : null,
                        Url = row.Table.Columns.Contains("Url") ? row["Url"] : null,
                        Tiempo_Visualizacion = row.Table.Columns.Contains("Tiempo_Visualizacion") ? row["Tiempo_Visualizacion"] : null,
                        Fecha_Inicio = row.Table.Columns.Contains("Fecha_Inicio") ? row["Fecha_Inicio"] : null,
                        Fecha_Fin = row.Table.Columns.Contains("Fecha_Fin") ? row["Fecha_Fin"] : null,
                        Imagen = imagen,
                        Video = video,
                        RutaMultimedia = rutaPublica,
                        Tipo = !string.IsNullOrWhiteSpace(video) ? "video" : "imagen",
                        Estado = row.Table.Columns.Contains("Estado") ? row["Estado"] : null
                    });
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Clear();
                respuesta.Add(new { Error = ex.Message });
                return respuesta;
            }
        }

        /// <summary>
        /// Convierte la ruta UNC almacenada para un popup a una URL accesible desde la UI.
        /// No modifica la ruta guardada, solo entrega una versión navegable basada en la carpeta local de imágenes.
        /// </summary>
        private string ResolverRutaPublicaPopup(string rutaRemota)
        {
            if (string.IsNullOrWhiteSpace(rutaRemota) || HttpContext.Current == null)
                return null;

            string ambiente = ConfigurationManager.AppSettings.Get("ambiente") ?? "DESA";
            string baseRemota = ConfigurationManager.AppSettings.Get("pathRemote") ?? string.Empty;

            string segmentoDesdeAmbiente = null;
            int idx = rutaRemota.IndexOf(ambiente, StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                segmentoDesdeAmbiente = rutaRemota.Substring(idx);
            }
            else if (!string.IsNullOrWhiteSpace(baseRemota) && rutaRemota.StartsWith(baseRemota, StringComparison.OrdinalIgnoreCase))
            {
                segmentoDesdeAmbiente = rutaRemota.Substring(baseRemota.Length);
            }

            if (string.IsNullOrWhiteSpace(segmentoDesdeAmbiente))
                return null;

            string rutaRelativa = $"~/Imagenes/{segmentoDesdeAmbiente.Replace("\\", "/")}";
            return VirtualPathUtility.ToAbsolute(rutaRelativa);
        }
    }
}
