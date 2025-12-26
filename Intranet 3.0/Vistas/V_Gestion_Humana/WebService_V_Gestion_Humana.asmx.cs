using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BRL;
using Intranet_3._0.Interna;
using System.Configuration;
using System.IO;

namespace Intranet_3._0.Vistas.V_Gestion_Humana
{
    /// <summary>
    /// Descripción breve de WebService_V_Gestion_Humana
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService_V_Gestion_Humana : System.Web.Services.WebService
    {
        string pathLog = "";
        string ipServer = "";
        [WebMethod]
        public List<string[]> cargar_card_colaborador(string Id_Usuario, string Id_Sede, string txt_search, string init, string fin)
        {
            try
            {
                string imagenPerfilLocal = "../../Content/img/perfilD.png";
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
    
                AG_Utils utilidades = new AG_Utils();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string archivoRemoto = "";
                string rutaCompleta = "";
                string archivoLocal = "";
                List<string[]> list = new List<string[]>();

                //consulta datos de usuario actual
                DataTable dtUsuario;
                DCL.Int_Usuarios objUsuario = new DCL.Int_Usuarios();
                objUsuario.Id_Usuario = Convert.ToInt32(Id_Usuario);
                dtUsuario = Int_Usuarios_BRL.SelectTable(objUsuario, 65);

                //Verifica si pertenece a Mantenimiento(03) o Conductores(04) y les asigna sus respectivos directorios
                if (dtUsuario.Rows[0]["Id_AreaOrganizacion"].ToString() == "03" || dtUsuario.Rows[0]["Id_AreaOrganizacion"].ToString() == "04" || dtUsuario.Rows[0]["Cargo"].ToString().Contains("OPERADOR"))
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Rol = Convert.ToInt32(Id_Sede);
                    obj.Usuario = txt_search;
                    obj.Inicio = Convert.ToInt32(init);
                    obj.Fin = Convert.ToInt32(fin);

                    dt = Int_Usuarios_BRL.SelectTable(obj, 49);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string[] array = new string[6];
                            array[0] = row["Nombre"].ToString();
                            array[1] = row["Cargo"].ToString();
                            array[2] = row["Email_Empresa"].ToString();
                            array[3] = row["UNE"].ToString();
                            if (row["Anexo_Foto"].ToString() == "" || row["Anexo_Foto"] == null)
                            {
                                array[4] = imagenPerfilLocal;
                            }
                            else
                            {
                                array[4] = row["Anexo_Foto"].ToString();
                            }
                            
                            array[5] = row["Celular"].ToString() + row["Ext"].ToString();

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
                else
                {
                    DataTable dt_;
                    DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                    obj_.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    dt_ = Int_Usuarios_BRL.SelectTable(obj_, 48);
                    if (dt_.Rows[0]["Nombre_Rol"].ToString().Contains("Humana") || dt_.Rows[0]["Nombre_Rol"].ToString().Contains("Admin"))
                    {
                        DataTable dt;
                        DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                        obj.Id_Rol = Convert.ToInt32(Id_Sede);
                        obj.Usuario = txt_search;
                        obj.Inicio = Convert.ToInt32(init);
                        obj.Fin = Convert.ToInt32(fin);
                        //dt = Int_Usuarios_BRL.SelectTable(obj, 50);



                        dt = Int_Usuarios_BRL.SelectTable(obj, 66);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string[] array = new string[6];
                                array[0] = row["Nombre"].ToString();
                                array[1] = row["Cargo"].ToString();
                                array[2] = row["Email_Empresa"].ToString();
                                array[3] = row["UNE"].ToString();
                                archivoRemoto = row["Anexo_Foto"].ToString();
                                rutaCompleta = "";
                                archivoLocal = "";

                                if (archivoRemoto.Contains("perfilD.png") || archivoRemoto.Contains("PerfilD.png"))
                                {
                                    if (archivoRemoto == imagenPerfilLocal)
                                    {
                                        array[4] = archivoRemoto;
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                else
                                {
                                    if (archivoRemoto.Contains('\\') && archivoRemoto.Contains(ambiente))
                                    {
                                        var anexoFoto = archivoRemoto.Split('\\');
                                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                                        int[] indexArray = new int[index];
                                        var removerLista = new List<int>(indexArray);
                                        for (int i = 0; i < removerLista.Count; i++)
                                        {
                                            arrayAnexoFoto.RemoveAt(0);
                                        }
                                        rutaCompleta = String.Join("\\", arrayAnexoFoto);
                                        string rutaPerfilLocal = $@"{pathServer}\{arrayAnexoFoto[0]}\{arrayAnexoFoto[1]}\{arrayAnexoFoto[2]}\{arrayAnexoFoto[3]}\{arrayAnexoFoto[4]}\";
                                        archivoLocal = $@"{pathServer}{rutaCompleta}";
                                        if (utilidades.impersonateValidUser())
                                        {
                                            if (!Directory.Exists(rutaPerfilLocal))
                                            {
                                                Directory.CreateDirectory(rutaPerfilLocal);
                                            }
                                            if (!File.Exists(archivoLocal))
                                            {
                                                if (conectaAdjuntos)
                                                {
                                                    if (File.Exists(archivoRemoto))
                                                    {
                                                        File.Copy(archivoRemoto, $@"{archivoLocal}", true);
                                                    }
                                                    else
                                                    {
                                                        array[4] = imagenPerfilLocal;
                                                    }
                                                }
                                                else
                                                {
                                                    array[4] = imagenPerfilLocal;
                                                }
                                            }
                                            else
                                            {
                                                array[4] = "../../Imagenes/" + rutaCompleta;
                                            }
                                            utilidades.undoImpersonation();
                                        }
                                        rutaCompleta = rutaCompleta.Replace(@"\", "/");
                                       
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                array[5] = row["Celular"].ToString() + row["Ext"].ToString();
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
                    else
                    {
                        DataTable dt;
                        DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                        obj.Id_Rol = Convert.ToInt32(Id_Sede);
                        obj.Usuario = txt_search;
                        obj.Inicio = Convert.ToInt32(init);
                        obj.Fin = Convert.ToInt32(fin);
                        dt = Int_Usuarios_BRL.SelectTable(obj, 50);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string[] array = new string[6];
                                array[0] = row["Nombre"].ToString();
                                array[1] = row["Cargo"].ToString();
                                array[2] = row["Email_Empresa"].ToString();
                                array[3] = row["UNE"].ToString();
                                archivoRemoto = row["Anexo_Foto"].ToString();
                                rutaCompleta = "";
                                archivoLocal = "";

                                if (archivoRemoto.Contains("perfilD.png") || archivoRemoto.Contains("PerfilD.png"))
                                {
                                    if (archivoRemoto == imagenPerfilLocal)
                                    {
                                        array[4] = archivoRemoto;
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                else
                                {
                                    if (archivoRemoto.Contains('\\') && archivoRemoto.Contains(ambiente))
                                    {
                                        var anexoFoto = archivoRemoto.Split('\\');
                                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                                        int[] indexArray = new int[index];
                                        var removerLista = new List<int>(indexArray);
                                        for (int i = 0; i < removerLista.Count; i++)
                                        {
                                            arrayAnexoFoto.RemoveAt(0);
                                        }
                                        rutaCompleta = String.Join("\\", arrayAnexoFoto);
                                        string rutaPerfilLocal = $@"{pathServer}\{arrayAnexoFoto[0]}\{arrayAnexoFoto[1]}\{arrayAnexoFoto[2]}\{arrayAnexoFoto[3]}\{arrayAnexoFoto[4]}\";
                                        archivoLocal = $@"{pathServer}{rutaCompleta}";
                                        if (utilidades.impersonateValidUser())
                                        {
                                            if (!Directory.Exists(rutaPerfilLocal))
                                            {
                                                Directory.CreateDirectory(rutaPerfilLocal);
                                            }
                                            if (!File.Exists(archivoLocal))
                                            {
                                                if (File.Exists(archivoRemoto))
                                                {
                                                    File.Copy(archivoRemoto, $@"{archivoLocal}", true);
                                                    array[4] = "../../Imagenes/" + rutaCompleta;
                                                }
                                                else
                                                {
                                                    array[4] = imagenPerfilLocal;
                                                }
                                            }
                                            else
                                            {
                                                array[4] = "../../Imagenes/" + rutaCompleta;
                                            }
                                            utilidades.undoImpersonation();
                                        }
                                        rutaCompleta = rutaCompleta.Replace(@"\", "/");
                                        
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                array[5] = row["Celular"].ToString() + row["Ext"].ToString();
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
        public List<string[]> cargar_card_colaborador_all(string Id_Usuario, string txt_search, string init, string fin)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string imagenPerfilLocal = "../../Content/img/perfilD.png";
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
    
                string archivoRemoto = "";
                string rutaCompleta = "";
                string archivoLocal = "";
                List<string[]> list = new List<string[]>();

                //consulta datos de usuario actual
                DataTable dtUsuario;
                DCL.Int_Usuarios objUsuario = new DCL.Int_Usuarios();
                objUsuario.Id_Usuario = Convert.ToInt32(Id_Usuario);
                dtUsuario = Int_Usuarios_BRL.SelectTable(objUsuario, 65);

                //Verifica si pertenece a Mantenimiento(03) o Conductores(04) y les asigna sus respectivos directorios
                if (dtUsuario.Rows[0]["Id_AreaOrganizacion"].ToString() == "03" || dtUsuario.Rows[0]["Id_AreaOrganizacion"].ToString() == "04" || dtUsuario.Rows[0]["Cargo"].ToString().Contains("OPERADOR"))
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Usuario = txt_search;
                    obj.Inicio = Convert.ToInt32(init);
                    obj.Fin = Convert.ToInt32(fin);
                    dt = Int_Usuarios_BRL.SelectTable(obj, 55);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string[] array = new string[6];
                            array[0] = row["Nombre"].ToString();
                            array[1] = row["Cargo"].ToString();
                            array[2] = row["Email_Empresa"].ToString();
                            array[3] = row["UNE"].ToString();
                            archivoRemoto = row["Anexo_Foto"].ToString();
                            rutaCompleta = "";
                            archivoLocal = "";

                            if (archivoRemoto.Contains("perfilD.png") || archivoRemoto.Contains("PerfilD.png"))
                            {
                                if (archivoRemoto == imagenPerfilLocal)
                                {
                                    array[4] = archivoRemoto;
                                }
                                else
                                {
                                    array[4] = imagenPerfilLocal;
                                }
                            }
                            else
                            {
                                if (archivoRemoto.Contains('\\') && archivoRemoto.Contains(ambiente))
                                {
                                    var anexoFoto = archivoRemoto.Split('\\');
                                    List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                                    int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                                    int[] indexArray = new int[index];
                                    var removerLista = new List<int>(indexArray);
                                    for (int i = 0; i < removerLista.Count; i++)
                                    {
                                        arrayAnexoFoto.RemoveAt(0);
                                    }
                                    rutaCompleta = String.Join("\\", arrayAnexoFoto);
                                    string rutaPerfilLocal = $@"{pathServer}\{arrayAnexoFoto[0]}\{arrayAnexoFoto[1]}\{arrayAnexoFoto[2]}\{arrayAnexoFoto[3]}\";
                                    archivoLocal = $@"{pathServer}{rutaCompleta}";
                                    if (utilidades.impersonateValidUser())
                                    {
                                        if (!Directory.Exists(rutaPerfilLocal))
                                        {
                                            Directory.CreateDirectory(rutaPerfilLocal);
                                        }
                                        if (!File.Exists(archivoLocal))
                                        {
                                            if (File.Exists(archivoRemoto))
                                            {
                                                File.Copy(archivoRemoto, $@"{archivoLocal}", true);
                                            }
                                            else
                                            {
                                                array[4] = imagenPerfilLocal;
                                            }
                                        }
                                        utilidades.undoImpersonation();
                                    }
                                    rutaCompleta = rutaCompleta.Replace(@"\", "/");
                                    array[4] = "../../Imagenes/" + rutaCompleta;
                                }
                                else
                                {
                                    array[4] = imagenPerfilLocal;
                                }
                            }
                            array[5] = row["Celular"].ToString() + row["Ext"].ToString();
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
                else
                {
                    DataTable dt_;
                    DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                    obj_.Id_Usuario = Convert.ToInt32(Id_Usuario);
                    dt_ = Int_Usuarios_BRL.SelectTable(obj_, 48);
                    if (dt_.Rows[0]["Nombre_Rol"].ToString().Contains("Humana") || dt_.Rows[0]["Nombre_Rol"].ToString().Contains("Admin"))
                    {
                        DataTable dt;
                        DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                        obj.Usuario = txt_search;
                        obj.Inicio = Convert.ToInt32(init);
                        obj.Fin = Convert.ToInt32(fin);
                        dt = Int_Usuarios_BRL.SelectTable(obj, 64);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string[] array = new string[6];
                                array[0] = row["Nombre"].ToString();
                                array[1] = row["Cargo"].ToString();
                                array[2] = row["Email_Empresa"].ToString();
                                array[3] = row["UNE"].ToString();
                                archivoRemoto = row["Anexo_Foto"].ToString();
                                rutaCompleta = "";
                                archivoLocal = "";

                                if (archivoRemoto.Contains("perfilD.png") || archivoRemoto.Contains("PerfilD.png"))
                                {
                                    if (archivoRemoto == imagenPerfilLocal)
                                    {
                                        array[4] = archivoRemoto;
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                else
                                {
                                    if (archivoRemoto.Contains('\\') && archivoRemoto.Contains(ambiente))
                                    {
                                        var anexoFoto = archivoRemoto.Split('\\');
                                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                                        int[] indexArray = new int[index];
                                        var removerLista = new List<int>(indexArray);
                                        for (int i = 0; i < removerLista.Count; i++)
                                        {
                                            arrayAnexoFoto.RemoveAt(0);
                                        }
                                        rutaCompleta = String.Join("\\", arrayAnexoFoto);
                                        string rutaPerfilLocal = $@"{pathServer}\{arrayAnexoFoto[0]}\{arrayAnexoFoto[1]}\{arrayAnexoFoto[2]}\{arrayAnexoFoto[3]}\{arrayAnexoFoto[4]}\";
                                        archivoLocal = $@"{pathServer}{rutaCompleta}";
                                        if (utilidades.impersonateValidUser())
                                        {
                                            if (!Directory.Exists(rutaPerfilLocal))
                                            {
                                                Directory.CreateDirectory(rutaPerfilLocal);
                                            }
                                            if (!File.Exists(archivoLocal))
                                            {

                                                if (File.Exists(archivoRemoto))
                                                {
                                                    File.Copy(archivoRemoto, $@"{archivoLocal}", true);
                                                }
                                                else
                                                {
                                                    array[4] = imagenPerfilLocal;
                                                }
                                            }
                                            utilidades.undoImpersonation();
                                        }
                                        rutaCompleta = rutaCompleta.Replace(@"\", "/");
                                        array[4] = "../../Imagenes/" + rutaCompleta;
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                array[5] = row["Celular"].ToString() + row["Ext"].ToString();
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
                    else
                    {
                        DataTable dt;
                        DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                        obj.Usuario = txt_search;
                        obj.Inicio = Convert.ToInt32(init);
                        obj.Fin = Convert.ToInt32(fin);
                        dt = Int_Usuarios_BRL.SelectTable(obj, 56);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                string[] array = new string[6];
                                array[0] = row["Nombre"].ToString();
                                array[1] = row["Cargo"].ToString();
                                array[2] = row["Email_Empresa"].ToString();
                                array[3] = row["UNE"].ToString();
                                archivoRemoto = row["Anexo_Foto"].ToString();
                                rutaCompleta = "";
                                archivoLocal = "";

                                if (archivoRemoto.Contains("perfilD.png") || archivoRemoto.Contains("PerfilD.png"))
                                {
                                    if (archivoRemoto == imagenPerfilLocal)
                                    {
                                        array[4] = archivoRemoto;
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                else
                                {
                                    if (archivoRemoto.Contains('\\') && archivoRemoto.Contains(ambiente))
                                    {
                                        var anexoFoto = archivoRemoto.Split('\\');
                                        List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                                        int[] indexArray = new int[index];
                                        var removerLista = new List<int>(indexArray);
                                        for (int i = 0; i < removerLista.Count; i++)
                                        {
                                            arrayAnexoFoto.RemoveAt(0);
                                        }
                                        rutaCompleta = String.Join("\\", arrayAnexoFoto);
                                        string rutaPerfilLocal = $@"{pathServer}\{arrayAnexoFoto[0]}\{arrayAnexoFoto[1]}\{arrayAnexoFoto[2]}\{arrayAnexoFoto[3]}\{arrayAnexoFoto[4]}\";
                                        archivoLocal = $@"{pathServer}{rutaCompleta}";
                                        if (utilidades.impersonateValidUser())
                                        {
                                            if (!Directory.Exists(rutaPerfilLocal))
                                            {
                                                Directory.CreateDirectory(rutaPerfilLocal);
                                            }
                                            if (!File.Exists(archivoLocal))
                                            {
                                                if (File.Exists(archivoRemoto))
                                                {
                                                    File.Copy(archivoRemoto, $@"{archivoLocal}", true);
                                                }
                                                else
                                                {
                                                    array[4] = imagenPerfilLocal;
                                                }
                                            }
                                            utilidades.undoImpersonation();
                                        }
                                        rutaCompleta = rutaCompleta.Replace(@"\", "/");
                                        array[4] = "../../Imagenes/" + rutaCompleta;
                                    }
                                    else
                                    {
                                        array[4] = imagenPerfilLocal;
                                    }
                                }
                                array[5] = row["Celular"].ToString() + row["Ext"].ToString();
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

        /*[WebMethod]
        public List<string[]> cargar_card_colaborador(string Id_Usuario, string Id_Sede, string txt_search, string init, string fin)
        {
            try
            {
                List<string[]> list = new List<string[]>();

                DataTable dt_;
                DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                obj_.Id_Usuario = Convert.ToInt32(Id_Usuario);
                dt_ = Int_Usuarios_BRL.SelectTable(obj_, 48);
                if ()
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Rol = Convert.ToInt32(Id_Sede);
                    obj.Usuario = txt_search;
                    obj.Inicio = Convert.ToInt32(init);
                    obj.Fin = Convert.ToInt32(fin);
                    dt = Int_Usuarios_BRL.SelectTable(obj, 49);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string[] array = new string[5];
                            array[0] = row["Nombre"].ToString();
                            array[1] = row["Cargo"].ToString();
                            array[2] = row["Email_Empresa"].ToString();
                            array[3] = row["UNE"].ToString();
                            array[4] = row["Anexo_Foto"].ToString();

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
                else
                {
                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Rol = Convert.ToInt32(Id_Sede);
                    obj.Usuario = txt_search;
                    obj.Inicio = Convert.ToInt32(init);
                    obj.Fin = Convert.ToInt32(fin);
                    dt = Int_Usuarios_BRL.SelectTable(obj, 50);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string[] array = new string[5];
                            array[0] = row["Nombre"].ToString();
                            array[1] = row["Cargo"].ToString();
                            array[2] = row["Email_Empresa"].ToString();
                            array[3] = row["UNE"].ToString();
                            array[4] = row["Anexo_Foto"].ToString();

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


            }
            catch (Exception ex)
            {
                List<string[]> list = new List<string[]>();
                string[] array = new string[1];
                array[0] = ex.ToString();
                list.Add(array);
                return list;
            }
        }*/
    }
}
