using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;

namespace Intranet_3._0.Vistas.V_Perfil
{
    public partial class V_Aplicativos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            { 
                LinkDesprediblesNomina.PreRender += new EventHandler(LinkDesprediblesNomina_PreRender); 
            }
        }
        protected void LinkDesprediblesNomina_Click(object sender, EventArgs e)
        { 
            // Obtener el esquema (http o https)
            string scheme = Request.Url.Scheme; 
            
            // Obtener el nombre del host (localhost, dominio.com, etc.)
            string host = Request.Url.Host; 
            
            // Obtener el puerto si es diferente del puerto por defecto (80 para http, 443 para https)
            string port = Request.Url.Port != 80 && Request.Url.Port != 443 ? ":" + Request.Url.Port.ToString() : 
                string.Empty; 
            
            // Obtener el Id_Usuario de la URL actual
            string id_user = Request.QueryString["Id_Usuario"].ToString();

            // Construir la URL completa incluyendo el dominio, puerto y ruta 
            string newUrl = $"{scheme}://{host}{port}/Vistas/V_Nomina/V_DesprendiblesNomina?Id_Usuario={id_user}&Id_Grupo=6&Id_Vista=22"; 
            
            // Redirigir a la nueva URL
            Response.Redirect(newUrl); 
        }

        protected void LinkDesprediblesNomina_PreRender(object sender, EventArgs e) 
        { 
            LinkDesprediblesNomina.Attributes.Add("data-name", "DESPRENDIBLES"); 
            LinkDesprediblesNomina.Attributes.Add("data-description", "DE PAGO"); 
        }
    }
}