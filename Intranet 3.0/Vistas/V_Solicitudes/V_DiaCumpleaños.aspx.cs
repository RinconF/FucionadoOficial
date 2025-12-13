using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Solicitudes
{
    public partial class V_DiaCumpleaños : System.Web.UI.Page
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
                        cargar_tabla_vista();
                        Cargar_Drop();
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

        //Se crea contador para evitar repeticiones
        protected void cargar_tabla_vista()
        {
            try
            {
                tbl_grupos.Rows.Clear();
                HtmlTableCell Id_Solicitud = new HtmlTableCell();
                HtmlTableCell TipoSolicitud = new HtmlTableCell();
                HtmlTableCell FechaTentativa1 = new HtmlTableCell();
                HtmlTableCell FechaTentativa2 = new HtmlTableCell();
                HtmlTableCell FechaTentativa3 = new HtmlTableCell();
                HtmlTableCell EstadoSolicitud = new HtmlTableCell();
                HtmlTableCell FechaCreacion = new HtmlTableCell();
                HtmlTableCell Accion = new HtmlTableCell();

                HtmlTableRow th = new HtmlTableRow();

                Id_Solicitud.InnerText = "Id_Solicitud";
                TipoSolicitud.InnerText = "Tipo Solicitud";
                FechaTentativa1.InnerText = "Fecha Tentativa1";
                FechaTentativa2.InnerText = "Fecha Tentativa2";
                FechaTentativa3.InnerText = "Fecha Tentativa3";
                EstadoSolicitud.InnerText = "EstadoSolicitud";
                FechaCreacion.InnerText = "FechaCreacion";
                Accion.InnerText = "Accion";
                th.Attributes.Add("class", "th");

                th.Cells.Add(Id_Solicitud);
                th.Cells.Add(TipoSolicitud);
                th.Cells.Add(FechaTentativa1);
                th.Cells.Add(FechaTentativa2);
                th.Cells.Add(FechaTentativa3);
                th.Cells.Add(EstadoSolicitud);
                th.Cells.Add(FechaCreacion);
                th.Cells.Add(Accion);

                tbl_grupos.Rows.Add(th);

                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_UsuarioCreacion = Request.QueryString["Id_Usuario"].ToString();
                DataTable data = Int_Solicitud_BRL.SelectTable(solicitud, 0);
                if (data.Rows.Count != 0)
                {
                    foreach (DataRow row in data.Rows)
                    {
                        HtmlTableCell cell1 = new HtmlTableCell();
                        HtmlTableCell cell2 = new HtmlTableCell();
                        HtmlTableCell cell3 = new HtmlTableCell();
                        HtmlTableCell cell4 = new HtmlTableCell();
                        HtmlTableCell cell5 = new HtmlTableCell();
                        HtmlTableCell cell6 = new HtmlTableCell();
                        HtmlTableCell cell7 = new HtmlTableCell();
                        HtmlTableCell cell8 = new HtmlTableCell();

                        HtmlTableRow tableRow = new HtmlTableRow();

                        cell1.InnerText = row["Id_Solicitud"].ToString();
                        cell2.InnerText = row["TipoSolicitud"].ToString();
                        cell3.InnerText = row["FechaTentativa1"].ToString();
                        cell4.InnerText = row["FechaTentativa2"].ToString();
                        cell5.InnerText = row["FechaTentativa3"].ToString();
                        cell6.InnerText = row["EstadoSolicitud"].ToString();
                        cell7.InnerText = row["FechaCreacion"].ToString();

                        var rd_estado = new HtmlGenericControl("input");
                        rd_estado.Attributes.Add("type", "radio");
                        rd_estado.Attributes.Add("name", "rd_estado_vista");
                        rd_estado.Attributes["value"] = row["Id_Solicitud"].ToString();
                        cell8.Controls.Add(rd_estado);

                        tableRow.Cells.Add(cell1);
                        tableRow.Cells.Add(cell2);
                        tableRow.Cells.Add(cell3);
                        tableRow.Cells.Add(cell4);
                        tableRow.Cells.Add(cell5);
                        tableRow.Cells.Add(cell5);
                        tableRow.Cells.Add(cell6);
                        tableRow.Cells.Add(cell7);
                        tableRow.Cells.Add(cell8);

                        tbl_grupos.Rows.Add(tableRow);
                    }
                }
                else
                {
                    HtmlTableCell cell1 = new HtmlTableCell();
                    HtmlTableCell cell2 = new HtmlTableCell();
                    HtmlTableCell cell3 = new HtmlTableCell();
                    HtmlTableCell cell4 = new HtmlTableCell();
                    HtmlTableCell cell5 = new HtmlTableCell();
                    HtmlTableCell cell6 = new HtmlTableCell();
                    HtmlTableCell cell7 = new HtmlTableCell();
                    HtmlTableCell cell8 = new HtmlTableCell();

                    HtmlTableRow tableRow = new HtmlTableRow();

                    cell1.InnerText = "No hay datos";
                    cell2.InnerText = "No hay datos";
                    cell3.InnerText = "No hay datos";
                    cell4.InnerText = "No hay datos";
                    cell5.InnerText = "No hay datos";
                    cell6.InnerText = "No hay datos";
                    cell7.InnerText = "No hay datos";

                    var rd_estado = new HtmlGenericControl("input");
                    rd_estado.Attributes.Add("Type", "radio");
                    rd_estado.Attributes.Add("name", "rd_estado_vista");
                    rd_estado.Attributes.Add("value", "0");
                    cell8.Controls.Add(rd_estado);

                    tableRow.Cells.Add(cell1);
                    tableRow.Cells.Add(cell2);
                    tableRow.Cells.Add(cell3);
                    tableRow.Cells.Add(cell4);
                    tableRow.Cells.Add(cell5);
                    tableRow.Cells.Add(cell6);
                    tableRow.Cells.Add(cell7);
                    tableRow.Cells.Add(cell8);

                    tbl_grupos.Rows.Add(tableRow);
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
                var nuevo = ddl_tipoSolicitud.SelectedValue;
                Int_Solicitud solicitud = new Int_Solicitud();
                DataTable data = Int_Solicitud_BRL.SelectTable(solicitud, 1);
                ddl_tipoSolicitud.DataSource = data;
                ddl_tipoSolicitud.DataValueField = "Id_TipoSolicitud";
                ddl_tipoSolicitud.DataTextField = "Nombre";
                ddl_tipoSolicitud.DataBind();
                ddl_tipoSolicitud.Items.Insert(0, new ListItem("-Seleccione Solicitud-", ""));

                ddl_tipoSolicitudActualizar.DataSource = data;
                ddl_tipoSolicitudActualizar.DataValueField = "Id_TipoSolicitud";
                ddl_tipoSolicitudActualizar.DataTextField = "Nombre";
                ddl_tipoSolicitudActualizar.DataBind();
                ddl_tipoSolicitudActualizar.Items.Insert(0, new ListItem("-Seleccione Solicitud-", ""));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        protected void LnkCrear_Click(object sender, EventArgs e)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_TipoSolicitud = Convert.ToInt32(tipoSolicitud.Value);
                solicitud.Id_UsuarioCreacion = Request.QueryString["Id_Usuario"].ToString();
                DataTable data = Int_Solicitud_BRL.SelectTable(solicitud, 10);
                string estadoSolic = "";
                bool solicitudAbierta = false;
                if (data.Rows.Count != 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        estadoSolic = data.Rows[i]["EstadoSolicitud"].ToString();
                        if (estadoSolic == "Abierta" || estadoSolic == "En Proceso")
                        {
                            solicitudAbierta = true;
                            break;
                        }
                        else
                        {
                            solicitudAbierta = false;
                        }
                    }

                }
                if (!solicitudAbierta)
                {
                    solicitud.FechaTentativa1 = FechaTentativa1.Text;
                    solicitud.FechaTentativa2 = FechaTentativa2.Text;
                    solicitud.FechaTentativa3 = FechaTentativa3.Text;

                    Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 3);
                    Page.Response.Redirect(Page.Request.Url.ToString(), true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('Ya existe una solicitud creada del mismo tipo. Solicitud NO CREADA. Por favor verifique.');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        protected void LnkActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_TipoSolicitud = Convert.ToInt32(tipoSolicitudActualizar.Value);
                solicitud.FechaTentativa1 = FechaTentativa1Actualizar.Text;
                solicitud.FechaTentativa2 = FechaTentativa2Actualizar.Text;
                solicitud.FechaTentativa3 = FechaTentativa3Actualizar.Text;
                solicitud.Id_UsuarioActualizacion = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_Solicitud = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());
                Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 5);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LnkAnular_Click(object sender, EventArgs e)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
               solicitud.Id_EstadoSolicitud = 5;
                solicitud.Id_Solicitud = Convert.ToInt32(Convert.ToInt32(Request.Form["rd_estado_vista"].ToString()));
                Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 9);
                Page.Response.Redirect(Page.Request.Url.ToString(), false);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
    }
}