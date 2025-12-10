using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL
{
    public class Info_Evaluaciones
    {
        private int mvarIdEvaluacion;
        private string mvarNombre;
        private string mvarDescripcion;
        private int mvarId_UsuarioCreacion;
        private int mvarId_UsuarioActualiza;
        private DateTime? mvarFechaCreacion;
        private DateTime? mvarFechaActualizacion;
        private bool? mvarEstado;

        public int IdEvaluacion
        {
            get
            {
                return mvarIdEvaluacion;
            }
            set
            {
                mvarIdEvaluacion = value;
            }
        }
        public string Nombre
        {
            get
            {
                return mvarNombre;
            }
            set
            {
                mvarNombre = value;
            }
        }
        public string Descripcion
        {
            get
            {
                return mvarDescripcion;
            }
            set
            {
                mvarDescripcion = value;
            }
        }
     
        public int Id_UsuarioCreacion
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
        public int Id_UsuarioActualiza
        {
            get
            {
                return mvarId_UsuarioActualiza;
            }
            set
            {
                mvarId_UsuarioActualiza = value;
            }
        }
        public DateTime? FechaCreacion
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
        public DateTime? FechaActualizacion
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
        public bool? Estado
        {
            get
            {
                return mvarEstado;
            }
            set
            {
                mvarEstado = value;
            }
        }

        public Info_Evaluaciones() { }

        public Info_Evaluaciones(
             int varIdEvaluacion,
             string varNombre,
             string varDescripcion,
             int varId_UsuarioCreacion,
             int varId_UsuarioActualiza,
             DateTime? varFechaCreacion,
             DateTime? varFechaActualizacion,
             bool? varEstado
        )
        {
            mvarIdEvaluacion = varIdEvaluacion;
            mvarNombre = varNombre;
            mvarDescripcion = varDescripcion;
            mvarId_UsuarioCreacion = varId_UsuarioCreacion;
            mvarId_UsuarioActualiza = varId_UsuarioActualiza;
            mvarFechaCreacion = varFechaCreacion;
            mvarFechaActualizacion = varFechaActualizacion;
            mvarEstado = varEstado;
        }

        public Info_Evaluaciones(IDataRecord obj)
        {
            mvarIdEvaluacion = Convert.ToInt32(obj["IdEvaluacion"]);
            mvarNombre = Convert.ToString(obj["Nombre"]);
            mvarDescripcion = Convert.ToString(obj["Descripcion"]);
            mvarId_UsuarioCreacion = Convert.ToInt32(obj["Id_UsuarioCreacion"]);
            mvarId_UsuarioActualiza = Convert.ToInt32(obj["Id_UsuarioActualiza"]);
            mvarFechaCreacion = Convert.ToDateTime(obj["FechaCreacion"]);
            mvarFechaActualizacion = Convert.ToDateTime(obj["FechaActualizacion"]);
            mvarEstado = Convert.ToBoolean(obj["Estado"]);
        }

        public Info_Evaluaciones(DataRow obj)
        {
            mvarIdEvaluacion = Convert.ToInt32(obj["IdEvaluacion"]);
            mvarNombre = Convert.ToString(obj["Nombre"]);
            mvarDescripcion = Convert.ToString(obj["Descripcion"]);
            mvarId_UsuarioCreacion = Convert.ToInt32(obj["Id_UsuarioCreacion"]);
            mvarId_UsuarioActualiza = Convert.ToInt32(obj["Id_UsuarioActualiza"]);
            mvarFechaCreacion = Convert.ToDateTime(obj["FechaCreacion"]);
            mvarFechaActualizacion = Convert.ToDateTime(obj["FechaActualizacion"]);
            mvarEstado = Convert.ToBoolean(obj["Estado"]);
        }                
    }
}

