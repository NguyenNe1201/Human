using Human.Models.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Human.DO
{
    public class DbHelper : IDisposable
    {
        public SqlConnection _SqlConnect;
        public SqlCommand _SqlCommand;
        public SqlDataAdapter _SqlDataAdapter;
        public DataTable dt;
        public DbHelper()
        {
            // string ConnectString = "Data Source=192.168.90.15;Initial Catalog=CONECT_STAFF;User ID=sa;Password=hrconect";
            string ConnectString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _SqlConnect = new SqlConnection(ConnectString);
            _SqlCommand = _SqlConnect.CreateCommand();
            _SqlCommand.CommandType = CommandType.StoredProcedure;
            _SqlConnect.Open();
        }

        public IEnumerable<T> Executereader<T>(string SpName, string[] parameters, object[] values)
        {
            try
            {
                _SqlCommand.CommandText = SpName;
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != string.Empty)
                    {
                        SqlParameter p_param = new SqlParameter();
                        if (values[i] == null || values[i].Equals(string.Empty))
                        {
                            values[i] = DBNull.Value;
                        }

                        if (p_param.SqlDbType == SqlDbType.DateTime & values[i].Equals(Convert.ToDateTime("01/01/0001")))
                            values[i] = SqlDateTime.Null;
                        p_param.ParameterName = parameters[i];
                        p_param.Value = values[i];
                        _SqlCommand.Parameters.Add(p_param);

                    }
                }
                return GetList<T>(_SqlCommand.ExecuteReader());
            }
            catch (Exception ex)
            {

                return null;
            }
            finally
            {
                Dispose();
            }

        }
        public DataTable return_DataTable(string spName, string[] parameters, object[] values)
        {
            try
            {
                _SqlCommand.CommandText = spName;

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != string.Empty)
                    {
                        SqlParameter p_param = new SqlParameter();
                        if (values[i] == null || values[i].Equals(string.Empty))
                        {
                            values[i] = DBNull.Value;
                        }

                        if (p_param.SqlDbType == SqlDbType.DateTime & values[i].Equals(Convert.ToDateTime("01/01/0001")))
                            values[i] = SqlDateTime.Null;
                        p_param.ParameterName = parameters[i];
                        p_param.Value = values[i];
                        _SqlCommand.Parameters.Add(p_param);
                    }
                }
                _SqlDataAdapter = new SqlDataAdapter();
                _SqlDataAdapter.SelectCommand = _SqlCommand;
                dt = new DataTable();
                _SqlDataAdapter.Fill(dt);


            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                Dispose();
            }
            return dt;
        }
        public static IEnumerable<T> GetList<T>(SqlDataReader reader)
        {
            List<T> listT = new List<T>();
            while (reader.Read())
            {
                var item = CreateInstance<T>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var colname = reader.GetName(i);
                    var value = reader.GetValue(i);
                    var property = item.GetType().GetProperty(colname);
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(item, value);
                    }
                }
                listT.Add(item);

            }
            return listT;
        }
        public static T CreateInstance<T>()
        {
            var type = typeof(T);
            if (type == typeof(string))
            {
                return (T)(object)String.Empty;
            }
            else
            {
                return (T)FormatterServices.GetUninitializedObject(type);
            }
        }
        public int execStoreProcedure(string SpName, string[] parameters, object[] values)
        {
            int affectedRows = 0;
            try
            {
                _SqlCommand.CommandText = SpName;
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != string.Empty)
                    {
                        SqlParameter p_param = new SqlParameter();
                        if (values[i] == null || values[i].Equals(string.Empty))
                        {
                            values[i] = DBNull.Value;
                        }

                        if (p_param.SqlDbType == SqlDbType.DateTime & values[i].Equals(Convert.ToDateTime("01/01/0001")))
                            values[i] = SqlDateTime.Null;
                        p_param.ParameterName = parameters[i];
                        p_param.Value = values[i];
                        _SqlCommand.Parameters.Add(p_param);

                    }
                }

                affectedRows = _SqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                Dispose();
            }
            return affectedRows;
        }
        public void Dispose()
        {
            _SqlDataAdapter?.Dispose();
            _SqlCommand?.Dispose();
            _SqlConnect?.Close();
            _SqlConnect?.Dispose();
        }
    }
}