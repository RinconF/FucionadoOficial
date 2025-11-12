using System; 
using System.Data; 
using DCL; 
 
namespace DAL 
{ 
public class ENC_PreguntaFactory : FactoryBase 
{ 
public ENC_PreguntaFactory() { } 
 
public ENC_Pregunta Load(ENC_Pregunta _obj) 
{ 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", 0, ParameterDirection.Input); 
ExecuteReader(); 
while (Read()) 
{ 
_obj = new ENC_Pregunta(GetDataReader()); 
} 
return _obj; 
} 
catch (Exception e) 
{ 
throw e; 
} 
} 
 
public ENC_PreguntaCollection SelectByParams(ENC_Pregunta _obj, int Action) 
{ 
ENC_PreguntaCollection Collection = new ENC_PreguntaCollection(); 
try 
{ 
AddParameters(_obj); 
AddCmdParameter("@Action", Action, ParameterDirection.Input); 
ExecuteReader(); 
while (Read()) 
{ 
Collection.Add(new ENC_Pregunta(GetDataReader())); 
} 
} 
catch (Exception e) 
{ 
throw e; 
} 
return Collection; 
} 
 
public DataTable SelectTable(ENC_Pregunta _obj, int Action) 
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
public int InsertOrUpdate(ENC_Pregunta _obj, int Action) 
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
private void AddParameters(ENC_Pregunta _obj) 
{ 
 CreateCommand("SP_ENC_Pregunta", true); 
AddCmdParameter("@Id_Pregunta", _obj.Id_Pregunta, ParameterDirection.Input); 
AddCmdParameter("@Id_Encuesta", _obj.Id_Encuesta, ParameterDirection.Input); 
AddCmdParameter("@Id_Tipo_Pregunta", _obj.Id_Tipo_Pregunta, ParameterDirection.Input); 
AddCmdParameter("@Pregunta", _obj.Pregunta, ParameterDirection.Input); 
AddCmdParameter("@Estado", _obj.Estado, ParameterDirection.Input); 
AddCmdParameter("@Usuario_Creacion", _obj.Usuario_Creacion, ParameterDirection.Input); 
AddCmdParameter("@Fecha_Creacion", _obj.Fecha_Creacion, ParameterDirection.Input); 
AddCmdParameter("@Usuario_Actualiza", _obj.Usuario_Actualiza, ParameterDirection.Input); 
AddCmdParameter("@Fecha_Actualiza", _obj.Fecha_Actualiza, ParameterDirection.Input); 
}}}