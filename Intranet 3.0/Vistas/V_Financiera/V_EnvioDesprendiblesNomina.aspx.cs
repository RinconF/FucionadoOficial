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
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static Intranet_3._0.Vistas.V_Financiera.V_EnvioDesprendiblesNomina;
using DCL;
using System.Text;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
using System.Threading;


namespace Intranet_3._0.Vistas.V_Financiera
{
    public partial class V_EnvioDesprendiblesNomina : System.Web.UI.Page
    {
       DataTable tablaDatosNomina;
       DataTable tablaDatosNominaNoSalarial;
       DataTable tablaDatosCedula;

        // Declaro variables
        string mes = null;
        string año = null;
        string periodo = null;
        string Lote = null;


        protected void Page_Init(object sender, EventArgs e)
        {
           
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // VISIBILIDAD PRESERTIMANADA DE LOS CAMPOS A CONSULTAR
                if (!IsPostBack)
                {
                    CargarAños();
                    // Obtener período actual desde la base de datos
                    var periodoActual = ObtenerPeriodoActual();

                    // Establece los valores en los controles de la interfaz
                    DropDownListYear.SelectedValue = periodoActual.Año.ToString();
                    DropDownListMonth.SelectedValue = periodoActual.Mes.ToString();
                }
                Seccion_Lotes.Visible = false;

            }
            catch { }
        }

