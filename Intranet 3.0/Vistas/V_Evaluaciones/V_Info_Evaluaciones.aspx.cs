using BRL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Evaluaciones
{
    public partial class V_Info_Evaluaciones : System.Web.UI.Page
    {
        public int TotalFinalizadas { get; set; }
        public int TotalPendientes { get; set; }
        public string NombreEvaluacion { get; set; }
        public int IdEvaluacion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                // Obtener el mensaje de disponibilidad de evaluaciones
                Info_Evaluaciones Obj = new DCL.Info_Evaluaciones();

                DataTable dtEvaluacionEstado = Info_Evaluaciones_BRL.SelectTable(Obj, 7);

                if (dtEvaluacionEstado.Rows.Count > 0)
                {
                    string sidEvaluacion = dtEvaluacionEstado.Rows[0]["c01_id_evaluacion"].ToString();
                    IdEvaluacion = Convert.ToInt32(sidEvaluacion);
                    Obj.IdEvaluacion = IdEvaluacion;

                    // Mostrar todo si hay evaluaciones disponibles
                    encuestaPanel.Visible = true;
                    lblMensaje.Visible = false; // Asegurarse de ocultar el mensaje si hay Evaluaciones

                    // Obtener el nombre de la Evaluacion
                    var dtNombreEvaluacion = Info_Evaluaciones_BRL.SelectTable(Obj, 6);

                    if (dtNombreEvaluacion.Rows.Count > 0)
                    {
                        NombreEvaluacion = dtNombreEvaluacion.Rows[0]["c01_nombre"].ToString();
                    }

                    // Obteniendo el total de Evaluaciones finalizadas
                    var dtFinalizadas = Info_Evaluaciones_BRL.SelectTable(Obj, 0);

                    if (dtFinalizadas.Rows.Count > 0)
                    {
                        TotalFinalizadas = Convert.ToInt32(dtFinalizadas.Rows[0]["TOTAL FINALIZADAS"]);
                    }

                    // Obteniendo el total de Evaluaciones pendientes
                    var dtPendientes = Info_Evaluaciones_BRL.SelectTable(Obj, 1);
                    if (dtPendientes.Rows.Count > 0)
                    {
                        TotalPendientes = Convert.ToInt32(dtPendientes.Rows[0]["TOTAL PENDIENTES"]);
                    }

                }
                else
                {
                    // Mostrar mensaje en lugar de ocultar el panel
                    lblMensaje.Text = "No hay evaluaciones activas";
                    lblMensaje.Visible = true; // Mostrar el mensaje
                    encuestaPanel.Visible = false; // Ocultar panel de encuesta
                }
            
        }



        protected void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado del dropdown
            string tipoInforme = ddlTipoInforme.SelectedValue;

            // Validar si el usuario seleccionó una opción válida
            if (string.IsNullOrEmpty(tipoInforme))
            {
                Response.Write("<script>alert('Por favor, seleccione un tipo de informe antes de continuar.');</script>");
                return;
            }

            var dataSet = dataSetEvaluacionGeneral();

            // Lógica según el tipo de informe seleccionado
            switch (tipoInforme)
            {
                case "general":
                    dataSet = dataSetEvaluacionGeneral();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformeGeneral.xls");
                    break;
                case "sede":
                    dataSet = dataSetEvaluacionSede();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformePorSede.xls");
                    break;
                case "cargo":
                    dataSet = dataSetEvaluacionCargo();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformePorCargo.xls");
                    break;
                case "respuestas":
                    dataSet = dataSetEvaluacionRespuesta();
                    Response.Clear();
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=InformeDeRespuestas.xls");
                    break;
                default:
                    Response.Write("<script>alert('Opción inválida seleccionada.');</script>");
                    break;
            }

            string excelContent = GenerarExcel(dataSet);

            
            Response.Write(excelContent);
            Response.End();

        }


        //Trae la informacion de los action de la tabla SP_Info_Evaluacion
        private DataSet dataSetEvaluacionGeneral()
        {   
            Info_Evaluaciones Obj = new DCL.Info_Evaluaciones();
            Obj.IdEvaluacion = IdEvaluacion;
            var dataSet = new DataSet();
            
            // Obtener las tablas de Evaluaciones finalizadas y pendientes
            var finalizadas = Info_Evaluaciones_BRL.SelectTable(Obj, 0).Copy();
            var pendientes = Info_Evaluaciones_BRL.SelectTable(Obj, 1).Copy();

            // Crear una nueva tabla combinada con un solo nombre
            var totalTable = new DataTable("Indice de Evaluacion");
              
            // Añadir columnas de la tabla de Evaluaciones finalizadas
            foreach (DataColumn col in finalizadas.Columns)
            {
                totalTable.Columns.Add("TOTAL EVALUACIONES FINALIZADAS", col.DataType);
            }
            
            // Añadir columnas de la tabla de Evaluaciones pendientes
            foreach (DataColumn col in pendientes.Columns)
            {
                totalTable.Columns.Add("TOTAL EVALUACIONES PENDIENTES", col.DataType);
            }

            // Combinar filas (el número de filas será igual al máximo entre las dos tablas)
            int maxRows = Math.Max(pendientes.Rows.Count, finalizadas.Rows.Count);
            for (int i = 0; i < maxRows; i++)
            {
                var newRow = totalTable.NewRow();

                // Copiar datos de la tabla de finalizadas
                if (i < finalizadas.Rows.Count)
                {
                    for (int j = 0; j < finalizadas.Columns.Count; j++)
                    {
                        newRow["TOTAL EVALUACIONES FINALIZADAS"] = finalizadas.Rows[i][j];
                    }
                }

                // Copiar datos de la tabla de pendientes
                if (i < pendientes.Rows.Count)
                {
                    for (int j = 0; j < pendientes.Columns.Count; j++)
                    {
                        newRow["TOTAL EVALUACIONES PENDIENTES"] = pendientes.Rows[i][j];
                    }
                }

                totalTable.Rows.Add(newRow);
            }


            // Añadir la tabla combinada al DataSet
            dataSet.Tables.Add(totalTable);

            // Añadir otras tablas al DataSet

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 5).Copy());
            dataSet.Tables[1].TableName = "Evaluaciones Finalizadas";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 3).Copy());
            dataSet.Tables[2].TableName = "Evaluaciones No Realizadas";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 2).Copy());
            dataSet.Tables[3].TableName = "Evaluaciones Incompletas";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 10).Copy());
            dataSet.Tables[4].TableName = "Indice de Aprobacion";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 8).Copy());
            dataSet.Tables[5].TableName = "Evaluaciones Aprobadas";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 9).Copy());
            dataSet.Tables[6].TableName = "Evaluaciones Reprobadas";

            

            return dataSet;
        }

        private DataSet dataSetEvaluacionSede()
        {
            Info_Evaluaciones Obj = new DCL.Info_Evaluaciones();
            Obj.IdEvaluacion = IdEvaluacion;
            var dataSet = new DataSet();
            var indiceSede = Info_Evaluaciones_BRL.SelectTable(Obj, 11).Copy();
            var totalTable = new DataTable("Indice de Evaluacion");

            foreach (DataColumn col in indiceSede.Columns)
            {
                totalTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row in indiceSede.Rows)
            {
                var newRow = totalTable.NewRow();
                for (int i = 0; i < indiceSede.Columns.Count; i++)
                {
                    newRow[i] = row[i];
                }
                totalTable.Rows.Add(newRow);
            }


            dataSet.Tables.Add(totalTable);

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 2).Copy());
            dataSet.Tables[1].TableName = "Eva. Incompletas";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 12).Copy());
            dataSet.Tables[2].TableName = "Indice de Aprobacion";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 8).Copy());
            dataSet.Tables[3].TableName = "Personal Aprobado";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 9).Copy());
            dataSet.Tables[4].TableName = "Personal Reprobado";

            return dataSet;
        }

        private DataSet dataSetEvaluacionCargo()
        {
            Info_Evaluaciones Obj = new DCL.Info_Evaluaciones();
            Obj.IdEvaluacion = IdEvaluacion;
            var dataSet = new DataSet();      
            var pendientes = Info_Evaluaciones_BRL.SelectTable(Obj, 13).Copy();
            var totalTable = new DataTable("Indice de Evaluacion");
                     
            foreach (DataColumn col in pendientes.Columns)
            {
                totalTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row in pendientes.Rows)
            {
                var newRow = totalTable.NewRow();
                for (int i = 0; i < pendientes.Columns.Count; i++)
                {
                    newRow[i] = row[i];
                }
                totalTable.Rows.Add(newRow);
            }
                      
            dataSet.Tables.Add(totalTable);

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 2).Copy());
            dataSet.Tables[1].TableName = "Eva. Incompletas";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 14).Copy());
            dataSet.Tables[2].TableName = "Indice de Aprobacion";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 8).Copy());
            dataSet.Tables[3].TableName = "Personal Aprobado";

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 9).Copy());
            dataSet.Tables[4].TableName = "Personal Reprobado";


            return dataSet;
        }

        private DataSet dataSetEvaluacionRespuesta()
        {
            Info_Evaluaciones Obj = new DCL.Info_Evaluaciones();
            Obj.IdEvaluacion = IdEvaluacion;
            var dataSet = new DataSet();

            // Obtener datos de repuestas frecuentes
            var frecuencia = Info_Evaluaciones_BRL.SelectTable(Obj, 15).Copy();
            var totalTable = new DataTable("Frecuencia de Respuestas");

            foreach (DataColumn col in frecuencia.Columns)
            {
                totalTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row in frecuencia.Rows)
            {
                var newRow = totalTable.NewRow();
                for (int i = 0; i < frecuencia.Columns.Count; i++)
                {
                    newRow[i] = row[i];
                }
                totalTable.Rows.Add(newRow);
            }

            dataSet.Tables.Add(totalTable);

            dataSet.Tables.Add(Info_Evaluaciones_BRL.SelectTable(Obj, 4).Copy());
            dataSet.Tables[1].TableName = "Respuestas de Evaluaciones";


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