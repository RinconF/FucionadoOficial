using BRL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;


namespace Intranet_3._0.Vistas.V_Comunicacion
{
    /// <summary>
    /// Web Service para gestión de documentos corporativos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebService_V_Comunicacion : System.Web.Services.WebService
    {
        #region DOCUMENTOS
        
        /// <summary>
        /// Obtiene todos los documentos activos
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string[]> ObtenerDocumentos()
        {
            try
            {
                List<string[]> list = new List<string[]>();
                Int_Documentos obj = new Int_Documentos();
                
                // Action 5: SELECT ALL - Documentos activos
                Int_DocumentosCollection documentos = Int_Documentos_BRL.SelectByParams(obj, 5);

                if (documentos != null && documentos.Count > 0)
                {
                    foreach (Int_Documentos doc in documentos)
                    {
                        string[] array = new string[8];
                        array[0] = doc.Id_Documentos?.ToString() ?? "0";
                        array[1] = doc.Titulo ?? "";
                        array[2] = doc.Descripcion ?? "";
                        array[3] = doc.Archivo ?? "";
                        array[4] = !string.IsNullOrEmpty(doc.Archivo) ? Path.GetFileName(doc.Archivo) : "Sin archivo";
                        array[5] = doc.Url ?? "";
                        array[6] = doc.FechaCreacion?.ToString("dd/MM/yyyy HH:mm") ?? "";
                        array[7] = (doc.Estado ?? false) ? "Activo" : "Inactivo";

                        list.Add(array);
                    }
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                }

                return list;
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = "Error: " + ex.Message;
                list.Add(array);
                return list;
            }
        }

        /// <summary>
        /// Obtiene un documento específico por ID
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string[]> ObtenerDocumentoPorId(string Id_Documento)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                Int_Documentos obj = new Int_Documentos();
                obj.Id_Documentos = Convert.ToInt32(Id_Documento);
                
                // Action 2: LOAD - Cargar documento por ID
                Int_Documentos doc = Int_Documentos_BRL.Load(obj);

                if (doc != null && doc.Id_Documentos != null)
                {
                    string[] array = new string[8];
                    array[0] = doc.Id_Documentos?.ToString() ?? "0";
                    array[1] = doc.Titulo ?? "";
                    array[2] = doc.Descripcion ?? "";
                    array[3] = doc.Archivo ?? "";
                    array[4] = !string.IsNullOrEmpty(doc.Archivo) ? Path.GetFileName(doc.Archivo) : "Sin archivo";
                    array[5] = doc.Url ?? "";
                    array[6] = doc.FechaCreacion?.ToString("dd/MM/yyyy HH:mm") ?? "";
                    array[7] = (doc.Estado ?? false) ? "Activo" : "Inactivo";

                    list.Add(array);
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                }

                return list;
            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = "Error: " + ex.Message;
                list.Add(array);
                return list;
            }
        }

        /// <summary>
        /// Valida un archivo antes de subirlo
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ValidarArchivo(string nombreArchivo, int tamanoBytes)
        {
            try
            {
                // Validar extensión
                string extension = Path.GetExtension(nombreArchivo).ToLower();
                string[] extensionesPermitidas = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".zip", ".rar" };

                bool extensionValida = false;
                foreach (string ext in extensionesPermitidas)
                {
                    if (ext == extension)
                    {
                        extensionValida = true;
                        break;
                    }
                }

                if (!extensionValida)
                {
                    return "Error: Extensión no permitida. Solo se permiten: PDF, DOC, DOCX, XLS, XLSX, PPT, PPTX, ZIP, RAR";
                }

                // Validar tamaño (10 MB máximo)
                int tamanoMaximo = 10 * 1024 * 1024; // 10 MB
                if (tamanoBytes > tamanoMaximo)
                {
                    return "Error: El archivo excede el tamaño máximo de 10 MB";
                }

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        #endregion

        #region METODOS ORIGINALES (NOTICIAS, POPUP, ETC)
        
        // Mantener los métodos originales del WebService_V_Comunicacion
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
        public List<object> cargar_datos_modal_actualizar_Popup(int Id_Popup)
        {
            var lista = new List<object>();

            try
            {
                var obj = new Int_Popup { Id_Popup = Id_Popup };
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 3);

                if (dt.Rows.Count == 0)
                {
                    lista.Add(new { Id_Popup = 0 });
                    return lista;
                }

                DataRow row = dt.Rows[0];

                var rolesIdsValor = row.Table.Columns.Contains("RolesIds")
                    ? row["RolesIds"]
                    : row.Table.Columns.Contains("Roles_Ids")
                        ? row["Roles_Ids"]
                        : null;

                var popupDto = new
                {
                    Id_Popup = row.Table.Columns.Contains("Id_Popup") ? row["Id_Popup"] : null,
                    Titulo = row.Table.Columns.Contains("Titulo") ? row["Titulo"] : null,
                    Descripcion = row.Table.Columns.Contains("Descripcion") ? row["Descripcion"] : null,
                    Imagen = row.Table.Columns.Contains("Imagen") ? row["Imagen"] : null,
                    Video = row.Table.Columns.Contains("Video") ? row["Video"] : null,
                    Url = row.Table.Columns.Contains("Url") ? row["Url"] : null,
                    Tiempo_Visualizacion = row.Table.Columns.Contains("Tiempo_Visualizacion") ? row["Tiempo_Visualizacion"] : null,
                    Fecha_Inicio = row.Table.Columns.Contains("Fecha_Inicio") ? row["Fecha_Inicio"] : null,
                    Fecha_Fin = row.Table.Columns.Contains("Fecha_Fin") ? row["Fecha_Fin"] : null,
                    Estado = row.Table.Columns.Contains("Estado") ? row["Estado"] : null,
                    RolesIds = rolesIdsValor
                };

                lista.Add(popupDto);
                return lista;
            }
            catch (Exception ex)
            {
                lista.Clear();
                lista.Add(new { Error = ex.Message });
                return lista;
            }
        }

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

        #endregion
    }
}