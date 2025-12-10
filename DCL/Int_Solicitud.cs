using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class Int_Solicitud
    {
        private int? mvarId_Solicitud;
        private int? mvarId_TipoSolicitud;
        private int? mvarId_EstadoSolicitud;
        private string mvarFechaTentativa1;
        private string mvarFechaTentativa2;
        private string mvarFechaTentativa3;
        private string mvarObservacion;
        private string mvarId_UsuarioGestiona;
        private string mvarFechaAprovada;
        private string mvarId_UsuarioCreacion;
        private string mvarId_UsuarioActualizacion;
        private string mvarFechaCreacion;
        private string mvarFechaActualizacion;

        public int? Id_Solicitud
        {
            get
            {
                return mvarId_Solicitud;
            }
            set
            {
                mvarId_Solicitud = value;
            }
        }

        public int? Id_TipoSolicitud
        {
            get
            {
                return mvarId_TipoSolicitud;
            }
            set
            {
                mvarId_TipoSolicitud = value;
            }
        }

        public int? Id_EstadoSolicitud
        {
            get
            {
                return mvarId_EstadoSolicitud;
            }
            set
            {
                mvarId_EstadoSolicitud = value;
            }
        }

        public string FechaTentativa1
        {
            get
            {
                return mvarFechaTentativa1;
            }
            set
            {
                mvarFechaTentativa1 = value;
            }
        }

        public string FechaTentativa2
        {
            get
            {
                return mvarFechaTentativa2;
            }
            set
            {
                mvarFechaTentativa2 = value;
            }
        }

        public string FechaTentativa3
        {
            get
            {
                return mvarFechaTentativa3;
            }
            set
            {
                mvarFechaTentativa3 = value;
            }
        }

        public string Observacion
        {
            get
            {
                return mvarObservacion;
            }
            set
            {
                mvarObservacion = value;
            }
        }

        public string Id_UsuarioGestiona
        {
            get
            {
                return mvarId_UsuarioGestiona;
            }
            set
            {
                mvarId_UsuarioGestiona = value;
            }
        }

        public string FechaAprovada
        {
            get
            {
                return mvarFechaAprovada;
            }
            set
            {
                mvarFechaAprovada = value;
            }
        }

        public string Id_UsuarioCreacion
        {
            get
            {
                return mvarId_UsuarioCreacion;
            }
            set
            {
                mvarId_UsuarioCreacion = value;
            }
        }

        public string Id_UsuarioActualizacion
        {
            get
            {
                return mvarId_UsuarioActualizacion;
            }
            set
            {
                mvarId_UsuarioActualizacion = value;
            }
        }

        public string FechaCreacion
        {
            get
            {
                return mvarFechaCreacion;
            }
            set
            {
                mvarFechaCreacion = value;
            }
        }

        public string FechaActualizacion
        {
            get
            {
                return mvarFechaActualizacion;
            }
            set
            {
                mvarFechaActualizacion = value;
            }
        }

        public Int_Solicitud() { }

        public Int_Solicitud(
            int? varId_Solicitud,
            int? varId_TipoSolicitud,
            int? varId_EstadoSolicitud,
            string varFechaTentativa1,
            string varFechaTentativa2,
            string varFechaTentativa3,
            string varObservacion,
            string varId_UsuarioGestiona,
            string varFechaAprovada,
            string varId_UsuarioCreacion,
            string varId_UsuarioActualizacion,
            string varFechaCreacion,
            string varFechaActualizacion
        )
        {
            mvarId_Solicitud = varId_Solicitud;
            mvarId_TipoSolicitud = varId_TipoSolicitud;
            mvarId_EstadoSolicitud = varId_EstadoSolicitud;
            mvarFechaTentativa1 = varFechaTentativa1;
            mvarFechaTentativa2 = varFechaTentativa2;
            mvarFechaTentativa3 = varFechaTentativa3;
            mvarObservacion = varObservacion;
            mvarId_UsuarioGestiona = varId_UsuarioGestiona;
            mvarFechaAprovada = varFechaAprovada;
            mvarId_UsuarioCreacion = varId_UsuarioCreacion;
            mvarId_UsuarioActualizacion = varId_UsuarioActualizacion;
            mvarFechaCreacion = varFechaCreacion;
            mvarFechaActualizacion = varFechaActualizacion;
        }

        public Int_Solicitud(IDataRecord dataRecord)
        {
            mvarId_Solicitud = Convert.ToInt32(dataRecord["Id_Solicitud"]);
            mvarId_TipoSolicitud = Convert.ToInt32(dataRecord["Id_TipoSolicitud"]);
            mvarId_EstadoSolicitud = Convert.ToInt32(dataRecord["Id_EstadoSolicitud"]);
            mvarFechaTentativa1 = Convert.ToString(dataRecord["FechaTentativa1"]);
            mvarFechaTentativa2 = Convert.ToString(dataRecord["FechaTentativa2"]);
            mvarFechaTentativa3 = Convert.ToString(dataRecord["FechaTentativa3"]);
            mvarObservacion = Convert.ToString(dataRecord["Observacion"]);
            mvarId_UsuarioGestiona = Convert.ToString(dataRecord["Id_UsuarioGestiona"]);
            mvarFechaAprovada = Convert.ToString(dataRecord["FechaAprovada"]);
            mvarId_UsuarioCreacion = Convert.ToString(dataRecord["Id_UsuarioCreacion"]);
            mvarId_UsuarioActualizacion = Convert.ToString(dataRecord["Id_UsuarioActualizacion"]);
            mvarFechaCreacion = Convert.ToString(dataRecord["FechaCreacion"]);
            mvarFechaActualizacion = Convert.ToString(dataRecord["FechaActualizacion"]);
        }

        public Int_Solicitud(DataRow dataRecord)
        {
            mvarId_Solicitud = Convert.ToInt32(dataRecord["Id_Solicitud"]);
            mvarId_TipoSolicitud = Convert.ToInt32(dataRecord["Id_TipoSolicitud"]);
            mvarId_EstadoSolicitud = Convert.ToInt32(dataRecord["Id_EstadoSolicitud"]);
            mvarFechaTentativa1 = Convert.ToString(dataRecord["FechaTentativa1"]);
            mvarFechaTentativa2 = Convert.ToString(dataRecord["FechaTentativa2"]);
            mvarFechaTentativa3 = Convert.ToString(dataRecord["FechaTentativa3"]);
            mvarObservacion = Convert.ToString(dataRecord["Observacion"]);
            mvarId_UsuarioGestiona = Convert.ToString(dataRecord["Id_UsuarioGestiona"]);
            mvarFechaAprovada = Convert.ToString(dataRecord["FechaAprovada"]);
            mvarId_UsuarioCreacion = Convert.ToString(dataRecord["Id_UsuarioCreacion"]);
            mvarId_UsuarioActualizacion = Convert.ToString(dataRecord["Id_UsuarioActualizacion"]);
            mvarFechaCreacion = Convert.ToString(dataRecord["FechaCreacion"]);
            mvarFechaActualizacion = Convert.ToString(dataRecord["FechaActualizacion"]);
        }
    }
}