        private (int Año, int Mes) ObtenerPeriodoActual()
        {
            try
            {
                // Llama al método SelectTable para ejecutar la acción 14
                DataTable lotesPendientes = Nomina_Desprendibles_BRL.SelectTable(new Nomina_Desprendibles(), 14);

                if (lotesPendientes.Rows.Count > 0)
                {
                    // Obtén los valores de la primera fila
                    int año = Convert.ToInt32(lotesPendientes.Rows[0]["Anio"]);
                    int mes = Convert.ToInt32(lotesPendientes.Rows[0]["Mes"]);

                    return (año, mes);
                }
                else
                {
                    throw new Exception("No se encontraron datos para el período actual.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al obtener el período actual desde la base de datos.");
            }
        }


        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos
                if (!ValidarCampos())
                { return; }

                //EjecutarConsultaAsync();
                GenerarLotesAsync();

            }
            catch { }
        }

        private async Task GenerarLotesAsync()
        {
            try
            {
                // Varibles para la consulta
                mes = DropDownListMonth.SelectedValue;
                año = DropDownListYear.SelectedValue;
                periodo = DropDownListPeriod.SelectedValue;

                // 1. Consultar si hay lotes pendientes
               DataTable lotesPendientes = Nomina_Desprendibles_BRL.SelectTable(new Nomina_Desprendibles(), 5);

                if (lotesPendientes == null || lotesPendientes.Rows.Count == 0)
                {
                    // Generar y guardar los lotes en la base de datos
                    await ObtenerGuardarLoteAsync(año, mes, periodo);
                }

                // 2. Consultar el total de lotes generados
               DataTable totalLotesData = Nomina_Desprendibles_BRL.SelectTable(new Nomina_Desprendibles(), 6);
                int totalLotesCount = totalLotesData != null && totalLotesData.Rows.Count > 0
                    ? Convert.ToInt32(totalLotesData.Rows[0]["TotalLote"])
                    : 0;

                // 3. Mostrar los lotes en la interfaz
                if (totalLotesCount > 0)
                {
                    CargarLotes(totalLotesCount);
                    totalLotes.InnerText = Convert.ToString(totalLotesCount);

                    // Convertir número de mes a texto
                    int mesNumero = Convert.ToInt32(totalLotesData.Rows[0]["Mes"]);
                    string mesTexto = ObtenerNombreMes(mesNumero);

                    // Asignar datos del lote
                    mesLote.InnerText = mesTexto;
                    añoLote.InnerText = Convert.ToString(totalLotesData.Rows[0]["Anio"]);
                    periodoLote.InnerText = Convert.ToString(totalLotesData.Rows[0]["Periodo"]);

                    Seccion_Lotes.Visible = true;
                }
                else { Seccion_Lotes.Visible = false; }

                // 4. Mostrar advertencia si hay lotes pendientes
                if (lotesPendientes != null && lotesPendientes.Rows.Count > 0)
                {
                    MostrarMensaje("Tiene lotes pendientes por gestionar, por favor valide.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores: log o mensaje para depuración
                Console.Error.WriteLine($"Error al generar lotes: {ex.Message}");
                MostrarMensaje("Ocurrió un error al generar los lotes. Por favor, inténtelo de nuevo.");
            }
        }

        private void CargarLotes(int totalLotesCount)
        {
            try
            {
                // Validar si ya existe el elemento con Value="0"
                var defaultItem = DropDownListLote.Items.FindByValue("0");

                if (defaultItem == null)
                {
                    // Si no existe, lo creamos
                    defaultItem = new System.Web.UI.WebControls.ListItem("Selecciona", "0");
                    DropDownListLote.Items.Add(defaultItem);
                }

                // Limpiar los elementos dinámicos pero conservar el predeterminado
                DropDownListLote.Items.Clear();
                DropDownListLote.Items.Add(defaultItem);

                for (int i = 1; i <= totalLotesCount; i++)
                {
                    // Agregar un item al DropDownList con el formato "Lote (número)"
                    DropDownListLote.Items.Add(new System.Web.UI.WebControls.ListItem($"Lote {i}", i.ToString()));
                }

            }
            catch { }
        }

        private async Task ObtenerGuardarLoteAsync(string año, string mes, string periodo)
        {
            try
            {
                // Tamaño del lote de datos
                int tamañoLote = 200;
                int filaInicial = 1;
                int filaFinal = tamañoLote;
                int numeroLote = 1;

                DataTable tablaDatos = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                {
                    Periodo = Convert.ToInt32(periodo),
                    Mes = Convert.ToInt32(mes),
                    Year = Convert.ToInt32(año)
                }, 13);

                if (tablaDatos == null || tablaDatos.Rows.Count == 0)
                {
                    MostrarMensaje("Para el mes, año y período seleccionado no se encontró nómina disponible, por favor intente nuevamente.");
                    return; // Salir si no hay más filas
                }

                // Iterar mientras haya más filas
                while (true)
                {
                    // Obtener la tabla con los colaboradores 
                   DataTable tablaDatosCedula = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                    {
                        Periodo = Convert.ToInt32(periodo),
                        Mes = Convert.ToInt32(mes),
                        Year = Convert.ToInt32(año),
                        FilaInicial = Convert.ToInt32(filaInicial),
                        FilaFinal = Convert.ToInt32(filaFinal)
                    }, 3);

                    if (tablaDatosCedula == null || tablaDatosCedula.Rows.Count == 0)
                        break;

                    // Recorrer cada fila del lote y guardarla en la base de datos
                    foreach (DataRow row in tablaDatosCedula.Rows)
                    {
                        Nomina_Desprendibles lote = new Nomina_Desprendibles
                        {
                            Numero_Lote = numeroLote,
                            N_Identificacion = Convert.ToInt32(row["Cedula"].ToString()),
                            Correo = Convert.ToString(row["Correo"].ToString()),
                            Mes = Convert.ToInt32(mes),
                            Year = Convert.ToInt32(año),
                            Periodo = Convert.ToInt32(periodo),
                        };
                        Nomina_Desprendibles_BRL.InsertOrUpdate(lote, 4);
                    }

                    // Actualizar rango
                    filaInicial += tamañoLote;
                    filaFinal += tamañoLote;
                    numeroLote++;
                }
            }
            catch { }
        }

        protected void ProcesarLotePDF(object sender, EventArgs e)
        {
            try
            {
                Lote = HiddenFieldLote.Value;
                if (string.IsNullOrEmpty(Lote) || Lote == "0")
                {
                    MostrarMensaje("No puede generar PDFs sin seleccionar un número de lote, por favor intente nuevamente.");
                    AbreSeccion();
                    return;
                }

                // Consultar datos del lote
                DataTable datosLote = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                { Numero_Lote = Convert.ToInt32(Lote) }, 7);

                if (datosLote == null || datosLote.Rows.Count <= 0)
                {
                    MostrarMensaje("No se encontraron datos para el lote " + Lote + ".");
                    AbreSeccion();
                    return;
                }

                // Verificar el estado de Generado_PDF
                int estadoPDF = Convert.ToInt32(datosLote.Rows[0]["Generado_PDF"]);
                if (estadoPDF == 1)
                {
                    MostrarMensaje($"El lote {Lote} ya tiene PDFs generados.");
                    AbreSeccion();
                    return;
                }

                // Obtener información del lote
                string periodo = Convert.ToString(datosLote.Rows[0]["Periodo"]);
                string mes = Convert.ToString(datosLote.Rows[0]["Mes"]);
                string año = Convert.ToString(datosLote.Rows[0]["Anio"]);

                // Consultar colaboradores del lote
                DataTable datosColaborador = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                {
                    Periodo = Convert.ToInt32(periodo),
                    Mes = Convert.ToInt32(mes),
                    Year = Convert.ToInt32(año),
                    Numero_Lote = Convert.ToInt32(Lote)
                }, 8);

                if (datosColaborador == null || datosColaborador.Rows.Count == 0)
                {
                    MostrarMensaje("No se encontraron colaboradores para el lote " + Lote + ".");
                    AbreSeccion();
                    return;
                }

                // Procesar cada colaborador y generar PDFs
                foreach (DataRow row in datosColaborador.Rows)
                {
                    string cedula = row["N_Identificacion"].ToString();

                    // Consultar datos de nómina para el colaborador
                    tablaDatosNomina = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                    {
                        Periodo = Convert.ToInt32(periodo),
                        Mes = Convert.ToInt32(mes),
                        Year = Convert.ToInt32(año),
                        N_Identificacion = Convert.ToInt32(cedula)
                    }, 0);

                    if (periodo == "2")
                    {
                        tablaDatosNominaNoSalarial = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles()
                        {
                            Periodo = Convert.ToInt32(periodo),
                            Mes = Convert.ToInt32(mes),
                            Year = Convert.ToInt32(año),
                            N_Identificacion = Convert.ToInt32(cedula)
                        }, 1);
                    }

                    // Generar PDF si se encuentran datos de nómina
                    if (tablaDatosNomina != null && tablaDatosNomina.Rows.Count > 0)
                    {
                        string pdfPath = GenerarPdfConDatos(tablaDatosNomina, tablaDatosNominaNoSalarial);
                        if (pdfPath != null)
                        {
                            // Actualizar la base de datos con la ruta del archivo y marcar como generado
                            Nomina_Desprendibles actualizaDatos = new Nomina_Desprendibles
                            {
                                Ruta_Archivo = pdfPath,
                                N_Identificacion = Convert.ToInt32(cedula),
                                Numero_Lote = Convert.ToInt32(Lote),
                            };
                            Nomina_Desprendibles_BRL.InsertOrUpdate(actualizaDatos, 9);
                        }

                        Console.WriteLine($"PDF generado para {cedula}: {pdfPath}");
                    }
                    else
                    {
                        Console.WriteLine($"No se encontraron datos de nómina para el colaborador {cedula}.");
                    }
                }

                MostrarMensaje("Proceso completado exitosamente para el lote "+ Lote +".");
                ScriptManager.RegisterStartupScript(this, GetType(), "EnableDropdown", "enableDropdown();", true);
                AbreSeccion();
            }
            catch { }
        }

        private string GenerarPdfConDatos(DataTable datosNomina, DataTable datosNoSalarial)
        {
            // Ruta relativa de la carpeta donde se almacenarán los PDFs
            string relativePath = "~/Desprendibles_Nomina_PDF/";
            string pdfDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Desprendibles_Nomina_PDF");

            // Crear directorio si no existe
            if (!Directory.Exists(pdfDirectory))
            { Directory.CreateDirectory(pdfDirectory); }

            string nombreArchivo = datosNomina.Rows[0][8].ToString();
            string nombreArchivoSinEspacios = nombreArchivo.Replace(" ", "");

            // Ruta física para guardar el PDF
            string pdfPath = Path.Combine(pdfDirectory, nombreArchivoSinEspacios + ".pdf");

            // Crear PDF con datos personalizados
            using (var document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30))
            {
                PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                // TABLA DEL ENCABEZADO
                PdfPTable tablaEncabezado = CrearTablaEncabezado(datosNomina);
                if (tablaEncabezado != null)
                {
                    document.Add(tablaEncabezado);
                }
                //TABLA 1 DE DATOS EMPLEADO
                PdfPTable tablaDatosEmpleado1 = CrearTablaDatosEmpleados1(datosNomina);
                if (tablaDatosEmpleado1 != null)
                {
                    document.Add(tablaDatosEmpleado1);
                }
                //TABLA 2 DE DATOS EMPLEADO
                PdfPTable tablaDatosEmpleado2 = CrearTablaDatosEmpleados2(datosNomina);
                if (tablaDatosEmpleado2 != null)
                {
                    document.Add(tablaDatosEmpleado2);
                }

                //TABLA CONCEPTOS HEADER
                PdfPTable tablaConceptosHeader = CreateTablaConceptosHeader(datosNomina);
                if (tablaConceptosHeader != null)
                {
                    document.Add(tablaConceptosHeader);
                }

                //TABLA CONCEPTOS CUERPO
                PdfPTable tablaConceptosCuerpo = CreateTablaConceptosCuerpo(datosNomina);
                if (tablaConceptosCuerpo != null)
                {
                    document.Add(tablaConceptosCuerpo);
                }

                //TABLA DE TOTALES
                PdfPTable tablaTotales = CreateTablaTotales(datosNomina);
                if (tablaTotales != null)
                {
                    document.Add(tablaTotales);
                }

                //DESPRENDIBLES NO SALARIALES
                if (datosNoSalarial != null)
                {
                    //TABLA ENCABEZADO DOCUMENTO DESPREDIBLE NO SALARIAL
                    PdfPTable tablaEncabezadoNoSalarial = CrearTablaEncabezadoNS(datosNoSalarial);
                    if (tablaEncabezadoNoSalarial != null)
                    {
                        document.Add(tablaEncabezadoNoSalarial);
                    }

                    //TABLA CONCEPTOS HEADER DOCUMENTO DESPREDIBLES NO SALARIAL
                    PdfPTable tablaConceptosHeaderNS = CreateTablaConceptosHeaderNS(datosNoSalarial);
                    if (tablaConceptosHeaderNS != null)
                    {
                        document.Add(tablaConceptosHeaderNS);
                    }

                    //TABLA CONCEPTOS CUERPO DOCUMENTO DESPRENDIBLES NO SALARIAL
                    PdfPTable tablaConceptosCuerpoNS = CreateTablaConceptosCuerpoNS(datosNoSalarial);
                    if (tablaConceptosCuerpoNS != null)
                    {
                        document.Add(tablaConceptosCuerpoNS);
                    }

                    //TABLA TOTALES DOCUMENTO DESPRENDIBLES NO SALARIAL
                    PdfPTable tablaTotalesNS = CreateTablaTotalesNS(datosNoSalarial);
                    if (tablaTotalesNS != null)
                    {
                        document.Add(tablaTotalesNS);
                    }
                }

                // Definir la fuente con tamaño 5
                Font smallFont = new Font(Font.FontFamily.HELVETICA, 6);
                // Crear el párrafo con el texto y la fuente definida
                Paragraph paragraph = new Paragraph("\nEste es un documento generado automáticamente.", smallFont);
                document.Add(paragraph);

                document.Close();
            }

            // Devolver la ruta relativa para almacenar en la base de datos
            return Path.Combine(relativePath, nombreArchivoSinEspacios + ".pdf").Replace("\\", "/");
        }

        protected async void btnEnviar_Click(object sender, EventArgs e)
        {
            List<Task<bool>> tareasEnvio = new List<Task<bool>>();
            List<string> rutasArchivosEnviados = new List<string>();

            try
            {
                Lote = HiddenFieldLote.Value;

                if (string.IsNullOrEmpty(Lote) || Lote == "0")
                {
                    MostrarMensaje("No puede enviar PDFs sin seleccionar un número de lote, por favor intente nuevamente.");
                    AbreSeccion();
                    return;
                }

                // Consultar datos del lote
                DataTable datosLoteEnviar = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles() { Numero_Lote = Convert.ToInt32(Lote) }, 2);

                if (datosLoteEnviar == null || datosLoteEnviar.Rows.Count <= 0)
                {
                    MostrarMensaje($"El lote {Lote} NO tiene PDFs generados.");
                    AbreSeccion();
                    return;
                }

                int loteE = Convert.ToInt32(datosLoteEnviar.Rows[0]["Numero_Lote"]);

                // LOTE DE DESPRENDIBLES A ENVIAR (TOMA LOS DATOS DE EL PRIMER FORMULARIO)
                DataTable ObtenerDatosLote = Nomina_Desprendibles_BRL.SelectTable(new DCL.Nomina_Desprendibles() { Numero_Lote = loteE }, 10);

                // Limitar el número de conexiones simultáneas
                int maxConcurrentConnections = 40;
                SemaphoreSlim semaphore = new SemaphoreSlim(maxConcurrentConnections);

                foreach (DataRow row in ObtenerDatosLote.Rows)
                {
                    string identificacion = row["N_Identificacion"].ToString();
                    string nombreColaborador = row["Nombre Colaborador"].ToString();
                    string correoDestinatario = row["Correo"].ToString();//"asistente.desarrollo1@etib.com.co";
                    string anio = row["Anio"].ToString();
                    string mes = row["Mes"].ToString();
                    string nombreMes = row["Nombre_Mes"].ToString();
                    string periodo = row["Periodo"].ToString();
                    string rutaArchivo = row["Ruta_Archivo"].ToString();
                    string generadoPDF = row["Generado_PDF"].ToString();

                    // VALIDA LA EXISTENCIA
                    if (generadoPDF == "True" && !string.IsNullOrWhiteSpace(correoDestinatario) && !string.IsNullOrWhiteSpace(identificacion))
                    {
                        // ASUNTO DEL CORREO
                        string asunto = $"{identificacion} - {nombreColaborador} - COMPROBANTE DE PAGO CORRESPONDIENTE A LA QUINCENA {periodo} DEL MES DE {nombreMes} DEL AÑO {anio}";
                        // CUERPO (VACIO)
                        string cuerpo = $"";

                        // ADJUNTO DEL ARCHIVO PDF 
                        string rutaPdf = Server.MapPath($"{rutaArchivo}");

                        // Añadir tarea de envío de correo a la lista
                        tareasEnvio.Add(EnviarCorreoConSemaforo(correoDestinatario, asunto, cuerpo, rutaPdf, semaphore));
                        rutasArchivosEnviados.Add(rutaPdf);
                    }
                }

                // Esperar a que se completen todas las tareas de envío de correos
                
                await Task.WhenAll(tareasEnvio); 
                // Llamada a la base de datos para eliminar registros, sin importar el resultado del envío de correos
                Nomina_Desprendibles_BRL.InsertOrUpdate(new DCL.Nomina_Desprendibles() { Numero_Lote = loteE }, 11);

                MostrarMensaje("Proceso completado exitosamente para el envío de correos correspondiente al lote " + Lote + ".");
                ScriptManager.RegisterStartupScript(this, GetType(), "EnableDropdown", "enableDropdown();", true);
                AbreSeccion();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorScript", $"alert('Error: {ex.Message}');", true);
            }
        }

        private async Task<bool> EnviarCorreoConSemaforo(string destinatario, string asunto, string cuerpo, string rutaPdf, SemaphoreSlim semaphore)
        {
            await semaphore.WaitAsync();
            try
            {
                return await EnviarCorreo(destinatario, asunto, cuerpo, rutaPdf);
            }
            finally
            {
                semaphore.Release();
            }
        }

        protected async Task<bool> EnviarCorreo(string destinatario, string asunto, string cuerpo, string rutaPdf)
        {
            // OBJETO MENSAJE
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage
            {
                Subject = asunto,
                Body = cuerpo,
                BodyEncoding = System.Text.Encoding.UTF8,
                From = new System.Net.Mail.MailAddress("nomina@etib.com.co")
            };

            if (!string.IsNullOrWhiteSpace(destinatario))
            {
                string[] temp = destinatario.Split(',');
                foreach (string row in temp)
                {
                    mailMessage.To.Add(row);
                }
            }

            // Verificar si el archivo PDF existe antes de adjuntarlo
            if (System.IO.File.Exists(rutaPdf))
            {
                // Adjuntar el archivo PDF
                mailMessage.Attachments.Add(new System.Net.Mail.Attachment(rutaPdf));
            }
            else
            {
                Console.WriteLine($"El archivo {rutaPdf} no existe.");
                return false; // No continuar si el archivo no existe
            }

            // CLIENTE SMTP
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient
            {
                Credentials = new System.Net.NetworkCredential("nomina@etib.com.co", "ke?4ploE6wC@"),
                EnableSsl = true,
                Port = 587,
                Host = "mail.etib.com.co"
            };

            try
            {
                // ENVIAR CORREO ELECTRÓNICO
                await smtpClient.SendMailAsync(mailMessage); // FORMA ASINCRÓNICA
                Console.WriteLine("Correo enviado con éxito.");
                return true;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción durante el envío del correo
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
                return false;
            }
        }

        protected void btnDepurar_Lotes(object sender, EventArgs e)
        {
            try
            {
                string pdfDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Desprendibles_Nomina_PDF");

                // Verificar si el directorio existe antes de intentar eliminarlo
                if (Directory.Exists(pdfDirectory))
                {
                    try
                    {
                        // Eliminar el directorio y su contenido
                        Directory.Delete(pdfDirectory, true);
                        Console.WriteLine("El directorio se ha eliminado correctamente.");
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"Error al eliminar el directorio: {ex.Message}");
                    }
                }
                else { Console.WriteLine("El directorio no existe, no se puede eliminar."); }

                // Hago un TRUNCATE TABLE en la tabla Int_Control_Desprendibles
                Nomina_Desprendibles tabla = new Nomina_Desprendibles { };
                Nomina_Desprendibles_BRL.InsertOrUpdate(tabla, 12);

                MostrarMensaje("Los lotes y los archivos pdfs generados fueron depurados exitosamente, ya puede generar uno nuevo.");
                return;
            }
            catch { }
        }

        #region Estilos PDF (Nomina)
        //Encabezado
        private PdfPTable CrearTablaEncabezado(DataTable datosNomina)
        {
            try
            {
                //Crea Tabla
                PdfPTable tablaEncabezado = new PdfPTable(3);
                tablaEncabezado.WidthPercentage = 100;
                tablaEncabezado.SpacingAfter = 2f; // Espacio antes de la tabla
                float[] anchos = new float[] { .5f, 4f, 1f };
                tablaEncabezado.SetWidths(anchos);

                //Logo Etib
                string imagePath = Server.MapPath("~/Content/img/logo_etib.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                logo.ScaleAbsolute(50, 50);

                PdfPCell celdaLogo = new PdfPCell(logo)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    Border = Rectangle.NO_BORDER,
                    Rowspan = 4
                };

                //Inserta logo a la tabla
                tablaEncabezado.AddCell(celdaLogo);

                //Fila 1:
                tablaEncabezado.AddCell(CrearCeldaEncabezado(datosNomina.Rows[0][32].ToString(), encabezadoFont, Element.ALIGN_CENTER, Element.ALIGN_TOP));//Titulo
                tablaEncabezado.AddCell(CrearCeldaEncabezado(datosNomina.Rows[0][36].ToString(), fechaHoraFont, Element.ALIGN_CENTER, Element.ALIGN_TOP));//Fecha

                //Fila 2: 
                tablaEncabezado.AddCell(CrearCeldaEncabezado(datosNomina.Rows[0][33].ToString(), encabezadoFont, Element.ALIGN_CENTER));
                tablaEncabezado.AddCell(CrearCeldaEncabezado("", textoEncabezadoFont));

                //Fila 3:
                tablaEncabezado.AddCell(CrearCeldaEncabezado(datosNomina.Rows[0][34].ToString(), textoEncabezadoFont, Element.ALIGN_CENTER));
                tablaEncabezado.AddCell(CrearCeldaEncabezado("", textoEncabezadoFont));

                //Fila 4:
                tablaEncabezado.AddCell(CrearCeldaEncabezado(datosNomina.Rows[0][35].ToString(), textoEncabezadoFont, Element.ALIGN_CENTER));
                tablaEncabezado.AddCell(CrearCeldaEncabezado("", textoEncabezadoFont));

                return tablaEncabezado;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla de conceptos de nomina: {ex.Message}");
                return null;
            }
        }

        //TABLA 1 DATOS EMPLEADO
        private PdfPTable CrearTablaDatosEmpleados1(DataTable datosNomina)
        {
            try
            {
                PdfPTable tablaDatosEmpleado1 = new PdfPTable(6);
                tablaDatosEmpleado1.WidthPercentage = 100;
                float[] anchos = new float[] { 12f, 22f, 12f, 18f, 10f, 16f };
                tablaDatosEmpleado1.SetWidths(anchos);

                // Fila 1
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("Página:", informacionColaboradorBoldFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("1", informacionColaboradorFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("Email:", informacionColaboradorBoldFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos(datosNomina.Rows[0][1].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("Documento:", informacionColaboradorBoldFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos(datosNomina.Rows[0][3].ToString(), informacionColaboradorFont));

                // Fila 2
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("Dirección:", informacionColaboradorBoldFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos(datosNomina.Rows[0][0].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("Teléfono:", informacionColaboradorBoldFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos(datosNomina.Rows[0][2].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos("Fecha de ingreso:", informacionColaboradorBoldFont));
                tablaDatosEmpleado1.AddCell(CrearCeldaDatos(datosNomina.Rows[0][4].ToString(), informacionColaboradorFont));

                // Crear una celda contenedora con el borde externo
                PdfPCell contenedorTabla1 = new PdfPCell(tablaDatosEmpleado1)
                {
                    Border = PdfPCell.NO_BORDER,
                    Padding = 4f
                };

                // Agregar bordes redondeados
                contenedorTabla1.CellEvent = new RoundedBorder();

                // Crear una tabla que actúe como marco
                PdfPTable tablaConBorde1 = new PdfPTable(1);
                tablaConBorde1.WidthPercentage = 100;
                tablaConBorde1.SpacingAfter = 10f;
                tablaConBorde1.AddCell(contenedorTabla1);

                return tablaConBorde1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla de datos del empleado: {ex.Message}");
                return null;
            }
        }

        //TABLA 2 DATOS EMPLEADO
        private PdfPTable CrearTablaDatosEmpleados2(DataTable datosNomina)
        {
            try
            {
                PdfPTable tablaDatosEmpleado2 = new PdfPTable(6);
                tablaDatosEmpleado2.WidthPercentage = 100;
                float[] anchos = new float[] { 12f, 22f, 12f, 18f, 10f, 16f };
                tablaDatosEmpleado2.SetWidths(anchos);

                // Fila 1: 
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Nombre Colaborador:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][5].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Cédula Número:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][8].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Entidad pensión:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][11].ToString(), informacionColaboradorFont));

                // Fila 2: 
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Cargo:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][6].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("No. de Cuenta Bancaria:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][9].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Entidad Bancaria:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][12].ToString(), informacionColaboradorFont));

                // Fila 3: 
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Sueldo básico:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][7].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("Entidad salud:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][10].ToString(), informacionColaboradorFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos("C. Costo:", informacionColaboradorBoldFont));
                tablaDatosEmpleado2.AddCell(CrearCeldaDatos(datosNomina.Rows[0][13].ToString(), informacionColaboradorFont));

                // Crear una celda contenedora con el borde externo
                PdfPCell contenedorTabla2 = new PdfPCell(tablaDatosEmpleado2)
                {
                    Border = PdfPCell.NO_BORDER,
                    Padding = 4f
                };

                // Agregar bordes redondeados
                contenedorTabla2.CellEvent = new RoundedBorder();

                // Crear una tabla que actúe como marco
                PdfPTable tablaConBorde2 = new PdfPTable(1);
                tablaConBorde2.WidthPercentage = 100;
                tablaConBorde2.SpacingAfter = 10f;
                tablaConBorde2.AddCell(contenedorTabla2);

                return tablaConBorde2;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla de datos del empleado: {ex.Message}");
                return null;
            }
        }

        //HEADER
        private PdfPTable CreateTablaConceptosHeader(DataTable datosNomina)
        {
            try
            {
                // Crear tabla para conceptos encabezado
                PdfPTable tablaConceptos = new PdfPTable(8); // 8 columnas
                tablaConceptos.WidthPercentage = 100;
                tablaConceptos.SetWidths(new float[] { 7f, 25f, 10f, 10f, 10f, 18f, 10f, 10f }); // Ajustar ancho relativo de columnas
                tablaConceptos.DefaultCell.Border = Rectangle.BOX;

                // Encabezados
                tablaConceptos.AddCell(CrearCeldaConceptos("Concepto", informacionColaboradorBoldFont));
                tablaConceptos.AddCell(CrearCeldaConceptos("Descripción", informacionColaboradorBoldFont));
                tablaConceptos.AddCell(CrearCeldaConceptos("Unidades", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));
                tablaConceptos.AddCell(CrearCeldaConceptos("Vlr devengo", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));
                tablaConceptos.AddCell(CrearCeldaConceptos("Vlr deducción", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));
                tablaConceptos.AddCell(CrearCeldaConceptos("Valor total", informacionColaboradorBoldFont, Element.ALIGN_CENTER));
                tablaConceptos.AddCell(CrearCeldaConceptos("Descontado", informacionColaboradorBoldFont, Element.ALIGN_CENTER));
                tablaConceptos.AddCell(CrearCeldaConceptos("Saldo", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));

                PdfPCell contenedorTablaConceptos = new PdfPCell(tablaConceptos)
                {
                    Border = PdfPCell.NO_BORDER,
                    Padding = 4f
                };

                // Agregar bordes redondeados
                contenedorTablaConceptos.CellEvent = new RoundedBorder();

                // Crear una tabla que actúe como marco
                PdfPTable tablaConBordesConceptos = new PdfPTable(1);
                tablaConBordesConceptos.WidthPercentage = 100;
                tablaConBordesConceptos.AddCell(contenedorTablaConceptos);

                return tablaConBordesConceptos;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al crear la tabla del encabezado: {ex.Message}");
                return null;

            }
        }

        //CUERPO
        private PdfPTable CreateTablaConceptosCuerpo(DataTable datosNomina)
        {
            try
            {
                // Crear tabla para conceptos encabezado
                PdfPTable tablaConceptosCuerpo = new PdfPTable(8); // 8 columnas
                tablaConceptosCuerpo.WidthPercentage = 100;
                tablaConceptosCuerpo.SetWidths(new float[] { 7f, 25f, 10f, 10f, 10f, 18f, 10f, 10f }); // Ajustar ancho relativo de columnas
                tablaConceptosCuerpo.DefaultCell.Border = Rectangle.BOX;

                // Filas dinámicas
                foreach (DataRow row in datosNomina.Rows)
                {
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Concepto"].ToString(), informacionColaboradorFont));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Descripción"].ToString(), informacionColaboradorFont));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Unidades"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Vlr devengo"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Vlr deducción"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Valor total"].ToString(), informacionColaboradorFont, Element.ALIGN_CENTER));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Descontado"].ToString(), informacionColaboradorFont, Element.ALIGN_CENTER));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Saldo"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                }

                PdfPCell contenedorTablaConceptos = new PdfPCell(tablaConceptosCuerpo)
                {
                    Border = PdfPCell.NO_BORDER,
                    Padding = 4f
                };

                PdfPTable tablaConBordesConceptos = new PdfPTable(1);
                tablaConBordesConceptos.WidthPercentage = 100;
                tablaConBordesConceptos.AddCell(contenedorTablaConceptos);

                return tablaConBordesConceptos;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al crear la tabla del encabezado: {ex.Message}");
                return null;

            }
        }

        private PdfPTable CreateTablaTotales(DataTable datosNomina)
        {
            try
            {
                PdfPTable tablaTotales = new PdfPTable(8); // 8 columnas
                tablaTotales.WidthPercentage = 100;
                tablaTotales.SetWidths(new float[] { 7f, 25f, 10f, 10f, 10f, 18f, 10f, 10f }); // Ajustar ancho relativo de columnas
                tablaTotales.DefaultCell.Border = Rectangle.BOX;
                //Fila 1:
                tablaTotales.AddCell(CrearCeldaTotales("Totales", informacionColaboradorFont, Element.ALIGN_CENTER, colSpan: 2));
                tablaTotales.AddCell(CrearCeldaTotales(datosNomina.Rows[0][28].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales(datosNomina.Rows[0][29].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales(datosNomina.Rows[0][30].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales($"Tipo de Cuenta: {datosNomina.Rows[0][27].ToString()}", informacionColaboradorFont, Element.ALIGN_CENTER));
                tablaTotales.AddCell(CrearCeldaTotales($"", informacionColaboradorFont, colSpan: 2));
                //Fila 2:
                tablaTotales.AddCell(CrearCeldaTotales("Neto a pagar: ", informacionColaboradorFont, Element.ALIGN_CENTER, colSpan: 2));
                tablaTotales.AddCell(CrearCeldaTotales(datosNomina.Rows[0][31].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales("", informacionColaboradorFont, colSpan: 5));
                //tablaTotales.AddCell(new PdfPCell(new Phrase($"Neto a pagar: {datosNomina.Rows[0][31].ToString()}", informacionColaboradorBoldFont)) { HorizontalAlignment = Element.ALIGN_LEFT });

                PdfPCell contenedorTabla = new PdfPCell(tablaTotales)
                {
                    Border = PdfPCell.TOP_BORDER,
                    BorderWidth = 1f,
                    Padding = 2f
                };

                PdfPTable tablaTotalesBordeSuperior = new PdfPTable(1);
                tablaTotalesBordeSuperior.WidthPercentage = 100;
                tablaTotalesBordeSuperior.AddCell(contenedorTabla);

                return tablaTotalesBordeSuperior;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla del encabezado: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Estilos PDF (Bonos)
        private PdfPTable CrearTablaEncabezadoNS(DataTable datosNoSalarial)
        {
            try
            {
                //Crea Tabla
                PdfPTable tablaEncabezado = new PdfPTable(3);
                tablaEncabezado.WidthPercentage = 100;
                tablaEncabezado.SpacingBefore = 10f;
                tablaEncabezado.SpacingAfter = 2f; // Espacio antes de la tabla
                float[] anchos = new float[] { .5f, 4f, 1f };
                tablaEncabezado.SetWidths(anchos);

                //Logo Etib
                string imagePath = Server.MapPath("~/Content/img/logo_etib.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                logo.ScaleAbsolute(50, 50);

                PdfPCell celdaLogo = new PdfPCell(logo)
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_TOP,
                    Border = Rectangle.NO_BORDER,
                };

                //Inserta logo a la tabla
                tablaEncabezado.AddCell(CrearCeldaEncabezado("", textoEncabezadoFont));

                //Fila 1:
                tablaEncabezado.AddCell(CrearCeldaEncabezado(datosNoSalarial.Rows[0][33].ToString(), encabezadoFont, Element.ALIGN_CENTER, Element.ALIGN_TOP));//Titulo
                tablaEncabezado.AddCell(CrearCeldaEncabezado("", fechaHoraFont, Element.ALIGN_CENTER, Element.ALIGN_TOP));//Fecha

                return tablaEncabezado;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla de conceptos de nomina: {ex.Message}");
                return null;
            }
        }

        //HEADER
        private PdfPTable CreateTablaConceptosHeaderNS(DataTable datosNoSalarial)
        {
            try
            {
                // Crear tabla para conceptos encabezado
                PdfPTable tablaConceptos = new PdfPTable(8); // 8 columnas
                tablaConceptos.WidthPercentage = 100;
                tablaConceptos.SetWidths(new float[] { 7f, 25f, 10f, 10f, 10f, 18f, 10f, 10f }); // Ajustar ancho relativo de columnas
                tablaConceptos.DefaultCell.Border = Rectangle.BOX;

                // Encabezados
                tablaConceptos.AddCell(CrearCeldaConceptos("Concepto", informacionColaboradorBoldFont));
                tablaConceptos.AddCell(CrearCeldaConceptos("Descripción", informacionColaboradorBoldFont));
                tablaConceptos.AddCell(CrearCeldaConceptos("Unidades", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));
                tablaConceptos.AddCell(CrearCeldaConceptos("Vlr devengo", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));
                tablaConceptos.AddCell(CrearCeldaConceptos("Vlr deducción", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));
                tablaConceptos.AddCell(CrearCeldaConceptos("Valor total", informacionColaboradorBoldFont, Element.ALIGN_CENTER));
                tablaConceptos.AddCell(CrearCeldaConceptos("Descontado", informacionColaboradorBoldFont, Element.ALIGN_CENTER));
                tablaConceptos.AddCell(CrearCeldaConceptos("Saldo", informacionColaboradorBoldFont, Element.ALIGN_RIGHT));

                PdfPCell contenedorTablaConceptos = new PdfPCell(tablaConceptos)
                {
                    Border = PdfPCell.NO_BORDER,
                    Padding = 4f
                };

                // Agregar bordes redondeados
                contenedorTablaConceptos.CellEvent = new RoundedBorder();

                // Crear una tabla que actúe como marco
                PdfPTable tablaConBordesConceptos = new PdfPTable(1);
                tablaConBordesConceptos.WidthPercentage = 100;
                tablaConBordesConceptos.AddCell(contenedorTablaConceptos);

                return tablaConBordesConceptos;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al crear la tabla del encabezado: {ex.Message}");
                return null;

            }
        }

        //CUERPO
        private PdfPTable CreateTablaConceptosCuerpoNS(DataTable datosNoSalarial)
        {
            try
            {
                // Crear tabla para conceptos encabezado
                PdfPTable tablaConceptosCuerpo = new PdfPTable(8); // 8 columnas
                tablaConceptosCuerpo.WidthPercentage = 100;
                tablaConceptosCuerpo.SetWidths(new float[] { 7f, 25f, 10f, 10f, 10f, 18f, 10f, 10f }); // Ajustar ancho relativo de columnas
                tablaConceptosCuerpo.DefaultCell.Border = Rectangle.BOX;

                // Filas dinámicas
                foreach (DataRow row in datosNoSalarial.Rows)
                {
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Concepto"].ToString(), informacionColaboradorFont));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Descripción"].ToString(), informacionColaboradorFont));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Unidades"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Vlr devengo"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Vlr deducción"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Valor total"].ToString(), informacionColaboradorFont, Element.ALIGN_CENTER));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Descontado"].ToString(), informacionColaboradorFont, Element.ALIGN_CENTER));
                    tablaConceptosCuerpo.AddCell(CrearCeldaConceptosCuerpo(row["Saldo"].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                }

                PdfPCell contenedorTablaConceptos = new PdfPCell(tablaConceptosCuerpo)
                {
                    Border = PdfPCell.NO_BORDER,
                    Padding = 4f
                };

                PdfPTable tablaConBordesConceptos = new PdfPTable(1);
                tablaConBordesConceptos.WidthPercentage = 100;
                tablaConBordesConceptos.AddCell(contenedorTablaConceptos);

                return tablaConBordesConceptos;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al crear la tabla del encabezado: {ex.Message}");
                return null;

            }
        }

        private PdfPTable CreateTablaTotalesNS(DataTable datosNoSalarial)
        {
            try
            {
                PdfPTable tablaTotales = new PdfPTable(8); // 8 columnas
                tablaTotales.WidthPercentage = 100;
                tablaTotales.SetWidths(new float[] { 7f, 25f, 10f, 10f, 10f, 18f, 10f, 10f }); // Ajustar ancho relativo de columnas
                tablaTotales.DefaultCell.Border = Rectangle.BOX;
                //Fila 1:
                tablaTotales.AddCell(CrearCeldaTotales("Totales", informacionColaboradorFont, Element.ALIGN_CENTER, colSpan: 2));
                tablaTotales.AddCell(CrearCeldaTotales(datosNoSalarial.Rows[0][28].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales(datosNoSalarial.Rows[0][29].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales(datosNoSalarial.Rows[0][30].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales($"", informacionColaboradorFont, colSpan: 3));
                //Fila 2:
                tablaTotales.AddCell(CrearCeldaTotales("Neto a pagar: ", informacionColaboradorFont, Element.ALIGN_CENTER, colSpan: 2));
                tablaTotales.AddCell(CrearCeldaTotales(datosNoSalarial.Rows[0][31].ToString(), informacionColaboradorFont, Element.ALIGN_RIGHT));
                tablaTotales.AddCell(CrearCeldaTotales("", informacionColaboradorFont, colSpan: 5));
                //tablaTotales.AddCell(new PdfPCell(new Phrase($"Neto a pagar: {datosNomina.Rows[0][31].ToString()}", informacionColaboradorBoldFont)) { HorizontalAlignment = Element.ALIGN_LEFT });

                PdfPCell contenedorTabla = new PdfPCell(tablaTotales)
                {
                    Border = PdfPCell.TOP_BORDER,
                    BorderWidth = 1f,
                    Padding = 2f
                };

                PdfPTable tablaTotalesBordeSuperior = new PdfPTable(1);
                tablaTotalesBordeSuperior.WidthPercentage = 100;
                tablaTotalesBordeSuperior.AddCell(contenedorTabla);

                return tablaTotalesBordeSuperior;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla del encabezado: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Estilos Genéricos del PDF
        // Definir los estilos de fuente para las celdas
        private Font informacionColaboradorFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL);
        private Font informacionColaboradorBoldFont = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD);

        // Definir el estilo de la celda para el encabezado
        private Font encabezadoFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD);
        private Font textoEncabezadoFont = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD);

        // Definir el estilo de la celda para los datos de fecha/hora
        private Font fechaHoraFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL);

        private PdfPCell CrearCeldaEncabezado(string texto, Font estilo, int horizontalAlignment = Element.ALIGN_LEFT, int verticalAlignment = Element.ALIGN_MIDDLE)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, estilo));
            celda.Border = Rectangle.NO_BORDER;  // Otras opciones si deseas más bordes
            celda.HorizontalAlignment = horizontalAlignment; // Ajuste de alineación horizontal
            celda.VerticalAlignment = verticalAlignment; // Ajuste de alineación
            return celda;
        }

        // Método para crear celdas con datos del empleado
        private PdfPCell CrearCeldaDatos(string texto, Font estilo, int colSpan = 1)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, estilo))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingTop = 5f,
                PaddingBottom = 5f,
                Border = Rectangle.NO_BORDER,
                Colspan = colSpan
            };
            return celda;
        }

        private PdfPCell CrearCeldaConceptos(string texto, Font estilo, int horizontalAlignment = Element.ALIGN_LEFT, int verticalAlignment = Element.ALIGN_MIDDLE, int colSpan = 1)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, estilo))
            {
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment,
                PaddingTop = 5f,
                PaddingBottom = 5f,
                Border = Rectangle.NO_BORDER,
                Colspan = colSpan
            };
            return celda;
        }

        private PdfPCell CrearCeldaConceptosCuerpo(string texto, Font estilo, int horizontalAlignment = Element.ALIGN_LEFT, int verticalAlignment = Element.ALIGN_MIDDLE, int colSpan = 1)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, estilo))
            {
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment,
                PaddingTop = 2f,
                PaddingBottom = 2f,
                Border = Rectangle.NO_BORDER,
                Colspan = colSpan
            };
            return celda;
        }

        // Clase para agregar bordes redondeados a la celda
        public class RoundedBorder : IPdfPCellEvent
        {
            public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
            {
                PdfContentByte cb = canvases[PdfPTable.BACKGROUNDCANVAS];
                cb.RoundRectangle(position.Left, position.Bottom, position.Width, position.Height, 10);
                cb.SetLineWidth(1f);
                cb.SetColorStroke(BaseColor.BLACK);
                cb.Stroke();
            }
        }

        private PdfPCell CrearCeldaTotales(string texto, Font estilo, int horizontalAlignment = Element.ALIGN_LEFT, int verticalAlignment = Element.ALIGN_MIDDLE, int colSpan = 1)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, estilo))
            {
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment,
                PaddingTop = 3f,
                PaddingBottom = 3f,
                Border = Rectangle.NO_BORDER,
                Colspan = colSpan
            };
            return celda;
        }
        #endregion

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

        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "MensajeAlerta", $"alert('{mensaje}');", true);
        }

        private void AbreSeccion()
        {
            Seccion_Lotes.Visible = true;
            Seccion_Botones.Visible = true;
            DropDownListLote.SelectedValue = "0";
        }

        private string ObtenerNombreMes(int mes)
        {
            switch (mes)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return "Mes desconocido";
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListPeriod.SelectedIndex = 0;
                Seccion_Lotes.Visible = false;
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
                    DropDownListYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                }
            }
            catch { }
        }

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí puedes manejar el evento cuando se selecciona un nuevo valor en cualquier DropDownList.
            DropDownList dropDown = sender as DropDownList;

            if (dropDown != null)
            {
                // Verifica cuál DropDownList activó el evento y realiza acciones específicas
                switch (dropDown.ID)
                {
                    case "DropDownListMonth":
                        // Acción para DropDownListMonth
                        break;
                    case "DropDownListYear":
                        // Acción para DropDownListYear
                        break;
                    case "DropDownListPeriod":
                        // Acción para DropDownListPeriod
                        break;
                }
            }
        }

    }
}
