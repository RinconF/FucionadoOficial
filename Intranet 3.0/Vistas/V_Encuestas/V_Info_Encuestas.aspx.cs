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
using System.IO;
using DCL;
using System.Web.Services.Description;

namespace Intranet_3._0.Vistas.V_Encuestas
{
    public partial class V_Info_Encuestas : System.Web.UI.Page
    {
        public int TotalFinalizadas { get; set; }
        public int TotalPendientes { get; set; }
        public int TotalIncompletas { get; set; }
        public string NombreEncuesta { get; set; }
        public int IdEncuesta { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtener el mensaje de disponibilidad de encuestas
            DataTable dtEncuestaEstado = Info_Encuestas_BRL.ObtenerDatosEncuesta(7, 0);
            if (dtEncuestaEstado.Rows.Count > 0)
            {
                string sidEncuesta = dtEncuestaEstado.Rows[0]["c01_id_encuesta"].ToString();
                IdEncuesta = Convert.ToInt32(sidEncuesta);
                // Mostrar todo si hay encuestas disponibles
                encuestaPanel.Visible = true;
                lblMensaje.Visible = false; // Asegurarse de ocultar el mensaje si hay encuestas

                // Obtener el nombre de la encuesta
                var dtNombreEncuesta = Info_Encuestas_BRL.ObtenerDatosEncuesta(6, IdEncuesta); // 6 = Action para obtener nombre
                if (dtNombreEncuesta.Rows.Count > 0)
                {
                    NombreEncuesta = dtNombreEncuesta.Rows[0]["c01_nombre"].ToString();
                }

                // Obteniendo el total de finalizadas
                var dtFinalizadas = Info_Encuestas_BRL.ObtenerDatosEncuesta(0, IdEncuesta);
                if (dtFinalizadas.Rows.Count > 0)
                {
                    TotalFinalizadas = Convert.ToInt32(dtFinalizadas.Rows[0]["TOTAL FINALIZADAS"]);
                }

                // Obteniendo el total de pendientes
                var dtPendientes = Info_Encuestas_BRL.ObtenerDatosEncuesta(1, IdEncuesta);
                if (dtPendientes.Rows.Count > 0)
                {
                    TotalPendientes = Convert.ToInt32(dtPendientes.Rows[0]["TOTAL PENDIENTES"]);
                }

                // Obteniendo el total de incompletas (contando las filas ya que no hay columna TOTAL)
                var dtIncompletas = Info_Encuestas_BRL.ObtenerDatosEncuesta(2, IdEncuesta);
                TotalIncompletas = dtIncompletas.Rows.Count;

                if (!IsPostBack)
                {
                    btnDescargarExcel.OnClientClick = "return confirmarDescargaInforme();";
                }
            }
            else
            {
                // Mostrar mensaje en lugar de ocultar el panel
                lblMensaje.Text = "No hay encuestas activas";
                lblMensaje.Visible = true; // Mostrar el mensaje
                encuestaPanel.Visible = false; // Ocultar panel de encuesta
            }
        }

        protected void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del dropdown
            string tipoInforme = ddlTipoInformeEnc.SelectedValue;

            // Validar si el usuario seleccionó una opción válida
            if (string.IsNullOrEmpty(tipoInforme))
            {
                Response.Write("<script>alert('Por favor, seleccione un tipo de informe antes de continuar.');</script>");
                return;
            }

            var dataSet = new DataSet();

            // Lógica según el tipo de informe seleccionado
            switch (tipoInforme)
            {
                case "general":
                    dataSet = ObtenerDataSetEncuestasGeneral();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformeGeneralEncuestas.xls");
                    break;
                case "sede":
                    dataSet = dataSetEncuestaSede();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformePorSedeEncuestas.xls");
                    break;
                case "cargo":
                    dataSet = dataSetEncuestaCargo();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformePorCargoEncuestas.xls");
                    break;
                case "respuestas":
                    dataSet = dataSetEncuestaRespuestas();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformeDeRespuestasEncuestas.xls");
                    break;
                default:
                    Response.Write("<script>alert('Opción inválida seleccionada.');</script>");
                    return;
            }

            string excelContent = GenerarExcel(dataSet);
            Response.Write(excelContent);
            Response.End();
        }

