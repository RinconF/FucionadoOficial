using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BRL;
using System.Web.UI.HtmlControls;

namespace OfertaEmpleo
{
    public partial class Questions_uno : Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id_Usuario"] == null)
                {
                    Page.Response.Redirect("~/Login", false);
                    return;
                }
                if (Response.Cookies.Count == 0 && Session["cerrar"] == null)
                {
                    Page.Response.Redirect("~/Login", false);
                    return;
                }
                mostrar_preguntas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        protected void mostrar_preguntas()
        {
            try
            {
                int numerador_preguntas = 1;
                //int ini = Convert.ToInt32(Session["ini"]);
                //int fin = Convert.ToInt32(Session["fin"]);
                int contador = Convert.ToInt32(Session["competencia"]);
                string[] Nombre = (string[])Session["nombreC"];
                Titulo_Question.InnerText = Nombre[contador].ToString();
                DataTable dta;
                DCL.ENC_Respuesta obj2 = new ENC_Respuesta();
                obj2.Respuesta = Nombre[contador].ToString();
                dta = ENC_Respuesta_BRL.SelectTable(obj2, 10);
               string dato = dta.Rows[0]["Id_Competencia"].ToString();
                //if (dta.Rows.Count > 0) { } else { }

                Panel pnl_preguntas = new Panel();
                pnl_preguntas.ID = "pnl_preguntas";
                pnl_preguntas_container.Controls.Add(pnl_preguntas);

                Panel pnl_ul = new Panel();
                pnl_ul.ID = "pnl_ul";
                //pnl_ul.Attributes.Add("class", "list-group");
                pnl_preguntas_container.Controls.Add(pnl_ul);

                DataTable dt;
                DCL.ENC_Respuesta obj = new DCL.ENC_Respuesta();
                obj.Id_Usuario_Responde = Convert.ToInt32(Session["Id_Ingreso"]);
                obj.Calificacion = Convert.ToInt32(dato);
                dt = BRL.ENC_Respuesta_BRL.SelectTable(obj,7);
                Session["fin"] = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        
                        Panel pnl_li = new Panel();
                        pnl_li.ID = "pnl_li" + row["Id_Respuesta"].ToString();
                        pnl_li.Attributes.Add("class", "encuesta-pregunta");
                        pnl_ul.Controls.Add(pnl_li);

                        Label lbl_li_txt = new Label();
                        lbl_li_txt.ID = "lbl_li_txt" + row["Id_Respuesta"].ToString();
                        lbl_li_txt.Text = row["Id_Pregunta"].ToString() + " - " + row["Pregunta"].ToString() + "<span style='color:#ff0000;'>*</span>";
                        numerador_preguntas++;
                        pnl_li.Controls.Add(lbl_li_txt);

                        Panel encuesta_respuesta_contenedor = new Panel();
                        pnl_li.Controls.Add(encuesta_respuesta_contenedor);
                        Panel encuesta_respuesta_contenedor2 = new Panel();
                        pnl_li.Controls.Add(encuesta_respuesta_contenedor2);
                        Panel encuesta_respuesta_contenedor3 = new Panel();
                        pnl_li.Controls.Add(encuesta_respuesta_contenedor3);
                        Panel encuesta_respuesta_contenedor4 = new Panel();
                        pnl_li.Controls.Add(encuesta_respuesta_contenedor4);
                        Panel pnl_rb = new Panel();
                        pnl_rb.ID = "pnl_rb" + row["Id_Respuesta"].ToString();
                        //pnl_rb.Attributes.Add("class", "btn-group");
                        pnl_rb.Attributes.Add("role", "group");
                        pnl_rb.Attributes.Add("aria-label", "First group");
                        pnl_li.Controls.Add(pnl_rb);

                        var lbl_a = new HtmlGenericControl("label");
                        var lbl_b = new HtmlGenericControl("label");
                        var lbl_c = new HtmlGenericControl("label");
                        var lbl_d = new HtmlGenericControl("label");

                        RadioButton txt_rba = new RadioButton();
                        txt_rba.ID = "A" + row["Id_Respuesta"].ToString();
                        txt_rba.GroupName = "radio" + row["Id_Respuesta"].ToString();
                        txt_rba.Attributes.Add("value", "1");
                        txt_rba.Attributes.Add("class", "encuesta-radio");
                        txt_rba.CheckedChanged += new EventHandler(rdb_Click);
                        txt_rba.AutoPostBack = false;
                        //txt_rba.CssClass = "radio";
                        encuesta_respuesta_contenedor.Controls.Add(txt_rba);
                        lbl_a.InnerText = "A";
                        lbl_a.Attributes.Add("for", "MainContent_A" + row["Id_Respuesta"].ToString());
                        lbl_a.Attributes.Add("class", "encuesta-radio encuesta-etiqueta");
                        //lbl_a.Attributes.Add("onclick", "a()");
                        encuesta_respuesta_contenedor.Controls.Add(lbl_a);

                        RadioButton txt_rbb = new RadioButton();
                        txt_rbb.ID = "B" + row["Id_Respuesta"].ToString();
                        txt_rbb.GroupName = "radio" + row["Id_Respuesta"].ToString();
                        txt_rbb.Attributes.Add("value", "2");
                        txt_rbb.Attributes.Add("class", "encuesta-radio");
                        txt_rbb.CheckedChanged += new EventHandler(rdb_Click);
                        txt_rbb.AutoPostBack = false;
                        encuesta_respuesta_contenedor2.Controls.Add(txt_rbb);
                        lbl_b.InnerText = "B";
                        lbl_b.Attributes.Add("for", "MainContent_B" + row["Id_Respuesta"].ToString());
                        lbl_b.Attributes.Add("class", "encuesta-radio encuesta-etiqueta");
                        encuesta_respuesta_contenedor2.Controls.Add(lbl_b);

                        RadioButton txt_rbc = new RadioButton();
                        txt_rbc.ID = "C" + row["Id_Respuesta"].ToString();
                        txt_rbc.GroupName = "radio" + row["Id_Respuesta"].ToString();
                        txt_rbc.Attributes.Add("value", "3");
                        txt_rbc.Attributes.Add("class", "encuesta-radio");
                        txt_rbc.CheckedChanged += new EventHandler(rdb_Click);
                        txt_rbc.AutoPostBack = false;
                        encuesta_respuesta_contenedor3.Controls.Add(txt_rbc);
                        lbl_c.InnerText = "C";
                        lbl_c.Attributes.Add("for", "MainContent_C" + row["Id_Respuesta"].ToString());
                        lbl_c.Attributes.Add("class", "encuesta-radio encuesta-etiqueta");
                        encuesta_respuesta_contenedor3.Controls.Add(lbl_c);

                        RadioButton txt_rbd = new RadioButton();
                        txt_rbd.ID = "D" + row["Id_Respuesta"].ToString();
                        txt_rbd.GroupName = "radio" + row["Id_Respuesta"].ToString();
                        txt_rbd.Attributes.Add("value", "4");
                        txt_rbd.Attributes.Add("class", "encuesta-radio");
                        txt_rbd.CheckedChanged += new EventHandler(rdb_Click);
                        txt_rbd.AutoPostBack = false;
                        encuesta_respuesta_contenedor4.Controls.Add(txt_rbd);
                        lbl_d.InnerText = "D";
                        lbl_d.Attributes.Add("for", "MainContent_D" + row["Id_Respuesta"].ToString());
                        lbl_d.Attributes.Add("class", "encuesta-radio encuesta-etiqueta");
                        encuesta_respuesta_contenedor4.Controls.Add(lbl_d);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rdb_Click(object sender, EventArgs e)
        {
            string id = ((RadioButton)sender).ID;
            string texto = ((RadioButton)sender).Text;

            DCL.ENC_Respuesta obj = new DCL.ENC_Respuesta();
            obj.Id_Respuesta = Convert.ToInt32(id.Substring(1,id.Length-1));
            obj.Respuesta = id.Substring(0, 1);
            ENC_Respuesta_BRL.InsertOrUpdate(obj,8);
        }

        protected void btn_siguientes_datos(object sender, EventArgs e)
        {
            try
            {
                int ini = Convert.ToInt32(Session["ini"]);
                int fin = Convert.ToInt32(Session["fin"]);

                int validacion = Convert.ToInt32(Session["validacion"]);
                int campos_completos = validacion + fin;
                int Sesion_Ingreso = Convert.ToInt32(Session["Id_Ingreso"]);
                DataTable dt_ = ENC_Respuesta_BRL.SelectTable(new ENC_Respuesta() { Id_Usuario_Responde = Sesion_Ingreso, Inicio = ini, Fin = campos_completos  }, 9);
                if (dt_.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "validacion_msg();", true);
                }
                else
                {
                    DataTable dt;
                    int _fin = Convert.ToInt32(Session["fin"]);
                    DCL.ENC_Respuesta obj = new DCL.ENC_Respuesta();
                    obj.Id_Usuario_Responde = Sesion_Ingreso;
                    dt = ENC_Respuesta_BRL.SelectTable(obj, 11);



                    if (dt.Rows.Count > 0)
                    {
                        ENC_Respuesta_BRL.InsertOrUpdate(obj, 12);
                        Response.Redirect("/Vistas/V_Encuesta_Organizacional/Questions_Observacion.aspx?Id_Usuario=" + Request.QueryString["Id_Usuario"] + "&Id_Grupo=" + Request.QueryString["Id_Grupo"] + (String.IsNullOrEmpty(Request.QueryString["Id_Vista"]) ? "" : "Id_Vista=" + Request.QueryString["Id_Vista"]), false);
                    }
                    else
                    {
                        //Session["ini"] = Convert.ToInt32(Session["ini"]) + 10;
                        //Session["fin"] = Convert.ToInt32(Session["fin"]) + 10;
                        Session["competencia"] = Convert.ToInt32(Session["competencia"]) + 1;
                        Session["validacion"] = campos_completos;
                        Response.Redirect("/Vistas/V_Encuesta_Organizacional/Questions_uno.aspx?Id_Usuario=" + Request.QueryString["Id_Usuario"] + "&Id_Grupo=" + Request.QueryString["Id_Grupo"] + (String.IsNullOrEmpty(Request.QueryString["Id_Vista"]) ? "" : "Id_Vista=" + Request.QueryString["Id_Vista"]), false);
                    }
                }  
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}