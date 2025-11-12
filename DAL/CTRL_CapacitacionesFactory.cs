using System; 
using System.Data; 
using DCL; 
 
namespace DAL 
{ 
public class CTRL_CapacitacionesFactory : FactoryBase 
{ 
public CTRL_CapacitacionesFactory() { } 
 
public CTRL_Capacitaciones Load(CTRL_Capacitaciones _obj) 
{ 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", 0, ParameterDirection.Input); 
ExecuteReader(); 
while (Read()) 
{ 
_obj = new CTRL_Capacitaciones(GetDataReader()); 
} 
return _obj; 
} 
catch (Exception e) 
{ 
throw e; 
} 
} 
 
public CTRL_CapacitacionesCollection SelectByParams(CTRL_Capacitaciones _obj, int Action) 
{ 
CTRL_CapacitacionesCollection Collection = new CTRL_CapacitacionesCollection(); 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", Action, ParameterDirection.Input); 
ExecuteReader(); 
while (Read()) 
{ 
Collection.Add(new CTRL_Capacitaciones(GetDataReader())); 
} 
} 
catch (Exception e) 
{ 
throw e; 
} 
return Collection; 
} 
 
public DataTable SelectTable(CTRL_Capacitaciones _obj, int Action) 
{ 
DataTable dt = new DataTable(); 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", Action, ParameterDirection.Input); 
dt = GetDataSet().Tables[0]; 
} 
catch (Exception e) 
{ 
throw e; 
} 
return dt; 
} 
public int InsertOrUpdate(CTRL_Capacitaciones _obj, int Action) 
{ 
int i; 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", Action, ParameterDirection.Input); 
ExecuteNonQuery(); 
i = 1; 
} 
catch (Exception e) 
{ 
i = -1; 
throw e; 
} 
return i; 
} 
private void AddParameters(CTRL_Capacitaciones _obj) 
{ 
 CreateCommand("SP_CTRL_Capacitaciones", true); 
AddCmdParameter("@Id_Capacitacion", _obj.Id_Capacitacion, ParameterDirection.Input); 
AddCmdParameter("@Nombre_Capacitacion", _obj.Nombre_Capacitacion, ParameterDirection.Input); 
AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input); 
AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input); 
AddCmdParameter("@Usuario_Actualiza", _obj.Usuario_Actualiza, ParameterDirection.Input); 
AddCmdParameter("@Fecha_Actualiza", _obj.Fecha_Actualiza, ParameterDirection.Input); 
AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input); 
AddCmdParameter("@Id_Acceso", _obj.Id_Acceso, ParameterDirection.Input); 
AddCmdParameter("@Id_IE", _obj.Id_IE, ParameterDirection.Input); 
AddCmdParameter("@Id_Sede", _obj.Id_Sede, ParameterDirection.Input); 
}}}