        // INFORME GENERAL
        private DataSet ObtenerDataSetEncuestasGeneral()
        {
            var dataSet = new DataSet();

            // Obtener las tablas de finalizadas, pendientes e incompletas
            var finalizadas = Info_Encuestas_BRL.ObtenerDatosEncuesta(0, IdEncuesta).Copy();
            var pendientes = Info_Encuestas_BRL.ObtenerDatosEncuesta(1, IdEncuesta).Copy();
            var incompletas = Info_Encuestas_BRL.ObtenerDatosEncuesta(2, IdEncuesta).Copy();

            // Crear una nueva tabla de resumen con totales
            var resumenTable = new DataTable("RESUMEN TOTAL");
            resumenTable.Columns.Add("CATEGORIA", typeof(string));
            resumenTable.Columns.Add("TOTAL", typeof(int));

            // Agregar filas con los totales
            var rowFinalizadas = resumenTable.NewRow();
            rowFinalizadas["CATEGORIA"] = "ENCUESTAS FINALIZADAS";
            rowFinalizadas["TOTAL"] = finalizadas.Rows.Count > 0 ? Convert.ToInt32(finalizadas.Rows[0]["TOTAL FINALIZADAS"]) : 0;
            resumenTable.Rows.Add(rowFinalizadas);

            var rowPendientes = resumenTable.NewRow();
            rowPendientes["CATEGORIA"] = "ENCUESTAS PENDIENTES";
            rowPendientes["TOTAL"] = pendientes.Rows.Count > 0 ? Convert.ToInt32(pendientes.Rows[0]["TOTAL PENDIENTES"]) : 0;
            resumenTable.Rows.Add(rowPendientes);

            var rowIncompletas = resumenTable.NewRow();
            rowIncompletas["CATEGORIA"] = "ENCUESTAS INCOMPLETAS (NO COMPLETARON)";
            rowIncompletas["TOTAL"] = incompletas.Rows.Count;
            resumenTable.Rows.Add(rowIncompletas);

            // Añadir la tabla de resumen al DataSet
            dataSet.Tables.Add(resumenTable);

            // Añadir tabla de No Completadas (detalle de incompletas)
            incompletas.TableName = "No Completadas";
            dataSet.Tables.Add(incompletas);

            // Añadir tabla de No Ingresaron
            dataSet.Tables.Add(Info_Encuestas_BRL.ObtenerDatosEncuesta(3, IdEncuesta).Copy());
            dataSet.Tables[2].TableName = "No Ingresaron";

            // Añadir tabla de Respuestas de la encuesta
            dataSet.Tables.Add(Info_Encuestas_BRL.ObtenerDatosEncuesta(4, IdEncuesta).Copy());
            dataSet.Tables[3].TableName = "Respuestas";

            // Añadir tabla de Finalizó Encuesta
            dataSet.Tables.Add(Info_Encuestas_BRL.ObtenerDatosEncuesta(5, IdEncuesta).Copy());
            dataSet.Tables[4].TableName = "Finalizo Encuesta";

            return dataSet;
        }

        // Informe Por Sede de Encuestas (2 pestañas)
        private DataSet dataSetEncuestaSede()
        {
            Info_Encuestas Obj = new DCL.Info_Encuestas();
            Obj.IdEncuesta = IdEncuesta;
            var dataSet = new DataSet();

            // Pestaña 1: Índice de Encuestados por Sede
            var indiceSede = Info_Encuestas_BRL.SelectTable(Obj, 10).Copy(); // Action 10: Índice por sede
            indiceSede.TableName = "Indice de Encuestados";
            dataSet.Tables.Add(indiceSede);

            // Pestaña 2: Encuestas Incompletas
            var incompletas = Info_Encuestas_BRL.SelectTable(Obj, 8).Copy(); // Action 8: Incompletas por sede
            incompletas.TableName = "Encuestas Incompletas";
            dataSet.Tables.Add(incompletas);

            // Pestaña 3: Agregar No Realizadas por Sede 
            var noRealizadas = Info_Encuestas_BRL.SelectTable(Obj, 9).Copy(); // Action 9: No realizadas
            noRealizadas.TableName = "No Realizadas por Sede";
            dataSet.Tables.Add(noRealizadas);

            return dataSet;
        }

