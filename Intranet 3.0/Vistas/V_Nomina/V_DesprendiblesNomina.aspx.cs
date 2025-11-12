using System;
using System.Data;
using BRL;
using Intranet_3._0.Interna;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Intranet_3._0.Vistas.V_Nomina
{
    public partial class V_DesprendiblesNomina : System.Web.UI.Page
    {
        DataTable tablaDatosNomina;
        DataTable tablaDatosNominaNoSalarial;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DesprendibleNomina.Visible = false;
                DesprendibleNoSalarial.Visible = false;
                if (!IsPostBack)
                {
                    CargarAños();
                }
                pnl_Resultado.Visible = false;
            }
            catch { }
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos
                if (!ValidarCampos())
                {
                    return;
                }

                // Obtener los valores seleccionados de los controles DropDownList
                string numDocumento = Session["numDocumento"].ToString().Trim();
                string month = DropDownListMonth.SelectedValue;
                string year = DropDownListYear.SelectedValue;
                string Periodo = DropDownListPeriod.SelectedValue;

                if (!string.IsNullOrEmpty(numDocumento))
                {
                    tablaDatosNomina = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                    {
                        Periodo = Convert.ToInt32(Periodo),
                        Mes = Convert.ToInt32(month),
                        Year = Convert.ToInt32(year),
                        N_Identificacion = Convert.ToInt32(numDocumento)
                    }, 0);

                    if (tablaDatosNomina.Rows.Count > 0)
                    {
                        // CARGO DATOS DEL COLABORADOR Y ENCABEZADO
                        if (tablaDatosNomina.Rows.Count > 0)
                        {
                            lbl_Dirección.Text = tablaDatosNomina.Rows[0][0].ToString();
                            lbl_Email.Text = tablaDatosNomina.Rows[0][1].ToString();
                            lbl_Telefono.Text = tablaDatosNomina.Rows[0][2].ToString();
                            lbl_IdentificadorDocumento.Text = tablaDatosNomina.Rows[0][3].ToString();
                            lbl_FechaIngreso.Text = tablaDatosNomina.Rows[0][4].ToString();
                            lbl_NombreColaborador.Text = tablaDatosNomina.Rows[0][5].ToString();
                            lbl_CargoColaborador.Text = tablaDatosNomina.Rows[0][6].ToString();
                            lbl_SueldoColaborador.Text = tablaDatosNomina.Rows[0][7].ToString();
                            lbl_CedulaColaborador.Text = tablaDatosNomina.Rows[0][8].ToString();
                            lbl_CuentaColaborador.Text = tablaDatosNomina.Rows[0][9].ToString();
                            lbl_EpsColaborador.Text = tablaDatosNomina.Rows[0][10].ToString();
                            lbl_EntidadPension.Text = tablaDatosNomina.Rows[0][11].ToString();
                            lbl_EntidadBancaria.Text = tablaDatosNomina.Rows[0][12].ToString();
                            lbl_ConceptoCosto.Text = tablaDatosNomina.Rows[0][13].ToString();
                            lbl_TipoCuenta.Text = tablaDatosNomina.Rows[0][27].ToString();
                            lbl_NombreEmpresa.Text = tablaDatosNomina.Rows[0][32].ToString();
                            lbl_NombreComprobante.Text = tablaDatosNomina.Rows[0][33].ToString();
                            lbl_NombreQuicena.Text = tablaDatosNomina.Rows[0][34].ToString();
                            lbl_Periodo.Text = tablaDatosNomina.Rows[0][35].ToString();
                            lbl_FechaHora.Text = tablaDatosNomina.Rows[0][36].ToString();

                        }

                        // RELACION DE CONCEPTOS
                        if (tablaDatosNomina.Rows.Count > 0)
                        {
                            for (int i = 0; i < tablaDatosNomina.Rows.Count; i++)
                            {
                                HtmlTableCell cell1 = new HtmlTableCell();
                                HtmlTableCell cell2 = new HtmlTableCell();
                                HtmlTableCell cell3 = new HtmlTableCell();
                                HtmlTableCell cell4 = new HtmlTableCell();
                                HtmlTableCell cell5 = new HtmlTableCell();
                                HtmlTableCell cell6 = new HtmlTableCell();
                                HtmlTableCell cell7 = new HtmlTableCell();
                                HtmlTableCell cell8 = new HtmlTableCell();

                                cell1.InnerText = tablaDatosNomina.Rows[i][14].ToString();
                                cell2.InnerText = tablaDatosNomina.Rows[i][15].ToString();
                                cell3.InnerText = tablaDatosNomina.Rows[i][16].ToString();
                                cell4.InnerText = tablaDatosNomina.Rows[i][17].ToString();
                                cell5.InnerText = tablaDatosNomina.Rows[i][18].ToString();
                                cell6.InnerText = tablaDatosNomina.Rows[i][19].ToString();
                                cell7.InnerText = tablaDatosNomina.Rows[i][20].ToString();
                                cell8.InnerText = tablaDatosNomina.Rows[i][21].ToString();

                                HtmlTableRow row = new HtmlTableRow();
                                row.Attributes.Add("style", "border: none;");
                                row.Cells.Add(cell1);
                                row.Cells.Add(cell2);
                                row.Cells.Add(cell3);
                                row.Cells.Add(cell4);
                                row.Cells.Add(cell5);
                                row.Cells.Add(cell6);
                                row.Cells.Add(cell7);
                                row.Cells.Add(cell8);
                                tbl_ConceptosNomina.Rows.Add(row);
                            }
                        }
                        else { tbl_ConceptosNomina.Visible = false; }

                        // TOTAL DE LOS CONCEPTOS
                        if (tablaDatosNomina.Rows.Count > 0)
                        {
                            lbl_TotalUnidades.Text = tablaDatosNomina.Rows[0][28].ToString();
                            lbl_TotalValorDevengo.Text = tablaDatosNomina.Rows[0][29].ToString();
                            lbl_TotalValorDeduccion.Text = tablaDatosNomina.Rows[0][30].ToString();
                            lbl_TotalNeto.Text = tablaDatosNomina.Rows[0][31].ToString();
                        }

                        // PROCESO PARA DESPRENDIBLE DE PAGOS NO SALARIALES
                        if (Periodo == "2")
                        {
                            tablaDatosNominaNoSalarial = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                            {
                                Periodo = Convert.ToInt32(Periodo),
                                Mes = Convert.ToInt32(month),
                                Year = Convert.ToInt32(year),
                                N_Identificacion = Convert.ToInt32(numDocumento)
                            }, 1);

                            // CARGO DATOS DEL COLABORADOR Y ENCABEZADO
                            if (tablaDatosNominaNoSalarial.Rows.Count > 0)
                            {
                                //lbl_NS_Dirección.Text = tablaDatosNominaNoSalarial.Rows[0][0].ToString();
                                //lbl_NS_Email.Text = tablaDatosNominaNoSalarial.Rows[0][1].ToString();
                                //lbl_NS_Telefono.Text = tablaDatosNominaNoSalarial.Rows[0][2].ToString();
                                //lbl_NS_IdentificadorDocumento.Text = tablaDatosNominaNoSalarial.Rows[0][3].ToString();
                                //lbl_NS_FechaIngreso.Text = tablaDatosNominaNoSalarial.Rows[0][4].ToString();
                                //lbl_NS_NombreColaborador.Text = tablaDatosNominaNoSalarial.Rows[0][5].ToString();
                                //lbl_NS_CargoColaborador.Text = tablaDatosNominaNoSalarial.Rows[0][6].ToString();
                                //lbl_NS_SueldoColaborador.Text = tablaDatosNominaNoSalarial.Rows[0][7].ToString();
                                //lbl_NS_CedulaColaborador.Text = tablaDatosNominaNoSalarial.Rows[0][8].ToString();
                                //lbl_NS_ConceptoCosto.Text = tablaDatosNominaNoSalarial.Rows[0][13].ToString();
                                //lbl_NS_TipoCuenta.Text = tablaDatosNominaNoSalarial.Rows[0][27].ToString();
                                //lbl_NS_NombreEmpresa.Text = tablaDatosNominaNoSalarial.Rows[0][32].ToString();
                                lbl_NS_NombreComprobante.Text = tablaDatosNominaNoSalarial.Rows[0][33].ToString();
                                //lbl_NS_NombreQuicena.Text = tablaDatosNominaNoSalarial.Rows[0][34].ToString();
                                //lbl_NS_Periodo.Text = tablaDatosNominaNoSalarial.Rows[0][35].ToString();
                                //lbl_NS_FechaHora.Text = tablaDatosNominaNoSalarial.Rows[0][36].ToString();

                                //Campos no necesarios para pagos NO salariales
                                //lbl_NS_CuentaColaborador.Text = tablaDatosNominaNoSalarial.Rows[0][9].ToString();
                                //lbl_NS_EpsColaborador.Text = tablaDatosNominaNoSalarial.Rows[0][10].ToString();
                                //lbl_NS_EntidadPension.Text = tablaDatosNominaNoSalarial.Rows[0][11].ToString();
                                //lbl_NS_EntidadBancaria.Text = tablaDatosNominaNoSalarial.Rows[0][12].ToString();

                                DesprendibleNoSalarial.Visible = true;
                            }
                            else { DesprendibleNoSalarial.Visible = false; }

                            // RELACION DE CONCEPTOS
                            if (tablaDatosNominaNoSalarial.Rows.Count > 0)
                            {
                                for (int i = 0; i < tablaDatosNominaNoSalarial.Rows.Count; i++)
                                {
                                    HtmlTableCell cell1 = new HtmlTableCell();
                                    HtmlTableCell cell2 = new HtmlTableCell();
                                    HtmlTableCell cell3 = new HtmlTableCell();
                                    HtmlTableCell cell4 = new HtmlTableCell();
                                    HtmlTableCell cell5 = new HtmlTableCell();
                                    HtmlTableCell cell6 = new HtmlTableCell();
                                    HtmlTableCell cell7 = new HtmlTableCell();
                                    HtmlTableCell cell8 = new HtmlTableCell();

                                    cell1.InnerText = tablaDatosNominaNoSalarial.Rows[i][14].ToString();
                                    cell2.InnerText = tablaDatosNominaNoSalarial.Rows[i][15].ToString();
                                    cell3.InnerText = tablaDatosNominaNoSalarial.Rows[i][16].ToString();
                                    cell4.InnerText = tablaDatosNominaNoSalarial.Rows[i][17].ToString();
                                    cell5.InnerText = tablaDatosNominaNoSalarial.Rows[i][18].ToString();
                                    cell6.InnerText = tablaDatosNominaNoSalarial.Rows[i][19].ToString();
                                    cell7.InnerText = tablaDatosNominaNoSalarial.Rows[i][20].ToString();
                                    cell8.InnerText = tablaDatosNominaNoSalarial.Rows[i][21].ToString();

                                    HtmlTableRow row = new HtmlTableRow();
                                    row.Attributes.Add("style", "border: none;");
                                    row.Cells.Add(cell1);
                                    row.Cells.Add(cell2);
                                    row.Cells.Add(cell3);
                                    row.Cells.Add(cell4);
                                    row.Cells.Add(cell5);
                                    row.Cells.Add(cell6);
                                    row.Cells.Add(cell7);
                                    row.Cells.Add(cell8);
                                    tbl_ConceptosNoSalariales.Rows.Add(row);
                                }
                                tbl_ConceptosNoSalariales.Visible = true;
                            }
                            else { tbl_ConceptosNoSalariales.Visible = false; }

                            // TOTAL DE LOS CONCEPTOS
                            if (tablaDatosNominaNoSalarial.Rows.Count > 0)
                            {
                                lbl_NS_TotalUnidades.Text = tablaDatosNominaNoSalarial.Rows[0][28].ToString();
                                lbl_NS_TotalValorDevengo.Text = tablaDatosNominaNoSalarial.Rows[0][29].ToString();
                                lbl_NS_TotalValorDeduccion.Text = tablaDatosNominaNoSalarial.Rows[0][30].ToString();
                                lbl_NS_TotalNeto.Text = tablaDatosNominaNoSalarial.Rows[0][31].ToString();

                                tbl_NS_TotalNeto.Visible = true;
                            }
                            else { tbl_NS_TotalNeto.Visible = false; }
                        }

                        DesprendibleNomina.Visible = true;
                        tbl_ConceptosNomina.Visible = true;
                        tbl_TotalesConcepto.Visible = true;
                        tbl_TotalNeto.Visible = true;
                        pnl_Resultado.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('Señor colaborador, no se han encontrado datos, por favor intente nuevamente.');", true);
                        DropDownListMonth.SelectedIndex = 0;
                        DropDownListYear.SelectedIndex = 0;
                        DropDownListPeriod.SelectedIndex = 0;
                        return;
                    }
                }
            }
            catch { }
        }

        private bool ValidarCampos()
        {
            // Verificar si los campos están vacíos
            if (string.IsNullOrEmpty(DropDownListMonth.SelectedValue) ||
                string.IsNullOrEmpty(DropDownListYear.SelectedValue) ||
                string.IsNullOrEmpty(DropDownListPeriod.SelectedValue)

                || DropDownListMonth.SelectedValue == "0" ||
                DropDownListYear.SelectedValue == "0" ||
                DropDownListPeriod.SelectedValue == "0")
            {
                // Mostrar mensaje de advertencia
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey2",
                    "alert('Los campos no pueden quedar vacíos, por favor intente nuevamente.');", true);
                return false;
            }
            return true;
        }

        private void LimpiarCamposYOcultarDesprendibles()
        {
            try
            {
                // Limpiar las tablas de conceptos
                tbl_ConceptosNomina.Rows.Clear();
                tbl_ConceptosNoSalariales.Rows.Clear();
                // Ocultar las secciones de desprendibles
                DesprendibleNomina.Visible = false;
                DesprendibleNoSalarial.Visible = false;
                tbl_ConceptosNomina.Visible = false;
                tbl_ConceptosNoSalariales.Visible = false;
                tbl_TotalesConcepto.Visible = false;
                tbl_TotalNeto.Visible = false;
                tbl_NS_TotalNeto.Visible = false;
            }
            catch { }
        }

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Llamar al método para limpiar campos y ocultar desprendibles
            LimpiarCamposYOcultarDesprendibles();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListMonth.SelectedIndex = 0;
                DropDownListYear.SelectedIndex = 0;
                DropDownListPeriod.SelectedIndex = 0;
                // Llamar al método para limpiar campos y ocultar desprendibles
                LimpiarCamposYOcultarDesprendibles();
            }
            catch { }
        }

        private void CargarAños()
        {
            try
            { 
                int añoActual = DateTime.Now.Year;
                int añosAMostrar = 24; // Número de años hacia atrás que quieres mostrar

                for (int i = añoActual; i >= añoActual - añosAMostrar; i--)
                {
                    DropDownListYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            catch {}
        }
    }
}   