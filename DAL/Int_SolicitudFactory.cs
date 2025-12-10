using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Int_SolicitudFactory : FactoryBase
    {
		public Int_Solicitud Load(Int_Solicitud _obj)
		{
			try
			{
				AddParameters(_obj);
				AddCmdParameter("@Action", 0, ParameterDirection.Input);
				ExecuteReader();
				while (Read())
				{
					_obj = new Int_Solicitud(GetDataReader());
				}
				return _obj;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Int_SolicitudCollection SelectByParams(Int_Solicitud _obj, int Action)
		{
			Int_SolicitudCollection Collection = new Int_SolicitudCollection();
			try
			{
				AddParameters(_obj);
				AddCmdParameter("@Action", Action, ParameterDirection.Input);
				ExecuteReader();
				while (Read())
				{
					Collection.Add(new Int_Solicitud(GetDataReader()));
				}
				return Collection;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable SelectTable(Int_Solicitud _obj, int Action)
		{
			DataTable dt = new DataTable();
			try
			{
				AddParameters(_obj);
				AddCmdParameter("@Action", Action, ParameterDirection.Input);
				return GetDataSet().Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int InsertOrUpdate(Int_Solicitud _obj, int Action)
		{
			try
			{
				AddParameters(_obj);
				AddCmdParameter("@Action", Action, ParameterDirection.Input);
				ExecuteNonQuery();
				return 1;
			}
			catch (Exception ex)
			{
				int i = -1;
				throw ex;
			}
		}

		private void AddParameters(Int_Solicitud _obj)
		{
			CreateCommand("Int_Solicitud", use_store_proc: true);
			AddCmdParameter("@Id_Solicitud", _obj.Id_Solicitud, ParameterDirection.Input);
			AddCmdParameter("@Id_TipoSolicitud", _obj.Id_TipoSolicitud, ParameterDirection.Input);
			AddCmdParameter("@Id_EstadoSolicitud", _obj.Id_EstadoSolicitud, ParameterDirection.Input);
			AddCmdParameter("@FechaTentativa1", _obj.FechaTentativa1, ParameterDirection.Input);
			AddCmdParameter("@FechaTentativa2", _obj.FechaTentativa2, ParameterDirection.Input);
			AddCmdParameter("@FechaTentativa3", _obj.FechaTentativa3, ParameterDirection.Input);
			AddCmdParameter("@Observacion", _obj.Observacion, ParameterDirection.Input);
			AddCmdParameter("@Id_UsuarioGestiona", _obj.Id_UsuarioGestiona, ParameterDirection.Input);
			AddCmdParameter("@FechaAprovada", _obj.FechaAprovada, ParameterDirection.Input);
			AddCmdParameter("@Id_UsuarioCreacion", _obj.Id_UsuarioCreacion, ParameterDirection.Input);
			AddCmdParameter("@Id_UsuarioActualizacion", _obj.Id_UsuarioActualizacion, ParameterDirection.Input);
			AddCmdParameter("@FechaCreacion", _obj.FechaCreacion, ParameterDirection.Input);
			AddCmdParameter("@FechaActualizacion", _obj.FechaActualizacion, ParameterDirection.Input);
		}
	}
}