        // Informe Por Cargo de Encuestas (2 pestañas)
        private DataSet dataSetEncuestaCargo()
        {
            Info_Encuestas Obj = new DCL.Info_Encuestas();
            Obj.IdEncuesta = IdEncuesta;
            var dataSet = new DataSet();

            // Pestaña 1: Índice de Encuestados por Cargo
            var indiceCargo = Info_Encuestas_BRL.SelectTable(Obj, 11).Copy(); // Action 11: Índice por cargo
            indiceCargo.TableName = "Indice de Encuestados";
            dataSet.Tables.Add(indiceCargo);

            // Pestaña 2: Encuestas Incompletas
            var incompletas = Info_Encuestas_BRL.SelectTable(Obj, 8).Copy(); // Action 8: Incompletas por cargo
            incompletas.TableName = "Encuestas Incompletas por cargo";
            dataSet.Tables.Add(incompletas);

            // Pestaña 3: Agregar No Realizadas por Cargo 
            var noRealizadas = Info_Encuestas_BRL.SelectTable(Obj, 9).Copy(); // Action 9: No realizadas
            noRealizadas.TableName = "No Realizadas por Cargo";
            dataSet.Tables.Add(noRealizadas);

            return dataSet;
        }

        // Informe de Respuestas con estadísticas adicionales 
        private DataSet dataSetEncuestaRespuestas()
        {
            Info_Encuestas Obj = new DCL.Info_Encuestas();
            Obj.IdEncuesta = IdEncuesta;
            var dataSet = new DataSet();

            // Pestaña 1: Frecuencia de Respuestas 
            var frecuencia = Info_Encuestas_BRL.SelectTable(Obj, 12).Copy(); // Action 12: Frecuencia de respuestas
            frecuencia.TableName = "Frecuencia de Respuestas";
            dataSet.Tables.Add(frecuencia);

            // Pestaña 2: Respuestas Completas 
            var respuestas = Info_Encuestas_BRL.SelectTable(Obj, 13).Copy(); // Action 13: Respuestas completas
            respuestas.TableName = "Respuestas de Encuesta";
            dataSet.Tables.Add(respuestas);

            // Pestaña 3: Estadísticas por Rango de Edad 
            var estadisticasEdad = Info_Encuestas_BRL.SelectTable(Obj, 14).Copy(); // Action 14: Estadísticas por edad
            estadisticasEdad.TableName = "Estadisticas por Edad";
            dataSet.Tables.Add(estadisticasEdad);

            return dataSet;
        }

        private string GenerarExcel(DataSet dataSet)
        {
            using (var sw = new System.IO.StringWriter())
            {
                sw.Write(@"<?xml version=""1.0""?>");
                sw.Write(@"<?mso-application progid=""Excel.Sheet""?>");
                sw.Write(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet"" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet"">");

                // Definir estilos para las celdas
                sw.Write(@"
    <Styles>
        <Style ss:ID=""HeaderStyle"">
            <Font ss:Bold=""1""/>
            <Interior ss:Color=""#D9E1F2"" ss:Pattern=""Solid""/>
            <Borders>
                <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
                <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
                <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
                <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
            </Borders>
        </Style>
        <Style ss:ID=""CellStyle"">
            <Borders>
                <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
                <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
                <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
                <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
            </Borders>
        </Style>
    </Styles>");

                foreach (DataTable table in dataSet.Tables)
                {
                    sw.Write($"<Worksheet ss:Name=\"{HttpUtility.HtmlEncode(table.TableName)}\">");
                    sw.Write("<Table>");

                    // Ajustar el ancho de las columnas a un tamaño predeterminado
                    foreach (DataColumn column in table.Columns)
                    {
                        // Asignar un ancho predeterminado amplio
                        sw.Write("<Column ss:Width=\"120\"/>");
                    }

                    // Crear encabezado con estilo
                    sw.Write("<Row>");
                    foreach (DataColumn column in table.Columns)
                        sw.Write($"<Cell ss:StyleID=\"HeaderStyle\"><Data ss:Type=\"String\">{HttpUtility.HtmlEncode(column.ColumnName)}</Data></Cell>");
                    sw.Write("</Row>");

                    // Crear filas con datos
                    foreach (DataRow row in table.Rows)
                    {
                        sw.Write("<Row>");
                        foreach (var item in row.ItemArray)
                        {
                            // Agregar espacios adicionales para mayor separación visual
                            string content = $"{HttpUtility.HtmlEncode(item?.ToString() ?? string.Empty)} ";
                            sw.Write($"<Cell ss:StyleID=\"CellStyle\"><Data ss:Type=\"String\">{content}</Data></Cell>");
                        }
                        sw.Write("</Row>");
                    }

                    sw.Write("</Table>");
                    sw.Write("</Worksheet>");
                }

                sw.Write("</Workbook>");
                return sw.ToString();
            }
        }
    }
}