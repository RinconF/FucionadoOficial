using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace Intranet_3._0.Vistas.V_Control_General
{
    public partial class V_Crear_Modulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            
            {

                if (Request.QueryString["Id_Usuario"] != null)
                {
                    if (Response.Cookies.Count > 0 && Session["cerrar"] != null)
                    {
                       if (!IsPostBack)
                        {
                            cargar_tabla_vista();
                            cargar_tabla_grupo();
                            cargar_drop();
                            
                        }
                        else
                        {
                            cargar_tabla_vista();
                            cargar_tabla_grupo();
                        }
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cargar_drop()
        {
            try
            {
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                dt = Int_Usuarios_BRL.SelectTable(obj, 4);
                drop_grupo.DataSource = dt;
                drop_grupo.DataTextField = "Nombre_Grupo";
                drop_grupo.DataValueField = "Id_Grupo_Vista";
                drop_grupo.DataBind();
                drop_grupo.Items.Insert(0, new ListItem("-Seleccione grupo-", ""));

                drop_grupos_vista.DataSource = dt;
                drop_grupos_vista.DataTextField = "Nombre_Grupo";
                drop_grupos_vista.DataValueField = "Id_Grupo_Vista";
                drop_grupos_vista.DataBind();
                drop_grupos_vista.Items.Insert(0, new ListItem("-Seleccione grupo-", ""));

                DataTable dt_inha_v;
                DCL.Int_Usuarios obj_inha_v = new DCL.Int_Usuarios();
                dt_inha_v = Int_Usuarios_BRL.SelectTable(obj_inha_v, 21);
                if (dt_inha_v.Rows.Count > 0)
                {
                    drop_estado_vista_re.DataSource = dt_inha_v;
                    drop_estado_vista_re.DataTextField = "Nombre_Vista";
                    drop_estado_vista_re.DataValueField = "Id_Vista";
                    drop_estado_vista_re.DataBind();
                    drop_estado_vista_re.Items.Insert(0, new ListItem("-Seleccione vista-", ""));
                }
                else
                {
                    btn_estado_vista.Attributes.Add("style", "display: none;");
                }


                DataTable dt_;
                DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                dt_ = Int_Usuarios_BRL.SelectTable(obj_, 6);
                drop_rol.DataSource = dt_;
                drop_rol.DataTextField = "Nombre_Rol";
                drop_rol.DataValueField = "Id_Rol";
                drop_rol.DataBind();
                drop_rol.Items.Insert(0, new ListItem("-Seleccione roles-", ""));


                drop_roles.DataSource = dt_;
                drop_roles.DataTextField = "Nombre_Rol";
                drop_roles.DataValueField = "Id_Rol";
                drop_roles.DataBind();
                drop_roles.Items.Insert(0, new ListItem("-Seleccione roles-", ""));


                DataTable dt_inha;
                DCL.Int_Usuarios obj_inha = new DCL.Int_Usuarios();
                dt_inha = Int_Usuarios_BRL.SelectTable(obj_inha, 12);
                if (dt_inha.Rows.Count > 0)
                {
                    drop_grupos_inhabilitado.DataSource = dt_inha;
                    drop_grupos_inhabilitado.DataTextField = "Nombre_Grupo";
                    drop_grupos_inhabilitado.DataValueField = "Id_Grupo_Vista";
                    drop_grupos_inhabilitado.DataBind();
                    drop_grupos_inhabilitado.Items.Insert(0, new ListItem("-Seleccione grupo-", ""));
                }
                else
                {
                    btn_estado_grupo.Attributes.Add("style", "display: none;");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //FUNCIONALES
        #region
        protected void crear_nuevo_modulo(object sender, EventArgs e)
        {
            try
            {
                string icon = txt_icono.Text.Trim();
                if (icon == "")
                {
                    icon = "<i class='fas fa-question'></i>";
                }

                if (
                    !String.IsNullOrEmpty(txt_vista.Text.Trim()) &&
                    !String.IsNullOrEmpty(txt_ruta.Text.Trim()) &&
                    !String.IsNullOrEmpty(txt_descrip.Text.Trim()) &&
                    drop_grupo.SelectedValue != string.Empty
                    )
                {
                    string parametro = Request.QueryString["Id_Usuario"].ToString();

                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Usuario = txt_vista.Text.Trim();
                    obj.Contraseña = txt_descrip.Text.Trim();
                    obj.Usuario_Creacion = icon;
                    obj.Anexo_Foto = txt_ruta.Text.Trim();

                    obj.Id_Usuario = Convert.ToInt32(drop_grupo.SelectedValue);
                    obj.Id_Rol = Convert.ToInt32(parametro);
                    Int_Usuarios_BRL.InsertOrUpdate(obj, 7);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected void actualizar_datos_vista(object sender, EventArgs e)
        {
            try
            {
                string Id_Vista = Request.Form["rd_estado_vista"].ToString();
                string id_user = Request.QueryString["Id_Usuario"].ToString();

                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Usuario = txt_vista_actu.Text.Trim();
                obj.Id_Rol = Convert.ToInt32(drop_estado_vista.SelectedValue);
                obj.Anexo_Foto = txt_vista_descrip_actu.Text.Trim();
                obj.Usuario_Creacion = txt_vista_icono_actu.Text.Trim();
                obj.Usuario_Actualizacion = txt_ruta_act.Text.Trim();
                obj.Contraseña = id_user;
                obj.Id_Usuario = Convert.ToInt32(Id_Vista);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 16);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected void actualizar_estado_vista(object sender, EventArgs e)
        {
            try
            {
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(drop_estado_vista_re.Value);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 20);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        protected void crear_nuevo_grupo(object sender, EventArgs e)
        {
            try
            {
                lnk_crear_.Visible = false;
                string icon = txt_grupo_icono.Text.Trim();
                if (icon == "")
                {
                    icon = "<i class='fas fa-question'></i>";
                }

                if (
                    !String.IsNullOrEmpty(txt_grupo.Text.Trim()) &&
                    !String.IsNullOrEmpty(txt_grupo_descrip.Text.Trim()) &&
                    drop_rol.SelectedValue != string.Empty
                    )
                {
                    string parametro = Request.QueryString["Id_Usuario"].ToString();

                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Usuario = txt_grupo.Text.Trim();
                    obj.Contraseña = txt_grupo_descrip.Text.Trim();
                    obj.Usuario_Creacion = icon;

                    obj.Id_Usuario = Convert.ToInt32(drop_rol.SelectedValue);
                    obj.Id_Rol = Convert.ToInt32(parametro);
                    Int_Usuarios_BRL.InsertOrUpdate(obj, 8);

                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected void actualizar_datos_grupo(object sender, EventArgs e)
        {
            try
            {
                string Id_Grupo = Request.Form["rd_estado_grupo"].ToString();
                string id_user = Request.QueryString["Id_Usuario"].ToString();

                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Usuario = txt_grupo_actu.Text.Trim();
                obj.Id_Rol = Convert.ToInt32(drop_estado_grupo.SelectedValue);
                obj.Anexo_Foto = txt_grupo_descrip_actu.Text.Trim();
                obj.Usuario_Creacion = txt_grupo_icono_actu.Text.Trim();
                obj.Usuario_Actualizacion = id_user;
                obj.Id_Usuario = Convert.ToInt32(Id_Grupo);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 11);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected void actualizar_estado_grupo(object sender, EventArgs e)
        {
            try
            {
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(drop_grupos_inhabilitado.Value);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 14);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
        //CARGAR DATOS TABLAS
        bool consultadoGrupo = false;
        #region
        protected void cargar_tabla_grupo()
        {
            try
            {
                if (consultadoGrupo == false)
                {
                    tbl_grupos.Rows.Clear();

                    HtmlTableCell N = new HtmlTableCell();
                    HtmlTableCell Nombre = new HtmlTableCell();
                    HtmlTableCell Vista = new HtmlTableCell();
                    HtmlTableCell Fecha = new HtmlTableCell();
                    HtmlTableCell Estado = new HtmlTableCell();
                    HtmlTableCell Usuario = new HtmlTableCell();
                    HtmlTableCell Accion = new HtmlTableCell();

                    HtmlTableRow th = new HtmlTableRow();

                    N.InnerText = "#";
                    Nombre.InnerText = "NOMBRE";
                    Vista.InnerText = "VISTAS ASIGNADAS";
                    Fecha.InnerText = "FECHA CREACIÓN";
                    Estado.InnerText = "ESTADO";
                    Usuario.InnerText = "USUARIO CREACIÓN";
                    Accion.InnerText = "ACCIÓN";
                    th.Attributes.Add("class", "th");

                    th.Cells.Add(N);
                    th.Cells.Add(Nombre);
                    th.Cells.Add(Vista);
                    th.Cells.Add(Fecha);
                    th.Cells.Add(Estado);
                    th.Cells.Add(Usuario);
                    th.Cells.Add(Accion);

                    tbl_grupos.Rows.Add(th);


                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    if (txt_filter_grupo.Text.Contains("0") || txt_filter_grupo.Text.Contains("1") || txt_filter_grupo.Text.Contains("2") || txt_filter_grupo.Text.Contains("3") || txt_filter_grupo.Text.Contains("4") || txt_filter_grupo.Text.Contains("5") || txt_filter_grupo.Text.Contains("6") || txt_filter_grupo.Text.Contains("7") || txt_filter_grupo.Text.Contains("8") || txt_filter_grupo.Text.Contains("9"))
                    {
                        obj.Usuario = "";
                    }
                    else
                    {
                        obj.Usuario = txt_filter_grupo.Text;
                    }

                    dt = Int_Usuarios_BRL.SelectTable(obj, 4);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            HtmlTableCell cell1 = new HtmlTableCell();
                            HtmlTableCell cell2 = new HtmlTableCell();
                            HtmlTableCell cell3 = new HtmlTableCell();
                            HtmlTableCell cell4 = new HtmlTableCell();
                            HtmlTableCell cell5 = new HtmlTableCell();
                            HtmlTableCell cell6 = new HtmlTableCell();
                            HtmlTableCell cell7 = new HtmlTableCell();

                            HtmlTableRow td = new HtmlTableRow();

                            cell1.InnerText = row["Id_Grupo_Vista"].ToString();
                            cell2.InnerText = row["Nombre_Grupo"].ToString();

                            DataTable dt_cant;
                            DCL.Int_Usuarios obj_cant = new DCL.Int_Usuarios();
                            obj_cant.Id_Rol = Convert.ToInt32(row["Id_Grupo_Vista"]);
                            dt_cant = Int_Usuarios_BRL.SelectTable(obj_cant, 15);
                            cell3.InnerText = dt_cant.Rows[0]["Total_Vista"].ToString();

                            cell4.InnerText = row["Fecha_Creacion"].ToString();
                            cell5.InnerText = row["Estado"].ToString();
                            cell6.InnerHtml = row["Usuario_Creacion"].ToString();

                            var rd_estado = new HtmlGenericControl("input");
                            rd_estado.Attributes.Add("Type", "radio");
                            rd_estado.Attributes.Add("name", "rd_estado_grupo");
                            rd_estado.Attributes.Add("value", row["Id_Grupo_Vista"].ToString());
                            cell7.Controls.Add(rd_estado);

                            td.Cells.Add(cell1);
                            td.Cells.Add(cell2);
                            td.Cells.Add(cell3);
                            td.Cells.Add(cell4);
                            td.Cells.Add(cell5);
                            td.Cells.Add(cell6);
                            td.Cells.Add(cell7);

                            tbl_grupos.Rows.Add(td);
                        }
                    }
                    consultadoGrupo = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        bool consultadoVista = false;
        protected void cargar_tabla_vista()
        {
            try
            {
                if (consultadoVista == false)
                {
                    tbl_grupos_vista.Rows.Clear();
                    HtmlTableCell N = new HtmlTableCell();
                    HtmlTableCell Nombre = new HtmlTableCell();
                    HtmlTableCell Fecha = new HtmlTableCell();
                    HtmlTableCell Estado = new HtmlTableCell();
                    HtmlTableCell Usuario = new HtmlTableCell();
                    HtmlTableCell Accion = new HtmlTableCell();

                    HtmlTableRow th = new HtmlTableRow();

                    N.InnerText = "#";
                    Nombre.InnerText = "NOMBRE";
                    Fecha.InnerText = "FECHA CREACIÓN";
                    Estado.InnerText = "ESTADO";
                    Usuario.InnerText = "USUARIO CREACIÓN";
                    Accion.InnerText = "ACCIÓN";
                    th.Attributes.Add("class", "th");

                    th.Cells.Add(N);
                    th.Cells.Add(Nombre);
                    th.Cells.Add(Fecha);
                    th.Cells.Add(Estado);
                    th.Cells.Add(Usuario);
                    th.Cells.Add(Accion);

                    tbl_vistas.Rows.Add(th);


                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();


                    if (txt_filter_vista.Text.Contains("0") || txt_filter_vista.Text.Contains("1") || txt_filter_vista.Text.Contains("2") || txt_filter_vista.Text.Contains("3") || txt_filter_vista.Text.Contains("4") || txt_filter_vista.Text.Contains("5") || txt_filter_vista.Text.Contains("6") || txt_filter_vista.Text.Contains("7") || txt_filter_vista.Text.Contains("8") || txt_filter_vista.Text.Contains("9"))
                    {
                        obj.Usuario = "";
                    }
                    else
                    {
                        obj.Usuario = txt_filter_vista.Text;
                    }
                    dt = Int_Usuarios_BRL.SelectTable(obj, 5);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            HtmlTableCell cell1 = new HtmlTableCell();
                            HtmlTableCell cell2 = new HtmlTableCell();
                            HtmlTableCell cell3 = new HtmlTableCell();
                            HtmlTableCell cell4 = new HtmlTableCell();
                            HtmlTableCell cell5 = new HtmlTableCell();
                            HtmlTableCell cell6 = new HtmlTableCell();

                            HtmlTableRow td = new HtmlTableRow();

                            cell1.InnerText = row["Id_Vista"].ToString();
                            cell2.InnerText = row["Nombre_Vista"].ToString();
                            cell3.InnerText = row["Fecha_Creacion"].ToString();
                            cell4.InnerText = row["Estado"].ToString();
                            cell5.InnerHtml = row["Usuario_Creacion"].ToString();

                            var rd_estado = new HtmlGenericControl("input");
                            rd_estado.Attributes.Add("Type", "radio");
                            rd_estado.Attributes.Add("name", "rd_estado_vista");
                            rd_estado.Attributes.Add("value", row["ID"].ToString());
                            cell6.Controls.Add(rd_estado);

                            td.Cells.Add(cell1);
                            td.Cells.Add(cell2);
                            td.Cells.Add(cell3);
                            td.Cells.Add(cell4);
                            td.Cells.Add(cell5);
                            td.Cells.Add(cell6);

                            tbl_vistas.Rows.Add(td);
                        }
                    }
                    consultadoVista = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        protected void txt_filter_grupo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_filter_grupo.Text !="")
                {
                    cargar_tabla_grupo();
                }
                
                //txt_filter_grupo.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txt_filter_vista_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_filter_vista.Text !="")
                {
                    cargar_tabla_vista();
                }
                //cargar_tabla_vista();
                //txt_filter_vista.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}