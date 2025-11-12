using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Web;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Web.UI;

namespace Intranet_3._0.Interna
{
    public class AG_Utils
    {

        private string usuario = "intal.bitenterprise";
        private string dominio = "etib";
        private string contraseña = "l1mpiez4100_";
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
        String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
        int impersonationLevel,
        ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        //string ipAdjuntos = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
        int iTiempoEspera = Convert.ToInt32(ConfigurationManager.AppSettings.Get("msPingServer"));
        string ipServerAttach = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
        const string CONST_ERRORCONEXIONSERV = "al intentar conectarse al servidor: ";
        const string CONST_ERRORPERMISOS = "al intentar acceder a archivos. ACCESO DENEGADO. ";
        const string CONST_ERROR = " - ERROR: ";

        public bool impersonateValidUser()
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(usuario, dominio, contraseña, LOGON32_LOGON_INTERACTIVE,
                LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        public void undoImpersonation()
        {
            impersonationContext.Undo();
        }




        /// <summary>
        /// Verifica disponibilidad de servidor
        /// </summary>
        /// <returns></returns>
        public bool Ping(string ip)
        {
            try
            {
                Ping HacerPing = new Ping();
                PingReply RespuestaPing;
                RespuestaPing = HacerPing.Send(ip, iTiempoEspera);

                if (RespuestaPing.Status == IPStatus.Success)
                {
                    //Console.WriteLine($"Ping a {ip} [{RespuestaPing.Address.ToString()}] Correcto Tiempo de respuesta {RespuestaPing.RoundtripTime}ms");
                    return true;
                }
                else
                {
                    //Console.WriteLine($"Error: Ping a {ip.ToString()}");
                    return false;
                }
            }
            catch (Exception)
            {
                //Console.WriteLine($"Error: Ping a {ipServer.ToString()}");
                return false;
            }
        }


        /// <summary>
        /// Escribe log de errores
        /// </summary>
        /// <param name="mensaje">Mensaje a almacenar</param>
        /// <param name="rutaLog">Ruta de almacenamiento de log</param>
        public void logError(string mensaje, string rutaLog)
        {
            if (impersonateValidUser())
            {
                if (!Directory.Exists(rutaLog))
                {
                    Directory.CreateDirectory(rutaLog);
                }
                rutaLog += "/error.log";
                using (StreamWriter sw = File.AppendText(rutaLog))
                {
                    sw.WriteLine($"{DateTime.Now} {mensaje}\n");
                }
            }
        }
       
       

        /// <summary>
        /// Validación de noticias (si existe una noticia con el ID a crear, elimina la imagen)
        /// </summary>
        /// <param name="rutaNoticias">Ruta carpeta donde se almacenan las imagenes de noticias</param>
        /// <param name="consecutivo">Id consecutivo de noticia</param>
        /// <param name="imagenNoticia">Ruta completa de la imagen</param>
        /// <returns></returns>
        public bool validarArchivoNoticia(string rutaNoticias, int consecutivo, string imagenNoticia, string log)
        {
            try
            {
                if (impersonateValidUser())
                {
                    if (!Directory.Exists(rutaNoticias))
                    {
                        Directory.CreateDirectory(rutaNoticias);
                    }
                    else
                    {
                        var archivosNoticias = Directory.GetFiles(rutaNoticias, "*.*");
                        foreach (var item in archivosNoticias)
                        {
                            if (item.Contains($"{consecutivo}-"))
                            {
                                File.Delete(item);
                            }
                        }
                    }
                    if (File.Exists(imagenNoticia))
                    {
                        File.Delete(imagenNoticia);
                    }
                    return true;
                }
                else
                {
                    logError(" - Error: No se poseen permisos para eliminar y/o sobreescribir imagen de noticia.", log);
                    return false;
                }
            }
            catch (Exception e)
            {
                logError($" - ERROR: {e.Message}.",log);
                return false;
            }
        }



        /// <summary>
        /// Eliminación de archivos temporales
        /// </summary>
        /// <param name="ruta1">Carpeta local</param>
        /// <param name="ruta2">Carpeta remota</param>
        /// <param name="carpetaOmitir">Carpeta que se omitirá durante la ejecución</param>
        /// <param name="documento">Número de documento del usuario</param>
        /// <param name="log">Archivo de almacenamiento de log</param>
        public void limpiezaCarpetas(string ruta1, string ruta2, string carpetaOmitir, string documento, string log)
        {
            try
            {
                if (Directory.Exists(ruta1))
                {
                    Stack<string> dirs = new Stack<string>(20);
                    dirs.Push(ruta1);
                    while (dirs.Count > 0)
                    {
                        string rutaCarpeta = "";
                        string carpetaActual = dirs.Pop();
                        if (!carpetaActual.Contains(carpetaOmitir))
                        {
                            string[] subDirs;
                            try
                            {
                                subDirs = System.IO.Directory.GetDirectories(carpetaActual);
                                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(carpetaActual);
                                if (!carpetaActual.Equals(ruta1))
                                {
                                    string DirP = di.Parent.Name;
                                    if (DirP.Equals(documento))
                                    {
                                        DirP = "";
                                    }
                                    else
                                    {
                                        DirP = di.Parent.Name + "\\";
                                    }
                                    string DirNom = di.Name;
                                    rutaCarpeta = $@"{ruta2}{DirP}{DirNom}";
                                }
                            }
                            catch (UnauthorizedAccessException e)
                            {
                                logError($"{DateTime.Now} - Error:{e.Message}", log);
                                continue;
                            }
                            catch (System.IO.DirectoryNotFoundException e)
                            {
                                logError($"{DateTime.Now} - Error:{e.Message}", log);
                                continue;
                            }

                            string[] files = null;
                            try
                            {
                                files = System.IO.Directory.GetFiles(carpetaActual);
                            }

                            catch (UnauthorizedAccessException e)
                            {

                                logError($"{DateTime.Now} - Error:{e.Message}", log);
                                continue;
                            }

                            catch (System.IO.DirectoryNotFoundException e)
                            {
                                logError($"{DateTime.Now} - Error:{e.Message}", log);
                                continue;
                            }
                            bool bl_CarpetaRemota = Directory.Exists(rutaCarpeta);
                            foreach (string archivoELocal in files)
                            {
                                try
                                {
                                    if (!bl_CarpetaRemota)
                                    {
                                        Directory.CreateDirectory(rutaCarpeta);
                                    }
                                    System.IO.FileInfo fi = new System.IO.FileInfo(archivoELocal);
                                    string archivoERemoto = $@"{rutaCarpeta}\{fi.Name}";
                                    if (!File.Exists(archivoERemoto))
                                    {
                                        if (!Directory.Exists(rutaCarpeta))
                                        {
                                            Directory.CreateDirectory(rutaCarpeta);
                                        }
                                        File.Copy(archivoELocal, $@"{archivoERemoto}", true);

                                    }
                                    File.Delete(archivoELocal);

                                    //var tempo = currentDir.Split('\\');
                                    //var temo1 = tempo.Reverse();


                                    //Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                                }
                                catch (System.IO.FileNotFoundException e)
                                {
                                    logError($"{DateTime.Now} - Error:{e.Message}", log);
                                    continue;
                                }
                            }
                            foreach (string str in subDirs)
                            {
                                dirs.Push(str);
                            }

                        }
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                logError($" - Error:{e.Message}", log);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                logError($" - Error:{e.Message}", log);
            }
        }


        /// <summary>
        /// Método para crear nuevas noticias o slideshow
        /// </summary>
        /// <param name="nombreOriginalArchivo"></param>
        /// <param name="extensionArchivo"></param>
        /// <param name="consecutivo"></param>
        /// <param name="rutaNoticiasLocal"></param>
        /// <param name="rutaNoticiasRemoto"></param>
        /// <param name="actionSelectSP"></param>
        /// <param name="selectArchivoRemoto"></param>
        /// <param name="actionInsertSP"></param>
        /// <param name="noticias"></param>
        /// <param name="ambiente"></param>
        /// <param name="ubicacionEsp"></param>
        /// <returns></returns>
        public string TratamientoNoticias(string nombreOriginalArchivo, string extensionArchivo, string consecutivo, string rutaNoticiasLocal, string rutaNoticiasRemoto, int actionSelectSP, int selectArchivoRemoto, int actionInsertSP, DCL.Int_Noticias noticias, string ambiente, string ubicacionEsp)
        {
            string imagenNoticiaLocal = "";
            string imagenNoticiaRemoto = "";
            AG_Utils utilidades = new AG_Utils();
            try
            {

                //ajusteNombreNoticia();
                string nombreArchivoNorm = System.Text.RegularExpressions.Regex.Replace(nombreOriginalArchivo.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                string nombreAjustadoArchivoSE = nombreArchivoNorm.Replace(" ", "_");
                nombreAjustadoArchivoSE = $"{consecutivo}-{nombreAjustadoArchivoSE}";
                string nombreFinalArchivo = $"{nombreAjustadoArchivoSE}{extensionArchivo}";

                System.Data.DataTable dt = BRL.Int_Noticias_BRL.SelectTable(noticias, actionSelectSP);


                if (actionSelectSP == 8)
                {
                    string archivoRemoto = dt.Rows[0][selectArchivoRemoto].ToString();
                    var anexoFoto = archivoRemoto.Split('\\');
                    List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                    int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                    int[] indexArray = new int[index];
                    var removerLista = new List<int>(indexArray);
                    string nombreArchivoRemoto = arrayAnexoFoto[8].ToString();
                    for (int i = 0; i < removerLista.Count; i++)
                    {
                        arrayAnexoFoto.RemoveAt(0);
                    }
                    string rutaCompleta = String.Join("\\", arrayAnexoFoto);
                }

                noticias.Id_Noticia = Convert.ToInt32(consecutivo);

                if (utilidades.impersonateValidUser())
                {
                    switch (ubicacionEsp)
                    {
                        case ("Local"):
                            imagenNoticiaLocal = Path.Combine(rutaNoticiasLocal, nombreFinalArchivo);
                            if (!Directory.Exists(rutaNoticiasLocal))
                            {
                                Directory.CreateDirectory(rutaNoticiasLocal);
                            }
                            else
                            {
                                var archivosNoticiasLocal = Directory.GetFiles(rutaNoticiasLocal, "*.*");
                                foreach (var item in archivosNoticiasLocal)
                                {
                                    if (item.Contains($"{consecutivo}-"))
                                    {
                                        File.Delete(item);
                                    }

                                }
                            }
                            if (File.Exists(imagenNoticiaLocal))
                            {
                                File.Delete(imagenNoticiaLocal);
                            }
                            return imagenNoticiaLocal;

                        case ("Remoto"):
                            imagenNoticiaRemoto = Path.Combine(rutaNoticiasRemoto, nombreFinalArchivo);
                            if (!Directory.Exists(rutaNoticiasRemoto))
                            {
                                Directory.CreateDirectory(rutaNoticiasRemoto);
                            }
                            else
                            {
                                var archivosNoticiasRemoto = Directory.GetFiles(rutaNoticiasRemoto, "*.*");
                                foreach (var item in archivosNoticiasRemoto)
                                {
                                    if (item.Contains($"{consecutivo}-"))
                                    {
                                        File.Delete(item);
                                    }
                                }
                            }
                            if (File.Exists(imagenNoticiaRemoto))
                            {
                                File.Delete(imagenNoticiaRemoto);
                            }
                            return imagenNoticiaRemoto;

                        default:
                            break;
                    }
                    utilidades.undoImpersonation();
                }










                //string archivoLocal = $@"{pathServer}{rutaCompleta}";








                //if (File.Exists(imagenNoticiaLocal) && !File.Exists(imagenNoticiaRemoto))
                //{
                //    File.Copy(imagenNoticiaLocal, imagenNoticiaRemoto, true);
                //}

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        /// <summary>
        /// Método para Actualizar noticias o slideshow
        /// </summary>
        /// <param name="nombreOriginalArchivo"></param>
        /// <param name="extensionArchivo"></param>
        /// <param name="consecutivo"></param>
        /// <param name="rutaNoticiasLocal"></param>
        /// <param name="rutaNoticiasRemoto"></param>
        /// <param name="actionInsertSP"></param>
        /// <param name="noticias"></param>
        /// <param name="ambiente"></param>
        /// <param name="ubicacionEsp"></param>
        /// <param name="archivoAguardar"></param>
        /// <returns></returns>
        public string TratamientoNoticias(string nombreFinalArchivo, string consecutivo, string rutaNoticiasLocal, string rutaNoticiasRemoto, int actionInsertSP, DCL.Int_Noticias noticias, string ambiente, string ubicacionEsp, System.Web.UI.WebControls.FileUpload archivoAguardar)
        {
            string imagenNoticiaLocal = "";
            string imagenNoticiaRemoto = "";
            AG_Utils utilidades = new AG_Utils();
            try
            {
                noticias.Id_Noticia = Convert.ToInt32(consecutivo);

                if (utilidades.impersonateValidUser())
                {
                    switch (ubicacionEsp)
                    {
                        case ("Local"):
                            imagenNoticiaLocal = Path.Combine(rutaNoticiasLocal, nombreFinalArchivo);
                            if (!Directory.Exists(rutaNoticiasLocal))
                            {
                                Directory.CreateDirectory(rutaNoticiasLocal);
                            }
                            else
                            {
                                var archivosNoticiasLocal = Directory.GetFiles(rutaNoticiasLocal, "*.*");
                                foreach (var item in archivosNoticiasLocal)
                                {
                                    if (item.Contains($"{consecutivo}-"))
                                    {
                                        File.Delete(item);
                                    }
                                }
                            }
                            if (File.Exists(imagenNoticiaLocal))
                            {
                                File.Delete(imagenNoticiaLocal);
                            }

                            archivoAguardar.SaveAs(imagenNoticiaLocal);
                            return imagenNoticiaLocal;

                        case ("Remoto"):
                            imagenNoticiaRemoto = Path.Combine(rutaNoticiasRemoto, nombreFinalArchivo);
                            if (!Directory.Exists(rutaNoticiasRemoto))
                            {
                                Directory.CreateDirectory(rutaNoticiasRemoto);
                            }
                            else
                            {
                                var archivosNoticiasRemoto = Directory.GetFiles(rutaNoticiasRemoto, "*.*");
                                foreach (var item in archivosNoticiasRemoto)
                                {
                                    if (item.Contains($"{consecutivo}-"))
                                    {
                                        File.Delete(item);
                                    }
                                }
                            }
                            if (File.Exists(imagenNoticiaRemoto))
                            {
                                File.Delete(imagenNoticiaRemoto);
                            }
                            archivoAguardar.SaveAs(imagenNoticiaRemoto);
                            return imagenNoticiaRemoto;

                        default:
                            break;
                    }
                    utilidades.undoImpersonation();
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }










        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombreFinalArchivo"></param>
        /// <param name="consecutivo"></param>
        /// <param name="rutaNoticiasLocal"></param>
        /// <param name="rutaNoticiasRemoto"></param>
        /// <param name="archivoAguardar"></param>
        /// <param name="Id_Usuario"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public (bool bl_GuardaImagenLocal, bool bl_GuardaImagenRemota, string rutaNoticiaRemoto) TratamientoNoticias(string nombreFinalArchivo, string consecutivo, string rutaNoticiasLocal, string rutaNoticiasRemoto, System.Web.UI.WebControls.FileUpload archivoAguardar, string Id_Usuario, string log)
        {
            bool bl_GuardaImagenLocal = false;
            bool bl_GuardaImagenRemota = false;
            string imagenNoticiaRemoto = "";
            string imagenNoticiaLocal = "";
            AG_Utils utilidades = new AG_Utils();
            var tamanioOrig = archivoAguardar.FileBytes;
            try
            {
                if (utilidades.impersonateValidUser())
                {
                    imagenNoticiaLocal = Path.Combine(rutaNoticiasLocal, nombreFinalArchivo);
                    if (!Directory.Exists(rutaNoticiasLocal))
                    {
                        Directory.CreateDirectory(rutaNoticiasLocal);
                    }
                    else
                    {
                        if (File.Exists(imagenNoticiaLocal))
                        {
                            File.Delete(imagenNoticiaLocal);
                        }
                        var archivosNoticiasLocal = Directory.GetFiles(rutaNoticiasLocal, "*.*");
                        foreach (var item in archivosNoticiasLocal)
                        {
                            if (item.Contains($"{consecutivo}-"))
                            {
                                File.Delete(item);
                            }
                        }
                    }
                    
                    if (tamanioOrig.Length > 0)
                    {
                        Thread.Sleep(500);
                        archivoAguardar.SaveAs(imagenNoticiaLocal);
                        Thread.Sleep(500);
                        var tamanioDestLocal= File.ReadAllBytes(imagenNoticiaLocal);

                        if (tamanioOrig.Length == tamanioDestLocal.Length)
                        {
                            bl_GuardaImagenLocal = true;
                        }
                        else
                        {
                            logError($"El archivo almacenado no coincide con el tamaño original.\nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario: {Id_Usuario}", log);
                        }

                    }
                    else
                    {
                        logError($"El archivo a cargar está dañado.\nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario: {Id_Usuario}", log);
                    }
                    if (bl_GuardaImagenLocal)
                    {
                        if (Ping(ipServerAttach))
                        {
                            imagenNoticiaRemoto = Path.Combine(rutaNoticiasRemoto, nombreFinalArchivo);
                            if (!Directory.Exists(rutaNoticiasRemoto))
                            {
                                Directory.CreateDirectory(rutaNoticiasRemoto);
                            }
                            else
                            {
                                if (File.Exists(imagenNoticiaRemoto))
                                {
                                    File.Delete(imagenNoticiaRemoto);
                                }
                                var archivosNoticiasRemoto = Directory.GetFiles(rutaNoticiasRemoto, "*.*");
                                foreach (var item in archivosNoticiasRemoto)
                                {
                                    if (item.Contains($"{consecutivo}-"))
                                    {
                                        File.Delete(item);
                                    }
                                }
                            }

                            Thread.Sleep(500);
                            archivoAguardar.SaveAs(imagenNoticiaRemoto);
                            Thread.Sleep(500);
                            //archivoAguardar.SaveAs(imagenNoticiaRemoto);
                            var tamanioDestRemoto = File.ReadAllBytes(imagenNoticiaRemoto);
                            if (tamanioOrig.Length == tamanioDestRemoto.Length)
                            {
                                bl_GuardaImagenRemota = true;
                            }
                        }
                        else
                        {
                            logError($"{CONST_ERRORCONEXIONSERV} {ipServerAttach}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario: {Id_Usuario}", log);
                        }


                    }

                  

                    utilidades.undoImpersonation();
                    return (bl_GuardaImagenLocal, bl_GuardaImagenRemota, imagenNoticiaRemoto);
                }
                else
                {
                    logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nRegistro no almacenado en BD. Los archivos no fueron almacenados.", log);
                    if (File.Exists(imagenNoticiaRemoto))
                        File.Delete(imagenNoticiaRemoto);
                    if (File.Exists(imagenNoticiaLocal))
                        File.Delete(imagenNoticiaLocal);
                    return (bl_GuardaImagenLocal, bl_GuardaImagenRemota, imagenNoticiaRemoto);
                }
            }
            catch (Exception ex)
            {
                logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nRegistro no almacenado en BD. Los archivos no fueron almacenados.", log);
                if (File.Exists(imagenNoticiaRemoto))
                    File.Delete(imagenNoticiaRemoto);
                if (File.Exists(imagenNoticiaLocal))
                    File.Delete(imagenNoticiaLocal);
                return (bl_GuardaImagenLocal, bl_GuardaImagenLocal, imagenNoticiaRemoto);
            }
        }























        

        public string AjusteNombreImagenNoticia(string nombreOriginalArchivo, string consecutivo, string extensionArchivo)
        {
            //ajusteNombreNoticia();
            string nombreArchivoNorm = System.Text.RegularExpressions.Regex.Replace(nombreOriginalArchivo.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            string nombreAjustadoArchivoSE = nombreArchivoNorm.Replace(" ", "_");
            nombreAjustadoArchivoSE = $"{consecutivo}-{nombreAjustadoArchivoSE}";
            string nombreFinalArchivo = $"{nombreAjustadoArchivoSE}{extensionArchivo}";
            return nombreFinalArchivo;
        }


       
    }
}
