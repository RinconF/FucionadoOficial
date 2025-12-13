using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BRL;

namespace Intranet_3._0.Vistas.V_Control_General
{
    public partial class V_Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id_Usuario"] != null)
            {
                if (Response.Cookies.Count > 0 && Session["cerrar"] != null)
                {
                    if (!IsPostBack)
                    {
                        cargar_tabla_roles();
                        cargar_drop();

                    }
                    else
                    {
                        cargar_tabla_roles();
                    }
                }
                else
                {
                    Page.Response.Redirect("~/Login", false);
                }
            }
            else
            {
                Page.Response.Redirect("~/Login", true);
            }
        }

        protected void cargar_drop()
        {
            try
            {
                DataTable dt_inha;
                DCL.Int_Usuarios obj_inha = new DCL.Int_Usuarios();
                dt_inha = Int_Usuarios_BRL.SelectTable(obj_inha, 25);
                if (dt_inha.Rows.Count > 0)
                {
                    drop_rol_inhabilitado.DataSource = dt_inha;
                    drop_rol_inhabilitado.DataTextField = "Nombre_Rol";
                    drop_rol_inhabilitado.DataValueField = "Id_Rol";
                    drop_rol_inhabilitado.DataBind();
                    drop_rol_inhabilitado.Items.Insert(0, new ListItem("-Seleccione rol-", ""));
                }
                else
                {
                    btn_estado_rol.Attributes.Add("style", "display: none;");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region
        protected void crear_nuevo_rol(object sender, EventArgs e)
        {
            try
            {
                if (
                    !String.IsNullOrEmpty(txt_rol.Text.Trim()) &&
                    !String.IsNullOrEmpty(txt_rol_descrip.Text.Trim())
                    )
                {
                    string parametro = Request.QueryString["Id_Usuario"].ToString();

                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Usuario = txt_rol.Text.Trim();
                    obj.Contraseña = txt_rol_descrip.Text.Trim();
                    obj.Usuario_Creacion = parametro;
                    Int_Usuarios_BRL.InsertOrUpdate(obj, 22);

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
        protected void actualizar_datos_roles(object sender, EventArgs e)
        {
            try
            {
                string Id_Rol = Request.Form["rd_estado_rol"].ToString();
                string id_user = Request.QueryString["Id_Usuario"].ToString();

                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Usuario = txt_rol_actu.Text.Trim();
                obj.Id_Rol = Convert.ToInt32(drop_estado_rol.SelectedValue);
                obj.Anexo_Foto = txt_rol_descrip_actu.Text.Trim();
                obj.Usuario_Actualizacion = id_user;
                obj.Id_Usuario = Convert.ToInt32(Id_Rol);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 24);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        protected void actualizar_estado_rol(object sender, EventArgs e)
        {
            try
            {
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(drop_rol_inhabilitado.Value);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 26);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region
        protected void cargar_tabla_roles()
        {
            try
            {
                tbl_roles.Rows.Clear();
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

                tbl_roles.Rows.Add(th);


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
                
                dt = Int_Usuarios_BRL.SelectTable(obj, 6);
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

                        cell1.InnerText = row["Id_Rol"].ToString();
                        cell2.InnerText = row["Nombre_Rol"].ToString();
                        cell3.InnerText = row["Fecha_Creacion"].ToString();
                        cell4.InnerText = row["Estado"].ToString();
                        cell5.InnerHtml = row["Usuario_Creacion"].ToString();

                        var rd_estado = new HtmlGenericControl("input");
                        rd_estado.Attributes.Add("Type", "radio");
                        rd_estado.Attributes.Add("name", "rd_estado_rol");
                        rd_estado.Attributes.Add("value", row["Id_Rol"].ToString());
                        cell6.Controls.Add(rd_estado);

                        td.Cells.Add(cell1);
                        td.Cells.Add(cell2);
                        td.Cells.Add(cell3);
                        td.Cells.Add(cell4);
                        td.Cells.Add(cell5);
                        td.Cells.Add(cell6);

                        tbl_roles.Rows.Add(td);
                    }
                }
                //txt_filter_grupo.Text = "";
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
                //cargar_tabla_roles();
                txt_filter_grupo.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}