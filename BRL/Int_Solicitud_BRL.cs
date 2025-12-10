using DAL;
using DCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRL
{
    public class Int_Solicitud_BRL
    {
		public static Int_Solicitud Load(Int_Solicitud ObjOpc, int Action)
		{
			return new Int_SolicitudFactory().Load(ObjOpc);
		}

		public static Int_SolicitudCollection SelectByParams(Int_Solicitud ObjOpc, int Action)
		{
			return new Int_SolicitudFactory().SelectByParams(ObjOpc, Action);
		}

		public static DataTable SelectTable(Int_Solicitud ObjOpc, int Action)
		{
			return new Int_SolicitudFactory().SelectTable(ObjOpc, Action);
		}

		public static int InsertarOrUpdate(Int_Solicitud ObjOpc, int Action)
		{
			return new Int_SolicitudFactory().InsertOrUpdate(ObjOpc, Action);
		}
	}
}
