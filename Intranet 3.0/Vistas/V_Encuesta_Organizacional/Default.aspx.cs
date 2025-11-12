using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BRL;

namespace OfertaEmpleo
{
    public partial class _Default : System.Web.UI.Page
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
                
                if (!IsPostBack)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "alerta();", true);
                    Session["ini"] = 1;
                    Session["fin"] = 10;
                    Session["competencia"] = 0;
                    Session["validacion"] = 0;
                    txt_cc.Text = Session["cc"].ToString();

                    //string[] ss = { "BIENESTAR", "ESTRATÉGICA", "SEGURIDAD Y SALUD EN EL TRABAJO", "TRABAJO EN EQUIPO", "COMUNICACIÓN", "DESARROLLO PERSONAL"}; JGC
                    string[] ss = { "BIENESTAR", "COMUNICACIÓN", "DESARROLLO PERSONAL", "TRABAJO EN EQUIPO / LIDERAZGO", "SEGURIDAD Y SALUD EN EL TRABAJO", "ESTRATEGIA"};
                    Session["nombreC"] = ss;

                    llenar_drop_proceso();
                    //llenar_dropdown_sedes();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void llenar_dropdown_sedes()
        //{
        //    try
        //    {
        //        DataTable dt_sedes = ENC_Encuesta_BRL.SelectTable(new ENC_Encuesta() { }, 8);

        //        drop_sede.DataSource = dt_sedes;
        //        drop_sede.DataTextField = "Desc_Sede";
        //        drop_sede.DataValueField = "Id_Sede";
        //        drop_sede.DataBind();
        //        drop_sede.Items.Insert(0, new ListItem("-Seleccione UNE-", ""));


        //        //edades
        //        DataTable dt_edad = ENC_Encuesta_BRL.SelectTable(new ENC_Encuesta() { }, 19);

        //        drop_edad.DataSource = dt_edad;
        //        drop_edad.DataTextField = "Descripcion";
        //        drop_edad.DataValueField = "Id_Edad";
        //        drop_edad.DataBind();
        //        drop_edad.Items.Insert(0, new ListItem("-Seleccione promedio edad-", ""));

        //        //antiguedad
        //        DataTable dt_antiguedad = ENC_Encuesta_BRL.SelectTable(new ENC_Encuesta() { }, 20);

        //        drop_antiguedad.DataSource = dt_antiguedad;
        //        drop_antiguedad.DataTextField = "Descripcion";
        //        drop_antiguedad.DataValueField = "Id_Antiguedad";
        //        drop_antiguedad.DataBind();
        //        drop_antiguedad.Items.Insert(0, new ListItem("-Seleccione antigüedad en la compañía-", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void llenar_drop_proceso()
        {
            try
            {
                DataTable dt_ = ENC_Encuesta_BRL.SelectTable(new ENC_Encuesta() { }, 7);

                drop_proceso.DataSource = dt_;
                drop_proceso.DataTextField = "Nombre";
                drop_proceso.DataValueField = "Id_Proceso";
                drop_proceso.DataBind();
                drop_proceso.Items.Insert(0, new ListItem("-Seleccione proceso-", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Registro usuario desde x
        protected void btn_registro_encuesta(object sender, EventArgs e)
        {

            ENC_Ingreso_Encuesta ObjEIE = new ENC_Ingreso_Encuesta();
            DataTable dt = ENC_Ingreso_Encuesta_BRL.SelectTable(ObjEIE, 0);

            Session["Id_Ingreso"] = dt.Rows[0]["Id_Ingreso_Encuesta"];
            Session["identificaion"] = txt_cc.Text;
        }

        //Registro datos de empleado
        protected void btn_registro_encuenta_inicio(object sender, EventArgs e)
        {

            DataTable dt;
            try
            {
                if (!String.IsNullOrEmpty(drop_proceso.Text.Trim()) && !String.IsNullOrEmpty(txt_cc.Text.Trim()))
                {
                    DataTable dto;
                    ENC_Ingreso_Encuesta_Pertenece_2 objEnc = new ENC_Ingreso_Encuesta_Pertenece_2();
                    objEnc.N_Identificacion = txt_cc.Text;


                    ENC_Ingreso_Encuesta_Pertenece_2 obj2 = new ENC_Ingreso_Encuesta_Pertenece_2();
                    obj2.N_Identificacion = txt_cc.Text;
                    dt = ENC_Ingreso_Encuesta_Pertenece_BRL_2.SelectTable(obj2, 1);
                    dto = ENC_Ingreso_Encuesta_Pertenece_BRL_2.SelectTable(obj2, 2);

                    if (dto.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "validacion_msg_defa3();", true);
                    }
                    else
                    {
                        //if (dt.Rows.Count > 0)
                        //{
                        //    btn_inicio_encuesta.Enabled = false;
                        //}
                        //else { }

                        if (dt.Rows.Count > 0)
                        {
                            ENC_Ingreso_Encuesta ObjEIE = new ENC_Ingreso_Encuesta();
                            DataTable dtx = ENC_Ingreso_Encuesta_BRL.SelectTable(ObjEIE, 0);

                            Session["Id_Ingreso"] = dtx.Rows[0]["Id_Ingreso_Encuesta"];
                            Session["identificaion"] = txt_cc.Text;
                            btn_inicio_encuesta.Enabled = false;
                            //ScriptManager.RegisterStartupScript(this.Page, GetType(), "Pop", "deshabilitar_btn_inicio();", true);
                            int Sesion_Ingreso = Convert.ToInt32(Session["Id_Ingreso"]);
                            var ss = dt.Rows[0]["Id_Sede"].ToString();
                            var aa = dt.Rows[0]["Id_IE"].ToString();

                            ENC_Ingreso_Encuesta_Pertenece_2 obj1 = new ENC_Ingreso_Encuesta_Pertenece_2();
                            obj1.Id_IE = Convert.ToInt32(aa);
                            obj1.Id_Sede = ss;
                            obj1.Id_Proceso = drop_proceso.SelectedValue;
                            obj1.Id_Ingreso_Encuesta = Sesion_Ingreso.ToString();
                            ENC_Ingreso_Encuesta_Pertenece_BRL_2.InsertOrUpdate(obj1, 0);
                            Session["operador"] = drop_proceso.SelectedValue;

                            Response.Redirect("/Vistas/V_Encuesta_Organizacional/Questions_uno.aspx?Id_Usuario=" + Request.QueryString["Id_Usuario"] + "&Id_Grupo=" + Request.QueryString["Id_Grupo"] + (String.IsNullOrEmpty(Request.QueryString["Id_Vista"]) ? "" : "Id_Vista=" + Request.QueryString["Id_Vista"]), false);
                        }
                        else { ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "validacion_msg_defa2();", true); }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "validacion_msg_defa();", true);
                }
                //if (
                //        (!String.IsNullOrEmpty(drop_sede.Text.Trim())) &&
                //        (!String.IsNullOrEmpty(drop_proceso.Text.Trim())) &&
                //        (!String.IsNullOrEmpty(drop_edad.Text.Trim())) &&
                //        (!String.IsNullOrEmpty(drop_antiguedad.Text.Trim()))
                //   )
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "Pop", "deshabilitar_btn_inicio();", true);
                //    int Sesion_Ingreso = Convert.ToInt32(Session["Id_Ingreso"]);
                //    ENC_Ingreso_Encuesta_Pertenece_BRL.InsertOrUpdate(new ENC_Ingreso_Encuesta_Pertenece()
                //    {
                //        Id_Sede = drop_sede.SelectedValue,
                //        Id_Proceso = drop_proceso.SelectedValue,
                //        Id_Ingreso_Encuesta = Sesion_Ingreso.ToString(),
                //        Edad = drop_edad.Text.ToString(),
                //        Antiguedad = drop_antiguedad.Text.ToString()

                //    }, 0);

                //    Session["operador"] = drop_proceso.SelectedValue;

                //    Response.Redirect("Questions/Questions_uno");
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "validacion_msg_defa();", true);
                //}
                   }  
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}