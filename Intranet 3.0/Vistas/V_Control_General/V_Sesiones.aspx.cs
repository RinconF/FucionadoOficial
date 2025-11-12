using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using BRL;

namespace Intranet_3._0.Vistas.V_Control_General
{
    public partial class V_Sesiones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id_Usuario"] != null)
            {
                //if (!IsPostBack)
                //{
                    cargar_tabla_sesiones();
               
                //}

            }
            else
            {
                Page.Response.Redirect("~/Login", true);
            }
        }

        protected void cargar_tabla_sesiones()
        {
            try
            {
                HtmlTableCell Foto = new HtmlTableCell();
                HtmlTableCell Nombre = new HtmlTableCell();
                HtmlTableCell Id = new HtmlTableCell();
                HtmlTableCell Estado = new HtmlTableCell();
                HtmlTableCell Fecha = new HtmlTableCell();
                HtmlTableCell Accion = new HtmlTableCell();

                HtmlTableRow th = new HtmlTableRow();

                Foto.InnerText = "FOTO";
                Nombre.InnerText = "NOMBRE";
                Id.InnerText = "Identificación";
                Estado.InnerText = "ESTADO";
                Fecha.InnerText = "FECHA Y HORA";
                Accion.InnerText = "ACCIÓN";
                th.Attributes.Add("class", "th");

                th.Cells.Add(Foto);
                th.Cells.Add(Nombre);
                th.Cells.Add(Id);
                th.Cells.Add(Estado);
                th.Cells.Add(Fecha);
                th.Cells.Add(Accion);

                tbl_sesiones.Rows.Add(th);


                DataTable dt;
                
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                if (!txt_filter_grupo.Text.All(char.IsDigit))
                {
                    obj.Usuario = "";
                }
                else
                {
                    obj.Usuario = txt_filter_grupo.Text;
                }
                dt = Int_Usuarios_BRL.SelectTable(obj, 33);
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

                        Panel pnl_foto = new Panel();
                        pnl_foto.CssClass = "img";
                        pnl_foto.Attributes.Add("style", "background-image:url('" + row["Anexo_Foto"].ToString() + "');");
                        cell1.Controls.Add(pnl_foto);

                        cell2.InnerText = row["Nombre"].ToString();
                        cell3.InnerText = row["N_Identificacion"].ToString();
                        cell4.InnerHtml = row["Estado_Sesion"].ToString() + " <i class='fas fa-circle' style='color: #2ecc71;'></i>";
                        cell5.InnerHtml = row["Fecha_Creacion"].ToString();

                        var rd_estado = new HtmlGenericControl("input");
                        rd_estado.Attributes.Add("Type", "radio");
                        rd_estado.Attributes.Add("name", "rd_estado_sesion");
                        rd_estado.Attributes.Add("value", row["Id_Usuario"].ToString());
                        cell6.Controls.Add(rd_estado);

                        td.Cells.Add(cell1);
                        td.Cells.Add(cell2);
                        td.Cells.Add(cell3);
                        td.Cells.Add(cell4);
                        td.Cells.Add(cell5);
                        td.Cells.Add(cell6);

                        tbl_sesiones.Rows.Add(td);
                    }
                }
                //txt_filter_grupo.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Cerrar_Sesion(object sender, EventArgs e)
        {
            try
            {
                string Id_Usuario = Request.Form["rd_estado_sesion"].ToString();
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 36);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void txt_filter_grupo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //cargar_tabla_sesiones();
                //txt_filter_grupo.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}