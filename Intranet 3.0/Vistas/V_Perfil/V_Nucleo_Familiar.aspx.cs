using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Intranet_3._0.Interna;

namespace Intranet_3._0.Vistas.V_Perfil
{
    public partial class V_Nucleo_Familiar : System.Web.UI.Page
    {
        string pathLog = "";
        string ipServer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    if (!IsPostBack)
                    {
                        DCL.Int_Nucleo_Familiar int_Nucleo_ = new DCL.Int_Nucleo_Familiar();
                        int_Nucleo_.Id_IE = Convert.ToInt32(Request.QueryString["Id_Usuario"].ToString());
                        DataTable identificacionUsuario = Int_Nucleo_Familiar_BRL.SelectTable(int_Nucleo_, 12);
                        Session["identifacion"] = identificacionUsuario.Rows[0]["Usuario"].ToString();
                        Cargar_Drop();
                        Cargar_Drop_Modal();
                    }
                }
                else
                {
                    Page.Response.Redirect("~/Login", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }


       


        protected void Cargar_Drop()
        {
            try
            {
                DataTable dt_drop_parentesco;
                DCL.Int_Nucleo_Familiar obj = new DCL.Int_Nucleo_Familiar();
                dt_drop_parentesco = Int_Nucleo_Familiar_BRL.SelectTable(obj, 2);
                drop_parentesco.DataSource = dt_drop_parentesco;
                drop_parentesco.DataTextField = "Parentesco";
                drop_parentesco.DataValueField = "Id_HVTF";
                drop_parentesco.DataBind();
                drop_parentesco.Items.Insert(0, new ListItem("-Seleccione Parentesco-", ""));

                DataTable dt_drop_genero;
                DCL.Int_Nucleo_Familiar obj_g = new DCL.Int_Nucleo_Familiar();
                dt_drop_genero = Int_Nucleo_Familiar_BRL.SelectTable(obj_g, 3);
                drop_genero.DataSource = dt_drop_genero;
                drop_genero.DataTextField = "Descripcion";
                drop_genero.DataValueField = "Id_Sexo";
                drop_genero.DataBind();
                drop_genero.Items.Insert(0, new ListItem("-Seleccione Genero-", ""));

                DataTable dt_drop_tipo_docu;
                DCL.Int_Nucleo_Familiar obj_t = new DCL.Int_Nucleo_Familiar();
                dt_drop_tipo_docu = Int_Nucleo_Familiar_BRL.SelectTable(obj_t, 4);
                drop_tipo.DataSource = dt_drop_tipo_docu;
                drop_tipo.DataTextField = "Descripcion";
                drop_tipo.DataValueField = "Id_TipoDoc";
                drop_tipo.DataBind();
                drop_tipo.Items.Insert(0, new ListItem("-Seleccione Tipo documento-", ""));

                DataTable dt_drop_escolaridad;
                DCL.Int_Nucleo_Familiar obj_e = new DCL.Int_Nucleo_Familiar();
                dt_drop_escolaridad = Int_Nucleo_Familiar_BRL.SelectTable(obj_e, 5);
                drop_escolaridad.DataSource = dt_drop_escolaridad;
                drop_escolaridad.DataTextField = "Nombre";
                drop_escolaridad.DataValueField = "Id_Esco";
                drop_escolaridad.DataBind();
                drop_escolaridad.Items.Insert(0, new ListItem("-Seleccione Escolaridad-", ""));

                DataTable dt_drop_ocupacion;
                DCL.Int_Nucleo_Familiar obj_o = new DCL.Int_Nucleo_Familiar();
                dt_drop_ocupacion = Int_Nucleo_Familiar_BRL.SelectTable(obj_o, 6);
                drop_ocupacion.DataSource = dt_drop_ocupacion;
                drop_ocupacion.DataTextField = "Nombre";
                drop_ocupacion.DataValueField = "Id_Ocupa";
                drop_ocupacion.DataBind();
                drop_ocupacion.Items.Insert(0, new ListItem("-Seleccione Ocupación-", ""));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Cargar_Drop_Modal()
        {
            try
            {
                DataTable dt_drop_parentesco;
                DCL.Int_Nucleo_Familiar obj = new DCL.Int_Nucleo_Familiar();
                dt_drop_parentesco = Int_Nucleo_Familiar_BRL.SelectTable(obj, 2);
                drop_parentesco_modal.DataSource = dt_drop_parentesco;
                drop_parentesco_modal.DataTextField = "Parentesco";
                drop_parentesco_modal.DataValueField = "Id_HVTF";
                drop_parentesco_modal.DataBind();
                drop_parentesco_modal.Items.Insert(0, new ListItem("-Seleccione Parentesco-", ""));

                DataTable dt_drop_genero;
                DCL.Int_Nucleo_Familiar obj_g = new DCL.Int_Nucleo_Familiar();
                dt_drop_genero = Int_Nucleo_Familiar_BRL.SelectTable(obj_g, 3);
                drop_genero_modal.DataSource = dt_drop_genero;
                drop_genero_modal.DataTextField = "Descripcion";
                drop_genero_modal.DataValueField = "Id_Sexo";
                drop_genero_modal.DataBind();
                drop_genero_modal.Items.Insert(0, new ListItem("-Seleccione Genero-", ""));

                DataTable dt_drop_tipo_docu;
                DCL.Int_Nucleo_Familiar obj_t = new DCL.Int_Nucleo_Familiar();
                dt_drop_tipo_docu = Int_Nucleo_Familiar_BRL.SelectTable(obj_t, 4);
                drop_tipo_modal.DataSource = dt_drop_tipo_docu;
                drop_tipo_modal.DataTextField = "Descripcion";
                drop_tipo_modal.DataValueField = "Id_TipoDoc";
                drop_tipo_modal.DataBind();
                drop_tipo_modal.Items.Insert(0, new ListItem("-Seleccione Tipo documento-", ""));

                DataTable dt_drop_escolaridad;
                DCL.Int_Nucleo_Familiar obj_e = new DCL.Int_Nucleo_Familiar();
                dt_drop_escolaridad = Int_Nucleo_Familiar_BRL.SelectTable(obj_e, 5);
                drop_escolaridad_modal.DataSource = dt_drop_escolaridad;
                drop_escolaridad_modal.DataTextField = "Nombre";
                drop_escolaridad_modal.DataValueField = "Id_Esco";
                drop_escolaridad_modal.DataBind();
                drop_escolaridad_modal.Items.Insert(0, new ListItem("-Seleccione Escolaridad-", ""));

                DataTable dt_drop_ocupacion;
                DCL.Int_Nucleo_Familiar obj_o = new DCL.Int_Nucleo_Familiar();
                dt_drop_ocupacion = Int_Nucleo_Familiar_BRL.SelectTable(obj_o, 6);
                drop_ocupacion_modal.DataSource = dt_drop_ocupacion;
                drop_ocupacion_modal.DataTextField = "Nombre";
                drop_ocupacion_modal.DataValueField = "Id_Ocupa";
                drop_ocupacion_modal.DataBind();
                drop_ocupacion_modal.Items.Insert(0, new ListItem("-Seleccione Ocupación-", ""));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void agregar_datos_familia(object sender, EventArgs e)
        {

            AG_Utils utilidades = new AG_Utils();
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                    ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                    bool conectaAdjuntos = utilidades.Ping(ipServer);
                    if (
                        txt_nombre.Text.Trim() != string.Empty &&
                        txt_apellido.Text.Trim() != string.Empty &&
                        drop_genero.SelectedValue != string.Empty &&
                        txt_celular.Text.Trim() != string.Empty &&
                        txt_id.Text.Trim() != string.Empty &&
                        txt_edad.Text.Trim() != string.Empty &&
                        drop_parentesco.SelectedValue != string.Empty &&
                        drop_escolaridad.SelectedValue != string.Empty &&
                        drop_ocupacion.SelectedValue != string.Empty
                        )
                    {
                        string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();

                        DataTable dtValidarDocumentoFamiliar;
                        DCL.Int_Nucleo_Familiar int_Nucleo_ = new DCL.Int_Nucleo_Familiar();
                        int_Nucleo_.Id_IE = Convert.ToInt32(Id_Usuario);
                        int_Nucleo_.Identificacion = txt_id.Text.Trim();
                        dtValidarDocumentoFamiliar = Int_Nucleo_Familiar_BRL.SelectTable(int_Nucleo_, 11);
                        DataTable identificacionUsuario = Int_Nucleo_Familiar_BRL.SelectTable(int_Nucleo_, 12);
                        if (dtValidarDocumentoFamiliar.Rows.Count == 0)
                        {
                            DataTable dt_validar_usuario_login;
                            DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                            obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                            dt_validar_usuario_login = Int_Usuarios_BRL.SelectTable(obj, 29);
                            if (dt_validar_usuario_login.Rows.Count > 0)
                            {
                                string archivo_registro = "Ninguno";
                                string archivo_certificacion = "Ninguno";

                                string N_Identificacion = dt_validar_usuario_login.Rows[0]["Usuario"].ToString();

                                string discapacidad_familiar;

                                string ubicacion = $@"{pathRemote}archivos_usuarios\{Session["identifacion"]}\Documentos\NucleoFamiliar\";
                                if (txt_cual.Text.Trim() != string.Empty)
                                {
                                    discapacidad_familiar = txt_cual.Text.Trim();
                                }
                                else
                                {
                                    discapacidad_familiar = "Ninguna";
                                }

                                DCL.Int_Nucleo_Familiar obj_ = new DCL.Int_Nucleo_Familiar();
                                obj_.Id_IE = Convert.ToInt32(dt_validar_usuario_login.Rows[0]["Id_IE"]);
                                obj_.Nombre_Familiar = txt_nombre.Text.Trim();
                                obj_.Anexo_Tres = txt_apellido.Text.Trim();
                                obj_.Id_Genero = drop_genero.SelectedValue;
                                obj_.Id_Tipo_Doc = drop_tipo.SelectedValue;
                                obj_.Identificacion = txt_id.Text.Trim();
                                obj_.Anexo_Cuatro = txt_data.Text.Trim();
                                obj_.Edad = txt_edad.Text.Trim();
                                obj_.Celular = txt_celular.Text.Trim();
                                obj_.Id_Parentesco = drop_parentesco.SelectedValue;
                                obj_.Id_Escolaridad = drop_escolaridad.SelectedValue;
                                obj_.Id_Ocupa = drop_ocupacion.SelectedValue;
                                obj_.Discapacidad = discapacidad_familiar;
                                if (utilidades.impersonateValidUser())
                                {
                                    if (txt_file_registro.HasFile)
                                    {
                                        if (conectaAdjuntos)
                                        {
                                            if (!Directory.Exists(ubicacion))
                                            {
                                                Directory.CreateDirectory(ubicacion);
                                            }
                                            string archivo = Path.Combine(ubicacion, $@"RC_{txt_id.Text.Trim()}.pdf");
                                            txt_file_registro.SaveAs(archivo);
                                            archivo_registro = archivo;
                                        }
                                        
                                    }

                                    if (txt_file_certificacion.HasFile)
                                    {
                                        if (!Directory.Exists(ubicacion))
                                        {
                                            Directory.CreateDirectory(ubicacion);
                                        }
                                        string archivo = Path.Combine(ubicacion, $@"CE_{txt_id.Text.Trim()}.pdf");
                                        txt_file_certificacion.SaveAs(archivo);
                                        archivo_certificacion = archivo;
                                    }

                                    utilidades.undoImpersonation();
                                }
                                obj_.Anexo_Reg_Civil = archivo_registro;
                                obj_.Anexo_Dos = archivo_certificacion;
                                Int_Nucleo_Familiar_BRL.InsertOrUpdate(obj_, 1);

                                txt_nombre.Text = string.Empty;
                                txt_apellido.Text = string.Empty;
                                drop_genero.SelectedValue = string.Empty;
                                txt_celular.Text = string.Empty;
                                drop_tipo.SelectedValue = string.Empty;
                                txt_id.Text = string.Empty;
                                txt_data.Text = string.Empty;
                                txt_edad.Text = string.Empty;
                                drop_parentesco.SelectedValue = string.Empty;
                                drop_escolaridad.SelectedValue = string.Empty;
                                drop_ocupacion.SelectedValue = string.Empty;
                                txt_cual.Text = string.Empty;

                                Page.Response.Redirect(Page.Request.Url.ToString(), false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "alerta", "validarIdentificacionFamiliar()", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "alerta", "mensaje_campos_vacios()", false);
                    }
                }
            }
            catch (Exception ex)
            {
               
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                throw ex;
            }
        }

        protected void actualizar_datos_familia(object sender, EventArgs e)
        {
            AG_Utils utilidades = new AG_Utils();

            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                    string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                    string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                    string archivo_registro = "";
                    string archivo_certificacion = "";
                    string archivoLocalRegistroC = "";
                    string archivoLocalCertificado = "";
                    bool conectaAdjuntos = false;
                    string ubicacion = $@"{pathRemote}archivos_usuarios\{Session["identifacion"]}\Documentos\NucleoFamiliar\";

                    if (
                        txt_nombre_modal.Text.Trim() != string.Empty &&
                        txt_apellido_modal.Text.Trim() != string.Empty &&
                        drop_genero_modal.SelectedValue != string.Empty &&
                        txt_numero_modal.Text.Trim() != string.Empty &&
                        txt_id_modal.Text.Trim() != string.Empty &&
                        txt_edad_modal.Text.Trim() != string.Empty &&
                        drop_parentesco_modal.SelectedValue != string.Empty &&
                        drop_escolaridad_modal.SelectedValue != string.Empty &&
                        drop_ocupacion_modal.SelectedValue != string.Empty
                        )
                    {
                        
                        

                        string Id_Familia = number_familia.Text.Trim().Replace("#", "");
                        string discapacidad_familiar;

                        
                        if (txt_cual_modal.Text.Trim() != string.Empty)
                        {
                            discapacidad_familiar = txt_cual_modal.Text.Trim();
                        }
                        else
                        {
                            discapacidad_familiar = "Ninguna";
                        }

                        DCL.Int_Nucleo_Familiar obj_ = new DCL.Int_Nucleo_Familiar();
                        obj_.Nombre_Familiar = txt_nombre_modal.Text.Trim();
                        obj_.Anexo_Tres = txt_apellido_modal.Text.Trim();
                        obj_.Id_Genero = drop_genero_modal.SelectedValue;
                        obj_.Id_Tipo_Doc = drop_tipo_modal.SelectedValue;
                        obj_.Identificacion = txt_id_modal.Text.Trim();
                        obj_.Anexo_Cuatro = txt_date_modal.Text.Trim();
                        obj_.Edad = txt_edad_modal.Text.Trim();
                        obj_.Celular = txt_numero_modal.Text.Trim();
                        obj_.Id_Parentesco = drop_parentesco_modal.SelectedValue;
                        obj_.Id_Escolaridad = drop_escolaridad_modal.SelectedValue;
                        obj_.Id_Ocupa = drop_ocupacion_modal.SelectedValue;
                        obj_.Discapacidad = discapacidad_familiar;
                        obj_.Id_Familiar = Convert.ToInt32(Id_Familia);
                        archivo_registro = Session["AnexoRegistroCivil"].ToString();
                        archivo_certificacion = Session["AnexoDos"].ToString();






                        //if (!archivo_registro.Equals("Ninguno") && txt_file_registro.FileName.Contains("NINGUN"))
                        //{
                        //    string archivoRemotoRegistroC = archivo_registro;
                        //    var anexoFotoAnexo_Dos = archivoRemotoRegistroC.Split('\\');
                        //    List<String> arrayAnexo_Dos = new List<string>(anexoFotoAnexo_Dos);
                        //    int indexAnexo_Dos = arrayAnexo_Dos.FindIndex(x => x == ambiente);
                        //    int[] indexArrayAnexo_Dos = new int[indexAnexo_Dos];
                        //    var removerListaAnexo_Dos = new List<int>(indexArrayAnexo_Dos);
                        //    for (int i = 0; i < removerListaAnexo_Dos.Count; i++)
                        //    {
                        //        arrayAnexo_Dos.RemoveAt(0);
                        //    }
                        //    archivoRemotoRegistroC = String.Join("\\", arrayAnexo_Dos);

                        //    archivoLocalRegistroC = $@"{pathServer}{archivoRemotoRegistroC}";
                        //    List<string> rutalocalTempRegistroC = archivoLocalRegistroC.Split('\\').ToList();
                        //    int indexRemotaAnexo_Dos = rutalocalTempRegistroC.Count;
                        //    rutalocalTempRegistroC.RemoveAt(--indexRemotaAnexo_Dos);
                        //    string rutaCarpetaLocalRegistroC = String.Join(@"\", rutalocalTempRegistroC) + "\\";
                        //    conectaAdjuntos = utilidades.Ping(anexoFotoAnexo_Dos[2]);
                        //}


                        //if (!archivo_certificacion.Equals("Ninguno"))
                        //{
                        //    string archivoRemotoCertificacion = archivo_certificacion;
                        //    var anexoFotoAnexo_Dos = archivoRemotoCertificacion.Split('\\');
                        //    List<String> arrayAnexo_Dos = new List<string>(anexoFotoAnexo_Dos);
                        //    int indexAnexo_Dos = arrayAnexo_Dos.FindIndex(x => x == ambiente);
                        //    int[] indexArrayAnexo_Dos = new int[indexAnexo_Dos];
                        //    var removerListaAnexo_Dos = new List<int>(indexArrayAnexo_Dos);
                        //    for (int i = 0; i < removerListaAnexo_Dos.Count; i++)
                        //    {
                        //        arrayAnexo_Dos.RemoveAt(0);
                        //    }
                        //    archivoRemotoCertificacion = String.Join("\\", arrayAnexo_Dos);

                        //    archivoLocalCertificado = $@"{pathServer}{archivoRemotoCertificacion}";
                        //    List<string> rutalocalTempCertificado = archivoLocalCertificado.Split('\\').ToList();
                        //    int indexRemotaAnexo_Dos = rutalocalTempCertificado.Count;
                        //    rutalocalTempCertificado.RemoveAt(--indexRemotaAnexo_Dos);
                        //    string rutaLocalCompletaCertificado = String.Join(@"\", rutalocalTempCertificado) + "\\";
                        //    conectaAdjuntos = utilidades.Ping(anexoFotoAnexo_Dos[2]);
                        //}

                        





























                        if (drop_parentesco_modal.SelectedItem.Text.Equals(@"Hijo(a)"))
                        {
                            if (utilidades.impersonateValidUser())
                            {
                                //if (string.IsNullOrEmpty(txt_file_registro.FileName) && archivo_registro != "Ninguno" && File.Exists(archivo_registro))
                                //{
                                //    obj_.Anexo_Reg_Civil = archivo_registro;
                                //}
                                //else
                                //{
                                    if (file_a_modal.HasFile)
                                    {
                                        if (!Directory.Exists(ubicacion))
                                        {
                                            Directory.CreateDirectory(ubicacion);
                                        }
                                        string archivo_a = Path.Combine(ubicacion, $@"RC_{txt_id_modal.Text.Trim()}.pdf");
                                        file_a_modal.SaveAs(archivo_a);
                                        archivo_registro = archivo_a;
                                        obj_.Anexo_Reg_Civil = archivo_registro;
                                    }
                                    else
                                    {
                                        obj_.Anexo_Reg_Civil = archivo_registro;
                                    }
                                //}

                                //if (txt_file_certificacion.FileName =="" && archivo_certificacion != "Ninguno" && File.Exists(archivo_certificacion))
                                //{
                                //    obj_.Anexo_Dos = archivo_certificacion;
                                //}
                                //else
                                //{
                                    if (file_b_modal.HasFile)
                                    {
                                        if (!Directory.Exists(ubicacion))
                                        {
                                            Directory.CreateDirectory(ubicacion);
                                        }
                                        string archivo_b = Path.Combine(ubicacion, $@"CE_{txt_id_modal.Text.Trim()}.pdf");
                                        file_b_modal.SaveAs(archivo_b);
                                        archivo_certificacion = archivo_b;
                                        obj_.Anexo_Dos = archivo_certificacion;
                                    }

                                    else
                                    {
                                        obj_.Anexo_Dos = archivo_certificacion;
                                    }
                                //}

                                utilidades.undoImpersonation();
                            }
                        }
                        else
                        {
                            if (File.Exists(archivo_registro)) 
                            {
                                File.Delete(archivo_registro);
                            }
                            //if (File.Exists(archivoLocalRegistroC))
                            //{
                            //    File.Delete(archivoLocalRegistroC);
                            //}
                            if (File.Exists(archivo_certificacion))
                            {
                                File.Delete(archivo_certificacion);
                            }
                            //if (File.Exists(archivoLocalCertificado))
                            //{
                            //    File.Delete(archivoLocalCertificado);
                            //}
                            obj_.Anexo_Reg_Civil = "Ninguno";
                            obj_.Anexo_Dos = "Ninguno";
                        }
 
                        Int_Nucleo_Familiar_BRL.InsertOrUpdate(obj_, 8);
                        Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "alerta", "mensaje_campos_vacios()", false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
