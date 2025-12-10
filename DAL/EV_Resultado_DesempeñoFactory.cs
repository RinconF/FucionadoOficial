using System; 
using System.Data; 
using DCL; 
 
namespace DAL 
{ 
public class EV_Resultado_DesempeñoFactory : FactoryBase 
{ 
public EV_Resultado_DesempeñoFactory() { } 
 
public EV_Resultado_Desempeño Load(EV_Resultado_Desempeño _obj) 
{ 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", 0, ParameterDirection.Input); 
ExecuteReader(); 
while (Read()) 
{ 
_obj = new EV_Resultado_Desempeño(GetDataReader()); 
} 
return _obj; 
} 
catch (Exception e) 
{ 
throw e; 
} 
} 
 
public EV_Resultado_DesempeñoCollection SelectByParams(EV_Resultado_Desempeño _obj, int Action) 
{ 
EV_Resultado_DesempeñoCollection Collection = new EV_Resultado_DesempeñoCollection(); 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", Action, ParameterDirection.Input); 
ExecuteReader(); 
while (Read()) 
{ 
Collection.Add(new EV_Resultado_Desempeño(GetDataReader())); 
} 
} 
catch (Exception e) 
{ 
throw e; 
} 
return Collection; 
} 
 
public DataTable SelectTable(EV_Resultado_Desempeño _obj, int Action) 
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
public int InsertOrUpdate(EV_Resultado_Desempeño _obj, int Action) 
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
private void AddParameters(EV_Resultado_Desempeño _obj) 
{ 
 CreateCommand("SP_EV_Resultado_Desempeño", true); 
AddCmdParameter("@Id_EV_Desempeño", _obj.Id_EV_Desempeño, ParameterDirection.Input); 
AddCmdParameter("@Id_IE", _obj.Id_IE, ParameterDirection.Input); 
AddCmdParameter("@Id_Consideracion_Item", _obj.Id_Consideracion_Item, ParameterDirection.Input); 
AddCmdParameter("@Auto_Evaluacion", _obj.Auto_Evaluacion, ParameterDirection.Input); 
AddCmdParameter("@Jefe_Evaluacion", _obj.Jefe_Evaluacion, ParameterDirection.Input);
AddCmdParameter("@Total", _obj.Total, ParameterDirection.Input);
AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input); 
AddCmdParameter("@Usuario_Actualizacion", _obj.Usuario_Actualizacion, ParameterDirection.Input); 
AddCmdParameter("@Fecha_Actualizacion", _obj.Fecha_Actualizacion, ParameterDirection.Input); 
AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input); 
}}}