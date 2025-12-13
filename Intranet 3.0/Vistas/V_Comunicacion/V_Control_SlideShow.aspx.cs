using Intranet_3._0.Interna;
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
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_SlideShow : System.Web.UI.Page
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

                    // if (!IsPostBack)
                    // {
                    //}
                       
                            cargar_tabla_vista();
                            

                       
                        //}

                    }
                    else
                    {
                        Page.Response.Redirect("~/Login", false);
                    }
                }
                else
                {
                    Page.Response.Redirect("~/Login", false);
                }
            }
            catch (Exception)
            {
                Page.Response.Redirect("~/Login", true);
            }
        }

    protected void lnk_crear__Click(object sender, EventArgs e)
        {
            string imagenNoticiaRemoto = "";
            string imagenNoticiaLocal = "";
            AG_Utils utilidades = new AG_Utils();
            try
            {
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                pathLog = Server.MapPath(@"~/logs");
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string rutaNoticiasRemoto = $@"{pathRemote}publicaciones\Slideshow\";
                string rutaNoticiasLocal = pathServer + ambiente + "\\intranet\\publicaciones\\Slideshow\\";
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();
                int consecutivo;
                
                if (fud_Adjunto.HasFile)
                {
                    var archivoAguardar = fud_Adjunto;
                    DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                    noticias.Descripcion = txt_descripcion.Text;

                    if (conectaAdjuntos)
                    {
                        DataTable dataTable = BRL.Int_Noticias_BRL.SelectTable(noticias, 18);
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
                        string nombreOriginalArchivo = txt_descripcion.Text;
                        string extensionArchivo = Path.GetExtension(fud_Adjunto.FileName);
                        string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(nombreOriginalArchivo, consecutivo.ToString(), extensionArchivo);
                        int actionInsertSlideshow = 15;

                        (bool bl_GuardaImagenLocal, bool bl_GuardaImagenRemota, string rutaNoticiaRemoto) = utilidades.TratamientoNoticias(nombreFinalArchivo, consecutivo.ToString(), rutaNoticiasLocal, rutaNoticiasRemoto, fud_Adjunto, Id_Usuario, pathLog);
                        imagenNoticiaRemoto = rutaNoticiaRemoto;
                        if (bl_GuardaImagenLocal && bl_GuardaImagenRemota && !string.IsNullOrEmpty(imagenNoticiaRemoto))
                        {
                            noticias.Imagen = imagenNoticiaRemoto;
                            BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionInsertSlideshow);
                        }
                        else
                        {
                            utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nRegistro SLIDESHOW no almacenado en BD. Los archivos no fueron almacenados.", pathLog);
                            if (utilidades.impersonateValidUser())
                            {
                                if (File.Exists(imagenNoticiaRemoto))
                                    File.Delete(imagenNoticiaRemoto);
                                if (File.Exists(imagenNoticiaLocal))
                                    File.Delete(imagenNoticiaLocal);
                                utilidades.undoImpersonation();
                            }
                        }



                        //string nombreArchivoNorm = Regex.Replace(nombreArchivo.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                        //nombreArchivo = nombreArchivoNorm.Replace(" ", "_");
                        //nombreArchivo = $"{consecutivo}-{nombreArchivo}";
                        //string nombreCompletoArchivo = $"{nombreArchivo}{Path.GetExtension(fud_Adjunto.FileName)}";

                        //archivoNoticiaRemoto = Path.Combine(RutaNoticiasRemoto, nombreCompletoArchivo);
                        //imagenNoticiaLocal = rutaNoticiasLocal + nombreCompletoArchivo;

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
                        //    BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, 15);
                        //}
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
                utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nRegistro SLIDESHOW no almacenado en BD. Los archivos no fueron almacenados.", pathLog);
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

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");

                DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                noticias.Id_Noticia = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());
                DataTable dt = BRL.Int_Noticias_BRL.SelectTable(noticias, 12);
                BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, 14);

                if (utilidades.impersonateValidUser())
                {
                string archivoRemoto = dt.Rows[0][2].ToString(); 
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
                }

                utilidades.undoImpersonation();

                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualizar Slideshow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Actualizar_datos_publicacion(object sender, EventArgs e)
        {
            string imagenNoticiaRemoto = "";
            string imagenNoticiaLocal = "";
            AG_Utils utilidades = new AG_Utils();
            try
            {
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string rutaNoticiasRemoto = $@"{pathRemote}publicaciones\Slideshow\";
                string rutaNoticiasLocal = pathServer + ambiente + $@"\intranet\publicaciones\Slideshow\";
                string consecutivo = $"{Request.Form["rd_estado_vista"]}";
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();
                DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                noticias.Id_Noticia = (int?)Convert.ToInt64(consecutivo);
                noticias.Descripcion = txt_descripcion_pub.Text;
                int actionActualizarNoticia = 16;

                if (fud_Adjunto_pub.HasFile)
                {
                    
                    //noticias.Titulo = txt_descripcion_pub.Text;


                    if (conectaAdjuntos)
                    {
                        string nombreOriginalArchivo = txt_descripcion_pub.Text;
                        string extensionArchivo = Path.GetExtension(fud_Adjunto_pub.FileName);
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
                        }
                    }
                    else
                    {
                        utilidades.logError($"{CONST_ERROR}{CONST_ERRORCONEXIONSERV} {ipServer}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario:  {Id_Usuario}", pathLog);
                    }
                    
                }
                Page.Response.Redirect(Page.Request.Url.ToString(), false);
                //if (!string.IsNullOrEmpty(datos_img.Text) && !fud_Adjunto_pub.HasFile)
                //{
                //    string abc = datos_img.Text;
                //    noticias.Imagen = datos_img.Text;

                //    BRL.Int_Noticias_BRL.InsertOrUpdate(noticias, actionActualizarNoticia);
                //}
            }
            catch (Exception ex)
            {
                utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}\nRegistro no almacenado en BD. Los archivos no fueron almacenados.", pathLog);
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
                    HtmlTableCell ID = new HtmlTableCell();
                    HtmlTableCell Descripcion = new HtmlTableCell();
                    HtmlTableCell Fecha_creacion = new HtmlTableCell();
                    HtmlTableCell Accion = new HtmlTableCell();

                    HtmlTableRow th = new HtmlTableRow();

                    N.InnerText = "#";
                    ID.InnerText = "ID";
                    Descripcion.InnerText = "DESCRIPCION";
                    Fecha_creacion.InnerText = "FECHA DE CREACION";
                    Accion.InnerText = "ACCION";
                    th.Attributes.Add("class", "th");

                    th.Cells.Add(N);
                    th.Cells.Add(ID);
                    th.Cells.Add(Descripcion);
                    th.Cells.Add(Fecha_creacion);
                    th.Cells.Add(Accion);

                    tbl_grupos.Rows.Add(th);

                    DataTable data;
                    DCL.Int_Noticias noticias = new DCL.Int_Noticias();
                    string buscar = txt_filter_grupo.Text;
                    noticias.Descripcion = buscar;

                    data = BRL.Int_Noticias_BRL.SelectTable(noticias, 13);
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

                            HtmlTableRow tableRow = new HtmlTableRow();

                            cell1.InnerText = numNoticia.ToString();
                            cell2.InnerText = row["Id_SlideShow"].ToString();
                            cell3.InnerText = row["Descripcion"].ToString();
                            cell4.InnerText = row["Fecha_Creacion"].ToString();

                            Session["slider"] = row["Imagen"].ToString();

                            var rd_estado = new HtmlGenericControl("input");
                            rd_estado.Attributes.Add("Type", "radio");
                            rd_estado.Attributes.Add("name", "rd_estado_vista");
                            rd_estado.Attributes.Add("value", row["Id_SlideShow"].ToString());
                            cell5.Controls.Add(rd_estado);

                            tableRow.Cells.Add(cell1);
                            tableRow.Cells.Add(cell2);
                            tableRow.Cells.Add(cell3);
                            tableRow.Cells.Add(cell4);
                            tableRow.Cells.Add(cell5);

                            tbl_grupos.Rows.Add(tableRow);
                            numNoticia++;
                        }
                    }
                    else
                    {

                        noticias.Descripcion = "";
                        data = BRL.Int_Noticias_BRL.SelectTable(noticias, 13);
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

                                HtmlTableRow tableRow = new HtmlTableRow();

                                cell1.InnerText = numNoticia.ToString();
                                cell2.InnerText = row["Id_SlideShow"].ToString();
                                cell3.InnerText = row["Descripcion"].ToString();
                                cell4.InnerText = row["Fecha_Creacion"].ToString();

                                Session["slider"] = row["Imagen"].ToString();

                                var rd_estado = new HtmlGenericControl("input");
                                rd_estado.Attributes.Add("Type", "radio");
                                rd_estado.Attributes.Add("name", "rd_estado_vista");
                                rd_estado.Attributes.Add("value", row["Id_SlideShow"].ToString());
                                cell5.Controls.Add(rd_estado);

                                tableRow.Cells.Add(cell1);
                                tableRow.Cells.Add(cell2);
                                tableRow.Cells.Add(cell3);
                                tableRow.Cells.Add(cell4);
                                tableRow.Cells.Add(cell5);

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

        protected void txt_filter_grupo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                }
                if (txt_filter_grupo.Text !="")
                {
                    cargar_tabla_vista();
                }
                
                //txt_filter_grupo.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        static string ajusteNombreArchivos(string nombreOriginalArchivo, string extensionArchivo, string consecutivo)
        {
            string nombreArchivoNorm = Regex.Replace(nombreOriginalArchivo.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            string nombreAjustadoArchivoSE = nombreArchivoNorm.Replace(" ", "_");
            nombreAjustadoArchivoSE = $"{consecutivo}-{nombreAjustadoArchivoSE}";
            return _ = $"{nombreAjustadoArchivoSE}{extensionArchivo}";
        }
    }
}