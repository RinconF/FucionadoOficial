using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Intranet_3._0
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["cerrar"] != null)
            {
                if (Session["cerrar"].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "alert('La sesion ha expirado');", true);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login_Datos(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txt_user.Text.Trim()) && !String.IsNullOrEmpty(txt_pass.Text.Trim()))
                {
                    int tiempoSesion = Convert.ToInt32(ConfigurationManager.AppSettings.Get("tiempoSesion"));
                    DCL.Int_Usuarios int_Usuarios = new DCL.Int_Usuarios();
                    int_Usuarios.Usuario = txt_user.Text.Trim();
                    DataTable dataTable = Int_Usuarios_BRL.SelectTable(int_Usuarios, 63);
                    if (dataTable.Rows.Count == 0)
                    {
                        //paso por parametro a la funcion vacio la funcion mostrar_iniciar_sesion para que no recargue en restablecer clave JGC
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "error", "validar_existe(mostrar_iniciar_sesion());", true);
                        return;
                    }


                    string encrytar = GetMD5(txt_pass.Text.Trim());
                    DataTable db;
                    DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                    obj.Usuario = txt_user.Text.Trim();
                    DataTable table = Int_Usuarios_BRL.SelectTable(obj, 59);
                    if (table.Rows[0][0].ToString() == "1")
                    {
                        if (txt_pass.Text.Trim() == dataTable.Rows[0]["FechaExpedicion"].ToString())
                        {
                            encrytar = GetMD5(dataTable.Rows[0]["FechaExpedicion"].ToString());
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "error_", "validar_datos();", true);
                            return;
                        }
                    }

                    obj.Contraseña = encrytar;
                    db = Int_Usuarios_BRL.SelectTable(obj, 0);
                    if (db.Rows.Count > 0)
                    {
                        if (db.Rows[0]["Id_Usuario"].ToString() == "NO")
                        {
                            //paso por parametro a la funcion vacio la funcion mostrar_iniciar_sesion para que no recargue en restablecer clave JGC
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "error", "validar_existe(mostrar_iniciar_sesion());", true);
                            return;
                        }
                        if (db.Rows[0]["Estado"].ToString() != "False")
                        {
                            DataTable db_;
                            DCL.Int_Usuarios obj_i = new DCL.Int_Usuarios();
                            obj_i.Id_Usuario = Convert.ToInt32(db.Rows[0]["Id_Usuario"]);
                            db_ = Int_Usuarios_BRL.SelectTable(obj_i, 35);
                            if (db_.Rows.Count == 0 || db_.Rows.Count != 0)
                            {
                                DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                                obj_.Id_Usuario = Convert.ToInt32(db.Rows[0]["Id_Usuario"]);
                                Int_Usuarios_BRL.InsertOrUpdate(obj_, 34);

                                HttpCookie httpCookie = new HttpCookie("login");
                                httpCookie.Values.Add("userid", db.Rows[0]["Id_Usuario"].ToString());
                                httpCookie.Expires = DateTime.Now.AddMinutes(tiempoSesion);
                                Response.Cookies.Add(httpCookie);
                                Session["cerrar"] = "1";
                                Session["cc"] = txt_user.Text;

                                //Inserta automaticamente el codigo_sae 
                                Int_Usuarios_BRL.InsertOrUpdate(int_Usuarios, 68);


                                Response.Redirect
                                (
                                    "./Default.aspx" +
                                    "?Id_Usuario=" + db.Rows[0]["Id_Usuario"].ToString(), false
                                );
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "sesion", "sesion();", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "error", "validar();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "error_", "validar_datos();", true);
                    }
                }
                else
                {
                    //paso por parametro a la funcion vacio la funcion mostrar_iniciar_sesion para que no recargue en restablecer clave JGC
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "vacio", "vacio(mostrar_iniciar_sesion());", true);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }


        protected void Restablecer_Clave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txt_cc.Text.Trim()) && !String.IsNullOrEmpty(txt_fec_exp.Text.Trim()))
                {
                    int tiempoSesion = Convert.ToInt32(ConfigurationManager.AppSettings.Get("tiempoSesion"));
                    DCL.Int_Usuarios int_Usuarios = new DCL.Int_Usuarios();
                    int_Usuarios.Usuario = txt_cc.Text.Trim();
                    DataTable dataTable = Int_Usuarios_BRL.SelectTable(int_Usuarios, 67);
                    if (dataTable.Rows.Count == 0)
                    {
                        //paso por parametro a la funcion vacio la funcion mostrar_iniciar_sesion para que no recargue en restablecer clave JGC
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "error", "validar_existe(mostrar_restablecer_clave());", true);
                        return;
                    }
                    else if (dataTable.Rows[0]["FechaExpedicion"].ToString() == txt_fec_exp.Text.Trim())
                    {
                        DCL.Int_Usuarios obj_ = new DCL.Int_Usuarios();
                        obj_.Id_Usuario = Convert.ToInt32(dataTable.Rows[0]["Id_Usuario"]);
                        Int_Usuarios_BRL.InsertOrUpdate(obj_, 34);
                        HttpCookie httpCookie = new HttpCookie("login");
                        httpCookie.Values.Add("userid", dataTable.Rows[0]["Id_Usuario"].ToString());
                        httpCookie.Expires = DateTime.Now.AddMinutes(tiempoSesion);
                        Response.Cookies.Add(httpCookie);
                        Session["cerrar"] = "1";
                        //se crea un objeto cambio pass y una tabla cambio pass para colocar de contraseña el numero de documento y al ingresar se dispare el evento Cambio de contraseña obligatorio JGC
                        DCL.Int_Usuarios obj_cambiopass = new DCL.Int_Usuarios();
                        obj_cambiopass.Id_Usuario = obj_.Id_Usuario;
                        DataTable dt_cambiopass = Int_Usuarios_BRL.SelectTable(obj_cambiopass, 29);
                        obj_cambiopass.Usuario = dt_cambiopass.Rows[0]["Usuario"].ToString();
                        obj_cambiopass.Contraseña = GetMD5(dt_cambiopass.Rows[0]["Usuario"].ToString());
                        Int_Usuarios_BRL.InsertOrUpdate(obj_cambiopass, 47);
                        Session["cc"] = txt_cc.Text;

                        Response.Redirect
                                (
                                    "./Default.aspx" +
                                    "?Id_Usuario=" + dataTable.Rows[0]["Id_Usuario"].ToString(), false
                                );

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "error_", "validar_datos_rc();", true);
                    }

                }
                else
                {
                    //paso por parametro a la funcion vacio la funcion mostrar_restablecer_clave para que no recargue en inicio JGC
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "vacio", "vacio(mostrar_restablecer_clave());", true);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
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