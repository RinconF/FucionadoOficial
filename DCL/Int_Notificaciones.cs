using System;
using System.Data;
namespace DCL
{
    public class Int_Notificaciones
    {
        #region Propiedades 
        Int32? mvarId_Notificacion = null;
        public Int32? Id_Notificacion
        {
            get { return mvarId_Notificacion; }
            set { mvarId_Notificacion = value; }
        }
        String mvarId_Usuario_Emisor = null;
        public String Id_Usuario_Emisor
        {
            get { return mvarId_Usuario_Emisor; }
            set { mvarId_Usuario_Emisor = value; }
        }
        String mvarId_Usuario_Receptor = null;
        public String Id_Usuario_Receptor
        {
            get { return mvarId_Usuario_Receptor; }
            set { mvarId_Usuario_Receptor = value; }
        }
        String mvarAsunto = null;
        public String Asunto
        {
            get { return mvarAsunto; }
            set { mvarAsunto = value; }
        }
        String mvarMensaje = null;
        public String Mensaje
        {
            get { return mvarMensaje; }
            set { mvarMensaje = value; }
        }
        String mvarFecha_Creacion = null;
        public String Fecha_Creacion
        {
            get { return mvarFecha_Creacion; }
            set { mvarFecha_Creacion = value; }
        }
        #endregion
        #region Constructores 
        public Int_Notificaciones() { }
        public Int_Notificaciones(
            Int32? varId_Notificacion,
            String varId_Usuario_Emisor,
            String varId_Usuario_Receptor,
            String varAsunto,
            String varMensaje,
            String varFecha_Creacion
            )
        {
            mvarId_Notificacion = varId_Notificacion;
            mvarId_Usuario_Emisor = varId_Usuario_Emisor;
            mvarId_Usuario_Receptor = varId_Usuario_Receptor;
            mvarAsunto = varAsunto;
            mvarMensaje = varMensaje;
            mvarFecha_Creacion = varFecha_Creacion;
        }
        public Int_Notificaciones(IDataRecord obj)
        {
            mvarId_Notificacion = Convert.ToInt32(obj["Id_Notificacion"]);
            mvarId_Usuario_Emisor = Convert.ToString(obj["Id_Usuario_Emisor"]);
            mvarId_Usuario_Receptor = Convert.ToString(obj["Id_Usuario_Receptor"]);
            mvarAsunto = Convert.ToString(obj["Asunto"]);
            mvarMensaje = Convert.ToString(obj["Mensaje"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);

        }
        public Int_Notificaciones(DataRow obj)
        {
            mvarId_Notificacion = Convert.ToInt32(obj["Id_Notificacion"]);
            mvarId_Usuario_Emisor = Convert.ToString(obj["Id_Usuario_Emisor"]);
            mvarId_Usuario_Receptor = Convert.ToString(obj["Id_Usuario_Receptor"]);
            mvarAsunto = Convert.ToString(obj["Asunto"]);
            mvarMensaje = Convert.ToString(obj["Mensaje"]);
            mvarFecha_Creacion = Convert.ToString(obj["Fecha_Creacion"]);
        }
        #endregion
    }

}
