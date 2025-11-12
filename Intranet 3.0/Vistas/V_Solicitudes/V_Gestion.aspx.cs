using BRL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Solicitudes
{
    public partial class V_Gestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cargar_tabla_vista();
                Cargar_Drop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void cargar_tabla_vista()
        {
            try
            {
                tbl_grupos.Rows.Clear();
                HtmlTableCell Id_Solicitud = new HtmlTableCell();
                HtmlTableCell Cedula = new HtmlTableCell();
                HtmlTableCell CodigoSae = new HtmlTableCell();
                HtmlTableCell Solicitante = new HtmlTableCell();
                HtmlTableCell Cargo = new HtmlTableCell();
                HtmlTableCell TipoSolicitud = new HtmlTableCell();
                HtmlTableCell FechaTentativa1 = new HtmlTableCell();
                HtmlTableCell FechaTentativa2 = new HtmlTableCell();
                HtmlTableCell FechaTentativa3 = new HtmlTableCell();
                HtmlTableCell FechaNacimiento = new HtmlTableCell();
                HtmlTableCell EstadoSolicitud = new HtmlTableCell();
                HtmlTableCell FechaCreacion = new HtmlTableCell();
                HtmlTableCell FechaActualizacion = new HtmlTableCell();
                HtmlTableCell Accion = new HtmlTableCell();

                HtmlTableRow th = new HtmlTableRow();

                Id_Solicitud.InnerText = "Id_Solicitud";
                Cedula.InnerText = "Cedula";
                CodigoSae.InnerText = "Codigo Sae";
                Solicitante.InnerText = "Solicitante";
                Cargo.InnerText = "Cargo";
                TipoSolicitud.InnerText = "Tipo Solicitud";
                FechaTentativa1.InnerText = "Fecha Tentativa1";
                FechaTentativa2.InnerText = "Fecha Tentativa2";
                FechaTentativa3.InnerText = "Fecha Tentativa3";
                FechaNacimiento.InnerText = "Fecha Nacimiento";
                EstadoSolicitud.InnerText = "EstadoSolicitud";
                FechaCreacion.InnerText = "Fecha Creacion";
                FechaActualizacion.InnerText = "Fecha Actualizacion";
                Accion.InnerText = "Accion";
                th.Attributes.Add("class", "th");

                th.Cells.Add(Id_Solicitud);
                th.Cells.Add(Cedula);
                th.Cells.Add(CodigoSae);
                th.Cells.Add(Solicitante);
                th.Cells.Add(Cargo);
                th.Cells.Add(TipoSolicitud);
                th.Cells.Add(FechaTentativa1);
                th.Cells.Add(FechaTentativa2);
                th.Cells.Add(FechaTentativa3);
                th.Cells.Add(FechaNacimiento);
                th.Cells.Add(EstadoSolicitud);
                th.Cells.Add(FechaCreacion);
                th.Cells.Add(FechaActualizacion);
                th.Cells.Add(Accion);

                tbl_grupos.Rows.Add(th);

                Int_Solicitud solicitud = new Int_Solicitud();
                DataTable data = Int_Solicitud_BRL.SelectTable(solicitud, 6);
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
                        HtmlTableCell cell9 = new HtmlTableCell();
                        HtmlTableCell cell10 = new HtmlTableCell();
                        HtmlTableCell cell11 = new HtmlTableCell();
                        HtmlTableCell cell12 = new HtmlTableCell();
                        HtmlTableCell cell13 = new HtmlTableCell();
                        HtmlTableCell cell14 = new HtmlTableCell();

                        HtmlTableRow tableRow = new HtmlTableRow();

                        cell1.InnerText = row["Id_Solicitud"].ToString();
                        cell2.InnerText = row["Cedula"].ToString();
                        cell3.InnerText = row["CodigoSae"].ToString();
                        cell4.InnerText = row["Solicitante"].ToString();
                        cell5.InnerText = row["Cargo"].ToString();
                        cell6.InnerText = row["TipoSolicitud"].ToString();
                        cell7.InnerText = row["FechaTentativa1"].ToString();
                        cell8.InnerText = row["FechaTentativa2"].ToString();
                        cell9.InnerText = row["FechaTentativa3"].ToString();
                        cell10.InnerText = row["FechaNacimiento"].ToString();
                        cell11.InnerText = row["EstadoSolicitud"].ToString();
                        cell12.InnerText = row["FechaCreacion"].ToString();
                        cell13.InnerText = row["FechaActualizacion"].ToString();

                        var rd_estado = new HtmlGenericControl("input");
                        rd_estado.Attributes.Add("type", "radio");
                        rd_estado.Attributes.Add("name", "rd_estado_vista");
                        rd_estado.Attributes["value"] = row["Id_Solicitud"].ToString();
                        cell14.Controls.Add(rd_estado);

                        tableRow.Cells.Add(cell1);
                        tableRow.Cells.Add(cell2);
                        tableRow.Cells.Add(cell3);
                        tableRow.Cells.Add(cell4);
                        tableRow.Cells.Add(cell5);
                        tableRow.Cells.Add(cell6);
                        tableRow.Cells.Add(cell7);
                        tableRow.Cells.Add(cell8);
                        tableRow.Cells.Add(cell9);
                        tableRow.Cells.Add(cell10);
                        tableRow.Cells.Add(cell11);
                        tableRow.Cells.Add(cell12);
                        tableRow.Cells.Add(cell13);
                        tableRow.Cells.Add(cell14);

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
                    HtmlTableCell cell9 = new HtmlTableCell();
                    HtmlTableCell cell10 = new HtmlTableCell();
                    HtmlTableCell cell11 = new HtmlTableCell();
                    HtmlTableCell cell12 = new HtmlTableCell();
                    HtmlTableCell cell13 = new HtmlTableCell();
                    HtmlTableCell cell14 = new HtmlTableCell();

                    HtmlTableRow tableRow = new HtmlTableRow();

                    cell1.InnerText = "No hay datos";
                    cell2.InnerText = "No hay datos";
                    cell3.InnerText = "No hay datos";
                    cell4.InnerText = "No hay datos";
                    cell5.InnerText = "No hay datos";
                    cell6.InnerText = "No hay datos";
                    cell7.InnerText = "No hay datos";
                    cell8.InnerText = "No hay datos";
                    cell9.InnerText = "No hay datos";
                    cell10.InnerText = "No hay datos";
                    cell11.InnerText = "No hay datos";
                    cell12.InnerText = "No hay datos";
                    cell13.InnerText = "No hay datos";

                    var rd_estado = new HtmlGenericControl("input");
                    rd_estado.Attributes.Add("Type", "radio");
                    rd_estado.Attributes.Add("name", "rd_estado_vista");
                    rd_estado.Attributes.Add("value", "0");
                    cell14.Controls.Add(rd_estado);

                    tableRow.Cells.Add(cell1);
                    tableRow.Cells.Add(cell2);
                    tableRow.Cells.Add(cell3);
                    tableRow.Cells.Add(cell4);
                    tableRow.Cells.Add(cell5);
                    tableRow.Cells.Add(cell6);
                    tableRow.Cells.Add(cell7);
                    tableRow.Cells.Add(cell8);
                    tableRow.Cells.Add(cell9);
                    tableRow.Cells.Add(cell10);
                    tableRow.Cells.Add(cell11);
                    tableRow.Cells.Add(cell12);
                    tableRow.Cells.Add(cell13);
                    tableRow.Cells.Add(cell14);

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

                //ddl_tipoSolicitudActualizar.DataSource = data;
                //ddl_tipoSolicitudActualizar.DataValueField = "Id_TipoSolicitud";
                //ddl_tipoSolicitudActualizar.DataTextField = "Nombre";
                //ddl_tipoSolicitudActualizar.DataBind();
                //ddl_tipoSolicitudActualizar.Items.Insert(0, new ListItem("-Seleccione Solicitud-", ""));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void LnkGestionar_Click(object sender, EventArgs e)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_EstadoSolicitud = 2;
                solicitud.Id_UsuarioGestiona = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_UsuarioActualizacion = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_Solicitud = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());
                solicitud.Observacion = "En proceso";
                Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 7);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LnkAceptar_Click(object sender, EventArgs e)
        {
            
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_EstadoSolicitud = 3;
                solicitud.Id_UsuarioGestiona = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_UsuarioActualizacion = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_Solicitud = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());
                solicitud.Observacion = Observaciones.Text;
                solicitud.FechaAprovada = fechaAceptada.Value;
                if (fechaAceptada.Value != "")
                {
                    Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 7);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey2", "alert('Solicitud actualizada con éxito.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "alert('ERROR: Por favor seleccione una fecha.');", true);
                }
                

                Page.Response.Redirect(Page.Request.Url.ToString(), false);

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LnkRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_EstadoSolicitud = 4;
                solicitud.Id_UsuarioGestiona = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_UsuarioActualizacion = Request.QueryString["Id_Usuario"].ToString();
                solicitud.Id_Solicitud = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());
                solicitud.Observacion = Observaciones.Text;
                Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 7);

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}