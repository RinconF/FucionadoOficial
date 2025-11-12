using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BRL;
using System.Security.Cryptography;
using System.Text;
using SimpleImpersonation;
using System.IO;
using System.Web.Script.Services;
using RestSharp;
using System.Configuration;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Intranet_3._0.Interna;

namespace Intranet_3._0.Vistas.V_Perfil
{
    /// <summary>
    /// Descripción breve de WebService_V_Perfil
    /// </summary>
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService_V_Perfil : System.Web.Services.WebService
    {
        string pathLog = "";
        [WebMethod]
        public List<string[]> Cargar_Datos_Usuario(string Id_Usuario)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                List<string[]> list = new List<string[]>();

                DataTable dt;
                DCL.Info_Empleado obj = new DCL.Info_Empleado();
                obj.Id_IE = Convert.ToInt32(Id_Usuario);
                dt = Info_Empleado_BRL.SelectTable(obj, 9);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[3];
                        array[0] = row["Id_Ciudad"].ToString();
                        array[1] = row["Id_LocalidadResidencia"].ToString();
                        array[2] = row["Id_BarrioResidencia"].ToString();

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
        public string Actualizar_Informacion_Colaborador(
            string N_Identificacion,
            string Celular,
            string Email_Personal,
            string Dir_Residencia,
            string Id_EstadoCivil,
            string Drop_estrato,
            string Drop_etnico,
            string Drop_Depart,
            string Drop_Depart_id,
            string Drop_Ciudad,
            string Drop_Localidad,
            string Drop_Barrio,
            string Drop_Genero,
            string Drop_Vivienda,
            string Hobbie
        )
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                if (Drop_Depart == "CUNDINAMARCA")
                {
                    Drop_Localidad = "0";
                    Drop_Barrio = "0";
                }

                DCL.Info_Empleado obj = new DCL.Info_Empleado();
                obj.N_Identificacion = N_Identificacion;
                obj.Celular = Celular;
                obj.Email_Personal = Email_Personal;
                obj.Dir_Residencia = Dir_Residencia;
                obj.Id_EstadoCivil = Convert.ToInt32(Id_EstadoCivil);
                obj.Id_Dpto = Convert.ToInt32(Drop_Depart_id);
                obj.Id_CiudadResidencia = Convert.ToInt32(Drop_Ciudad);
                if (!Drop_Localidad.Equals("null") && Drop_Localidad != "")
                {
                    obj.Id_LocalidadResidencia = Convert.ToInt32(Drop_Localidad);
                }
                if (Drop_Barrio !="" && !Drop_Barrio.Equals("null"))
                {
                    obj.Id_BarrioResidencia = Convert.ToInt32(Drop_Barrio);
                }

                obj.Id_Sexo = Convert.ToInt32(Drop_Genero);
                Info_Empleado_BRL.InsertarOrUpdate(obj, 0);

                DataTable dt;
                DCL.Info_Empleado obj_ = new DCL.Info_Empleado();
                obj_.Id_IE = Convert.ToInt32(N_Identificacion);
                dt = Info_Empleado_BRL.SelectTable(obj_, 4);
                if (dt.Rows.Count > 0)
                {
                    DCL.Info_Empleado _obj = new DCL.Info_Empleado();
                    _obj.Id_IE = Convert.ToInt32(N_Identificacion);
                    _obj.Id_TipoDoc = Convert.ToInt32(Drop_estrato);
                    _obj.Id_Dpto = Convert.ToInt32(Drop_etnico);
                    _obj.Id_CiudadResidencia = Convert.ToInt32(Drop_Vivienda);
                    _obj.Observaciones = Hobbie;
                    Info_Empleado_BRL.InsertarOrUpdate(_obj, 6);
                }
                else
                {
                    DCL.Info_Empleado _obj = new DCL.Info_Empleado();
                    _obj.Id_IE = Convert.ToInt32(N_Identificacion);
                    _obj.Id_TipoDoc = Convert.ToInt32(Drop_estrato);
                    _obj.Id_Dpto = Convert.ToInt32(Drop_etnico);
                    _obj.Id_CiudadResidencia = Convert.ToInt32(Drop_Vivienda);
                    _obj.Observaciones = Hobbie;
                    Info_Empleado_BRL.InsertarOrUpdate(_obj, 5);
                }

                string retorno = "1";
                return retorno;
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}", pathLog);
                string retorno = "0";
                return retorno;
            }
        }

        [WebMethod]
        public string Actualizar_Contacto_Emergencia(
            string Id_Usuario,
            string Nombre,
            string Apellido,
            string Id_Parentesco,
            string Direccion,
            string Celular
        )
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                DataTable dt;
                DCL.Info_Empleado obj = new DCL.Info_Empleado();
                obj.Id_IE = Convert.ToInt32(Id_Usuario);
                dt = Info_Empleado_BRL.SelectTable(obj, 2);
                if (dt.Rows.Count > 0)
                {
                    DCL.Info_Empleado obj_ = new DCL.Info_Empleado();
                    obj_.Email_Personal = Nombre;
                    obj_.Email_Empresa = Apellido;
                    obj_.Id_Sexo = Convert.ToInt32(Id_Parentesco);
                    obj_.Ruta_HV = Direccion;
                    obj_.Celular = Celular;
                    obj_.N_Identificacion = Id_Usuario;
                    Info_Empleado_BRL.InsertarOrUpdate(obj_, 1);
                }
                else
                {
                    DCL.Info_Empleado obj_ = new DCL.Info_Empleado();
                    obj_.Email_Personal = Nombre;
                    obj_.Email_Empresa = Apellido;
                    obj_.Id_Sexo = Convert.ToInt32(Id_Parentesco);
                    obj_.Ruta_HV = Direccion;
                    obj_.Celular = Celular;
                    obj_.N_Identificacion = Id_Usuario;
                    Info_Empleado_BRL.InsertarOrUpdate(obj_, 3);
                }

                string retorno = "1";
                return retorno;
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}", pathLog);
                string retorno = "0";
                return retorno;
            }
        }

        [WebMethod]
        public string Actualizar_Contraseña(
            string txt_actual_pass,
            string txt_nueva_pass,
            string txt_conf_pass,
            string Id_Usuario
        )
            
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string retornar = "";

                if (
                    !String.IsNullOrEmpty(txt_actual_pass.Trim()) &&
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
                        string encrytar = GetMD5(txt_actual_pass.Trim());
                        string encrytar_nueva = GetMD5(txt_nueva_pass.Trim());
                        if (dt.Rows[0]["Contraseña"].ToString() == encrytar)
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
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}", pathLog);
                string retornar = "error";
                return retornar;
            }
        }

        [WebMethod]

        public List<string[]> Cargar_Datos_Familiar(string Id_Usuario)
        {
            string ipServer = "";

            AG_Utils utilidades = new AG_Utils();
            try
            {
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                
                pathLog = Server.MapPath(@"~/logs");

                List<string[]> list = new List<string[]>();

                DataTable dt;
                DCL.Int_Nucleo_Familiar obj = new DCL.Int_Nucleo_Familiar();
                obj.Id_IE = Convert.ToInt32(Id_Usuario);
                dt = Int_Nucleo_Familiar_BRL.SelectTable(obj, 0);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string rutaCompletaAnexo_RC = row["Anexo_Reg_Civil"].ToString();
                        string rutaCompletaAnexo_CE = row["Anexo_Dos"].ToString();
                        string localTempAnexo_RC = "";
                        string localTempAnexo_CE = "";
                        bool bl_ExisteRC_Remoto = false;
                        bool bl_ExisteRC_Local = false;
                        bool bl_ExisteCE_Remoto = false;
                        bool bl_ExisteCE_Local = false;
                        bool conectaAdjuntos = false;

                        if (rutaCompletaAnexo_RC != "Ninguno")
                        {
                            string archivoRemotoAnexo_Reg_Civil = row["Anexo_Reg_Civil"].ToString();
                            var anexoFotoAnexo_Reg_Civil = archivoRemotoAnexo_Reg_Civil.Split('\\');
                            List<String> arrayAnexo_Reg_Civil = new List<string>(anexoFotoAnexo_Reg_Civil);
                            int indexAnexo_Reg_Civil = arrayAnexo_Reg_Civil.FindIndex(x => x == ambiente);
                            int[] indexArrayAnexo_Reg_Civil = new int[indexAnexo_Reg_Civil];
                            var removerLista = new List<int>(indexArrayAnexo_Reg_Civil);
                            for (int i = 0; i < removerLista.Count; i++)
                            {
                                arrayAnexo_Reg_Civil.RemoveAt(0);
                            }
                            rutaCompletaAnexo_RC = String.Join("\\", arrayAnexo_Reg_Civil);
                            localTempAnexo_RC = $@"{pathServer}{rutaCompletaAnexo_RC}";
                            List<string> rutalocalTemp = localTempAnexo_RC.Split('\\').ToList();
                            int indexRemota = rutalocalTemp.Count;
                            rutalocalTemp.RemoveAt(--indexRemota);
                            string rutaLocalCompleta = String.Join(@"\", rutalocalTemp) + "\\";
                            conectaAdjuntos = utilidades.Ping(anexoFotoAnexo_Reg_Civil[2]);
                            if (utilidades.impersonateValidUser())
                            {
                                if (conectaAdjuntos)
                                {
                                    bl_ExisteRC_Local = File.Exists(localTempAnexo_RC);
                                    bl_ExisteRC_Remoto = File.Exists(archivoRemotoAnexo_Reg_Civil);
                                    if (!bl_ExisteRC_Local && bl_ExisteRC_Remoto)
                                    {
                                        if (!Directory.Exists(rutaLocalCompleta))
                                        {
                                            Directory.CreateDirectory(rutaLocalCompleta);
                                        }
                                        File.Copy(archivoRemotoAnexo_Reg_Civil, $@"{localTempAnexo_RC}", true);
                                        bl_ExisteRC_Local = true;
                                    }


                                    else if (bl_ExisteRC_Local && !bl_ExisteRC_Remoto)
                                    {
                                        File.Copy(localTempAnexo_RC, $@"{archivoRemotoAnexo_Reg_Civil}", true);
                                    }
                                    else if (bl_ExisteRC_Local && bl_ExisteRC_Remoto)
                                    {
                                        //FileInfo TamanioArchivoRemoto = new FileInfo(archivoRemotoAnexo_Reg_Civil);
                                        //FileInfo TamanioArchivoLocal = new FileInfo(localTempAnexo_RC);
                                        //double tamanioArchivoRemoto = TamanioArchivoRemoto.Length;
                                        //double hashArchivoRemoto = TamanioArchivoRemoto.GetHashCode();
                                        //double tamanioArchivoLocal = TamanioArchivoRemoto.Length;
                                        //double hashArchivoLocal = TamanioArchivoRemoto.GetHashCode();
                                        //if (tamanioArchivoRemoto != tamanioArchivoLocal || hashArchivoRemoto != hashArchivoLocal)
                                        //{
                                            File.Copy(archivoRemotoAnexo_Reg_Civil, $@"{localTempAnexo_RC}", true);
                                        //}
                                    }
                                    else if (!bl_ExisteRC_Local && !bl_ExisteRC_Remoto)
                                    {
                                        utilidades.logError($"{DateTime.Now}\nNo existen archivos locales ni remotos:\nNo se puede obtener documento {rutaCompletaAnexo_RC}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}. \nUsuario:  {Id_Usuario}", pathLog);
                                    }

                                }
                                else
                                {
                                    utilidades.logError($"{DateTime.Now}\nError al intentar conectarse al servidor: {ipServer}. \nNo se puede obtener documento {rutaCompletaAnexo_RC}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}. \nUsuario:  {Id_Usuario}", pathLog);
                                }
                                utilidades.undoImpersonation();
                            }
                            else
                            {
                                utilidades.logError($"{DateTime.Now}\nError al intentar acceder a archivos en: {ipServer}. \nACCESO DENEGADO. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}. \nUsuario:  {Id_Usuario}", pathLog);

                            }


                        }

                        if (rutaCompletaAnexo_CE != "Ninguno")
                        {
                            string archivoRemotoAnexo_Dos = row["Anexo_Dos"].ToString();
                            var anexoFotoAnexo_Dos = archivoRemotoAnexo_Dos.Split('\\');
                            List<String> arrayAnexo_Dos = new List<string>(anexoFotoAnexo_Dos);
                            int indexAnexo_Dos = arrayAnexo_Dos.FindIndex(x => x == ambiente);
                            int[] indexArrayAnexo_Dos = new int[indexAnexo_Dos];
                            var removerListaAnexo_Dos = new List<int>(indexArrayAnexo_Dos);
                            for (int i = 0; i < removerListaAnexo_Dos.Count; i++)
                            {
                                arrayAnexo_Dos.RemoveAt(0);
                            }
                            rutaCompletaAnexo_CE = String.Join("\\", arrayAnexo_Dos);

                            localTempAnexo_CE = $@"{pathServer}{rutaCompletaAnexo_CE}";
                            List<string> rutalocalTempAnexo_Dos = localTempAnexo_CE.Split('\\').ToList();
                            int indexRemotaAnexo_Dos = rutalocalTempAnexo_Dos.Count;
                            rutalocalTempAnexo_Dos.RemoveAt(--indexRemotaAnexo_Dos);
                            string rutaLocalCompletaAnexo_Dos = String.Join(@"\", rutalocalTempAnexo_Dos) + "\\";
                            conectaAdjuntos = utilidades.Ping(anexoFotoAnexo_Dos[2]);
                            if (utilidades.impersonateValidUser())
                            {
                                if (conectaAdjuntos)
                                {
                                    bl_ExisteCE_Remoto = File.Exists(archivoRemotoAnexo_Dos);
                                    bl_ExisteCE_Local = File.Exists(localTempAnexo_CE);
                                    if (!bl_ExisteCE_Local && bl_ExisteCE_Remoto)
                                    {
                                        if (!Directory.Exists(rutaLocalCompletaAnexo_Dos))
                                        {
                                            Directory.CreateDirectory(rutaLocalCompletaAnexo_Dos);
                                        }
                                        File.Copy(archivoRemotoAnexo_Dos, $@"{localTempAnexo_CE}", true);
                                        bl_ExisteCE_Local = true;
                                    }

                                    else if (bl_ExisteCE_Local && !bl_ExisteCE_Remoto)
                                    {
                                        File.Copy(localTempAnexo_CE, $@"{archivoRemotoAnexo_Dos}", true);
                                    }
                                    else if (bl_ExisteCE_Local && bl_ExisteCE_Remoto)
                                    {
                                        //FileInfo InfoArchivoRemoto = new FileInfo(archivoRemotoAnexo_Dos);
                                        //FileInfo InfoArchivoLocal = new FileInfo(localTempAnexo_CE);
                                        //double tamanioArchivoRemoto = InfoArchivoRemoto.Length;
                                        //double hashArchivoRemoto = InfoArchivoRemoto.GetHashCode();
                                        //double tamanioArchivoLocal = InfoArchivoRemoto.Length;
                                        //double hashArchivoLocal = InfoArchivoRemoto.GetHashCode();
                                        //string nombreArchivo = InfoArchivoRemoto.Name;
                                        //if (tamanioArchivoRemoto != tamanioArchivoLocal || hashArchivoRemoto != hashArchivoLocal)
                                        //{
                                            File.Copy(archivoRemotoAnexo_Dos, $@"{localTempAnexo_CE}", true);
                                        //}
                                    }
                                    else if (!bl_ExisteCE_Local && !bl_ExisteCE_Remoto)
                                    {
                                        utilidades.logError($"{DateTime.Now}\nNo existen archivos locales ni remotos:\nNo se puede obtener documento {archivoRemotoAnexo_Dos}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}. \nUsuario:  {Id_Usuario}", pathLog);
                                    }

                                }
                                else
                                {
                                    utilidades.logError($"{DateTime.Now}\nError al intentar conectarse al servidor: {ipServer}. \nNo se pueden obtener documento {archivoRemotoAnexo_Dos}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}. \nUsuario:  {Id_Usuario}", pathLog);
                                }
                                utilidades.undoImpersonation();
                            }
                            else
                            {
                                utilidades.logError($"{DateTime.Now}\nError al intentar acceder a archivos en: {ipServer}. \nACCESO DENEGADO. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}. \nUsuario:  {Id_Usuario}", pathLog);
                            }
                        }

                        string[] array = new string[16];
                        array[0] = row["Id_Familiar"].ToString();
                        array[1] = row["Id_IE"].ToString();
                        array[2] = row["Nombre_Familiar"].ToString();
                        array[3] = row["Id_Genero"].ToString();
                        array[4] = row["Id_Tipo_Doc"].ToString();
                        array[5] = row["Identificacion"].ToString();
                        array[6] = row["Edad"].ToString();
                        array[7] = row["Celular"].ToString();
                        array[8] = row["Id_Parentesco"].ToString();
                        array[9] = row["Id_Escolaridad"].ToString();
                        array[10] = row["Id_Ocupa"].ToString();
                        array[11] = row["Discapacidad"].ToString();
                        if (bl_ExisteRC_Local && !rutaCompletaAnexo_RC.Equals("Ninguno"))
                        {
                            array[12] = $@"../../Imagenes/{rutaCompletaAnexo_RC.Replace('\\', '/')}";
                        }
                        else
                        {
                            array[12] = "Ninguno";
                        }
                        if (bl_ExisteCE_Local && !rutaCompletaAnexo_CE.Equals("Ninguno"))
                        {
                            array[13] = $@"../../Imagenes/{rutaCompletaAnexo_CE.Replace('\\', '/')}";
                        }
                        else
                        {
                            array[13] = "Ninguno";
                        }
                        array[14] = row["Apellido"].ToString();
                        array[15] = row["Fecha_Nacimineto"].ToString();

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
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}\nUsuario:{Id_Usuario}", pathLog);
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);

                return list;
            }
        }

        [WebMethod(enableSession: true)]
        public List<string[]> Cargar_Datos_Familiar_Modal(string Id_Familiar)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                List<string[]> list = new List<string[]>();

                DataTable dt;
                DCL.Int_Nucleo_Familiar obj = new DCL.Int_Nucleo_Familiar();
                obj.Id_Familiar = Convert.ToInt32(Id_Familiar);
                dt = Int_Nucleo_Familiar_BRL.SelectTable(obj, 10);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[16];
                        array[0] = row["Id_Familiar"].ToString();
                        array[1] = row["Id_IE"].ToString();
                        array[2] = row["Nombre_Familiar"].ToString();
                        array[3] = row["Id_Genero"].ToString();
                        array[4] = row["Id_Tipo_Doc"].ToString();
                        array[5] = row["Identificacion"].ToString();
                        array[6] = row["Fecha_Nacimineto"].ToString();
                        array[7] = row["Celular"].ToString();
                        array[8] = row["Id_Parentesco"].ToString();
                        array[9] = row["Id_Escolaridad"].ToString();
                        array[10] = row["Id_Ocupa"].ToString();
                        array[11] = row["Discapacidad"].ToString();
                        array[12] = row["Anexo_Reg_Civil"].ToString();
                        array[13] = row["Anexo_Dos"].ToString();
                        array[14] = row["Apellido"].ToString();
                        array[15] = row["Edad"].ToString();

                        Session["AnexoRegistroCivil"] = array[12].ToString();
                        Session["AnexoDos"] = array[13].ToString();

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
        public string Cambiar_Estado_Familia(string Id_Familiar)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                DCL.Int_Nucleo_Familiar obj = new DCL.Int_Nucleo_Familiar();
                obj.Id_Familiar = Convert.ToInt32(Id_Familiar);
                Int_Nucleo_Familiar_BRL.InsertOrUpdate(obj, 7);

                string retornar = "1";

                return retornar;
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}\nError en método: {System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()}\n{ex.ToString()}", pathLog);
                string retornar = "0";
                return retornar;
            }
        }

        public string Guardar_Foto_Pefil(string foto, string Id_Usuario)
        {
            //¿NO SE USA? validar AGR 06-06-2022
            try
            {
                string retonar = "";

                if (!String.IsNullOrEmpty(foto))
                {
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Anexo_Foto = foto;
                    obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    Int_Usuarios_BRL.InsertOrUpdate(obj, 45);

                    retonar = "1";
                }
                else
                {
                    retonar = "0";
                }

                return retonar;
            }
            catch (Exception ex)
            {
                string retonar = "error";
                Console.WriteLine(ex);
                return retonar;
            }
        }

        [WebMethod]
        public List<string[]> cargar_drop_ciudad(string Id_Depart)
        {
            try
            {
                List<string[]> list = new List<string[]>();

                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(Id_Depart);
                dt = Int_Usuarios_BRL.SelectTable(obj, 52);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[2];
                        array[0] = row["Id_Ciudad"].ToString();
                        array[1] = row["Ciudad"].ToString();

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
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);

                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_drop_localidad()
        {
            try
            {
                List<string[]> list = new List<string[]>();

                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                dt = Int_Usuarios_BRL.SelectTable(obj, 53);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[2];
                        array[0] = row["Id_Localidad"].ToString();
                        array[1] = row["Localidad"].ToString();

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
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);

                return list;
            }
        }

        [WebMethod]
        public List<string[]> cargar_drop_barrios(string Id_Localidad)
        {
            try
            {
                List<string[]> list = new List<string[]>();

                DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Rol = Convert.ToInt32(Id_Localidad);
                dt = Int_Usuarios_BRL.SelectTable(obj, 54);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string[] array = new string[2];
                        array[0] = row["Id_Barrio"].ToString();
                        array[1] = row["Barrio"].ToString();
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
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);

                return list;
            }
        }

        [WebMethod]
        public string SubirArchivos(string Archivo, string Id_Usuario)
        {
            //No se usa? validar AGR 06-06-2022
            try
            {
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Anexo_Foto = Archivo;
                obj.Id_Usuario = Convert.ToInt32(Id_Usuario);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 45);

                return "1";
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw ex;
            }
        }


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
