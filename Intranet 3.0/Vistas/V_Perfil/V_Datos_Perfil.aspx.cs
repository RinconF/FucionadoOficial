using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using SimpleImpersonation;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Web.UI.HtmlControls;
using Intranet_3._0.Interna;

namespace Intranet_3._0.Vistas.V_Perfil
{
    public partial class V_Datos_Perfil : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    if (Response.Cookies.Count > 0)
                    {
                        if (!IsPostBack)
                        {
                            DCL.Int_Nucleo_Familiar int_Nucleo_ = new DCL.Int_Nucleo_Familiar();
                            int_Nucleo_.Id_IE = Convert.ToInt32(Request.QueryString["Id_Usuario"].ToString());
                            DataTable identificacionUsuario = Int_Nucleo_Familiar_BRL.SelectTable(int_Nucleo_, 12);
                            Session["identifacion"] = identificacionUsuario.Rows[0]["Usuario"].ToString();
                            Cargar_Drop();
                            Cargar_Contacto_Emergencia();
                        }
                        Cargar_Datos_Colaborador();
                    }

                }
            }
            catch (Exception)
            {
                Page.Response.Redirect("~/Login", true);
            }
            
        }

        protected void Cargar_Datos_Colaborador()
        {
            try
            {
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();

                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                dt = Int_Usuarios_BRL.SelectTable(obj, 40);
                if (dt.Rows.Count > 0)
                {
                    txt_name.InnerHtml = dt.Rows[0]["Nombre"].ToString();
                    txt_cargo.InnerHtml = "<i class='fas fa-tags'></i> " + dt.Rows[0]["Cargo"].ToString();
                    txt_fecha_ing.InnerHtml = "<i class='far fa-calendar-alt'></i> Fecha de ingreso <span>" + dt.Rows[0]["Fecha_Ingreso"].ToString() + "</span>";

                    txt_Number.InnerHtml = "<i class='fas fa-mobile-alt'></i> Número contacto: <span>" + dt.Rows[0]["Celular"].ToString() + "</span>";
                    txt_mail.InnerHtml = "<i class='fas fa-envelope'></i> Correo personal: <span>" + dt.Rows[0]["Email_Personal"].ToString() + "</span>";
                    txt_mail_corporativo.InnerHtml = "<i class='fas fa-mail-bulk'></i> Correo corporativo: <span>" + dt.Rows[0]["Email_Empresa"].ToString() + "</span>";
                    txt_fecha_naci.InnerHtml = "<i class='fas fa-birthday-cake'></i> Fecha nacimiento: <span>" + dt.Rows[0]["Fecha_Nacimiento"].ToString() + "</span>";
                    txt_direc.InnerHtml = "<i class='fas fa-map-marked-alt'></i> Dirección residencia: <span>" + dt.Rows[0]["Dir_Residencia"].ToString() + "</span>";

                    txt_estado.InnerHtml = "<i class='far fa-handshake'></i> Estado civil: <span>" + dt.Rows[0]["Estado_Civil"].ToString() + "</span>";
                    txt_estrato.InnerHtml = "<i class='far fa-chart-bar'></i> Estrato: <span>" + dt.Rows[0]["Estrato"].ToString() + "</span>";
                    txt_etnia.InnerHtml = "<i class='fas fa-users'></i> Grupo étnico: <span>" + dt.Rows[0]["Etnia"].ToString() + "</span>";

                    txt_genero.InnerHtml = "<i class='fas fa-restroom'></i> Genero: <span>" + dt.Rows[0]["Genero"].ToString() + "</span>";
                    txt_vivienda.InnerHtml = "<i class='fas fa-home'></i> Vivienda: <span>" + dt.Rows[0]["Vivienda"].ToString() + "</span>";
                    txt_hobbie.InnerHtml = "<i class='fas fa-dice'></i> Hobbie: <span>" + dt.Rows[0]["Hobbie"].ToString() + "</span>";

                    txt_usu.InnerHtml = "<i class='fas fa-user-tie'></i> Usuario: <span>" + dt.Rows[0]["Usuario"].ToString() + "</span>";
                    txt_fecha_intranet.InnerHtml = "<i class='fas fa-calendar-check'></i> Fecha creación: <span>" + dt.Rows[0]["Fecha_Creacion"].ToString() + "</span>";

                    //modal actualizar
                    txt_number_actu.Text = dt.Rows[0]["Celular"].ToString();
                    txt_mail_actu.Text = dt.Rows[0]["Email_Personal"].ToString();
                    txt_direccion_actu.Text = dt.Rows[0]["Dir_Residencia"].ToString();
                    drop_estado.SelectedValue = dt.Rows[0]["Id_EstadoCivil"].ToString();
                    drop_estrato.SelectedValue = dt.Rows[0]["Id_Estrato"].ToString();
                    drop_etnico.SelectedValue = dt.Rows[0]["Id_Grupo_Etnico"].ToString();
                    drop_genero.SelectedValue = dt.Rows[0]["Id_Sexo"].ToString();
                    drop_vivienda.SelectedValue = dt.Rows[0]["Id_Tipo_Vivienda"].ToString();
                    txt_hobbie_act.Text = dt.Rows[0]["Hobbie"].ToString();
                    txt_ciudadUsuario.Text = dt.Rows[0]["Id_CiudadResidencia"].ToString();
                    txt_localidadUsuario.Text = dt.Rows[0]["Id_LocalidadResidencia"].ToString();
                    txt_barrioUsuario.Text = dt.Rows[0]["Id_BarrioResidencia"].ToString();


                    DataTable dt_;
                    DCL.Info_Empleado obj_ = new DCL.Info_Empleado();
                    obj_.Id_IE = Convert.ToInt32(Id_Usuario);
                    dt_ = Info_Empleado_BRL.SelectTable(obj_, 9);
                    if (dt_.Rows.Count > 0)
                    {
                        txt_ciudad.InnerHtml = "<i class='fas fa-globe-americas'></i> Ciudad: <span>" + dt_.Rows[0]["Ciudad"].ToString() + "</span>";
                        txt_locali.InnerHtml = "<i class='fas fa-location-arrow'></i> Localidad: <span>" + dt_.Rows[0]["Localidad"].ToString() + "</span>";
                        txt_barrio.InnerHtml = "<i class='fas fa-street-view'></i> Barrio: <span>" + dt_.Rows[0]["Barrio"].ToString() + "</span>";

                        //modal
                        if (dt_.Rows[0]["Ciudad"].ToString() == "BOGOTA D.C.")
                        {
                            drop_depart.SelectedValue = "11";
                        }
                        else
                        {
                            drop_depart.SelectedValue = "25";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Cargar_Drop()
        {
            try
            {
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                dt = Int_Usuarios_BRL.SelectTable(obj, 41);
                drop_estado.DataSource = dt;
                drop_estado.DataTextField = "Descripcion";
                drop_estado.DataValueField = "Id_EstadoCivil";
                drop_estado.DataBind();

                DataTable _dt;
                DCL.Int_Usuarios _obj = new DCL.Int_Usuarios();
                _dt = Int_Usuarios_BRL.SelectTable(_obj, 42);
                drop_estrato.DataSource = _dt;
                drop_estrato.DataTextField = "Nombre";
                drop_estrato.DataValueField = "Id_Estrato";
                drop_estrato.DataBind();

                DataTable dt_;
                DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                dt_ = Int_Usuarios_BRL.SelectTable(obj_, 43);
                drop_etnico.DataSource = dt_;
                drop_etnico.DataTextField = "Nombre";
                drop_etnico.DataValueField = "Id_Grupo_Etnico";
                drop_etnico.DataBind();

                DataTable _dt_;
                DCL.Info_Empleado _obj_ = new DCL.Info_Empleado();
                _dt_ = Info_Empleado_BRL.SelectTable(_obj_, 8);
                drop_parentesco.DataSource = _dt_;
                drop_parentesco.DataTextField = "Parentesco";
                drop_parentesco.DataValueField = "Id_HVTF";
                drop_parentesco.DataBind();

                DataTable dt_genero;
                DCL.Int_Usuarios obj_genero = new DCL.Int_Usuarios();
                dt_genero = Int_Usuarios_BRL.SelectTable(obj_genero, 57);
                drop_genero.DataSource = dt_genero;
                drop_genero.DataTextField = "Descripcion";
                drop_genero.DataValueField = "Id_Sexo";
                drop_genero.DataBind();

                DataTable dt_vivienda;
                DCL.Int_Usuarios obj_vivienda = new DCL.Int_Usuarios();
                dt_vivienda = Int_Usuarios_BRL.SelectTable(obj_vivienda, 58);
                drop_vivienda.DataSource = dt_vivienda;
                drop_vivienda.DataTextField = "nombre";
                drop_vivienda.DataValueField = "Id_Tipo_Vivienda";
                drop_vivienda.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Cargar_Contacto_Emergencia()
        {
            try
            {
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();

                DataTable dt;
                DCL.Info_Empleado obj = new DCL.Info_Empleado();
                obj.Id_IE = Convert.ToInt32(Id_Usuario);
                dt = Info_Empleado_BRL.SelectTable(obj, 7);
                if (dt.Rows.Count > 0)
                {

                    txt_nombre_cont.InnerHtml = dt.Rows[0]["Nombre_CE"].ToString() + " " + dt.Rows[0]["Apellidos_CE"].ToString();
                    txt_parentesco_cont.InnerHtml = "<i class='fas fa-tags'></i> " + dt.Rows[0]["Parentesco"].ToString();
                    txt_number_cont.InnerHtml = "<i class='fas fa-mobile-alt'></i> Número contacto: <span>" + dt.Rows[0]["Celular_CE"].ToString() + "</span>";
                    txt_direccion_cont.InnerHtml = "<i class='fas fa-map-marked-alt'></i> Dirección residencia: <span>" + dt.Rows[0]["Direccion_CE"].ToString() + "</span>";

                    txt_nombre_contacto.Text = dt.Rows[0]["Nombre_CE"].ToString();
                    txt_apellido_contacto.Text = dt.Rows[0]["Apellidos_CE"].ToString();
                    drop_parentesco.SelectedValue = dt.Rows[0]["Id_HVTF"].ToString();
                    txt_number_contacto.Text = dt.Rows[0]["Celular_CE"].ToString();
                    txt_direc_contacto.Text = dt.Rows[0]["Direccion_CE"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        protected void Actualizar_Contacto_Emergencia(object sender, EventArgs e)
        {
            try
            {
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();

                DataTable dt;
                DCL.Info_Empleado obj = new DCL.Info_Empleado();
                obj.Id_IE = Convert.ToInt32(Id_Usuario);
                dt = Info_Empleado_BRL.SelectTable(obj, 2);
                if (dt.Rows.Count > 0)
                {
                    DCL.Info_Empleado obj_ = new DCL.Info_Empleado();
                    obj_.Email_Personal = txt_nombre_contacto.Text.Trim();
                    obj_.Email_Empresa = txt_apellido_contacto.Text.Trim();
                    obj_.Id_Sexo = Convert.ToInt32(drop_parentesco.SelectedValue);
                    obj_.Ruta_HV = txt_direc_contacto.Text.Trim();
                    obj_.Celular = txt_number_contacto.Text.Trim();
                    obj_.N_Identificacion = Id_Usuario;
                    Info_Empleado_BRL.InsertarOrUpdate(obj_, 1);
                }
                else
                {
                    DCL.Info_Empleado obj_ = new DCL.Info_Empleado();
                    obj_.Email_Personal = txt_nombre_contacto.Text.Trim();
                    obj_.Email_Empresa = txt_apellido_contacto.Text.Trim();
                    obj_.Id_Sexo = Convert.ToInt32(drop_parentesco.SelectedValue);
                    obj_.Ruta_HV = txt_direc_contacto.Text.Trim();
                    obj_.Celular = txt_number_contacto.Text.Trim();
                    obj_.N_Identificacion = Id_Usuario;
                    Info_Empleado_BRL.InsertarOrUpdate(obj_, 3);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        protected void lnk_actualizar_foto_Click(object sender, EventArgs e)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote") + @"archivos_usuarios\";
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer")+ ambiente + @"\Intranet\archivos_usuarios\");
                string imagenLocal = "";
                string imagenRemota = "";
                string numDocumento = Session["identifacion"].ToString();
                string rutaPerfilRemoto = $@"{pathRemote}{numDocumento}\Perfil\";
                string rutaPerfilLocal = $@"{pathServer}{numDocumento}\Perfil\";
                imagenLocal = rutaPerfilLocal + $@"{numDocumento}" + Path.GetExtension(file_foto.FileName);
                imagenRemota = rutaPerfilRemoto + $@"{numDocumento}" + Path.GetExtension(file_foto.FileName);
                //string archivo = "";

                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                if (file_foto.HasFile)
                {
                    var size = file_foto.FileBytes.Length;
                   
                    if (utilidades.impersonateValidUser())
                    {
                        if (!Directory.Exists(rutaPerfilRemoto))
                        {
                            Directory.CreateDirectory(rutaPerfilRemoto);
                        }

                        if (!Directory.Exists(rutaPerfilLocal))
                        {
                            Directory.CreateDirectory(rutaPerfilLocal);
                        }
                        //if (imagenRemota != null && imagenRemota != $@"{pathRemote}\perfilD.png")
                        //{
                        foreach (var item in Directory.GetFiles(rutaPerfilRemoto, "*.*"))
                        {
                            if (item.Contains($"{numDocumento}"))
                            {
                                File.Delete(item);
                            }
                        }
                        file_foto.SaveAs(imagenRemota);
                        foreach (var item in Directory.GetFiles(rutaPerfilLocal, "*.*"))
                        {
                            if (item.Contains($"{numDocumento}"))
                            {
                                File.Delete(item);
                            }
                        }
                       // }
                       

                        //string extension = Path.GetExtension(file_foto.FileName);
                        //imagenRemota = Path.Combine(rutaPerfilRemoto, $@"{numDocumento.ToString().Replace(" ", String.Empty)}{extension}");
                        file_foto.SaveAs(imagenLocal);
                        
                        
                        obj.Anexo_Foto = imagenRemota;
                        obj.Id_Usuario = Convert.ToInt32(Request.QueryString["Id_Usuario"].ToString());
                        Int_Usuarios_BRL.InsertOrUpdate(obj, 45);
                        utilidades.undoImpersonation();
                    }

                    
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}