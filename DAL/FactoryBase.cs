using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class FactoryBase
    {
        //objeto para la conexion a base de dartos
        private SqlConnection connection = null;
        //objeto para la ejecucion de sentencias
        private SqlCommand cmd = null;
        //objeto para la lectura de registros
        private SqlDataReader dr = null;

        public FactoryBase()
        {
            connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("keyconnection"));
        }
        public FactoryBase(string sConnection)
        {
            connection = new SqlConnection(sConnection);
        }

        /// <summary>
        /// Ejecucion de sentencia o procedimiento
        /// </summary>
        /// <param name="sql">Nombre del procedimiento o sentencia a ejecutar</param>
        /// <param name="use_store_proc">Si se invoca un procedimiento almacenado</param>
        public void CreateCommand(string sql, bool use_store_proc)
        {
            cmd = new SqlCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            if (use_store_proc)
                cmd.CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Adiciona un parametro en la ejecucion de la sentencia o procedimiento
        /// </summary>
        /// <param name="name">Nombre del parametro</param>
        /// <param name="value">Valor del parametro</param>
        /// <param name="parameterDirection">Direccin del parametro (Entrada o Salida)</param>
        public void AddCmdParameter(string name, object value, ParameterDirection parameterDirection)
        {
            SqlParameter param = new SqlParameter(name, value);
            param.Direction = parameterDirection;
            cmd.Parameters.Add(param);
        }

        /// <summary>
        /// Funcion que ejecuta sentencia o procedimiento
        /// </summary>
        /// <returns>returna un entero informando de la cantidad de registros afectados</returns>
        public int ExecuteNonQuery()
        {
            int r;
            try
            {
                OpenConnection();
                r = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseConnection();
            }
            return r;
        }

        /// <summary>
        /// Funcion que ejecuta sentencia o procedimiento
        /// </summary>
        /// <returns>returna el valor devuelto por la ejecucin en la base de datos</returns>
        public object ExecuteScalar()
        {
            object o;
            try
            {
                OpenConnection();
                o = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseConnection();
            }
            return o;
        }

        /// <summary>
        /// Funcion que retorna la informacion en un DataReader
        /// </summary>
        /// <returns></returns>
        public SqlDataReader ExecuteReader()
        {
            try
            {
                OpenConnection();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception Ex)
            {
                CloseConnection();
                throw Ex;
            }
        }

        /// <summary>
        /// Funcion que permite la lectura de datos de un DataReader
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            if (dr.Read())
                return true;
            else
                CloseConnection();
            return false;
        }

        public object GetValue(string name)
        {
            return dr[name];
        }

        public object GetValue(int index)
        {
            return dr[index];
        }

        public SqlDataReader GetDataReader()
        {
            return dr;
        }

        /// <summary>
        /// Funcin que retorna la informaicn en un DatatSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            try
            {
                OpenConnection();
                DataSet ds = new DataSet();
                cmd.CommandTimeout = 240;// Tiempo de espera para la ejecución de la consulta en segundos (4 minutos) a nivel de base de datos
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Metodo que vuelca datos de forma masiva en una tabla
        /// </summary>
        /// <param name="TableDestination">Nombre de la tabla en la que se vuelca la informacin</param>
        /// <param name="Lector">DataReader que contiene la informacion a insertar en la base de datos</param>
        public void BulkCopy(string TableDestination, IDataReader Lector)
        {
            try
            {
                OpenConnection();
                SqlBulkCopy objcopy = new SqlBulkCopy(connection);
                objcopy.BulkCopyTimeout = 300;
                objcopy.DestinationTableName = TableDestination;
                objcopy.WriteToServer(Lector);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Metodo que permite la apertura de la base de datos
        /// </summary>
        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Metodo que permite el cierre de la conexin a la base de datos
        /// </summary>
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}