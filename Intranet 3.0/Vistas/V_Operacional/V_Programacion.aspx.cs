using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BRL;
using DCL;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Drawing;
using System.Configuration;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Web.Services.Description;
using iTextSharp.text.pdf.parser;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Ajax.Utilities;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Intranet_3._0.Interna;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Intranet_3._0.Vistas.V_Operacional
{
    public partial class V_Programacion : System.Web.UI.Page
    {
        public DataTable dtHorario;
        public string codigo = "", conductor = "", fecha = "", asignacion = "", amplitud = "", produccion = "";
        public DataTable dtPart1;
        public DataTable dtPart2;
        public string IniPar1, FinPart1;
        public string IniPar2, FinPart2;
        public string parada;
        public string Mensaje = "";
        string codigoSAE = "";
        string numDocumento = "";
        string fechaInicio = "";
        string fechaFin = "";
        string tipoUsuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //AGR_12-10-2022:Al cargar la página se verifica que la variable de sesion "Id_Usuario" contenga datos
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["Cargo"].ToString()))
                    {
                        pnl_Resultado.Visible = false;
                        tipoUsuario = Session["Cargo"].ToString();
                        if (!IsPostBack)
                        {

                            //AGR_12-10-2022:En caso de que el usuario sea un operador, llena campos de texto txtCedula, txtCode y bloquea los mismos. En caso de cargo diferente, no bloquea estos controles.
                            if (tipoUsuario.Contains("OPERADOR"))
                            {
                                codigoSAE = Session["codSAE"].ToString();
                                numDocumento = Session["numDocumento"].ToString();
                                txtCedula.Text = numDocumento;
                                txtCode.Text = codigoSAE.Replace("ET0", "");
                                if (txtCode.Text.StartsWith("9"))
                                {
                                    txtCode.Text = txtCode.Text.Remove(0, 1);
                                    txtCode.Text = "7" + txtCode.Text;
                                }
                                txtCedula.Enabled = false;
                                txtCode.Enabled = false;
                            }
                            else
                            {
                                txtCedula.Enabled = true;
                                txtCode.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Page.Response.Redirect("~/Login", true);
            }
            
        }

        #region Eventos Controles
        //botones
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_Resultado.Dispose();
                //AGR_12-10-2022:Consulta la información del colaborador según los parámetros dados
                ConsultarInfo();
                if (dtHorario != null)
                {
                    if (dtHorario.Rows.Count == 0)
                    {
                        MostrarError("No se encontró información, verifique los datos ingresados.");
                        return;
                    }

                //AGR_12-10-2022: Separa la información en tablas (horario)
                    SepararTablas(dtHorario);

                    //AGR_12-10-2022: muestra el control pnl_resultado en caso de que los registros obtenidos en el método anterios, arroje resultados
                    if (dtHorario.Rows.Count > 0)
                    {
                        if (!pnl_Resultado.Visible)
                        {
                            pnl_Resultado.Visible = true;
                        }
                    }
                }
                else
                {
                    pnl_Resultado.Visible = false;
                }
            }
            catch(Exception ex)
            {

            }
        }

        
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            LimpiarCampos(tipoUsuario);
        }

        /// <summary>
        /// AGR_12-10-2022: boton para limpiar campos e iniciar una nueva consulta
        /// </summary>
        /// <param name="tipoUsuario"></param>
        private void LimpiarCampos(string tipoUsuario)
        {
            dtHorario = null;
            Calendar1.SelectedDate = DateTime.Now;
            Cal_1.Visible = false;
            Calendar2.SelectedDate = DateTime.Now;
            Cal_2.Visible = false;
            txtFecIni.Text = "";
            txtFecIni.Text.DefaultIfEmpty();
            txtFecFin.Text = "";
            txtFecFin.Text.DefaultIfEmpty();
            pnl_Resultado.Visible = false;

            //AGR_13-10-2022: se implementa switch pensando en perfiles a futuro
            if (tipoUsuario.Contains("OPERADOR"))
            {
                tipoUsuario = "OPERADOR";
            }
            switch (tipoUsuario)
            {
                case ("OPERADOR"):
                   
                    break;

                default:
                    txtCode.Text = "";
                    txtCode.Text.DefaultIfEmpty();
                    txtCedula.Text = "";
                    txtCedula.Text.DefaultIfEmpty();
                    lblinfoCod.Text = "";
                    lblInfoConductor.Text = "";
                    break;
            }
        }

        
        //calendarios
        protected void abrirFechaInicio(object sender, EventArgs e)
        {

            //AGR_12-10-2022:Validación de visualización de calendarios, oculta o muestra según se requiera.
            if (Cal_1.Visible)
            {
                Cal_1.Visible = false;
            }
            else
            {
                Calendar1.SelectedDates.Clear();
                Cal_1.Visible = true;
            }
            if (Cal_2.Visible)
            {
                Cal_2.Visible = false;
            }
        }


        //protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        //{

        ////AGR_11-10-2022: Funcion para limitar el calendario a fecha de inicio un mes
        ////No es muy amigable con el usuario. Deshabilitado pero se mantiene código en caso de llegar a requerirse.Habilitar desde los enventos del control front modo diseño
        //DateTime control1 = DateTime.Today.AddMonths(-1);

        //    if (e.Day.Date > control1)
        //    {
        //        e.Day.IsSelectable = true;
        //    }
        //    else
        //    {
        //        e.Day.IsSelectable = false;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('No es posible consultar programación en estas fechas. La fecha inicial es inferior a la permitida: un(1) mes.');", true);

        //    }
        //}

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime limiteFechaInicio;
            txtFecIni.Text = Calendar1.SelectedDate.ToShortDateString();

            //AGR_12-10-2022:Se realiza validación, en caso de ser operador, no se permitirá seleccionar una fecha inferior a un mes "DateTime.Today.AddMonths(-1)". cargo diferente, 3 meses
            if (tipoUsuario.Contains("OPERADOR"))
            {
                limiteFechaInicio = DateTime.Today.AddMonths(-1);
            }
            else
            {
                limiteFechaInicio = DateTime.Today.AddMonths(-3);
            }

            //AGR_12-10-2022:Presenta mensaje en caso de que la fecha inicial sea superior a la fecha final
            if (Calendar1.SelectedDate < limiteFechaInicio)
            {
                if (tipoUsuario.Contains("OPERADOR"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('No es posible consultar programación en estas fechas. La fecha inicial es inferior a la permitida: un(1) mes.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey2", "alert('No es posible consultar programación en estas fechas. La fecha inicial es inferior a la permitida: tres(3) meses.');", true);
                }
                txtFecIni.Text = "";
            }

            //AGR_12-10-2022:inmediatamente se cambie la fecha, el control se ocultará
            Cal_1.Visible = false;
        }
        protected void abrirFechaFin(object sender, EventArgs e)
        {
            //AGR_12-10-2022:Validación de visualización de calendarios, oculta o muestra según se requiera.
            if (Cal_2.Visible)
            {
                Cal_2.Visible = false;
            }
            else
            {
                Calendar2.SelectedDates.Clear();
                Cal_2.Visible = true;
            }

            if (Cal_1.Visible)
            {
                Cal_1.Visible = false;
            }
        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            txtFecFin.Text = Calendar2.SelectedDate.ToShortDateString();
            Cal_2.Visible = false;
        }

        #endregion

        #region Generar Plantillas

        private void ConsultarInfo()
        {
            //AGR_12-10-2022: oculta calendarios y limpia datatable
            Cal_1.Visible = false;
            Cal_2.Visible = false;
            dtHorario = null;
            try
            {
                DCL.ConsultaH ObjConsul = new DCL.ConsultaH();

                if (string.IsNullOrEmpty(codigoSAE) && !string.IsNullOrEmpty(txtCode.Text))
                {
                    //AGR_12-10-2022:reemplaza código SAE de textbox(Ej: 701234) por el código establecido en mensajes de programación y base de datos BIT_V2 (ET0901234)
                    if (txtCode.Text.StartsWith("7"))
                    {
                        ObjConsul.Codigo = "ET09" + txtCode.Text.Remove(0, 1);
                    }
                    else
                    {
                        ObjConsul.Codigo = "ET0" + txtCode.Text;
                    }

                }
                else
                {
                    ObjConsul.Codigo = codigoSAE;
                }

                ObjConsul.Cedula = txtCedula.Text.Trim();

                //AGR_12-10-2022:Valida que los campos fecha inicial y final se encuentren diligenciados
                if (string.IsNullOrEmpty(txtFecIni.Text) || string.IsNullOrEmpty(txtFecFin.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey2", "alert('Debe seleccionar una fecha Inicial y una fecha Final.');", true);
                }
                else
                {
                    //AGR_12-10-2022: valida que la fecha final sea superior a la fecha inicial
                    if (Convert.ToDateTime(txtFecIni.Text) > Convert.ToDateTime(txtFecFin.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "alert('La fecha final debe ser superior a la fecha inicial.');", true);
                    }
                    else
                    {
                        fechaInicio = txtFecIni.Text.Trim();
                        ObjConsul.Fecha = fechaInicio;
                        fechaFin = txtFecFin.Text.Trim();
                        ObjConsul.Hora = fechaFin;

                        //AGR_12-10-2022:Ejecuta action de base de datos según sea operador (action 0) o no (action 2)
                        if (tipoUsuario.Contains("OPERADOR"))
                        {
                            DataTable dtOperadorInfo = ConsultaH_BRL.selectHorario(ObjConsul, 3);

                            if (dtOperadorInfo.Rows.Count > 0)
                            {
                                DataRow row = dtOperadorInfo.Rows[0];

                                string freewayCode = row["FreewayCode"].ToString();
                                string isJoined = row["isJoined"].ToString();
                                string identificationDocument = row["identificationDocument"].ToString();
                                string bitEnterpriseCode = row["BitEnterpriseCode"].ToString();
                                string identificationDocumentBit = row["IdentificationDocumentBit"].ToString();

                                if (isJoined == "False")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('Hubo un problema al consultar la programacíon. Por favor, dirígete al área de programación para más detalles.');", true);
                                    return;
                                }

                                if ((string.IsNullOrEmpty(freewayCode) && isJoined == "True") || (!string.IsNullOrEmpty(freewayCode) && string.IsNullOrEmpty(bitEnterpriseCode) || (string.IsNullOrEmpty(freewayCode) && string.IsNullOrEmpty(bitEnterpriseCode) && isJoined == "True")))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey2", "alert('No tienes un codigo asignado. Por favor, dirígete al área adminsitrativa o área de programación.');", true);
                                    return;
                                }


                                if (!string.IsNullOrEmpty(freewayCode) && !string.IsNullOrEmpty(bitEnterpriseCode) && freewayCode != bitEnterpriseCode)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey5", "alert('Hubo un problema con tu código. No es posible consultar la programación. Por favor, dirígete al área adminsitrativa o área de programación para más detalles.');", true);
                                    return;
                                }

                                if (isJoined == "True")
                                {
                                    dtHorario = ConsultaH_BRL.selectHorario(ObjConsul, 0);
                                    if (dtHorario.Rows.Count == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey5", "alert('La busqueda no arrojó resultados. Consulte nuevamente en unos minutos.');", true);
                                        pnl_Resultado.Visible = false;
                                    }
                                    else
                                    {
                                        pnl_Resultado.Visible = true;
                                    }

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "alert('Código o cédula erróneos o no existentes');", true);
                                return;
                            }
                        }
                        else
                        {

                            DataTable dtOperadorInfo = ConsultaH_BRL.selectHorario(ObjConsul, 3);

                            if (dtOperadorInfo.Rows.Count > 0)
                            {
                                DataRow row = dtOperadorInfo.Rows[0];

                                string freewayCode = row["FreewayCode"].ToString();
                                string isJoined = row["isJoined"].ToString();
                                string identificationDocument = row["identificationDocument"].ToString();
                                string bitEnterpriseCode = row["BitEnterpriseCode"].ToString();
                                string identificationDocumentBit = row["IdentificationDocumentBit"].ToString();

                                if (isJoined == "False")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('El operador está inactivo en Freeway.');", true);
                                    return;
                                }

                                if (string.IsNullOrEmpty(freewayCode) && string.IsNullOrEmpty(bitEnterpriseCode) && isJoined == "True")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "alert('El operador está activo en Freeway pero no tiene código en Freeway ni en Bit Enterprise.');", true);
                                    return;
                                }


                                if (!string.IsNullOrEmpty(freewayCode) && string.IsNullOrEmpty(bitEnterpriseCode))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "alert('El operador tiene código en Freeway pero no en Bit Enterprise.');", true);
                                    return;
                                }

                                if (string.IsNullOrEmpty(freewayCode) && isJoined == "True")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey2", "alert('El operador está activo en Freeway pero no tiene código asignado en Freeway.');", true);
                                    return;
                                }

                                if (!string.IsNullOrEmpty(freewayCode) && !string.IsNullOrEmpty(bitEnterpriseCode) && freewayCode != bitEnterpriseCode)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey5", "alert('El código del operador no tiene paridad entre Freeway y Bit Enterprise.');", true);
                                    return;
                                }

                                if (isJoined == "True")
                                {
                                    dtHorario = ConsultaH_BRL.selectHorario(ObjConsul, 2);
                                    if (dtHorario.Rows.Count == 0)
                                    {
                                        lblinfoCod.Text = "";
                                        lblInfoConductor.Text = "";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "alert('No existen registros asociados, por favor verifique e intente nuevamente.');", true);
                                        return;
                                    }
                                    else
                                    {
                                        pnl_Resultado.Visible = true;
                                    }

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey3", "alert('Código o cédula erróneos o no existentes');", true);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "alert('" + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// Método para separar en parte1 y parte2 la tabla de horarios
        /// </summary>
        /// <param name="dtHorarios"></param>
        public void SepararTablas(DataTable dtHorarios)
        {
            try
            {
                int cont = 0;
                foreach (DataRow row in dtHorarios.Rows)
                {
                    if (codigo != row["Codigo"].ToString() || fecha != row["Fecha"].ToString() || asignacion != row["Asignacion"].ToString())
                    {
                        if (cont > 0)
                        {
                            if (dtPart1 != null)
                            {
                                DelColumns(dtPart1);
                            }
                            if (dtPart2 != null)
                            {
                                DelColumns(dtPart2);
                            }
                            AddHorario();
                        }

                        IniPar1 = null;
                        FinPart1 = null;
                        IniPar2 = null;
                        FinPart2 = null;

                        codigo = row["Codigo"].ToString();
                        conductor = row["Conductor"].ToString();
                        fecha = row["Fecha"].ToString();
                        asignacion = row["Asignacion"].ToString();
                        amplitud = row["Amplitud"].ToString();
                        produccion = row["Produccion"].ToString();
                        IniPar1 = row["InicioParte"].ToString();
                        FinPart1 = row["FinParte"].ToString();
                        parada = row["Parada"].ToString();
                        dtPart1 = new DataTable();
                        dtPart2 = new DataTable();
                        dtPart1 = dtHorarios.Clone();
                        dtPart2 = dtHorarios.Clone();

                        if (cont == 0)
                        {
                            AddHead();
                        }

                        cont = cont + 1;

                    }

                    if (IniPar1 == row["InicioParte"].ToString() && FinPart1 == row["FinParte"].ToString() && IniPar2 == null && FinPart2 == null)
                    {
                        dtPart1.Rows.Add(row.ItemArray);
                    }
                    else if (IniPar1 != row["InicioParte"].ToString() && FinPart1 != row["FinParte"].ToString() && IniPar2 == null && FinPart2 == null)
                    {
                        IniPar2 = row["InicioParte"].ToString();
                        FinPart2 = row["FinParte"].ToString();
                        dtPart2.Rows.Add(row.ItemArray);
                    }
                    else if (IniPar2 == row["InicioParte"].ToString() && FinPart2 == row["FinParte"].ToString())
                    {
                        dtPart2.Rows.Add(row.ItemArray);
                    }
                }
                if (dtPart1 != null)
                {
                    DelColumns(dtPart1);
                }
                if (dtPart2 != null)
                {
                    DelColumns(dtPart2);
                }

                AddHorario();
                AddMessage();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "alert('" + ex.Message + "');", true);
            }
        }
        private void AddHead()
        {
            lblinfoCod.Text = codigo;
            lblInfoConductor.Text = conductor;
        }



        //Se agrega contador para que sea posible mostrar progreso en consulta (evitar recarga).
        private int contador = 0;
        //AGR_12-10-2022:Agrega horario mediante un control de usuario "ascx"
        private void AddHorario()
        {
            if (asignacion.Contains("Descanso"))
            {
                addVacios();
            }
            else
            {
                HorarioDia FrmHorario = (HorarioDia)Page.LoadControl("~/Controles/HorarioDia.ascx");
                FrmHorario.ID = "Horario" + contador;
                FrmHorario.LabelFecha.Text = Convert.ToDateTime(fecha).ToString("dddd", new CultureInfo("es-ES")) + ", " + fecha.Substring(0, 10);
                FrmHorario.LabelAsignacion.Text = asignacion;
                FrmHorario.labelAmplitud.Text = amplitud;
                FrmHorario.labelProd.Text = produccion;
                FrmHorario.labelpar.Text = "Parte de Trabajo 1 (" + IniPar1 + " - " + FinPart1 + ")";
                FrmHorario.gvHorOne.DataSource = dtPart1;
                FrmHorario.gvHorOne.DataBind();
                int colWidth = 250;
                if (colWidth > 0)
                {
                    for (int i = 0; i < FrmHorario.gvHorOne.Columns.Count; i++)
                    {
                        FrmHorario.gvHorOne.Columns[i].ItemStyle.Width = colWidth;
                    }
                }

                // Determinar índice de Hora_Fin y ocultar su header
                int horaFinIndexParte1 = ObtenerIndiceHoraFinYOcultarlo(FrmHorario.gvHorOne);
                // Construir tabla + agregar fila final y combinar celdas (lo que añadimos antes)
                ProcesarFilasYAgregarFinal(FrmHorario.gvHorOne, horaFinIndexParte1);
                // Asegurar que la columna Hora_Fin esté oculta en todas las filas
                OcultarHoraFinEnFilas(FrmHorario.gvHorOne, horaFinIndexParte1);

                FrmHorario.labelpar2.Text = "Parte de Trabajo 2 (" + IniPar2 + " - " + FinPart2 + ")";
                FrmHorario.gvHorTwo.DataSource = dtPart2;
                FrmHorario.gvHorTwo.DataBind();
                if (colWidth > 0)
                {
                    for (int i = 0; i < FrmHorario.gvHorTwo.Columns.Count; i++)
                    {
                        FrmHorario.gvHorTwo.Columns[i].ItemStyle.Width = colWidth;
                    }
                }
                // Determinar índice de Hora_Fin y ocultar su header
                int horaFinIndexParte2 = ObtenerIndiceHoraFinYOcultarlo(FrmHorario.gvHorTwo);
                // Construir tabla + agregar fila final y combinar celdas (lo que añadimos antes)
                ProcesarFilasYAgregarFinal(FrmHorario.gvHorTwo, horaFinIndexParte2);
                // Asegurar que la columna Hora_Fin esté oculta en todas las filas
                OcultarHoraFinEnFilas(FrmHorario.gvHorTwo, horaFinIndexParte2);
                Panel1.Controls.Add(FrmHorario);
                ViewState["controlsadded"] = true;
                contador++;
                FrmHorario.gvHorOne.Dispose();
                FrmHorario.gvHorTwo.Dispose();
            }
        }

        #region Métodos auxiliares para el metodo AddHorario - LGO-27082025
        // ---------------------- Métodos auxiliares nuevos ----------------------
        // Busca la columna "Hora_Fin" en el header, la oculta y devuelve su índice. Si no la encuentra devuelve el índice de la última columna.
        private int ObtenerIndiceHoraFinYOcultarlo(GridView grid)
        {
            int horaFinIndex = -1;
            if (grid.HeaderRow != null)
            {
                for (int c = 0; c < grid.HeaderRow.Cells.Count; c++)
                {
                    string headerText = grid.HeaderRow.Cells[c].Text?.Trim();
                    if (!string.IsNullOrEmpty(headerText) &&
                        headerText.Equals("Hora_Fin", StringComparison.OrdinalIgnoreCase))
                    {
                        horaFinIndex = c;
                        break;
                    }
                }
                if (horaFinIndex == -1)
                    horaFinIndex = grid.HeaderRow.Cells.Count - 1;

                if (horaFinIndex >= 0 && horaFinIndex < grid.HeaderRow.Cells.Count)
                    grid.HeaderRow.Cells[horaFinIndex].Visible = false;
            }
            else
            {
                // Si no hay header, asumir última columna (si hay filas)
                if (grid.Rows.Count > 0)
                    horaFinIndex = grid.Rows[0].Cells.Count - 1;
            }
            return horaFinIndex;
        }

        // Procesa las filas: oculta Hora_Fin en filas previas, crea fila final completa y combina con la penúltima.
        private void ProcesarFilasYAgregarFinal(GridView grid, int horaFinIndex)
        {
            int totalFilas = grid.Rows.Count;
            for (int i = 0; i < totalFilas; i++)
            {
                GridViewRow fila = grid.Rows[i];

                // Para todas las filas excepto la última original: ocultar la columna Hora_Fin
                if (i < totalFilas - 1)
                {
                    fila.Cells[fila.Cells.Count - 1].Visible = false;
                }
                else
                {
                    // Última fila original: obtener horaFinal y crear fila final bien formada
                    GridViewRow ultimaFila = grid.Rows[i];
                    string horaFinal = ultimaFila.Cells[ultimaFila.Cells.Count - 1].Text; // Última columna = Hora fin

                    GridViewRow nuevaFila = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);

                    // Crear la fila completa con el mismo número de celdas que el header
                    int columnas = (grid.HeaderRow != null) ? grid.HeaderRow.Cells.Count : ultimaFila.Cells.Count;
                    for (int col = 0; col < columnas; col++)
                    {
                        TableCell celda = new TableCell();
                        celda.Text = (col == 0) ? horaFinal : "";
                        celda.CssClass = "grid-cell";
                        nuevaFila.Cells.Add(celda);
                    }

                    // Agregar la fila al GridView (se añade a Controls[0].Controls)
                    grid.Controls[0].Controls.Add(nuevaFila);

                    // Combinar la penúltima (ultimaFila) con la nueva fila creada
                    CombinarPenultimaConNueva(ultimaFila, nuevaFila, horaFinIndex);
                }
            }
        }

        // Combina (rowspan + mueve contenido) de la fila superior con la inferior (la inferior puede ser la nueva fila creada)
        private void CombinarPenultimaConNueva(GridViewRow topRow, GridViewRow bottomRow, int horaFinIndex)
        {
            int maxCols = Math.Min(topRow.Cells.Count, bottomRow.Cells.Count);

            for (int c = 1; c < maxCols; c++)
            {
                if (c == horaFinIndex) continue;

                TableCell topCell = topRow.Cells[c];
                TableCell bottomCell = bottomRow.Cells[c];
                if (topCell == null || bottomCell == null) continue;

                string contenidoSuperior = topCell.Text.Trim();
                string contenidoInferior = bottomCell.Text.Trim();

                if (!string.IsNullOrEmpty(contenidoInferior))
                {
                    topCell.Text = !string.IsNullOrEmpty(contenidoSuperior)
                        ? contenidoSuperior + "<br/>" + contenidoInferior
                        : contenidoInferior;
                }

                int currentRowSpan = topCell.RowSpan > 1 ? topCell.RowSpan : 1;
                topCell.RowSpan = currentRowSpan + 1;

                bottomCell.Visible = false;
            }
        }

        // Oculta la columna Hora_Fin en todas las filas (seguridad adicional)
        private void OcultarHoraFinEnFilas(GridView grid, int horaFinIndex)
        {
            if (horaFinIndex < 0) return;
            int totalFilas = grid.Rows.Count;
            for (int i = 0; i < totalFilas; i++)
            {
                GridViewRow fila = grid.Rows[i];
                if (fila.Cells.Count > horaFinIndex)
                    fila.Cells[horaFinIndex].Visible = false;
            }
        }

        #endregion

        private void addVacios()
        {
            HorarioVacio FrmVacio = (HorarioVacio)Page.LoadControl("~/Controles/HorarioVacio.ascx");
            FrmVacio.LabelFecha.Text = Convert.ToDateTime(fecha).ToString("dddd", new CultureInfo("es-ES")) + ", " + fecha.Substring(0, 10);
            FrmVacio.LabelInfoAsig.Text = asignacion;
            FrmVacio.LabelInfoProd.Text = produccion;
            Panel1.Controls.Add(FrmVacio);

            ViewState["controlsadded"] = true;
        }
        private void DelColumns(DataTable dtpart)
        {
            dtpart.Columns.Remove("Codigo");
            dtpart.Columns.Remove("Cedula");
            dtpart.Columns.Remove("Conductor");
            dtpart.Columns.Remove("Fecha");
            dtpart.Columns.Remove("Asignacion");
            dtpart.Columns.Remove("Amplitud");
            dtpart.Columns.Remove("Produccion");
            dtpart.Columns.Remove("InicioParte");
            dtpart.Columns.Remove("FinParte");
        }
        string mensaje = "";
        string mensaje1 = "";
        string mensaje2 = "";
        private void AddMessage()
        {
            if (CargarExcel())
            {
                CtrMensaje FrmMensaje = (CtrMensaje)Page.LoadControl("~/Controles/CtrMensaje.ascx");
                FrmMensaje.ID = "Mensaje";
                FrmMensaje.LabelMensaje1.Text = mensaje1;
                FrmMensaje.LabelMensaje2.Text = mensaje2;
                Panel1.Controls.Add(FrmMensaje);
                ViewState["controlsadded"] = true;
            }
        }

        #endregion

        private bool CargarExcel()
        {
            try
            {
                if (dtHorario.Rows.Count > 0)
                {
                    string ruta_archivo = ConfigurationManager.AppSettings.Get("ArchivoMensajesProgramacion");
                    //string conStr = String.Format(ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString, ruta_archivo, "Yes"); //Anterior lógica
                    AG_Utils permisoUsuario = new AG_Utils();
                    if (permisoUsuario.impersonateValidUser())
                    {
                        if (!System.IO.File.Exists(ruta_archivo))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('Mensajes no encontrados. Favor informar a programación inmediatamente.');", true);
                            EnviarCorreo("Consulta no puede encontrar el archivo", "Por favor verificar ya que la consulta no pudo encontrar el archivo de los mensajes.", null);
                            MostrarError("La plataforma está presentando algunos inconvenientes al presentar mensajes, por favor intente nuevamente en unos minutos.");
                            return false;
                        }
                    }
                    //OleDbConnection connExcel = new OleDbConnection(conStr); //Anterior lógicaa
                    //DataTable dtMensaje = Import_To_DataTableAntiguo(connExcel); //Anterior lógica

                    DataTable dtMensaje = Import_To_DataTableNuevo(ruta_archivo);
                    if (dtMensaje != null && dtMensaje.Rows.Count > 0)
                    {
                        mensaje1 = dtMensaje.Rows[0][0].ToString();
                        mensaje2 = dtMensaje.Rows[1][0].ToString();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            
            catch (Exception ex)
            {
                EnviarCorreo("Error no controlado", "", ex);
                return false;
            }
        }

        #region Método NUEVO para consultar los mensajes de la programación LGO-26022025

        private DataTable Import_To_DataTableNuevo(string rutaArchivo)
        {
            // Se crea un DataTable con una única columna llamada "Mensaje"
            DataTable dt = new DataTable();
            dt.Columns.Add("Mensaje", typeof(string));

            try
            {
                // Se abre el archivo Excel en modo solo lectura
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(rutaArchivo, false))
                {
                    WorkbookPart wbPart = doc.WorkbookPart;
                    // Se obtiene la primera hoja del archivo
                    Sheet sheet = wbPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(sheet.Id));
                    SheetData sheetData = wsPart.Worksheet.Elements<SheetData>().First();


                    bool mensajeEncontrado = false;
                    string codigo = string.Empty;
                    string condicionCodigoSAE = string.Empty;
                    string condicionDocumento = string.Empty;

                    // Validación inicial del código y cédula ingresados
                    if (!string.IsNullOrEmpty(txtCode.Text))
                    {
                        // AGR_14-10-2022: Se retiran los primeros caracteres del código según el tipo de usuario
                        if (tipoUsuario.Contains("OPERADOR"))
                        {
                            codigo = Session["codSAE"].ToString().Remove(0, 4);
                        }
                        else
                        {
                            codigo = txtCode.Text.Remove(0, 1);
                        }

                        condicionCodigoSAE = $"%{codigo}";
                    }
                    else if (!string.IsNullOrEmpty(txtCedula.Text))
                    {
                        if (tipoUsuario.Contains("OPERADOR"))
                        {
                            codigo = Session["codSAE"].ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtCode.Text))
                            {
                                codigo = txtCode.Text.Remove(0, 1);
                            }
                        }

                        condicionDocumento = txtCedula.Text;
                    }


                    // Se recorren todas las filas de la hoja
                    foreach (Row row in sheetData.Elements<Row>())
                    {
                        var celdas = row.Elements<Cell>().ToList();
                        if (celdas.Count < 5) continue;

                        // Se obtienen los valores de la celda de identificación y la celda de mensaje
                        string codigoExcel = ObtenerValorCelda(wbPart, celdas[0]);
                        string identificacion = ObtenerValorCelda(wbPart, celdas[1]);
                        string mensaje = ObtenerValorCelda(wbPart, celdas[4]);

                        // Validaciones para encontrar mensajes basados en los campos ingresados por el usuario
                         
                        if (!string.IsNullOrEmpty(condicionCodigoSAE) && codigoExcel.EndsWith(codigo))
                        {
                            dt.Rows.Add(mensaje);
                            mensajeEncontrado = true;
                        }
                        else if (!string.IsNullOrEmpty(condicionDocumento) && identificacion == condicionDocumento)
                        {
                            dt.Rows.Add(mensaje);
                            mensajeEncontrado = true;
                        }

                    }

                    // Si no se encontró ningún mensaje, se agrega un mensaje por defecto
                    if (!mensajeEncontrado)
                    {
                        dt.Rows.Add("No hay mensajes.");
                    }

                    // Mensaje final estándar para todos los usuarios
                    dt.Rows.Add("Recuerde que usted es muy importante para ETIB S.A.S. Que tenga un excelente turno.");
                }
                return dt;
            }
            catch (Exception ex)
            {
                EnviarCorreo("Error al leer el archivo", "Error al consultar la información del archivo", ex);
                return null;
            }

        }

        // Método para obtener el valor real de una celda en el archivo de Excel
        private string ObtenerValorCelda(WorkbookPart wbPart, Cell cell)
        {
            if (cell == null || cell.CellValue == null) return "";

            string value = cell.CellValue.InnerText;

            // Verifica si la celda es un "Shared String" (almacenado en una tabla de cadenas compartidas)
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return wbPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>()
                       .ElementAt(int.Parse(value)).InnerText;
            }
            return value;
        }

        #endregion

        #region Método antiguo para consultar los mensajes de la programación
        private DataTable Import_To_DataTableAntiguo(OleDbConnection connExcel)
        {
            DataTable dt = new DataTable();

            try
            {
                //se establecen variables
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmdExcel.Connection = connExcel;

                // se abre conexión para ver las tablas 
                connExcel.Open();
                DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                connExcel.Close();

                // se vuelve a conectar para consultar la información
                if (dtExcelSchema != null && dtExcelSchema.Rows.Count > 0 && !String.IsNullOrEmpty(dtExcelSchema.Rows[0]["TABLE_NAME"].ToString()))
                {
                    connExcel.Open();
                    string condicionCodigoSAE = "";
                    string condicionDocumento = "";
                    string codigo = "";
                    if (!string.IsNullOrEmpty(txtCode.Text))
                    {
                        //AGR_14-10-2022: Se retiran los primeros caracteres del código para utilizar la funcion LIKE
                        if (tipoUsuario.Contains("OPERADOR"))
                        {
                            codigo = Session["codSAE"].ToString();
                            codigo = codigo.Remove(0, 4);
                            //06878
                        }
                        else
                        {
                            codigo = txtCode.Text.Remove(0, 1);
                        }

                       
                        condicionCodigoSAE = $"codigo LIKE '%{codigo}'";
                        cmdExcel.CommandText = "SELECT Mensaje From [" + dtExcelSchema.Rows[0]["TABLE_NAME"].ToString() + "] WHERE " + condicionCodigoSAE;
                        oda.SelectCommand = cmdExcel;
                        oda.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            if (!string.IsNullOrEmpty(txtCedula.Text))
                            {
                                condicionDocumento = "Identificacion = " + txtCedula.Text;
                                cmdExcel.CommandText = "SELECT Mensaje From [" + dtExcelSchema.Rows[0]["TABLE_NAME"].ToString() + "] WHERE " + condicionDocumento;
                                oda.SelectCommand = cmdExcel;
                                oda.Fill(dt);
                            }
                        }
                    }

                    else if (!string.IsNullOrEmpty(txtCedula.Text))
                    {
                        if (tipoUsuario.Contains("OPERADOR"))
                        {
                            codigo = Session["codSAE"].ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtCode.Text))
                            {
                                codigo = txtCode.Text.Remove(0, 1);
                            }
                            //codigo = "ET09" + codigo;
                        }
                        condicionDocumento = "Identificacion = " + txtCedula.Text;
                        cmdExcel.CommandText = "SELECT Mensaje From [" + dtExcelSchema.Rows[0]["TABLE_NAME"].ToString() + "] WHERE " + condicionDocumento;
                        oda.SelectCommand = cmdExcel;
                        oda.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            if (!string.IsNullOrEmpty(codigo))
                            {
                                condicionCodigoSAE = $"codigo LIKE '%{codigo}'";
                                cmdExcel.CommandText = "SELECT Mensaje From [" + dtExcelSchema.Rows[0]["TABLE_NAME"].ToString() + "] WHERE " + condicionCodigoSAE;
                                oda.SelectCommand = cmdExcel;
                                oda.Fill(dt);
                            }
                        }
                    }
                    
                    //AGR_14-10-2022: en caso de no obtener datos, adiciona mensaje
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add("No hay mensajes.");
                    }
                    //AGR_14-10-2022: se adiciona "Mensaje motivacional" xD
                    dt.Rows.Add("Recuerde que usted es muy importante para ETIB S.A.S. Que tenga un excelente turno.");
                }
                // se finaliza OleDbCommand de excel
                connExcel.Close();
                // se retorna la información obtenida
                return dt;
            }
            catch (Exception ex)
            {
                if (connExcel != null)
                {
                    connExcel.Close();
                }
                return null;
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "alert('" + ex.Message + "');", true);
                EnviarCorreo("Error al leer el archivo", "Error al consultar la información del archivo", ex);
            }
        }
        #endregion

        #region Alertas

        public void MostrarError(string codigo)
        {
            if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alerta", "$(document).ready(function () { $.MessageBox('" + codigo + "'); });", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "$(document).ready(function () { $.MessageBox('" + codigo + "'); });", true);
            }
        }

        //Validar método ya que NO FUNCIONA
        public void EnviarCorreo(string Asunto, string Body, Exception ex)
        {
            try
            {
                /*-------------------------MENSAJE DE CORREO----------------------*/
                //Creamos un nuevo Objeto de mensaje
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje 
                //con la propiedad CC se vera el destinatario en el correo
                //con la propiedad Bcc sera oculto el destinatario en el correo


                //Asunto
                mmsg.Subject = "Error Consulta FreeWay (etib.com.co) - " + Asunto.Trim();
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                string CC = ConfigurationManager.AppSettings.Get("Correos_Administradores").ToString();

                if (!string.IsNullOrEmpty(CC) && !string.IsNullOrWhiteSpace(CC))
                {
                    string[] temp = CC.Split(',');
                    if (temp.Length > 0)
                    {
                        foreach (string row in temp)
                        {
                            mmsg.CC.Add(row);
                        }
                    }
                }

                //Cuerpo del Mensaje
                mmsg.Body = "<div style='-apple-system,BlinkMacSystemFont," + '"' + "Segoe UI" + '"' + ",Roboto," + '"' + "Helvetica Neue" + '"' + ",Arial,sans-serif," + '"' + "Apple Color Emoji" + '"' + "," + '"' + "Segoe UI Emoji" + '"' + "," + '"' + "Segoe UI Symbol" + '"' + "'> <b>Error con la consulta:</b> " + Body + " : " + ex.Message + ". <br /><br /><br />" + ((ex != null) ? ("<b>Traza del error</b>: <br /> <b>Traza:</b> " + ex.StackTrace + " <br /><br /> <b>Lugar:</b> " + ex.TargetSite + " <br /><br /> <b>Fuente:</b> " + ex.Source) : "") + "</div>";

                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress("consulta.freeway@etib.com.co");

                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials = new System.Net.NetworkCredential("bit@etib.com.co", "*Buffalo");//"3t1b8o5@2016&"

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
                cliente.Port = 25;

                cliente.Host = "mail.etib.com.co";

                /*-------------------------ENVIO DE CORREO----------------------*/
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException _ex)
            {
                MostrarError("Error al enviar el correo: " + _ex.Message + ". <br /> Por favor notificar al administrador.");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey4", "DescargaPDF();", true);
            //lblStatus.Text = "Processing Completed";
        }
        

        #endregion
    }
}