using BRL;
using DCL;
using DocumentFormat.OpenXml.Wordprocessing;
using Intranet_3._0.Interna;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Services;

namespace Intranet_3._0
{
    /// <summary>
    /// Descripción breve de WebService_Default
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]

    public class WebService_Default : System.Web.Services.WebService
    {
        string pathLog = "";
        [WebMethod]
        public List<string[]> cargar_card_noticia()
        {
            string ipServer = "";

            AG_Utils utilidades = new AG_Utils();
            try
            {
                pathLog = Server.MapPath(@"~/logs");

                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                List<string[]> list = new List<string[]>();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                DataTable dt;
                DCL.Int_Noticias obj = new DCL.Int_Noticias();
                dt = Int_Noticias_BRL.SelectTable(obj, 0);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string archivoRemoto = row["Imagen"].ToString();
                        var anexoFoto = archivoRemoto.Split('\\');
                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                        int[] indexArray = new int[index];
                        var removerLista = new List<int>(indexArray);
                        for (int i = 0; i < removerLista.Count; i++)
                        {
                            arrayAnexoFoto.RemoveAt(0);
                        }
                        string rutaCompleta = String.Join("\\", arrayAnexoFoto);

                        string localTemp = $@"{pathServer}{rutaCompleta}";
                        List<string> rutalocalTemp = localTemp.Split('\\').ToList();
                        int indexRemota = rutalocalTemp.Count;
                        rutalocalTemp.RemoveAt(--indexRemota);
                        string rutaLocalCompleta = String.Join(@"\", rutalocalTemp) + "\\";

                        if (utilidades.impersonateValidUser())
                        {
                            if (!Directory.Exists(rutaLocalCompleta))
                            {
                                Directory.CreateDirectory(rutaLocalCompleta);
                            }
                            if (conectaAdjuntos)
                            {
                                if (!File.Exists(localTemp))
                                {
                                    if (File.Exists(archivoRemoto))
                                    {
                                        File.Copy(archivoRemoto, $@"{localTemp}", true);
                                    }
                                }
                            }
                            else
                            {
                                //utilidades.logError($"{DateTime.Now}\nError al intentar conectarse al servidor: {ipServer}. \nNo se puede obtener imagenes de noticias. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}.", pathLog);
                            }

                            utilidades.undoImpersonation();
                        }
                        string[] array = new string[5];
                        array[0] = row["Id_Noticia"].ToString();
                        array[1] = row["Titulo"].ToString();
                        array[2] = row["Descripcion"].ToString();
                        if (File.Exists(localTemp))
                        {
                            array[3] = $@"Imagenes/{rutaCompleta.Replace('\\', '/')}";
                        }
                        else
                        {
                            array[3] = $@"Content/img/Ilustracion_grupo.png";
                        }

                        array[4] = row["Fecha_Creacion"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);

                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_card_noticia_recientes()
        {
            string ipServer = "";

            AG_Utils utilidades = new AG_Utils();
            try
            {
                pathLog = Server.MapPath(@"~/logs");
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                List<string[]> list = new List<string[]>();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                DataTable dt;
                DCL.Int_Noticias obj = new DCL.Int_Noticias();
                dt = Int_Noticias_BRL.SelectTable(obj, 1);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string archivoRemoto = row["Imagen"].ToString();
                        var anexoFoto = archivoRemoto.Split('\\');
                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                        int[] indexArray = new int[index];
                        var removerLista = new List<int>(indexArray);
                        for (int i = 0; i < removerLista.Count; i++)
                        {
                            arrayAnexoFoto.RemoveAt(0);
                        }
                        string rutaCompleta = String.Join("\\", arrayAnexoFoto);

                        string localTemp = $@"{pathServer}{rutaCompleta}";
                        List<string> rutalocalTemp = localTemp.Split('\\').ToList();
                        int indexRemota = rutalocalTemp.Count;
                        rutalocalTemp.RemoveAt(--indexRemota);
                        string rutaLocalCompleta = String.Join(@"\", rutalocalTemp) + "\\";


                        if (utilidades.impersonateValidUser())
                        {
                            if (!Directory.Exists(rutaLocalCompleta))
                            {
                                Directory.CreateDirectory(rutaLocalCompleta);
                            }
                            if (conectaAdjuntos)
                            {
                                if (!File.Exists(localTemp))
                                {
                                    if (File.Exists(archivoRemoto))
                                    {
                                        File.Copy(archivoRemoto, $@"{localTemp}", true);
                                    }
                                }
                            }
                            else
                            {
                                // utilidades.logError($"{DateTime.Now}\nError al intentar conectarse al servidor: {ipServer}. \nNo se puede obtener imagenes de noticias recientes. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}.", pathLog);
                            }

                            utilidades.undoImpersonation();
                        }

                        string[] array = new string[5];
                        array[0] = row["Id_Noticia"].ToString();
                        array[1] = row["Titulo"].ToString();
                        array[2] = row["Descripcion"].ToString();
                        if (File.Exists(localTemp))
                        {
                            array[3] = $@"Imagenes/{rutaCompleta.Replace('\\', '/')}";
                        }
                        else
                        {
                            array[3] = $@"Content/img/Ilustracion_grupo.png";
                        }
                        array[4] = row["Fecha_Creacion"].ToString();
                        list.Add(array);
                    }
                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);

                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_card_slideshow()
        {
            string ipServer = "";

            AG_Utils utilidades = new AG_Utils();
            try
            {
                pathLog = Server.MapPath(@"~/logs");

                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Noticias obj = new DCL.Int_Noticias();
                dt = Int_Noticias_BRL.SelectTable(obj, 2);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string archivoRemoto = row["Imagen"].ToString();
                        var anexoFoto = archivoRemoto.Split('\\');
                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                        int[] indexArray = new int[index];
                        var removerLista = new List<int>(indexArray);
                        for (int i = 0; i < removerLista.Count; i++)
                        {
                            arrayAnexoFoto.RemoveAt(0);
                        }
                        string rutaCompleta = String.Join("\\", arrayAnexoFoto);

                        string localTemp = $@"{pathServer}{rutaCompleta}";
                        List<string> rutalocalTemp = localTemp.Split('\\').ToList();
                        int indexRemota = rutalocalTemp.Count;
                        rutalocalTemp.RemoveAt(--indexRemota);
                        string rutaLocalCompleta = String.Join(@"\", rutalocalTemp) + "\\";

                        if (utilidades.impersonateValidUser())
                        {
                            if (!Directory.Exists(rutaLocalCompleta))
                            {
                                Directory.CreateDirectory(rutaLocalCompleta);
                            }
                            if (conectaAdjuntos)
                            {
                                if (!File.Exists(localTemp))
                                {
                                    if (File.Exists(archivoRemoto))
                                    {
                                        File.Copy(archivoRemoto, $@"{localTemp}", true);
                                    }
                                }
                            }
                            else
                            {
                                utilidades.logError($"{DateTime.Now}\nError al intentar conectarse al servidor: {ipServer}. \nNo se puede obtener imagenes de noticias. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}.", pathLog);
                            }

                            utilidades.undoImpersonation();
                        }
                        string[] array = new string[3];
                        array[0] = row["Id_SlideShow"].ToString();
                        if (File.Exists(localTemp))
                        {
                            array[1] = $@"Imagenes/{rutaCompleta.Replace('\\', '/')}";
                        }
                        else
                        {
                            array[1] = $@"Content/img/Ilustracion_grupo.png";
                        }
                        array[2] = row["Fecha_Creacion"].ToString();
                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }


            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);

                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public string Validar_Password(string Id_Usuario)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string retornar = "";

                if (Id_Usuario != "null")
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    dt = Int_Usuarios_BRL.SelectTable(obj, 29);
                    if (dt.Rows.Count > 0)
                    {
                        string encrytar = GetMD5(dt.Rows[0]["Usuario"].ToString());

                        if (encrytar == dt.Rows[0]["Contraseña"].ToString())
                        {
                            retornar = "1";
                        }
                        else
                        {
                            retornar = "0";
                        }
                    }
                    else
                    {
                        retornar = "vacio";
                    }
                }
                else
                {
                    retornar = "vacio";
                }

                return retornar;
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                string retornar = ex.ToString();
                return retornar;
            }
        }

        [WebMethod]
        public string Validar_Datos_Restablecer_Pass(string Usuario, string Correo)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string retornar = "";

                if (Usuario != "" && Correo != "")
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Usuario = Usuario;
                    obj.Contraseña = Correo;
                    dt = Int_Usuarios_BRL.SelectTable(obj, 46);
                    if (dt.Rows.Count > 0)
                    {
                        string encrytar = GetMD5(dt.Rows[0]["Usuario"].ToString());
                        DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                        obj_.Usuario = Usuario;
                        obj_.Contraseña = encrytar;
                        Int_Usuarios_BRL.InsertOrUpdate(obj_, 47);

                        retornar = "1";
                    }
                    else
                    {
                        retornar = "0";
                    }
                }
                else
                {
                    retornar = "vacio";
                }

                return retornar;
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                string retornar = ex.ToString();
                return retornar;
            }
        }

        [WebMethod]
        public string Actualizar_Contraseña(string txt_nueva_pass, string txt_conf_pass, string Id_Usuario)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string retornar = "";

                if (
                    !String.IsNullOrEmpty(txt_nueva_pass.Trim()) &&
                    !String.IsNullOrEmpty(txt_conf_pass.Trim())
                  )
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    dt = Int_Usuarios_BRL.SelectTable(obj, 29);
                    if (dt.Rows.Count > 0)
                    {
                        string encrytar_nueva = GetMD5(txt_nueva_pass.Trim());
                        if (dt.Rows[0]["Contraseña"].ToString() != encrytar_nueva)
                        {
                            DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                            obj_.Id_Usuario = Convert.ToInt32(Id_Usuario);
                            obj_.Contraseña = encrytar_nueva;
                            Int_Usuarios_BRL.InsertOrUpdate(obj_, 44);

                            retornar = "1";
                        }
                        else
                        {
                            retornar = "0";
                        }
                    }
                }
                else
                {
                    retornar = "vacio";
                }

                return retornar;
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                string retornar = "error";
                Console.WriteLine(ex);
                return retornar;
            }
        }

        [WebMethod]
        public List<string[]> verificacionTratamientoDatos(string idUsuario)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                List<string[]> list = new List<string[]>();
                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(idUsuario);
                dt = Int_Usuarios_BRL.SelectTable(obj, 61);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[2];
                        array[0] = row["id_tratamiendoDatos"].ToString();
                        array[1] = row["Nombre"].ToString();

                        list.Add(array);
                    }

                    return list;
                }
                else
                {
                    string[] array = new string[1];
                    array[0] = "0";
                    list.Add(array);
                    return list;
                }
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }

        [WebMethod]
        public string actualizarTratamientoDatos(string idUsuario, string accion)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(idUsuario);
                obj.Inicio = Convert.ToInt32(accion);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 62);

                return "1";
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                return ex.ToString();
            }
        }

        #region Evaluaciones
        [WebMethod]
        public ArrayList validarEvaluaciones(string Int_Id_UsuarioE)
        {
            try
            {
                // CONSULTA SI HAY EVALUACIONES HABILITADAS (ESTADO = 1)
                DCL.Int_01_EVA_Evaluaciones eva_habilitadas = new DCL.Int_01_EVA_Evaluaciones();
                DataTable dt_eva_habilitadas = Int_01_EVA_Evaluaciones_BRL.SelectTable(eva_habilitadas, 0);

                if (dt_eva_habilitadas.Rows.Count == 1)
                {
                    //NOTA APROBATORIA
                    decimal nota_aprob = Convert.ToDecimal(dt_eva_habilitadas.Rows[0]["c01_puntos_aprobacion"]);

                    // SI LA EVALUACIÓN ES SECTORIZADA (ID_TIPO_EVALUACION = 2)
                    if (Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_id_tipo_Evaluacion"]) == 2)
                    {
                        DCL.Int_01_EVA_Evaluaciones eva_sectorizada = new DCL.Int_01_EVA_Evaluaciones();
                        eva_sectorizada.Int_Id_Usuario = Convert.ToInt32(Int_Id_UsuarioE);
                        eva_sectorizada.Id_Evaluacion = Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_id_Evaluacion"]);
                        DataTable dt_eva_sectorizada = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(eva_sectorizada, 5);

                        if (dt_eva_sectorizada.Rows.Count == 0)
                        {
                            // CUANDO LA EVALUACIÓN ESTÁ SECTORIZADA PERO EL USUARIO NO HACE PARTE DE ESA POBLACIÓN OBJETIVO
                            return null;
                        }
                    }

                    // CONSULTA EL ID_INFO_EMPLEADO CON EL ID_USUARIO DE INTRANET
                    DCL.Int_01_EVA_Evaluaciones consulta_info_empleado = new DCL.Int_01_EVA_Evaluaciones();
                    consulta_info_empleado.Int_Id_Usuario = Convert.ToInt32(Int_Id_UsuarioE);
                    DataTable dt_consulta_info_empleado = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(consulta_info_empleado, 0);

                    if (dt_consulta_info_empleado.Rows.Count > 0)
                    {
                        // CONSULTA SI EL COLABORADOR YA INICIÓ PREVIAMENTE UNA EVALUACIÓN
                        DCL.Int_01_EVA_Evaluaciones consulta_evaluacion_iniciada = new DCL.Int_01_EVA_Evaluaciones();
                        consulta_evaluacion_iniciada.Id_Info_Empleado = Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]);
                        consulta_evaluacion_iniciada.Id_Evaluacion = Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_id_Evaluacion"]);
                        DataTable dt_consulta_evaluacion_iniciada = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(consulta_evaluacion_iniciada, 1);
                        
                        
                        DataRow intento_actual = null; //variable que almacena datos de el intento del colaborador
                        
                        //EN CASO DE NO SER SU PRIMER INTENTO
                        if (dt_consulta_evaluacion_iniciada.Rows.Count > 0)
                        {
                            //CONSULTA TODOS SUS INTENTOS
                            foreach (DataRow row in dt_consulta_evaluacion_iniciada.Rows)
                            {
                                //CONSULTA SI EL INTENTO FUE APROBADO
                                DCL.Int_01_EVA_Evaluaciones consulta_evaluacion_aprobada = new DCL.Int_01_EVA_Evaluaciones();
                                consulta_evaluacion_aprobada.Id_Ingreso_Evaluacion = Convert.ToInt32(row["c06_id_ingreso_evaluacion"]);
                                DataTable dt_estado_aprobacion = Int_11_EVA_Resultados_BRL.SelectTable(consulta_evaluacion_aprobada, 1);

                                if (dt_estado_aprobacion.Rows.Count > 0)
                                {
                                    if (dt_estado_aprobacion.Rows[0]["c11_estado_aprobacion"].ToString() == "True")
                                    {
                                        return null;//FUE APROBADO, NO TIENE QUE REPETIR EL INTENTO
                                    }
                                }
                                else
                                {
                                    intento_actual = row;//TOMA EL INTENTO NO FINALIZADO
                                    break;
                                }
                            }                           
                            // VALIDA SI EL USUARIO PUEDE HACER UN NUEVO INTENTO
                            if (intento_actual == null)
                            {
                                int n_intentos_colab = dt_consulta_evaluacion_iniciada.Rows.Count;
                                int n_intentos_permitidos = Convert.ToInt32(dt_consulta_evaluacion_iniciada.Rows[0]["c01_intentos_permitidos"]);

                                // SE CREA UN NUEVO INTENTO PARA EL USUARIO (AUN TIENE INTENTOS PERMITIDOS)
                                if (n_intentos_permitidos >= n_intentos_colab)
                                {
                                    return evaluacion_nueva(
                                        Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]),
                                        Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_id_Evaluacion"]),
                                        dt_eva_habilitadas.Rows[0]["c01_nombre"].ToString()
                                    );
                                }
                                else
                                {
                                    return null;//USUAIRO NO CUENTA CON MÁS INTENTOS
                                }
                            }
                            else
                            {
                                return evaluacion_iniciada(
                                    Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]),
                                    Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_id_Evaluacion"]),
                                    dt_eva_habilitadas.Rows[0]["c01_nombre"].ToString(),
                                    Convert.ToInt32(intento_actual["c06_id_ingreso_evaluacion"])
                                );
                            }
                        }
                        else
                        {
                            //ES SU PRIMER INTENTO
                            return evaluacion_nueva(
                                Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]),
                                Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_id_Evaluacion"]),
                                dt_eva_habilitadas.Rows[0]["c01_nombre"].ToString()
                            );
                        }
                    }
                    else
                    {
                        return null; // EL EMPLEADO NO EXISTE
                    }
                }
                else if (dt_eva_habilitadas.Rows.Count > 1)
                {
                    return null; // HAY MÁS DE UNA EVALUACIÓN HABILITADA
                }
                else
                {
                    return null; // NO HAY EVALUACIONES HABILITADAS
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ArrayList evaluacion_iniciada(int info_empleado, int evaluacion, string nombre_evaluacion, int id_ingreso_evaluacion)
        {
            try
            {
                ArrayList response = new ArrayList();

                Int32 ingreso_evaluacion = id_ingreso_evaluacion;

            PreguntasNoCargadas:

                // CARGA LAS PREGUNTAS ASOCIADAS A LA EVALUACIÓN
                DCL.Int_01_EVA_Evaluaciones cargar_preguntas = new DCL.Int_01_EVA_Evaluaciones();
                cargar_preguntas.Id_Evaluacion = evaluacion;
                cargar_preguntas.Id_Ingreso_Evaluacion = ingreso_evaluacion;
                DataTable dt_cargar_preguntas = Int_02_EVA_Preguntas_BRL.SelectTable(cargar_preguntas, 1);

                List<Dictionary<string, string>> preguntas_respuestas = new List<Dictionary<string, string>>();
                if (dt_cargar_preguntas.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_cargar_preguntas.Rows)
                    {
                        Dictionary<string, string> pregunta_respuesta = new Dictionary<string, string>();

                        // CARGA LOS VALORES ASOCIADOS A CADA PREGUNTA
                        DCL.Int_01_EVA_Evaluaciones cargar_respuestas = new DCL.Int_01_EVA_Evaluaciones();
                        cargar_respuestas.Id_Pregunta = Convert.ToInt32(dr["c04_id_pregunta"]);
                        DataTable dt_cargar_respuestas = Int_04_EVA_Respuestas_BRL.SelectTable(cargar_respuestas, 0);

                        pregunta_respuesta.Add("id_respuesta", dr["c04_id_respuesta"].ToString());
                        pregunta_respuesta.Add("id_pregunta", dr["c04_id_pregunta"].ToString());
                        pregunta_respuesta.Add("numero_pregunta", dr["c02_numero_pregunta"].ToString());
                        pregunta_respuesta.Add("pregunta", dr["c02_pregunta"].ToString());
                        pregunta_respuesta.Add("id_tipo_respuesta", dr["c02_id_tipo_respuesta"].ToString());
                        pregunta_respuesta.Add("competencia", dr["c07_competencia"].ToString());

                        string[] array_respuestas = new string[dt_cargar_respuestas.Rows.Count];
                        int i = 0;

                        //usa "." en vez de "," para no mostrar respuestas extra al usuario
                        foreach (DataRow dr2 in dt_cargar_respuestas.Rows)
                        {
                            string valorDecimal = dr2[1].ToString().Replace(',', '.');
                            array_respuestas[i] = dr2[0].ToString() + "-" + valorDecimal;
                            i++;
                        }

                        pregunta_respuesta.Add("criterios_respuesta", String.Join(",", array_respuestas));
                        preguntas_respuestas.Add(pregunta_respuesta);
                    }
                }
                else
                {
                    // CARGA LAS PREGUNTAS ASOCIADAS A LA EVALUACIÓN (segunda vez en caso de no encontrar preguntas)
                    DCL.Int_01_EVA_Evaluaciones cargar_preguntas2 = new DCL.Int_01_EVA_Evaluaciones();
                    cargar_preguntas2.Id_Evaluacion = evaluacion;
                    cargar_preguntas2.Id_Ingreso_Evaluacion = id_ingreso_evaluacion;
                    DataTable dt_cargar_preguntas2 = Int_02_EVA_Preguntas_BRL.SelectTable(cargar_preguntas2, 0);
                    goto PreguntasNoCargadas;
                }

                response.Add(nombre_evaluacion);
                response.Add(info_empleado);
                response.Add(ingreso_evaluacion);
                response.Add(evaluacion);
                response.Add(preguntas_respuestas);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private ArrayList evaluacion_nueva(Int32 info_empleado, Int32 evaluacion, string nombre_evaluacion) // PRIMERA VEZ QUE EL USAURIO ENTRA A UNA EVALUACIÓN
        {
            try
            {
                ArrayList response = new ArrayList();

                //CONSULTA LA SEDE Y EL GRUPO DEL COLABORADOR
                DCL.Int_01_EVA_Evaluaciones sede_grupo = new DCL.Int_01_EVA_Evaluaciones();
                sede_grupo.Id_Info_Empleado = info_empleado;
                DataTable dt_sede_grupo = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(sede_grupo, 2);

                if (dt_sede_grupo.Rows.Count > 0)
                {
                    //INSERTA UNA NUEVA EVALUACIÓN Y RETORNA EL ID_INGRESO_EVALUACION
                    DCL.Int_01_EVA_Evaluaciones insertar_nueva = new DCL.Int_01_EVA_Evaluaciones();
                    insertar_nueva.Id_Info_Empleado = info_empleado;
                    insertar_nueva.Id_Sede = Convert.ToInt32(dt_sede_grupo.Rows[0]["Id_Sede"]);
                    insertar_nueva.Id_Grupo_Empleado = Convert.ToInt32(dt_sede_grupo.Rows[0]["Id_GrupoEmpleados"]);
                    insertar_nueva.Id_Evaluacion = evaluacion;
                    DataTable dt_insertar_nueva = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(insertar_nueva, 3);

                    Int32 ingreso_evaluacion = Convert.ToInt32(dt_insertar_nueva.Rows[0][0]);

                PreguntasNoCargadas:

                    //CARGA LAS PREGUNTAS ASOCIADAS A LA EVALUACIÓN
                    DCL.Int_01_EVA_Evaluaciones cargar_preguntas = new DCL.Int_01_EVA_Evaluaciones();
                    cargar_preguntas.Id_Evaluacion = evaluacion;
                    cargar_preguntas.Id_Ingreso_Evaluacion = ingreso_evaluacion;
                    DataTable dt_cargar_preguntas = Int_02_EVA_Preguntas_BRL.SelectTable(cargar_preguntas, 0);

                    List<Dictionary<string, string>> preguntas_respuestas = new List<Dictionary<string, string>>();
                    if (dt_cargar_preguntas.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt_cargar_preguntas.Rows)
                        {
                            Dictionary<string, string> pregunta_respuesta = new Dictionary<string, string>();

                            //CARGA LOS VALORES ASOCIADOS A CADA PREGUNTA
                            DCL.Int_01_EVA_Evaluaciones cargar_respuestas = new DCL.Int_01_EVA_Evaluaciones();
                            cargar_respuestas.Id_Pregunta = Convert.ToInt32(dr["c04_id_pregunta"]);
                            DataTable dt_cargar_respuestas = Int_04_EVA_Respuestas_BRL.SelectTable(cargar_respuestas, 0);

                            pregunta_respuesta.Add("id_respuesta", dr["c04_id_respuesta"].ToString());
                            pregunta_respuesta.Add("id_pregunta", dr["c04_id_pregunta"].ToString());
                            pregunta_respuesta.Add("numero_pregunta", dr["c02_numero_pregunta"].ToString());
                            pregunta_respuesta.Add("pregunta", dr["c02_pregunta"].ToString());
                            pregunta_respuesta.Add("id_tipo_respuesta", dr["c02_id_tipo_respuesta"].ToString());
                            pregunta_respuesta.Add("competencia", dr["c07_competencia"].ToString());

                            string[] array_respuestas = new string[dt_cargar_respuestas.Rows.Count];
                            int i = 0;

                            foreach (DataRow dr2 in dt_cargar_respuestas.Rows)
                            {
                                string valorDecimal = dr2[1].ToString().Replace(',', '.');
                                array_respuestas[i] = dr2[0].ToString() + "-" + valorDecimal;
                                i++;
                            }

                            pregunta_respuesta.Add("criterios_respuesta", String.Join(",", array_respuestas));
                            preguntas_respuestas.Add(pregunta_respuesta);
                        }
                    }
                    else
                    {
                        goto PreguntasNoCargadas;
                    }

                    response.Add(nombre_evaluacion);
                    response.Add(info_empleado);
                    response.Add(ingreso_evaluacion);
                    response.Add(evaluacion);
                    response.Add(preguntas_respuestas);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public string responder_evaluacion_multiple(string ingreso_evaluacion, string evaluacion, ArrayList cantidad_checked, decimal suma_puntos, int Int_Id_UsuarioE)
        {
            try
            {
                // CONSULTA SI HAY EVALUACIONES HABILITADAS (ESTADO = 1)
                DCL.Int_01_EVA_Evaluaciones eva_habilitadas = new DCL.Int_01_EVA_Evaluaciones();
                DataTable dt_eva_habilitadas = Int_01_EVA_Evaluaciones_BRL.SelectTable(eva_habilitadas, 0);

                decimal nota_aprob = Convert.ToDecimal(dt_eva_habilitadas.Rows[0]["c01_puntos_aprobacion"]);//Obtiene la nota minima para aprobar
                decimal nota_user = Convert.ToDecimal(suma_puntos, CultureInfo.InvariantCulture);//Nota obtenida por el usuario
                nota_user = Math.Truncate(nota_user * 100) / 100; // Truncar a 2 decimales


                int max_intentos = Convert.ToInt32(dt_eva_habilitadas.Rows[0]["c01_intentos_permitidos"]);//Número de intentos permitidos

                string respuesta;
                Int32 id_pregunta = 0;

                for (int i = 0; i < cantidad_checked.Count; i++)
                {
                    respuesta = cantidad_checked[i].ToString();
                    int es_texto = respuesta.IndexOf("ENCUESTA-TEXTO");
                    int es_radio = respuesta.IndexOf("---");
                    int es_check = respuesta.IndexOf("___");

                    //VALIDO SI LA RESPUESTA ES TEXT-AREA, PREGUNTA DE TIPO ABIERTA
                    if (es_texto >= 0)
                    {
                        respuesta = (!String.IsNullOrEmpty(cantidad_checked[i].ToString().Substring(es_texto + 14)) ? cantidad_checked[i].ToString().Substring(es_texto + 14) : null);
                        id_pregunta = Convert.ToInt32(cantidad_checked[i].ToString().Substring(0, es_texto).Trim());
                    } 
                    //SI ES RADIO BUTTON, CAPTURO ID DE LA RESPUESTA Y RESPUESTA
                    else if (es_radio >= 0)
                    {
                        string respuesta_no_depurada = cantidad_checked[i].ToString().Substring(0, es_radio);
                        string[] respuesta_sin_nota = respuesta_no_depurada.Split('-');
                        respuesta = respuesta_sin_nota[0];
                        id_pregunta = Convert.ToInt32(cantidad_checked[i].ToString().Substring(es_radio).Replace("---", "").Trim());
                    }
                    //SI ES CHECK SE CAPTURA CADA CHECK POR PREGUNTA
                    else if (es_check >= 0)
                    {
                        bool primeravez = false;
                        string respuesta_check = "";
                        int id_pregunta_check = 0;

                        string respuesta_depurada = cantidad_checked[i].ToString().Substring(0, es_check);
                        string respuesta2 = respuesta.Substring(0, respuesta.Length - 1);
                        respuesta2 = respuesta2.Replace(",", "");
                        string[] respuesta_sin_nota = respuesta_depurada.Split('-');
                        string[] strArray = respuesta2.Split('\\');
                        string valor_actual = "0";


                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (!(j == strArray.Length - 1))
                            {
                                int index = strArray[j].IndexOf("___");
                                string valor = strArray[j].Replace("\\", "").Substring(index).Replace("___", "").Trim();

                                if (valor != valor_actual)
                                {
                                    if (primeravez)
                                    {
                                        DCL.Int_01_EVA_Evaluaciones respuesta_multiple_check = new Int_01_EVA_Evaluaciones();
                                        respuesta_multiple_check.Respuesta = respuesta_check;
                                        respuesta_multiple_check.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                                        respuesta_multiple_check.Id_Pregunta = Convert.ToInt32(id_pregunta_check);
                                        int actualiza_respuesta_binaria_check = Int_04_EVA_Respuestas_BRL.InsertOrUpdate(respuesta_multiple_check, 1);

                                        respuesta_check = "";
                                    }
                                    primeravez = true;

                                    valor_actual = valor;
                                    id_pregunta_check = Convert.ToInt32(valor);
                                    respuesta_check += "\\" + strArray[j].Replace("\\", "").Substring(0, index).Trim();
                                }
                                else
                                {
                                    respuesta_check += "\\" + strArray[j].Replace("\\", "").Substring(0, index).Trim();
                                }
                            }
                            else
                            {
                                int index = strArray[j].IndexOf("___");
                                if (valor_actual != strArray[j].Replace("\\", "").Substring(index).Replace("___", "").Trim())
                                {
                                    DCL.Int_01_EVA_Evaluaciones respuesta_multiple_check = new Int_01_EVA_Evaluaciones();
                                    respuesta_multiple_check.Respuesta = respuesta_check;
                                    respuesta_multiple_check.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                                    respuesta_multiple_check.Id_Pregunta = Convert.ToInt32(id_pregunta_check);
                                    int actualiza_respuesta_binaria_check = Int_04_EVA_Respuestas_BRL.InsertOrUpdate(respuesta_multiple_check, 1);

                                    respuesta_check = "";
                                }
                                int index2 = strArray[j].IndexOf("___");
                                string valor = strArray[j].Replace("\\", "").Substring(index2).Replace("___", "").Trim();
                                respuesta_check += "\\" + strArray[j].Replace("\\", "").Substring(0, index2).Trim();

                                DCL.Int_01_EVA_Evaluaciones respuesta_multiple_check2 = new Int_01_EVA_Evaluaciones();
                                respuesta_multiple_check2.Respuesta = respuesta_check;
                                respuesta_multiple_check2.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                                respuesta_multiple_check2.Id_Pregunta = Convert.ToInt32(valor);
                                int actualiza_respuesta_binaria_check2 = Int_04_EVA_Respuestas_BRL.InsertOrUpdate(respuesta_multiple_check2, 1);
                                goto salto;
                            }
                        }

                    }

                    //ACTUALIZA LA PREGUNTA CON SU RESPECTIVA RESPUESTA
                    DCL.Int_01_EVA_Evaluaciones respuesta_multiple = new Int_01_EVA_Evaluaciones();
                    respuesta_multiple.Respuesta = respuesta;
                    respuesta_multiple.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                    respuesta_multiple.Id_Pregunta = Convert.ToInt32(id_pregunta);
                    int actualiza_respuesta_binaria = Int_04_EVA_Respuestas_BRL.InsertOrUpdate(respuesta_multiple, 1);
                }
            salto:
                //CONSULTA SI HAY RESPUESTAS EN NULL PARA PRESENTAR ALERTA AL USUARIO
                DCL.Int_01_EVA_Evaluaciones respuestas_nulas = new Int_01_EVA_Evaluaciones();
                respuestas_nulas.Id_Evaluacion = Convert.ToInt32(evaluacion);
                respuestas_nulas.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                DataTable dt_respuestas_nulas = Int_04_EVA_Respuestas_BRL.SelectTable(respuestas_nulas, 2);

                if (dt_respuestas_nulas.Rows.Count > 0)
                {
                    return "¡Aún tiene preguntas pendientes por responder!";
                }
                else
                {
                    //COMPARO NOTA DE USUARIO CON EL NOTA APROBATORIA PARA DAR UN ESTADO DE APROBADO
                    bool aprobo_user = false;

                    if (nota_user >= nota_aprob)
                    {
                        aprobo_user = true;
                    }

                    //FINALIZA EL INTENTO CUANDO TODAS LAS PREGUNTAS FUERON RESPONDIDAS
                    DCL.Int_01_EVA_Evaluaciones finalizar_evaluacion = new DCL.Int_01_EVA_Evaluaciones();
                    finalizar_evaluacion.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                    int actualiza_finalizar_evaluacion = Int_06_EVA_Ingreso_Evaluacion_BRL.InsertOrUpdate(finalizar_evaluacion, 4);

                    DCL.Int_01_EVA_Evaluaciones resultados_evaluacion = new DCL.Int_01_EVA_Evaluaciones();
                    resultados_evaluacion.Id_Ingreso_Evaluacion = Convert.ToInt32(ingreso_evaluacion);
                    resultados_evaluacion.Resultado = nota_user;
                    resultados_evaluacion.Estado_Aprobacion = aprobo_user;
                    int insert_resultados_evaluacion = Int_11_EVA_Resultados_BRL.InsertOrUpdate(resultados_evaluacion, 0);

                    //COMPARO LA NOTA OBTENIDA POR EL USUARIO CONTRA LA NOTA APROBATORIA
                    if (nota_user >= nota_aprob)
                    {
                        return $"Evaluación aprobada y enviada con éxito. Su calificación es: {nota_user.ToString(CultureInfo.InvariantCulture)}."; //USUARIO APROBÓ LA EVALUACIÓN
                    }
                    else
                    {
                        //CONSULTA EL ID_INFO_EMPLEADO CON EL ID_USUARIO DE INTRANET
                        DCL.Int_01_EVA_Evaluaciones consulta_info_empleado = new DCL.Int_01_EVA_Evaluaciones();
                        consulta_info_empleado.Int_Id_Usuario = Convert.ToInt32(Int_Id_UsuarioE);
                        DataTable dt_consulta_info_empleado = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(consulta_info_empleado, 0);

                        int info_empleado = Convert.ToInt32(dt_consulta_info_empleado.Rows[0][0]);

                        //CONSULTO LA CANTIDAD DE INTENTOS DEL USUARIO
                        DCL.Int_01_EVA_Evaluaciones consulta_intentos_usuario = new DCL.Int_01_EVA_Evaluaciones();
                        consulta_intentos_usuario.Id_Info_Empleado = info_empleado;
                        consulta_intentos_usuario.Id_Evaluacion = Convert.ToInt32(evaluacion);
                        DataTable dt_numero_intento_usuario = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(consulta_intentos_usuario, 6);  

                        int intentos_user = Convert.ToInt32(dt_numero_intento_usuario.Rows[0][0]);

                        //COMPARO LA CANTIDAD DE INTENTOS QUE TIENE EL USUARIO CONTRA EL NÚMERO MÁXIMO DE INTENTOS PARA ESTA EVALUACIÓN
                        if (intentos_user < max_intentos)
                        {
                            //CONSULTA LA SEDE Y EL GRUPO DEL COLABORADOR
                            DCL.Int_01_EVA_Evaluaciones sede_grupo = new DCL.Int_01_EVA_Evaluaciones();
                            sede_grupo.Id_Info_Empleado = info_empleado;
                            DataTable dt_sede_grupo = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(sede_grupo, 2);

                            //INSERTA UN NUEVO INGRESO EVALUACIÓN PARA EL NUEVO INTENTO
                            DCL.Int_01_EVA_Evaluaciones insertar_nueva = new DCL.Int_01_EVA_Evaluaciones();
                            insertar_nueva.Id_Info_Empleado = info_empleado;
                            insertar_nueva.Id_Sede = Convert.ToInt32(dt_sede_grupo.Rows[0]["Id_Sede"]);
                            insertar_nueva.Id_Grupo_Empleado = Convert.ToInt32(dt_sede_grupo.Rows[0]["Id_GrupoEmpleados"]);
                            insertar_nueva.Id_Evaluacion = Convert.ToInt32(evaluacion);
                            insertar_nueva.Numero_Intento = intentos_user + 1;
                            DataTable dt_insertar_nueva = Int_06_EVA_Ingreso_Evaluacion_BRL.SelectTable(insertar_nueva, 7);

                            return $"1-Evaluación no aprobada. Su calificación es: {nota_user}. Le quedan {max_intentos - intentos_user} intentos. Por favor vuelva a intentar.";//USUARIO FALLÓ SU EVALUACIÓN, PERO TIENE UN NUEVO INTENTO
                        }
                        else
                        {
                            return $"2-Evaluación no aprobada. Su calificación es: {nota_user}, intentos agotados.";//USUARIO FALLÓ LA EVALUACIÓN Y SUPERÓ EL NÚMERO MÁXIMO DE INTENTOS
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Encuestas
        [WebMethod]
        public ArrayList validarEncuestas(string Int_Id_Usuario)
        {

            try
            {
                //CONSULTA SI HAY ENCUESTAS HABILITADAS (ESTADO = 1) - JGC
                DCL.Int_01_ENC_Encuestas enc_habilitadas = new DCL.Int_01_ENC_Encuestas();
                DataTable dt_enc_habilitadas = Int_01_ENC_Encuestas_BRL.SelectTable(enc_habilitadas, 0);
                if (dt_enc_habilitadas.Rows.Count == 1)
                {
                    //SI LA ENCUESTA ES SECTORIZADA (ID_TIPO_ENCUESTA = 2)
                    if (Convert.ToInt32(dt_enc_habilitadas.Rows[0]["c01_id_tipo_encuesta"]) == 2)
                    {
                        DCL.Int_01_ENC_Encuestas enc_sectorizada = new DCL.Int_01_ENC_Encuestas();
                        enc_sectorizada.Int_Id_Usuario = Convert.ToInt32(Int_Id_Usuario);
                        enc_sectorizada.Id_Encuesta = Convert.ToInt32(dt_enc_habilitadas.Rows[0]["c01_id_encuesta"]);
                        DataTable dt_enc_sectorizada = Int_06_ENC_Ingreso_Encuesta_BRL.SelectTable(enc_sectorizada, 5);
                        if (dt_enc_sectorizada.Rows.Count == 0)
                        {
                            //CUANDO LA ENCUESTA ESTÁ SECTORIZADA PERO EL USUARIO NO HACE PARTE DE ESA POBLACIÓN OBJETIVO NO LE MUESTRO LA ENCUESTA
                            return null;
                        }
                    }


                    //CONSULTA EL ID_INFO_EMPLEADO CON EL ID_USUARIO DE INTRANET - JGC
                    DCL.Int_01_ENC_Encuestas consulta_info_empleado = new DCL.Int_01_ENC_Encuestas();
                    consulta_info_empleado.Int_Id_Usuario = Convert.ToInt32(Int_Id_Usuario);
                    DataTable dt_consulta_info_empleado = Int_06_ENC_Ingreso_Encuesta_BRL.SelectTable(consulta_info_empleado, 0);

                    if (dt_consulta_info_empleado.Rows.Count > 0)
                    {
                        //CONSULTA SI EL COLABORADOR YA INICIÓ PREVIAMENTE UNA ENCUESTA
                        DCL.Int_01_ENC_Encuestas consulta_encuesta_iniciada = new DCL.Int_01_ENC_Encuestas();
                        consulta_encuesta_iniciada.Id_Info_Empleado = Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]);
                        consulta_encuesta_iniciada.Id_Encuesta = Convert.ToInt32(dt_enc_habilitadas.Rows[0]["c01_id_encuesta"]);
                        DataTable dt_consulta_encuesta_iniciada = Int_06_ENC_Ingreso_Encuesta_BRL.SelectTable(consulta_encuesta_iniciada, 1);

                        if (dt_consulta_encuesta_iniciada.Rows.Count > 0)
                        {
                            //CONSULTA SI EL COLABORADOR YA FINALIZÓ LA ENCUESTA QUE INICIÓ PREVIAMENTE
                            if (dt_consulta_encuesta_iniciada.Rows[0]["c06_finalizacion_encuesta"].ToString() == "True")
                            {
                                return null;//ENCUESTA FINALIZADA
                            }
                            else
                            {
                                return encuesta_iniciada(Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]), Convert.ToInt32(dt_enc_habilitadas.Rows[0]["c01_id_encuesta"]), dt_enc_habilitadas.Rows[0]["c01_nombre"].ToString(), Convert.ToInt32(dt_consulta_encuesta_iniciada.Rows[0]["c06_id_ingreso_encuesta"]));
                                //return "5"; //ENCUESTA INICIADA
                            }
                        }
                        else
                        {
                            return encuesta_nueva(Convert.ToInt32(dt_consulta_info_empleado.Rows[0]["Id_IE"]), Convert.ToInt32(dt_enc_habilitadas.Rows[0]["c01_id_encuesta"]), dt_enc_habilitadas.Rows[0]["c01_nombre"].ToString());
                            //return "4"; //NO HA INICIADO UNA ENCUESTA
                        }
                    }
                    else
                    {
                        return null; //EL EMPLEADO NO EXISTE
                    }
                }
                else if (dt_enc_habilitadas.Rows.Count > 1)
                {
                    return null; //HAY MÁS DE UNA ENCUESTA HABILITADA
                }
                else
                {
                    return null; //NO HAY ENCUESTAS HABILITADAS
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ArrayList encuesta_iniciada(Int32 info_empleado, Int32 encuesta, string nombre_encuesta, Int32 id_ingreso_encuesta)
        {
            try
            {
                ArrayList response = new ArrayList();

                Int32 ingreso_encuesta = id_ingreso_encuesta;

                PreguntasNoCargadas:

                //CARGA LAS PREGUNTAS ASOCIADAS A LA ENCUESTA
                DCL.Int_01_ENC_Encuestas cargar_preguntas = new DCL.Int_01_ENC_Encuestas();
                cargar_preguntas.Id_Encuesta = encuesta;
                cargar_preguntas.Id_Ingreso_Encuesta = ingreso_encuesta;
                DataTable dt_cargar_preguntas = Int_02_ENC_Preguntas_BRL.SelectTable(cargar_preguntas, 1);

                List<Dictionary<string, string>> preguntas_respuestas = new List<Dictionary<string, string>>();
                if (dt_cargar_preguntas.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_cargar_preguntas.Rows)
                    {
                        Dictionary<string, string> pregunta_respuesta = new Dictionary<string, string>();

                        //CARGA LOS VALORES ASOCIADOS A CADA PREGUNTA
                        DCL.Int_01_ENC_Encuestas cargar_respuestas = new DCL.Int_01_ENC_Encuestas();
                        cargar_respuestas.Id_Pregunta = Convert.ToInt32(dr["c04_id_pregunta"]);
                        DataTable dt_cargar_respuestas = Int_04_ENC_Respuestas_BRL.SelectTable(cargar_respuestas, 0);

                        pregunta_respuesta.Add("id_respuesta", dr["c04_id_respuesta"].ToString());
                        pregunta_respuesta.Add("id_pregunta", dr["c04_id_pregunta"].ToString());
                        pregunta_respuesta.Add("numero_pregunta", dr["c02_numero_pregunta"].ToString());
                        pregunta_respuesta.Add("pregunta", dr["c02_pregunta"].ToString());
                        pregunta_respuesta.Add("id_tipo_respuesta", dr["c02_id_tipo_respuesta"].ToString());
                        pregunta_respuesta.Add("competencia", dr["c07_competencia"].ToString());

                        string[] array_respuestas = new string[dt_cargar_respuestas.Rows.Count];
                        int i = 0;

                        foreach (DataRow dr2 in dt_cargar_respuestas.Rows)
                        {
                            array_respuestas[i] = dr2[0].ToString();
                            i++;
                        }

                        pregunta_respuesta.Add("criterios_respuesta", String.Join(",", array_respuestas));
                        preguntas_respuestas.Add(pregunta_respuesta);
                    }
                }
                else
                {
                    //CARGA LAS PREGUNTAS ASOCIADAS A LA ENCUESTA
                    DCL.Int_01_ENC_Encuestas cargar_preguntas2 = new DCL.Int_01_ENC_Encuestas();
                    cargar_preguntas2.Id_Encuesta = encuesta;
                    cargar_preguntas2.Id_Ingreso_Encuesta = ingreso_encuesta;
                    DataTable dt_cargar_preguntas2 = Int_02_ENC_Preguntas_BRL.SelectTable(cargar_preguntas2, 0);
                    goto PreguntasNoCargadas;
                }

                response.Add(nombre_encuesta);
                response.Add(info_empleado);
                response.Add(ingreso_encuesta);
                response.Add(encuesta);
                response.Add(preguntas_respuestas);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ArrayList encuesta_nueva(Int32 info_empleado, Int32 encuesta, string nombre_encuesta)
        {
            try
            {
                ArrayList response = new ArrayList();

                //CONSULTA LA SEDE Y EL GRUPO DEL COLABORADOR
                DCL.Int_01_ENC_Encuestas sede_grupo = new DCL.Int_01_ENC_Encuestas();
                sede_grupo.Id_Info_Empleado = info_empleado;
                DataTable dt_sede_grupo = Int_06_ENC_Ingreso_Encuesta_BRL.SelectTable(sede_grupo, 2);

                if (dt_sede_grupo.Rows.Count > 0)
                {
                    //INSERTA UNA NUEVA ENCUESTA Y RETORNA EL ID_INGRESO_ENCUESTA
                    DCL.Int_01_ENC_Encuestas insertar_nueva = new DCL.Int_01_ENC_Encuestas();
                    insertar_nueva.Id_Info_Empleado = info_empleado;
                    insertar_nueva.Id_Sede = Convert.ToInt32(dt_sede_grupo.Rows[0]["Id_Sede"]);
                    insertar_nueva.Id_Grupo_Empleado = Convert.ToInt32(dt_sede_grupo.Rows[0]["Id_GrupoEmpleados"]);
                    insertar_nueva.Id_Encuesta = encuesta;
                    DataTable dt_insertar_nueva = Int_06_ENC_Ingreso_Encuesta_BRL.SelectTable(insertar_nueva, 3);

                    Int32 ingreso_encuesta = Convert.ToInt32(dt_insertar_nueva.Rows[0][0]);

                    PreguntasNoCargadas:

                    //CARGA LAS PREGUNTAS ASOCIADAS A LA ENCUESTA
                    DCL.Int_01_ENC_Encuestas cargar_preguntas = new DCL.Int_01_ENC_Encuestas();
                    cargar_preguntas.Id_Encuesta = encuesta;
                    cargar_preguntas.Id_Ingreso_Encuesta = ingreso_encuesta;
                    DataTable dt_cargar_preguntas = Int_02_ENC_Preguntas_BRL.SelectTable(cargar_preguntas, 0);


                    List<Dictionary<string, string>> preguntas_respuestas = new List<Dictionary<string, string>>();
                    if (dt_cargar_preguntas.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt_cargar_preguntas.Rows)
                        {
                            Dictionary<string, string> pregunta_respuesta = new Dictionary<string, string>();

                            //CARGA LOS VALORES ASOCIADOS A CADA PREGUNTA
                            DCL.Int_01_ENC_Encuestas cargar_respuestas = new DCL.Int_01_ENC_Encuestas();
                            cargar_respuestas.Id_Pregunta = Convert.ToInt32(dr["c04_id_pregunta"]);
                            DataTable dt_cargar_respuestas = Int_04_ENC_Respuestas_BRL.SelectTable(cargar_respuestas, 0);

                            pregunta_respuesta.Add("id_respuesta", dr["c04_id_respuesta"].ToString());
                            pregunta_respuesta.Add("id_pregunta", dr["c04_id_pregunta"].ToString());
                            pregunta_respuesta.Add("numero_pregunta", dr["c02_numero_pregunta"].ToString());
                            pregunta_respuesta.Add("pregunta", dr["c02_pregunta"].ToString());
                            pregunta_respuesta.Add("id_tipo_respuesta", dr["c02_id_tipo_respuesta"].ToString());
                            pregunta_respuesta.Add("competencia", dr["c07_competencia"].ToString());

                            string[] array_respuestas = new string[dt_cargar_respuestas.Rows.Count];
                            int i = 0;

                            foreach (DataRow dr2 in dt_cargar_respuestas.Rows)
                            {
                                array_respuestas[i] = dr2[0].ToString();
                                i++;
                            }

                            pregunta_respuesta.Add("criterios_respuesta", String.Join(",", array_respuestas));
                            preguntas_respuestas.Add(pregunta_respuesta);
                        }
                    }
                    else
                    {
                        goto PreguntasNoCargadas;
                    }

                    response.Add(nombre_encuesta);
                    response.Add(info_empleado);
                    response.Add(ingreso_encuesta);
                    response.Add(encuesta);
                    response.Add(preguntas_respuestas);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public int responder_encuesta_binaria(string respuesta, string id_respuesta, string ingreso_encuesta)
        {
            try
            {
                //ACTUALIZA LA PREGUNTA CON SU RESPECTIVA RESPUESTA
                DCL.Int_01_ENC_Encuestas respuesta_binaria = new Int_01_ENC_Encuestas();
                respuesta_binaria.Respuesta = respuesta;
                respuesta_binaria.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                respuesta_binaria.Id_Pregunta = Convert.ToInt32(id_respuesta);
                int actualiza_respuesta_binaria = Int_04_ENC_Respuestas_BRL.InsertOrUpdate(respuesta_binaria, 1);

                if (actualiza_respuesta_binaria == 1)
                {
                    DCL.Int_01_ENC_Encuestas finalizar_encuesta = new DCL.Int_01_ENC_Encuestas();
                    finalizar_encuesta.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                    int actualiza_finalizar_encuesta = Int_06_ENC_Ingreso_Encuesta_BRL.InsertOrUpdate(finalizar_encuesta, 4);
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public int responder_encuesta_multiple(string ingreso_encuesta, string encuesta, ArrayList cantidad_checked, ArrayList id_preguntas)
        {
            try
            {
                string respuesta;
                Int32 id_pregunta = 0;

                for (int i = 0; i < cantidad_checked.Count; i++)
                {
                    respuesta = cantidad_checked[i].ToString();
                    int es_texto = respuesta.IndexOf("ENCUESTA-TEXTO");
                    int es_radio = respuesta.IndexOf("---");
                    int es_check = respuesta.IndexOf("___");

                    //VALIDO SI LA RESPUESTA ES TEXT-AREA, NUMERO O DATE PARA CAPTURAR ID DE LA RESPUESTA Y RESPUESTA 
                    if (es_texto >= 0)
                    {
                        respuesta = (!String.IsNullOrEmpty(cantidad_checked[i].ToString().Substring(es_texto + 14)) ? cantidad_checked[i].ToString().Substring(es_texto + 14) : null);
                        id_pregunta = Convert.ToInt32(cantidad_checked[i].ToString().Substring(0, es_texto).Trim());
                    }
                    //SI ES RADIO BUTTON, CAPTURO ID DE LA RESPUESTA Y RESPUESTA
                    else if (es_radio >= 0)
                    {
                        respuesta = cantidad_checked[i].ToString().Substring(0, es_radio);
                        id_pregunta = Convert.ToInt32(cantidad_checked[i].ToString().Substring(es_radio).Replace("---", "").Trim());
                    }
                    //SI ES CHECK SE CAPTURA CADA CHECK POR PREGUNTA
                    else if (es_check >= 0)
                    {
                        bool primeravez = false;
                        string respuesta_check = "";
                        int id_pregunta_check = 0;

                        string respuesta2 = respuesta.Substring(0, respuesta.Length - 1);
                        respuesta2 = respuesta2.Replace(",", "");
                        string[] strArray = respuesta2.Split('\\');
                        string valor_actual = "0";


                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (!(j == strArray.Length - 1))
                            {
                                int index = strArray[j].IndexOf("___");
                                string valor = strArray[j].Replace("\\", "").Substring(index).Replace("___", "").Trim();

                                if (valor != valor_actual)
                                {
                                    if (primeravez)
                                    {
                                        DCL.Int_01_ENC_Encuestas respuesta_multiple_check = new Int_01_ENC_Encuestas();
                                        respuesta_multiple_check.Respuesta = respuesta_check;
                                        respuesta_multiple_check.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                                        respuesta_multiple_check.Id_Pregunta = Convert.ToInt32(id_pregunta_check);
                                        int actualiza_respuesta_binaria_check = Int_04_ENC_Respuestas_BRL.InsertOrUpdate(respuesta_multiple_check, 1);

                                        respuesta_check = "";
                                    }
                                    primeravez = true;

                                    valor_actual = valor;
                                    id_pregunta_check = Convert.ToInt32(valor);
                                    respuesta_check += "\\" + strArray[j].Replace("\\", "").Substring(0, index).Trim();
                                }
                                else
                                {
                                    respuesta_check += "\\" + strArray[j].Replace("\\", "").Substring(0, index).Trim();
                                }
                            }
                            else
                            {
                                int index = strArray[j].IndexOf("___");
                                if (valor_actual != strArray[j].Replace("\\", "").Substring(index).Replace("___", "").Trim())
                                {
                                    DCL.Int_01_ENC_Encuestas respuesta_multiple_check = new Int_01_ENC_Encuestas();
                                    respuesta_multiple_check.Respuesta = respuesta_check;
                                    respuesta_multiple_check.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                                    respuesta_multiple_check.Id_Pregunta = Convert.ToInt32(id_pregunta_check);
                                    int actualiza_respuesta_binaria_check = Int_04_ENC_Respuestas_BRL.InsertOrUpdate(respuesta_multiple_check, 1);

                                    respuesta_check = "";
                                }
                                int index2 = strArray[j].IndexOf("___");
                                string valor = strArray[j].Replace("\\", "").Substring(index2).Replace("___", "").Trim();
                                respuesta_check += "\\" + strArray[j].Replace("\\", "").Substring(0, index2).Trim();

                                DCL.Int_01_ENC_Encuestas respuesta_multiple_check2 = new Int_01_ENC_Encuestas();
                                respuesta_multiple_check2.Respuesta = respuesta_check;
                                respuesta_multiple_check2.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                                respuesta_multiple_check2.Id_Pregunta = Convert.ToInt32(valor);
                                int actualiza_respuesta_binaria_check2 = Int_04_ENC_Respuestas_BRL.InsertOrUpdate(respuesta_multiple_check2, 1);
                                goto salto;
                            }
                        }

                    }

                    //ACTUALIZA LA PREGUNTA CON SU RESPECTIVA RESPUESTA
                    DCL.Int_01_ENC_Encuestas respuesta_multiple = new Int_01_ENC_Encuestas();
                    respuesta_multiple.Respuesta = respuesta;
                    respuesta_multiple.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                    respuesta_multiple.Id_Pregunta = Convert.ToInt32(id_pregunta);
                    int actualiza_respuesta_binaria = Int_04_ENC_Respuestas_BRL.InsertOrUpdate(respuesta_multiple, 1);

                    // !!!!!! IMPORTANTE - INICIO CODIGO --TEMPORAL-- PARA ACTUALIZAR DATOS PERSONALES !!!!!!

                    // ACTUALIZA EN "INFO_EMPLEADO" LA PREGUNTA DE ACTUALIZACIÓN DE DATOS PERSONALES CON SU RESPECTIVA RESPUESTA
                    // Mapeo: busca en el array id_preguntas la correspondencia para datos personales

                    // Busca el id_respuesta correspondiente en el array id_preguntas
                    // El array tiene estructura "id_respuesta-id_pregunta"
                    int id_pregunta_datapersonal = 0;
                    string id_respuesta_buscar = id_pregunta.ToString(); // El id_pregunta actual se usa como valor de búsqueda

                    // Recorrer el array id_preguntas para encontrar la correspondencia
                    for (int k = 0; k < id_preguntas.Count; k++)
                    {
                        string elemento = id_preguntas[k].ToString();
                        string[] partes = elemento.Split('-'); // Separa "id_respuesta-id_pregunta"

                        if (partes.Length == 2)
                        {
                            string id_respuesta_array = partes[0].Trim(); // Parte antes del '-'
                            string id_pregunta_array = partes[1].Trim();  // Parte después del '-'

                            // Si encontramos la correspondencia (id_pregunta_array == id_pregunta actual)
                            // tomamos el id_respuesta_array como el nuevo ID para datos personales
                            if (id_pregunta_array == id_respuesta_buscar)
                            {
                                id_pregunta_datapersonal = Convert.ToInt32(id_respuesta_array);
                                break; // Sale del bucle al encontrar la correspondencia
                            }
                        }
                    }

                    // Solo actualiza los datos personales si se encontró una correspondencia válida
                    if (id_pregunta_datapersonal > 0)
                    {
                        DCL.Int_01_ENC_Encuestas respuesta_datapersonal = new Int_01_ENC_Encuestas();
                        respuesta_datapersonal.Respuesta = respuesta;
                        respuesta_datapersonal.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                        respuesta_datapersonal.Id_Pregunta = id_pregunta_datapersonal; // Se usa el ID mapeado desde el array
                        int actualiza_respuesta_datapersonal = Int_04_ENC_Respuestas_BRL.InsertOrUpdate(respuesta_datapersonal, 3);
                    }

                    // !!!!!! IMPORTANTE - FIN CODIGO --TEMPORAL-- PARA ACTUALIZAR DATOS PERSONALES !!!!!!

                }
            salto:
                //CONSULTA SI HAY RESPUESTAS EN NULL PARA PRESENTAR ALERTA AL USUARIO
                DCL.Int_01_ENC_Encuestas respuestas_nulas = new Int_01_ENC_Encuestas();
                respuestas_nulas.Id_Encuesta = Convert.ToInt32(encuesta);
                respuestas_nulas.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                DataTable dt_respuestas_nulas = Int_04_ENC_Respuestas_BRL.SelectTable(respuestas_nulas, 2);

                if (dt_respuestas_nulas.Rows.Count > 0)
                {
                    return 0;
                }
                else
                {
                    //FINALIZA LA ENCUESTA CUANDO TODAS LAS PREGUNTAS FUERON RESPONDIDAS
                    DCL.Int_01_ENC_Encuestas finalizar_encuesta = new DCL.Int_01_ENC_Encuestas();
                    finalizar_encuesta.Id_Ingreso_Encuesta = Convert.ToInt32(ingreso_encuesta);
                    int actualiza_finalizar_encuesta = Int_06_ENC_Ingreso_Encuesta_BRL.InsertOrUpdate(finalizar_encuesta, 4);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
