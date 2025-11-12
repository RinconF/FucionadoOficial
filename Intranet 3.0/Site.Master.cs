using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;
using System.Security.Principal;
using System.Runtime.InteropServices;
using Intranet_3._0.Interna;
using System.Net.NetworkInformation;
using System.Threading;

namespace Intranet_3._0
{
    public partial class SiteMaster : MasterPage
    {
        string pathLog = "";
        const string CONST_USOIMAGENDEFAULT = "No se puede obtener imagen de perfil, se usará imagen por defecto. ";
        const string CONST_ERROR = " - ERROR: ";
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id_Usuario"] != null && Session["cerrar"] !=null)
                {
                    pathLog = Server.MapPath(@"~/logs");
                    int tiempoSesion = Convert.ToInt32(ConfigurationManager.AppSettings.Get("tiempoSesion"));
                    HttpCookie httpCookie = Request.Cookies["login"];
                    if (httpCookie == null)
                    {
                        if (Session["cerrar"] != null)
                        {
                            Session["cerrar"] = "0";
                        }
                        string url = Page.Request.Url.ToString();
                        string[] Separado = url.Split('/');
                        string Final = Separado[Separado.Length - 1];

                            string url_ = Final.Replace(Final, @"~/Login");
                            Page.Response.Redirect(url_, false);
                    }
                    else
                    {
                        if (httpCookie.Value.Split('=')[1] != Request.QueryString["Id_Usuario"])
                        {
                            string url = Page.Request.Url.ToString();
                            string[] Separado = url.Split('/');
                            string Final = Separado[Separado.Length - 1];
                            if (Final.Contains("Id_Vista"))
                            {
                                string url_ = Final.Replace(Final, "../../Login");
                                Page.Response.Redirect(url_, true);
                            }
                            else
                            {
                                string url_ = Final.Replace(Final, "./Login");
                                Page.Response.Redirect(url_, true);
                            }
                        }

                        string idUsuario = Request.QueryString["Id_Usuario"].ToString();
                        HttpCookie http = new HttpCookie("login");
                        http.Values.Add("userid", idUsuario);
                        http.Expires = DateTime.Now.AddMinutes(tiempoSesion);
                        Response.Cookies.Add(http);

                        if (!String.IsNullOrEmpty(idUsuario))
                        {
                            DCL.Int_Usuarios int_Usuarios = new DCL.Int_Usuarios();
                            int_Usuarios.Id_Usuario = Convert.ToInt32(idUsuario);
                            DataTable dataTable = Int_Usuarios_BRL.SelectTable(int_Usuarios, 60);
                            if (dataTable.Rows[0]["Id_Estado_Sesion"].ToString() == "2" || Session["cerrar"] == null)
                            {
                                string url = Page.Request.Url.ToString();
                                string[] Separado = url.Split('/');
                                string Final = Separado[Separado.Length - 1];

                                    string url_ = Final.Replace(Final, @"~/Login");
                                    Page.Response.Redirect(url_, false);

                            }
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Page.Request.Url.ToString();
            string[] Separado = url.Split('/');
            Session["numDocumento"] = "";
            Session["codSAE"] = "";
            string Final = Separado[Separado.Length - 1];
            if ((Final.Contains("Default") || Final.Contains("default") || Final.Contains("Vistas") || Final.Contains("V_")))
            {
                if (Request.QueryString["Id_Usuario"] == null || Session["cerrar"] == null)
                {
                    Page.Response.Redirect($@"~/Login", true);
                }
                else
                {
                    DCL.Int_Usuarios int_Usuarios = new DCL.Int_Usuarios();
                    int_Usuarios.Id_Usuario = Convert.ToInt32(Request.QueryString["Id_Usuario"]);
                    DataTable dataTable = Int_Usuarios_BRL.SelectTable(int_Usuarios, 60);
                    if (dataTable.Rows[0]["Id_Estado_Sesion"].ToString() == "2" || Session["cerrar"] == null)
                    {
                        string url1 = Page.Request.Url.ToString();
                        string[] Separado1 = url1.Split('/');
                        string Final1 = Separado1[Separado1.Length - 1];

                        string url_ = Final1.Replace(Final1, @"~/Login");
                        Page.Response.Redirect(url_, false);

                    }
                    else
                    {
                        Cargar_Datos(null, null);
                        barra_navegacion();
                        Cargar_Datos_Usuario();

                    }
                }
            }
            else if (Request.QueryString["Id_Usuario"] == null)
            {
                //Page.Response.Redirect($@"~/Login", true);
            }
            else if (Request.Cookies.Count == 0)
            {
                Page.Response.Redirect($@"~/Login", true);
            }
            else
            {
                Cargar_Datos(null, null);
                barra_navegacion();
                Cargar_Datos_Usuario();
            }

        }

        public void barra_navegacion()
        {
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    pathLog = Server.MapPath(@"~/logs");
                    if (Request.QueryString["Id_Grupo"] != null && Request.QueryString["Id_Vista"] != null)
                    {
                        pnl_navegacion.Attributes.Remove("style");
                        string id_user = Request.QueryString["Id_Usuario"].ToString();
                        string id_grupo = Request.QueryString["Id_Grupo"].ToString();
                        string id_vista = Request.QueryString["Id_Vista"].ToString();

                        DataTable db;
                        DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                        obj.Id_Usuario = Convert.ToInt32(id_grupo);
                        obj.Id_Rol = Convert.ToInt32(id_vista);
                        db = Int_Usuarios_BRL.SelectTable(obj, 3);
                        if (db.Rows.Count > 0)
                        {
                            //pnl_navegacion.Controls.Clear();
                            foreach (DataRow row in db.Rows)
                            {
                                var lnk_inicio = new HtmlGenericControl("a");
                                lnk_inicio.ID = "lnk_inicio";
                                lnk_inicio.InnerHtml = "<i class='fas fa-home'></i>" + "Inicio";
                                lnk_inicio.Attributes.Add("href", "../../Default.aspx?Id_Usuario=" + id_user);
                                pnl_navegacion.Controls.Add(lnk_inicio);

                                var lnk_grupo = new HtmlGenericControl("a");
                                lnk_grupo.ID = "lnk_grupo" + row["Id_Grupo_Vista"].ToString();
                                lnk_grupo.InnerHtml = " / " + row["Nombre_Grupo"].ToString() + " / ";
                                pnl_navegacion.Controls.Add(lnk_grupo);

                                var lnk_vista = new HtmlGenericControl("a");
                                lnk_vista.ID = "lnk_vista" + row["Id_Vista"].ToString();
                                lnk_vista.InnerHtml = row["Nombre_Vista"].ToString();
                                    lnk_vista.Attributes.Add("href", "../../" + row["Ruta"].ToString() + "?Id_Usuario=" + id_user + "&Id_Grupo=" + row["Id_Grupo_Vista"].ToString() + "&Id_Vista=" + row["Id_Vista"].ToString());
                                pnl_navegacion.Controls.Add(lnk_vista);

                                //cargar title
                                var lbl_title = new HtmlGenericControl("p");
                                lbl_title.ID = "lbl_title" + db.Rows[0]["Id_Grupo_Vista"].ToString();
                                lbl_title.InnerHtml = db.Rows[0]["Nombre_Vista"].ToString();
                                pnl_title.Controls.Add(lbl_title);
                                //carga title///
                            }

                        }
                    }
                    else
                    {
                        var lnk_inicio = new HtmlGenericControl("a");
                        lnk_inicio.ID = "lnk_inicio";
                        lnk_inicio.InnerHtml = "<i class='fas fa-home'></i>" + "Inicio";
                        pnl_navegacion.Controls.Add(lnk_inicio);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        protected void Cargar_Datos(object sender, EventArgs e)
        {
            string id_user = "";
            AG_Utils utilidades = new AG_Utils();
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    //string appProgramacion = ConfigurationManager.AppSettings.Get("appProgramacion");
                    pathLog = Server.MapPath(@"~/logs");
                    aside.Attributes.Remove("style");
                    id_user = Request.QueryString["Id_Usuario"].ToString();

                    DataTable db;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Usuario = Convert.ToInt32(id_user);
                    db = Int_Usuarios_BRL.SelectTable(obj, 1);
                    if (db.Rows.Count > 0)
                    {
                        foreach (DataRow row in db.Rows)
                        {
                            var li_menu = new HtmlGenericControl("li");
                            string txt_icon = "<br/><span style='font-size:1rem;'>" + row["Nombre_Grupo"].ToString() + "</span>";
                            li_menu.ID = "li_menu" + row["Id_Grupo_Vista"].ToString();
                            li_menu.InnerHtml = row["Icono_Grupo"].ToString() + txt_icon;
                            li_menu.Attributes.Add("class", row["Class_Grupo"].ToString());
                            li_menu.Attributes.Add("title", row["Nombre_Grupo"].ToString());
                            ul_aside_menu.Controls.Add(li_menu);

                            DataTable db_a;
                            DCL.Int_Usuarios obj_a = new DCL.Int_Usuarios();
                            obj_a.Id_Usuario = Convert.ToInt32(id_user);
                            obj_a.Id_Rol = Convert.ToInt32(row["Id_Grupo_Vista"]);
                            db_a = Int_Usuarios_BRL.SelectTable(obj_a, 2);
                            if (db_a.Rows.Count > 0)
                            {
                                var ul_menu = new HtmlGenericControl("ul");
                                ul_menu.ID = "ul_menu" + row["Id_Grupo_Vista"].ToString();
                                ul_menu.Attributes.Add("class", "ul_aside_submenu");
                                li_menu.Controls.Add(ul_menu);




                                foreach (DataRow row_a in db_a.Rows)
                                {

                                    string url_actual = HttpContext.Current.Request.Url.AbsoluteUri;
                                    string[] cadena = url_actual.Split('/');
                                    string url = cadena[cadena.Length - 1];

                                    LinkButton lnk_vista = new LinkButton();
                                    //if (row_a[2].ToString() == "Programación")
                                    //{
                                    //    lnk_vista.Attributes.Add("href", appProgramacion);
                                    //    lnk_vista.Attributes.Add("target", "_blank");
                                    //}
                                    //else
                                    //{

                                    if (row_a["Ruta"].ToString().StartsWith("http"))
                                    {
                                        lnk_vista.Attributes.Add("href", "#");
                                        lnk_vista.Attributes.Add("target", "_blanck");
                                        lnk_vista.Attributes.Add("rel", "noopener noreferrer");

                                        // Agregar evento para recargar la página
                                        lnk_vista.Attributes.Add("onclick", $"window.open('{row_a["Ruta"].ToString()}', '_blank'); window.location.reload(); return false;");
                                    }
                                    else
                                    {
                                        lnk_vista.Attributes.Add("href", "../../" + row_a["Ruta"].ToString() + "?Id_Usuario=" + id_user + "&Id_Grupo=" + row["Id_Grupo_Vista"].ToString() + "&Id_Vista=" + row_a["Id_Vista"].ToString());
                                        lnk_vista.Attributes.Add("onclick", "loading_init();");
                                    }
                                    
                                   
                                    //}
                                    ul_menu.Controls.Add(lnk_vista);

                                    var li_sub = new HtmlGenericControl("li");
                                    li_sub.InnerHtml = row_a["Icono_Vista"].ToString() + row_a["Nombre_Vista"].ToString();
                                    lnk_vista.Controls.Add(li_sub);
                                }
                            }
                        }
                    }
                    else
                    {
                        var li_menu = new HtmlGenericControl("li");
                        string txt_icon = "<br/><span style='font-size:10px;'>Inicio</span>";
                        li_menu.InnerHtml = "<i class='fas fa-home'></i>" + txt_icon;
                        li_menu.Attributes.Add("class", "li-home");
                        ul_aside_menu.Controls.Add(li_menu);
                    }
                }
                else
                {
                    pnl_navegacion.Attributes.Add("style", "display: none;");
                }
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}\nUsuario:{id_user}", pathLog);
            }

        }

        protected void Cargar_Datos_Usuario()
      {
            string id_user = "";
            string ipServer = "";

            AG_Utils utilidades = new AG_Utils();
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    pathLog = Server.MapPath(@"~/logs");
                    string imagenPerfilLocal = $@"~/Content/img/perfilD.png";
                    string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                    string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                    string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                    ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                    bool conectaAdjuntos = utilidades.Ping(ipServer);
                    string numDocumento = "";//Session["identifacion"].ToString();
                    string rutaPerfilRemoto = "";
                    string rutaPerfilLocal = "";
                    id_user = Request.QueryString["Id_Usuario"].ToString();

                    DataTable dt;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Id_Usuario = Convert.ToInt32(id_user);
                    dt = Int_Usuarios_BRL.SelectTable(obj, 29);
                    string rutaCompleta = "";
                   
                    if (dt.Rows.Count > 0)
                    {
                        txt_perfil_name.InnerHtml = dt.Rows[0]["Nombre"].ToString();
                        string imagenPerfilRemoto = dt.Rows[0]["Anexo_Foto"].ToString();
                        Session["imagenRemota"] = imagenPerfilRemoto;
                        Session["numDocumento"] = dt.Rows[0]["Usuario"].ToString();
                        Session["CodSAE"] = dt.Rows[0]["CodSAE"].ToString();
                        Session["Cargo"] = dt.Rows[0]["Cargo"].ToString();
                        if (imagenPerfilRemoto.Contains("perfilD.png") || imagenPerfilRemoto.Contains("PerfilD.png"))
                        {
                            if (imagenPerfilRemoto == imagenPerfilLocal)
                            {
                                imagenPerfilLocal = imagenPerfilRemoto;
                            }
                            Session["imagenPerfil"] = imagenPerfilLocal;

                            HtmlGenericControl divHtml = new HtmlGenericControl();
                            Image image = new Image();
                            image.ImageUrl = imagenPerfilLocal;
                            image.AlternateText = "Imagen de Usuario";
                            image.Attributes["class"] = "img_user";
                            divHtml.Controls.Add(image);

                            HtmlGenericControl div = new HtmlGenericControl();
                            Image image2 = new Image();
                            image2.ImageUrl = imagenPerfilLocal;
                            image2.AlternateText = "Imagen de Usuario";
                            image2.Attributes["class"] = "img_user";
                            div.Controls.Add(image2);
                            pnl_imagen.Controls.Add(div);
                        }
                        else
                        {
                            var anexoFoto = imagenPerfilRemoto.Split('\\');
                            List<String> arrayAnexoFoto = new List<string>(anexoFoto);
                            int index = arrayAnexoFoto.FindIndex(x => x == ambiente);
                            int[] indexArray = new int[index];
                            var removerLista = new List<int>(indexArray);
                            numDocumento = arrayAnexoFoto[7].ToString();
                            
                            for (int i = 0; i < removerLista.Count; i++)
                            {
                                arrayAnexoFoto.RemoveAt(0);
                            }
                            rutaCompleta = String.Join("\\", arrayAnexoFoto);
                            string archivoPerfilLocal = $@"{pathServer}{rutaCompleta}";
                            Session["localTemp"] = archivoPerfilLocal;
                            //limpiarDatos();
                            List<string> rutalocalTemp = archivoPerfilLocal.Split('\\').ToList();
                            int indexRemota = rutalocalTemp.Count;
                            rutalocalTemp.RemoveAt(--indexRemota);
                            rutaPerfilLocal = String.Join(@"\", rutalocalTemp) + "\\";
                            rutaPerfilRemoto = @"\" + anexoFoto[2] + @"\" + anexoFoto[3] + @"\" + anexoFoto[4] + @"\" + anexoFoto[5] + @"\" + anexoFoto[6] + @"\" + anexoFoto[7] + @"\" + anexoFoto[8] + @"\";
                            rutaCompleta = $@"~/Imagenes/{rutaCompleta.Replace('\\', '/')}";
                            bool bl_CarpetaPerfilLocal = false;
                            bool bl_ImagenPerfilRemoto = false;
                            bool bl_ImagenPerfilLocal = false;

                            //Valida que se posean permisos de lectura y escritura a nivel de servidor.
                            if (utilidades.impersonateValidUser())
                            {
                                //Verifica que el servidor que indica la ruta de imagen, exista.
                                bool servAdjuntos = utilidades.Ping(anexoFoto[2].ToString());

                                bl_CarpetaPerfilLocal = Directory.Exists(rutaPerfilLocal);
                                bl_ImagenPerfilLocal = File.Exists(archivoPerfilLocal);

                                if (servAdjuntos)
                                {
                                    bl_ImagenPerfilRemoto = File.Exists(imagenPerfilRemoto);

                                    //throw new Exception();
                                    if (!bl_ImagenPerfilLocal && bl_CarpetaPerfilLocal)
                                    {
                                        var temp = Directory.GetFiles(rutaPerfilLocal, "*.*");
                                        if (temp.Length > 0)
                                        {
                                            foreach (var item in temp)
                                            {
                                                if (item.Contains($"{numDocumento}"))
                                                {
                                                    File.Delete(item);
                                                }
                                            }
                                            if (bl_ImagenPerfilRemoto)
                                            {
                                                File.Copy(imagenPerfilRemoto, $@"{archivoPerfilLocal}", true);
                                                bl_ImagenPerfilLocal = true;
                                            }
                                        }
                                        else
                                        {
                                            if (bl_ImagenPerfilRemoto)
                                            {
                                                File.Copy(imagenPerfilRemoto, $@"{archivoPerfilLocal}", true);
                                                bl_ImagenPerfilLocal = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!bl_CarpetaPerfilLocal && bl_ImagenPerfilRemoto)
                                        {
                                            Directory.CreateDirectory(rutaPerfilLocal);
                                            File.Copy(imagenPerfilRemoto, $@"{archivoPerfilLocal}", true);
                                            bl_ImagenPerfilLocal = true;
                                        }
                                        else
                                        {
                                            if (!bl_ImagenPerfilRemoto && bl_ImagenPerfilLocal && servAdjuntos)
                                            {
                                                File.Copy(archivoPerfilLocal, $@"{imagenPerfilRemoto}", true);
                                                bl_ImagenPerfilRemoto = true;
                                            }
                                        }
                                    }

                                    string ubicacionRemota = $@"{pathRemote}archivos_usuarios\{arrayAnexoFoto[3]}\";
                                    string rutaUsuario = $"{pathServer}{arrayAnexoFoto[0]}\\{arrayAnexoFoto[1]}\\{arrayAnexoFoto[2]}\\{numDocumento}\\";

                                    string[] files = null;
                                    files = System.IO.Directory.GetFiles(rutaUsuario,"*.*",SearchOption.AllDirectories);
                                    
                                    string carpetaOmitir = "Perfil";

                                    //AGR 12-May-2021: INICIO MANEJO DE ARCHIVOS
                                    //Si la ruta local contiene más de un archivo, procede a verificar imagenes y/o documentos y eliminarlos exceptuando imagen de perfil si (existe). Si en el servidor remoto NO existe el archivo, se envía.
                                    if (files.Length >1)
                                    {
                                        utilidades.limpiezaCarpetas(rutaUsuario, ubicacionRemota, carpetaOmitir, numDocumento, pathLog);
                                    }
                                   
                                }
                                else
                                {
                                    utilidades.logError($"{DateTime.Now}{CONST_ERROR}{anexoFoto[2]} no responde.\nRuta: {rutaPerfilRemoto.Replace(@"\\",@"\")}\n{CONST_USOIMAGENDEFAULT}\nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario:  {numDocumento}", pathLog);
                                }



                                utilidades.undoImpersonation();
                            }
                            HtmlGenericControl divHtml1 = new HtmlGenericControl();
                            Image image1 = new Image();
                            rutaCompleta = rutaCompleta.Replace('\\', '/');

                            HtmlGenericControl divHtml2 = new HtmlGenericControl();
                            Image image3 = new Image();
                            if (bl_ImagenPerfilLocal)
                            {
                                image1.ImageUrl = rutaCompleta;
                                image3.ImageUrl = rutaCompleta;
                                Session["imagenPerfil"] = rutaCompleta;
                            }
                            else
                            {
                                image1.ImageUrl = imagenPerfilLocal;
                                image3.ImageUrl = imagenPerfilLocal;
                                Session["imagenPerfil"] = imagenPerfilLocal;
                            }
                            
                            image1.AlternateText = "Imagen de Usuario";
                            image3.AlternateText = "Imagen de Usuario";
                            image1.Attributes["class"] = "img_user";
                            image3.Attributes["class"] = "img_user";
                            divHtml1.Controls.Add(image1);
                            divHtml2.Controls.Add(image3);
                            pnl_imagen.Controls.Add(divHtml2);
                            
                        }
                        DataTable db;
                        DCL.Int_Usuarios obje = new DCL.Int_Usuarios();
                        db = Int_Usuarios_BRL.SelectTable(obje, 39);
                        foreach (DataRow row in db.Rows)
                        {
                            LinkButton lnk_vista_perfil = new LinkButton();
                                lnk_vista_perfil.Attributes.Add("href", "../../" + row["Ruta"].ToString() + "?Id_Usuario=" + id_user + "&Id_Grupo=" + row["Id_Grupo_Vista"].ToString() + "&Id_Vista=" + row["Id_Vista"].ToString());
                            lnk_vista_perfil.Attributes.Add("onclick", "loading_init();");
                            ul_mdl_perfil.Controls.Add(lnk_vista_perfil);
                            var li_sub_pefil = new HtmlGenericControl("li");
                            li_sub_pefil.InnerHtml = row["Icono_Vista"].ToString() + row["Nombre_Vista"].ToString();
                            lnk_vista_perfil.Controls.Add(li_sub_pefil);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                utilidades.logError($"{DateTime.Now}{CONST_ERROR}{e.Message}", pathLog);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                utilidades.logError($"{DateTime.Now}{CONST_ERROR}{e.Message}", pathLog);
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}\nUsuario:{ id_user}", pathLog);

                HtmlGenericControl divHtml1 = new HtmlGenericControl();
                Image image1 = new Image();
                image1.ImageUrl = $@"~/Content/img/perfilD.png";
                image1.AlternateText = "Imagen de Usuario";
                image1.Attributes["class"] = "img_user";
                divHtml1.Controls.Add(image1);

                HtmlGenericControl div1 = new HtmlGenericControl();
                Image image3 = new Image();
                image3.ImageUrl = $@"~/Content/img/perfilD.png";
                image3.AlternateText = "Imagen de Usuario";
                image3.Attributes["class"] = "img_user";
                div1.Controls.Add(image3);
                pnl_imagen.Controls.Add(div1);
                DataTable db;
                DCL.Int_Usuarios obje = new DCL.Int_Usuarios();
                db = Int_Usuarios_BRL.SelectTable(obje, 39);
                foreach (DataRow row in db.Rows)
                {
                    LinkButton lnk_vista_perfil = new LinkButton();
                    lnk_vista_perfil.Attributes.Add("href", "../../" + row["Ruta"].ToString() + "?Id_Usuario=" + id_user + "&Id_Grupo=" + row["Id_Grupo_Vista"].ToString() + "&Id_Vista=" + row["Id_Vista"].ToString());
                    lnk_vista_perfil.Attributes.Add("onclick", "loading_init();");
                    ul_mdl_perfil.Controls.Add(lnk_vista_perfil);
                    var li_sub_pefil = new HtmlGenericControl("li");
                    li_sub_pefil.InnerHtml = row["Icono_Vista"].ToString() + row["Nombre_Vista"].ToString();
                    lnk_vista_perfil.Controls.Add(li_sub_pefil);
                }
            }
        }

        
        protected void Cerrar_Sesion(object sender, EventArgs e)
        {
            AG_Utils utilidades = new AG_Utils();
            string id_user = Request.QueryString["Id_Usuario"].ToString();
            string Final = "";
            try
            {
                pathLog = Server.MapPath(@"~/logs");
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string parametro = Request.QueryString["Id_Usuario"].ToString();
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                obj.Id_Usuario = Convert.ToInt32(parametro);
                Int_Usuarios_BRL.InsertOrUpdate(obj, 36);

                DCL.Int_Nucleo_Familiar nucleo_Familiar = new DCL.Int_Nucleo_Familiar();
                nucleo_Familiar.Id_IE = Convert.ToInt32(parametro);
                DataTable dataTable = Int_Nucleo_Familiar_BRL.SelectTable(nucleo_Familiar, 12);

                string usuario = dataTable.Rows[0]["Usuario"].ToString();
                if (Session["localTemp"] != null)
                {
                    var imagenPerfilLocal = Session["localTemp"].ToString().Split('\\');
                    List<String> arrayImagenRemota = new List<string>(imagenPerfilLocal);
                    arrayImagenRemota.Reverse();
                    int index = arrayImagenRemota.FindIndex(x => x == usuario);
                    if (index > 0)
                    {
                        int[] indexArray = new int[index];
                        var removerLista = new List<int>(indexArray);
                        for (int i = 0; i < removerLista.Count; i++)
                        {
                            arrayImagenRemota.RemoveAt(0);
                        }
                        arrayImagenRemota.Reverse();
                        string rutaCompleta = String.Join("\\", arrayImagenRemota) + "\\";


                        if (Directory.Exists(rutaCompleta))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(rutaCompleta);
                            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                            foreach (DirectoryInfo directory in directoryInfos)
                            {
                                if (directory.Name != "Perfil")
                                {
                                    directory.Delete(true);
                                }
                            }
                        }
                    }
                }

                Response.Cookies.Clear();
                Response.Cookies.Remove("");
                Session["cerrar"] = null;
                string url = Page.Request.Url.ToString();
                string[] Separado = url.Split('/');
                Final = Separado[Separado.Length - 1];

                //string url_ = Final.Replace(Final, $@"~/Login");
                Page.Response.Redirect($@"~/Login", false);
            }
            catch (Exception ex)
            {
                utilidades.logError($"{DateTime.Now}{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}\nUsuario:{ id_user}", pathLog);
                //string url_ = Final.Replace(Final, $@"~/Login");
                Page.Response.Redirect($@"~/Login", false);
                Console.WriteLine(ex);
            }
        }
    }
}