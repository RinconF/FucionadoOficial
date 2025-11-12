using BRL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Intranet_3._0.Vistas.V_Solicitudes
{
    /// <summary>
    /// Descripción breve de WebService_V_Solicitudes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebService_V_Solicitudes : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string[]> ObtenerDatosSolicitudActualizar(string idSolicitud)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_Solicitud = Convert.ToInt32(idSolicitud);
                DataTable dt = Int_Solicitud_BRL.SelectTable(solicitud, 4);
                if (dt.Rows.Count > 0)
                {
                    string[] array = new string[13];
                    array[0] = dt.Rows[0]["Id_Solicitud"].ToString();
                    array[1] = dt.Rows[0]["Id_TipoSolicitud"].ToString();
                    array[2] = dt.Rows[0]["FechaTentativa1"].ToString();
                    array[3] = dt.Rows[0]["FechaTentativa2"].ToString();
                    array[4] = dt.Rows[0]["FechaTentativa3"].ToString();
                    array[5] = dt.Rows[0]["Observacion"].ToString();
                    array[6] = dt.Rows[0]["EstadoSolicitud"].ToString();
                    array[7] = dt.Rows[0]["Nombres"].ToString();
                    array[8] = dt.Rows[0]["N_Identificacion"].ToString();
                    array[9] = dt.Rows[0]["Cargo"].ToString();
                    array[10] = dt.Rows[0]["Codigo_sae"].ToString();
                    array[11] = dt.Rows[0]["FechaCreacion"].ToString();
                    



                    list.Add(array);

                    return list;
                }
                else
                {
                    string[] array = new string[dt.Rows.Count];
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
        public List<string[]> ObtenerDatosSolicitud(string idSolicitud)
        {
            try
            {
                List<string[]> list = new List<string[]>();
                Int_Solicitud solicitud = new Int_Solicitud();
                if (idSolicitud !="")
                {
                    solicitud.Id_Solicitud = Convert.ToInt32(idSolicitud);
                    DataTable dt = Int_Solicitud_BRL.SelectTable(solicitud, 8);
                    if (dt.Rows.Count > 0)
                    {
                        string[] array = new string[5];
                        array[0] = dt.Rows[0]["Id_Solicitud"].ToString();
                        array[1] = dt.Rows[0]["TipoSolicitud"].ToString();
                        array[2] = dt.Rows[0]["FechaAprobada"].ToString();
                        array[3] = dt.Rows[0]["Observacion"].ToString();
                        array[4] = dt.Rows[0]["EstadoSolicitud"].ToString();


                        list.Add(array);

                        return list;
                    }
                    else
                    {
                        string[] array = new string[dt.Rows.Count];
                        array[0] = "0";
                        list.Add(array);
                        return list;
                    }
                }
                return list;

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
        public string ObtenerFechaNacimiento(string Id_Usuario)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_Solicitud = Convert.ToInt32(Id_Usuario);
                DataTable dt = Int_Solicitud_BRL.SelectTable(solicitud, 2);
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToDateTime(dt.Rows[0]["Fecha_Nacimiento"].ToString()).ToString("yyyy-MM-dd");
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebMethod]
        public string AnularSolicitud(string idSolicitud)
        {
            try
            {
                Int_Solicitud solicitud = new Int_Solicitud();
                solicitud.Id_EstadoSolicitud = 5;
                solicitud.Id_Solicitud = Convert.ToInt32(idSolicitud);
                Int_Solicitud_BRL.InsertarOrUpdate(solicitud, 9);
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
                throw ex;
            }
        }
    }
}
