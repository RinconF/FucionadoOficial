using System;
using System.Data;
namespace DCL
{
    public class Int_Control_Acceso
    {
        #region Propiedades 
        Int32? mvarId_Ctrl_Acso = null;
        public Int32? Id_Ctrl_Acso
        {
            get { return mvarId_Ctrl_Acso; }
            set { mvarId_Ctrl_Acso = value; }
        }

        String mvarId_IE = null;
        public String Id_IE
        {
            get { return mvarId_IE; }
            set { mvarId_IE = value; }
        }

        String mvarIdentificacion = null;
        public String Identificacion
        {
            get { return mvarIdentificacion; }
            set { mvarIdentificacion = value; }
        }

        String mvarFecha_Hora = null;
        public String Fecha_Hora
        {
            get { return mvarFecha_Hora; }
            set { mvarFecha_Hora = value; }
        }

        String mvarId_Tp_Acso = null;
        public String Id_Tp_Acso
        {
            get { return mvarId_Tp_Acso; }
            set { mvarId_Tp_Acso = value; }
        }

        String mvarId_Sede = null;
        public String Id_Sede
        {
            get { return mvarId_Sede; }
            set { mvarId_Sede = value; }
        }

        String mvarUsuario_Creacion = null;
        public String Usuario_Creacion
        {
            get { return mvarUsuario_Creacion; }
            set { mvarUsuario_Creacion = value; }
        }

        String mvarTemperatura = null;
        public String Temperatura
        {
            get { return mvarTemperatura; }
            set { mvarTemperatura = value; }
        }
        #endregion
        #region Constructores 
        public Int_Control_Acceso() { }
        public Int_Control_Acceso(
            Int32? varId_Ctrl_Acso,
            String varId_IE,
            String varIdentificacion,
            String varFecha_Hora,
            String varId_Tp_Acso,
            String varId_Sede,
            String varUsuario_Creacion,
            String varTemperatura
            )
        {
            mvarId_Ctrl_Acso = varId_Ctrl_Acso;
            mvarId_IE = varId_IE;
            mvarIdentificacion = varIdentificacion;
            mvarFecha_Hora = varFecha_Hora;
            mvarId_Tp_Acso = varId_Tp_Acso;
            mvarId_Sede = varId_Sede;
            mvarUsuario_Creacion = varUsuario_Creacion;
            mvarTemperatura = varTemperatura;
        }
        public Int_Control_Acceso(IDataRecord obj)
        {
            mvarId_Ctrl_Acso = Convert.ToInt32(obj["Id_Ctrl_Acso"]);
            mvarId_IE = Convert.ToString(obj["Id_IE"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);
            mvarFecha_Hora = Convert.ToString(obj["Fecha_Hora"]);
            mvarId_Tp_Acso = Convert.ToString(obj["Id_Tp_Acso"]);
            mvarId_Sede = Convert.ToString(obj["Id_Sede"]);
            mvarUsuario_Creacion = Convert.ToString(obj["Usuario_Creacion"]);
            mvarTemperatura = Convert.ToString(obj["Temperatura"]);

        }
        public Int_Control_Acceso(DataRow obj)
        {
            mvarId_Ctrl_Acso = Convert.ToInt32(obj["Id_Ctrl_Acso"]);
            mvarId_IE = Convert.ToString(obj["Id_IE"]);
            mvarIdentificacion = Convert.ToString(obj["Identificacion"]);
            mvarFecha_Hora = Convert.ToString(obj["Fecha_Hora"]);
            mvarId_Tp_Acso = Convert.ToString(obj["Id_Tp_Acso"]);
            mvarId_Sede = Convert.ToString(obj["Id_Sede"]);
            mvarUsuario_Creacion = Convert.ToString(obj["Usuario_Creacion"]);
            mvarTemperatura = Convert.ToString(obj["Temperatura"]);
        }
        #endregion
    }

}
