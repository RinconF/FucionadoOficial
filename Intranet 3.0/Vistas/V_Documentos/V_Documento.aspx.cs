using System;
using System.IO;
using System.Web.UI;
using BRL;
using DCL;

namespace Intranet_3._0.Vistas.V_Documentos
{
    public partial class V_Documentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDocumentos();
            }
        }

        private void CargarDocumentos()
        {
            try
            {
                // Action 1: Solo documentos activos
                Int_Documentos obj = new Int_Documentos();
                Int_DocumentosCollection documentos = Int_Documentos_BRL.SelectByParams(obj, 1);

                if (documentos != null && documentos.Count > 0)
                {
                    rptDocumentos.DataSource = documentos;
                    rptDocumentos.DataBind();
                    phDocumentosVacio.Visible = false;
                }
                else
                {
                    rptDocumentos.DataSource = null;
                    rptDocumentos.DataBind();
                    phDocumentosVacio.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Log error
                rptDocumentos.DataSource = null;
                rptDocumentos.DataBind();
                phDocumentosVacio.Visible = true;
            }
        }

        protected string ObtenerIconoDocumento(string rutaArchivo)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
                return "fas fa-file fa-3x";

            string extension = Path.GetExtension(rutaArchivo).ToLower();

            switch (extension)
            {
                case ".pdf":
                    return "fas fa-file-pdf fa-3x";
                case ".doc":
                case ".docx":
                    return "fas fa-file-word fa-3x";
                case ".xls":
                case ".xlsx":
                    return "fas fa-file-excel fa-3x";
                case ".ppt":
                case ".pptx":
                    return "fas fa-file-powerpoint fa-3x";
                case ".zip":
                case ".rar":
                    return "fas fa-file-archive fa-3x";
                default:
                    return "fas fa-file fa-3x";
            }
        }
    }
}