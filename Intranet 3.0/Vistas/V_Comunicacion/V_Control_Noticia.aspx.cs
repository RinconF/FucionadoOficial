using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Intranet_3._0.Interna;
using Newtonsoft.Json;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Noticia : System.Web.UI.Page
    {
        string pathLog = "";
        string ipServer = "";
        const string CONST_ERRORCONEXIONSERV = "al intentar conectarse al servidor: ";
        const string CONST_ERRORPERMISOS = "al intentar acceder a archivos. ACCESO DENEGADO. ";
        const string CONST_ERROR = " - ERROR: ";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["Id_Usuario"] != null)
                {
                    if (Response.Cookies.Count > 0 && Session["cerrar"] != null)
                    {
                        //if (!IsPostBack)
                        //{
                            cargar_tabla_vista();
                        //}

                        ScriptManager.RegisterStartupScript(
                                    PanelUpdate,
                                    this.GetType(),
                                    "MyAction",
                                    "ejecutarDatos();",
                                    true);
                    }
                }
                else
                {
                    Page.Response.Redirect("~/Login", true);
                }
            }
            catch (Exception)
            {
                Page.Response.Redirect("~/Login", true);
            }
        }

        public void Crear_nueva_publicacion(object sender, EventArgs e)
        {
            string imagenNoticiaRemoto = "";
            string imagenNoticiaLocal = "";
            AG_Utils utilidades = new AG_Utils();

            try
            {
                pathLog = Server.MapPath(@"~/logs");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string rutaNoticiasRemoto = $@"{pathRemote}publicaciones\Noticias\";
                string rutaNoticiasLocal = pathServer + ambiente + $@"\intranet\publicaciones\Noticias\";
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();
                int consecutivo;
                DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                noticias.Titulo = txt_titulo.Text;
                noticias.Descripcion = txt_descripcion.Text;

                if (fud_Adjunto.HasFile)
                {
                    var archivoAguardar = fud_Adjunto;
                    

                    if (conectaAdjuntos)
                    {
                        DataTable dataTable = BRL.Int_Noticias_BRL.SelectTable(noticias, 17);
                        
                        string Resultado = dataTable.Rows[0][0].ToString();
                        if (System.String.IsNullOrEmpty(Resultado))
                        {
                            consecutivo = 1;
                        }
                        else
                        {
                            consecutivo = (int)Convert.ToInt64(dataTable.Rows[0].ItemArray.GetValue(0));
                            consecutivo++;
                        }


                        string nombreOriginalArchivo = txt_titulo.Text;
                        string extensionArchivo = Path.GetExtension(fud_Adjunto.FileName);
                        string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(nombreOriginalArchivo, consecutivo.ToString(), extensionArchivo);
                        int actionInsertNoticia = 3;

                        var (guardaImagenLocal, guardaImagenRemota, rutaNoticiaRemoto) = utilidades.TratamientoNoticias(nombreFinalArchivo, consecutivo.ToString(), rutaNoticiasLocal, rutaNoticiasRemoto, fud_Adjunto, Id_Usuario, pathLog);
                        imagenNoticiaRemoto = rutaNoticiaRemoto;
                        if (guardaImagenRemota && guardaImagenLocal && !string.IsNullOrEmpty(imagenNoticiaRemoto))
                        {
                            noticias.Imagen = imagenNoticiaRemoto;
                            BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionInsertNoticia);
                        }
                        else
                        {
                            utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nRegistro NOTICIA no almacenado en BD. Los archivos no fueron almacenados.", pathLog);
                            if (File.Exists(imagenNoticiaRemoto))
                                File.Delete(imagenNoticiaRemoto);
                            if (File.Exists(imagenNoticiaLocal))
                                File.Delete(imagenNoticiaLocal);
                        }
                        //imagenNoticiaRemoto = utilidades.TratamientoNoticias(nombreFinalArchivo, consecutivo.ToString(), rutaNoticiasLocal, rutaNoticiasRemoto, actionInsertSP, noticias, ambiente, "Remoto", fud_Adjunto);




                        //if (imagenNoticiaRemoto != "")
                        //{
                        //    noticias.Imagen = imagenNoticiaRemoto;
                        //    BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionInsertSP);
                        //    fud_Adjunto.SaveAs(imagenNoticiaRemoto);
                        //    if (imagenNoticiaLocal.Length > 1)
                        //    {
                        //        fud_Adjunto.SaveAs(imagenNoticiaLocal);
                        //    }
                        //}

















                        //bool bl_cargaNoticiaLocal = utilidades.validarArchivoNoticia(rutaNoticiasLocal, consecutivo, imagenNoticiaLocal, pathLog);
                        //bool bl_CargaNoticiaRemota = utilidades.validarArchivoNoticia(RutaNoticiasRemoto, consecutivo, archivoNoticiaRemoto, pathLog);

                        //if (bl_cargaNoticiaLocal)
                        //{
                        //    fud_Adjunto.SaveAs(imagenNoticiaLocal);
                        //}

                        //if (bl_CargaNoticiaRemota)
                        //{
                        //    fud_Adjunto.SaveAs(archivoNoticiaRemoto);
                        //}
                        //noticias.Id_Noticia = consecutivo;


                        //if (bl_cargaNoticiaLocal && !bl_CargaNoticiaRemota)
                        //{
                        //    File.Copy(imagenNoticiaLocal, archivoNoticiaRemoto, true);
                        //}




                        //noticias.Imagen = archivoNoticiaRemoto;
                        //if (bl_cargaNoticiaLocal || bl_CargaNoticiaRemota)
                        //{
                        //    BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, 3);
                        //}
                        //utilidades.undoImpersonation();
                    }
                    else
                    {
                        utilidades.logError($"{CONST_ERRORCONEXIONSERV} {ipServer}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario: {Id_Usuario}", pathLog);
                    }
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            
            catch (Exception ex)
            {
                utilidades.logError($"{CONST_ERROR} {System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}\nLos archivos NOTICIA no fueron almacenados.", pathLog);
                if (utilidades.impersonateValidUser())
                {
                    if (File.Exists(imagenNoticiaRemoto))
                        File.Delete(imagenNoticiaRemoto);
                    if (File.Exists(imagenNoticiaLocal))
                        File.Delete(imagenNoticiaLocal);
                    utilidades.undoImpersonation();
                }
                
                
            }
        }

        protected void Actualizar_datos_publicacion(object sender, EventArgs e)
        {  
            string imagenNoticiaRemoto = "";
            string imagenNoticiaLocal = "";
            AG_Utils utilidades = new AG_Utils();
            try
            {
                pathLog = Server.MapPath(@"~/logs");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string rutaNoticiasRemoto = $@"{pathRemote}publicaciones\Noticias\";
                string rutaNoticiasLocal = pathServer + ambiente + $@"\intranet\publicaciones\Noticias\";
                string consecutivo = $"{Request.Form["rd_estado_vista"]}";
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();
                DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                noticias.Id_Noticia = (int?)Convert.ToInt64(consecutivo);
                noticias.Descripcion = txt_descripcion_pub.Text;
                noticias.Titulo = txt_titulo_pub.Text;
                int actionActualizarNoticia = 7;

                if (fud_Adjunto_pub.HasFile)
                {
                    if (conectaAdjuntos)
                    {
                        string nombreOriginalArchivo = txt_titulo_pub.Text;
                        string extensionArchivo = Path.GetExtension(fud_Adjunto_pub.FileName);
                        //ajusteNombreArchivos(string nombreOriginalArchivo, string extensionArchivo);

                        //string nombreArchivoNorm = Regex.Replace(nombreOriginalArchivo.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                        //string nombreAjustadoArchivoSE = nombreArchivoNorm.Replace(" ", "_");
                        //nombreAjustadoArchivoSE = $"{consecutivo}-{nombreAjustadoArchivoSE}";
                        //int actionSelectSP = 8;
                        //int selectArchivoRemoto = 3;
                        string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(nombreOriginalArchivo, consecutivo.ToString(), extensionArchivo);
                       
                        var (guardaImagenLocal, guardaImagenRemota, rutaNoticiaRemoto) = utilidades.TratamientoNoticias(nombreFinalArchivo, consecutivo.ToString(), rutaNoticiasLocal, rutaNoticiasRemoto, fud_Adjunto_pub, Id_Usuario, pathLog);
                        imagenNoticiaRemoto = rutaNoticiaRemoto;
                        if (guardaImagenRemota && guardaImagenLocal && !string.IsNullOrEmpty(imagenNoticiaRemoto))
                        {
                            noticias.Imagen = imagenNoticiaRemoto;
                            BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionActualizarNoticia);
                        }
                        else
                        {
                            utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nActualización de registro no almacenado en BD. Los archivos no fueron almacenados.", pathLog);
                            if (utilidades.impersonateValidUser())
                            {
                                if (File.Exists(imagenNoticiaRemoto))
                                    File.Delete(imagenNoticiaRemoto);
                                if (File.Exists(imagenNoticiaLocal))
                                    File.Delete(imagenNoticiaLocal);
                                utilidades.undoImpersonation();
                            }
                            //throw new Exception();
                        }
                        //imagenNoticiaLocal = utilidades.TratamientoNoticias(nombreFinalArchivo, consecutivo, rutaNoticiasLocal, rutaNoticiasRemoto, actionInsertSP, noticias, ambiente, "Local", fud_Adjunto_pub);
                        //imagenNoticiaLocal = utilidades.TratamientoNoticias(nombreFinalArchivo, consecutivo, rutaNoticiasLocal, rutaNoticiasRemoto, actionInsertSP, noticias, ambiente, "Remoto", fud_Adjunto_pub);

                        //if (imagenNoticiaRemoto !="")
                        //{
                        //    noticias.Imagen = imagenNoticiaRemoto;
                        //    BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionInsertSP);
                        //    fud_Adjunto_pub.SaveAs(imagenNoticiaRemoto);
                        //    if (imagenNoticiaLocal.Length > 1)
                        //    {
                        //        fud_Adjunto_pub.SaveAs(imagenNoticiaLocal);
                        //    }
                        //}
                        //utilidades.undoImpersonation();
                    }
                    else
                    {
                        utilidades.logError($"{CONST_ERROR}{CONST_ERRORCONEXIONSERV} {ipServer}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario:  {Id_Usuario}", pathLog);
                    }

                    
                }

                //if(Session["imagen"] != null && !fud_Adjunto_pub.HasFile)
                //{
                //    noticias.Imagen = Session["imagen"].ToString();
                //    BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionActualizarNoticia);
                //}
                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nRegistro NOTICIA no almacenado en BD. Los archivos no fueron almacenados.", pathLog);
                if (utilidades.impersonateValidUser())
                {
                    if (File.Exists(imagenNoticiaRemoto))
                        File.Delete(imagenNoticiaRemoto);
                    if (File.Exists(imagenNoticiaLocal))
                        File.Delete(imagenNoticiaLocal);
                    utilidades.undoImpersonation();
                }
                throw ex;
            }
        }
        //Se crea contador para evitar repeticiones
        bool consultado = false;
        protected void cargar_tabla_vista()
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                if (consultado == false)
                {
                    tbl_grupos.Rows.Clear();
                    HtmlTableCell N = new HtmlTableCell();
                    HtmlTableCell Id = new HtmlTableCell();
                    HtmlTableCell Titulo = new HtmlTableCell();
                    HtmlTableCell Descripcion = new HtmlTableCell();
                    HtmlTableCell Fecha_creacion = new HtmlTableCell();
                    HtmlTableCell Accion = new HtmlTableCell();

                    HtmlTableRow th = new HtmlTableRow();

                    N.InnerText = "#";
                    Id.InnerText = "ID";
                    Titulo.InnerText = "TITULO";
                    Descripcion.InnerText = "DESCRIPCION";
                    Fecha_creacion.InnerText = "FECHA DE CREACION";
                    Accion.InnerText = "ACCION";
                    th.Attributes.Add("class", "th");

                    th.Cells.Add(N);
                    th.Cells.Add(Id);
                    th.Cells.Add(Titulo);
                    th.Cells.Add(Descripcion);
                    th.Cells.Add(Fecha_creacion);
                    th.Cells.Add(Accion);

                    tbl_grupos.Rows.Add(th);

                    DataTable data;
                    DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                    noticias.Titulo = txt_filter_grupo.Text;
                    data = BRL.Int_Noticias_BRL.SelectTable(noticias, 9);
                    if (data.Rows.Count > 0)
                    {
                        int numNoticia = 1;
                        foreach (DataRow row in data.Rows)
                        {
                            HtmlTableCell cell1 = new HtmlTableCell();
                            HtmlTableCell cell2 = new HtmlTableCell();
                            HtmlTableCell cell3 = new HtmlTableCell();
                            HtmlTableCell cell4 = new HtmlTableCell();
                            HtmlTableCell cell5 = new HtmlTableCell();
                            HtmlTableCell cell6 = new HtmlTableCell();

                            HtmlTableRow tableRow = new HtmlTableRow();

                            cell1.InnerText = numNoticia.ToString();
                            cell2.InnerText = row["Id_Noticia"].ToString();
                            cell3.InnerText = row["Titulo"].ToString();
                            cell4.InnerText = row["Descripcion"].ToString();
                            cell5.InnerText = row["Creacion"].ToString();

                            Session["imagen"] = row["Imagen"].ToString();

                            var rd_estado = new HtmlGenericControl("input");
                            rd_estado.Attributes.Add("Type", "radio");
                            rd_estado.Attributes.Add("name", "rd_estado_vista");
                            rd_estado.Attributes.Add("value", row["Id_Noticia"].ToString());
                            cell6.Controls.Add(rd_estado);

                            tableRow.Cells.Add(cell1);
                            tableRow.Cells.Add(cell2);
                            tableRow.Cells.Add(cell3);
                            tableRow.Cells.Add(cell4);
                            tableRow.Cells.Add(cell5);
                            tableRow.Cells.Add(cell6);

                            tbl_grupos.Rows.Add(tableRow);
                            numNoticia++;
                        }
                    }
                    else
                    {
                        noticias.Titulo = "";
                        data = BRL.Int_Noticias_BRL.SelectTable(noticias, 9);
                        if (data.Rows.Count > 0)
                        {
                            int numNoticia = 1;
                            foreach (DataRow row in data.Rows)
                            {
                                HtmlTableCell cell1 = new HtmlTableCell();
                                HtmlTableCell cell2 = new HtmlTableCell();
                                HtmlTableCell cell3 = new HtmlTableCell();
                                HtmlTableCell cell4 = new HtmlTableCell();
                                HtmlTableCell cell5 = new HtmlTableCell();
                                HtmlTableCell cell6 = new HtmlTableCell();

                                HtmlTableRow tableRow = new HtmlTableRow();

                                cell1.InnerText = numNoticia.ToString();
                                cell2.InnerText = row["Id_Noticia"].ToString();
                                cell3.InnerText = row["Titulo"].ToString();
                                cell4.InnerText = row["Descripcion"].ToString();
                                cell5.InnerText = row["Creacion"].ToString();

                                Session["imagen"] = row["Imagen"].ToString();

                                var rd_estado = new HtmlGenericControl("input");
                                rd_estado.Attributes.Add("Type", "radio");
                                rd_estado.Attributes.Add("name", "rd_estado_vista");
                                rd_estado.Attributes.Add("value", row["Id_Noticia"].ToString());
                                cell6.Controls.Add(rd_estado);

                                tableRow.Cells.Add(cell1);
                                tableRow.Cells.Add(cell2);
                                tableRow.Cells.Add(cell3);
                                tableRow.Cells.Add(cell4);
                                tableRow.Cells.Add(cell5);
                                tableRow.Cells.Add(cell6);

                                tbl_grupos.Rows.Add(tableRow);
                                numNoticia++;
                            }
                        }
                    }
                    consultado = true;
                }
            }
            catch (Exception ex)
            {
                utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}.", pathLog);
                throw ex;
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

            AG_Utils utilidades = new AG_Utils();
            try
            {
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");

                DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                noticias.Id_Noticia = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());
                DataTable dt = BRL.Int_Noticias_BRL.SelectTable(noticias, 10);
                BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, 11);

                if (utilidades.impersonateValidUser())
                {
                    string archivoRemoto = dt.Rows[0][3].ToString();
                    var anexoFoto = archivoRemoto.Split('\\');
                    List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                    int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                    int[] indexArray = new int[index];
                    var removerLista = new List<int>(indexArray);
                    string nombreArchivo = arrayAnexoFoto[8].ToString();
                    for (int i = 0; i < removerLista.Count; i++)
                    {
                        arrayAnexoFoto.RemoveAt(0);
                    }
                    string rutaCompleta = String.Join("\\", arrayAnexoFoto);

                    string localTemp = $@"{pathServer}{rutaCompleta}";

                    if (File.Exists(localTemp))
                        File.Delete(localTemp);
                    if (File.Exists(archivoRemoto))
                        File.Delete(archivoRemoto);
                    utilidades.undoImpersonation();
                }
                else
                {
                    utilidades.logError($"{CONST_ERROR}{CONST_ERRORPERMISOS}{System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}.", pathLog);
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}.", pathLog);
                throw ex;
            }
        }

        protected void txt_filter_grupo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargar_tabla_vista();
                txt_filter_grupo.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        
        //static bool IsValidImage(string filePath) 
        //{ 
        //    return File.Exists(filePath) && IsValidImage(new FileStream(filePath, FileMode.Open, FileAccess.Read)); 
        //}

        //static bool IsValidImage(Stream imageStream)
        //{
        //    if (imageStream.Length > 0)
        //    {
        //        byte[] header = new byte[4]; // Change size if needed.
        //        string[] imageHeaders = new[]
        //        {
        //            "\xFF\xD8", // JPEG
        //            "BM", // BMP
        //            "GIF", // GIF
        //            Encoding.ASCII.GetString(new byte[]{137, 80, 78, 71})
        //        }; // PNG
        //        imageStream.Read(header, 0, header.Length); 
        //        bool isImageHeader = imageHeaders.Count(str => Encoding.ASCII.GetString(header).StartsWith(str)) > 0; 
        //        if (isImageHeader == true) 
        //        {
        //            try 
        //            {
        //                System.Drawing.Image.FromStream(imageStream).Dispose();
        //                imageStream.Close();
        //                return true; 
        //            } 
        //            catch 
        //            {
        //            } 
        //        }
        //    }
        //    imageStream.Close(); return false;
        //}
        //public void validarArchivoNoticiaLocal(string rutaNoticiasLocal, string consecutivo, string imagenNoticiaLocal )
        //{
        //    if (!Directory.Exists(rutaNoticiasLocal))
        //    {
        //        Directory.CreateDirectory(rutaNoticiasLocal);
        //    }
        //    else
        //    {
        //        var archivosNoticiasLocal = Directory.GetFiles(rutaNoticiasLocal, "*.*");
        //        foreach (var item in archivosNoticiasLocal)
        //        {
        //            if (item.Contains($"{consecutivo}-"))
        //            {
        //                File.Delete(item);
        //            }

        //        }
        //    }
        //    if (File.Exists(imagenNoticiaLocal))
        //    {
        //        File.Delete(imagenNoticiaLocal);
        //    }
        //}
    }
}