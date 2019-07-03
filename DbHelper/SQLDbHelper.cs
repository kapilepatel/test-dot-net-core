using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace AG_MS_Authentication
{
    public class SQLDbHelper
    {
        public static DataTable GetTableFromSP(string connectionString, string sp, Dictionary<string, object> parametersCollection)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };

                foreach (KeyValuePair<string, object> parameter in parametersCollection)
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);

                DataSet dataSet = new DataSet();
                (new SqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();

            }
        }

        public static DataTable GetTableFromSP(string connectionString, string sp, SqlParameter[] prms)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                command.Parameters.AddRange(prms);

                DataSet dataSet = new DataSet();
                (new SqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataTable GetTableFromSP(string connectionString, string sp)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                DataSet dataSet = new DataSet();
                (new SqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public static void ExecuteNonQuery(string connectionString, string sp, SqlParameter[] prms)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                command.Parameters.AddRange(prms);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public static void ExecuteNonQuery(string connectionString, string sp, SqlParameter prms)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                prms.SqlDbType = SqlDbType.Structured;
                command.Parameters.Add(prms);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public static void ExecuteNonQuery(string connectionString, string sp, SqlParameter prm, SqlParameter[] prms)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                prm.SqlDbType = SqlDbType.Structured;
                command.Parameters.Add(prm);
                command.Parameters.AddRange(prms);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public static DataTable GetTableRow(string connectionString, string sp, SqlParameter[] prms)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                command.Parameters.AddRange(prms);
                connection.Open();

                DataSet dataSet = new DataSet();
                (new SqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataSet GetDatasetFromSP(string connectionString, string sp, SqlParameter[] prms)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                command.Parameters.AddRange(prms);

                DataSet dataSet = new DataSet();
                (new SqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int ExecuteNonQueryReturn(string connectionString, string sp, SqlParameter[] prms)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                command.Parameters.AddRange(prms);
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public static string ExecuteScalarFunction(string connectionString, string CommandText)
        {
            string Result = "";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                connection.Open();
                command = new SqlCommand(CommandText, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }

            return Result;

        }

        public static void ExecuteMultipleDatatable(string connectionString, string sp, SqlParameter[] prms, DataSet ds)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                command.Parameters.AddRange(prms);
                if (null != ds)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        SqlParameter parameter = new SqlParameter();
                        parameter.SqlDbType = SqlDbType.Structured;

                        //DataTable.TableName is the parameter Name
                        //e.g: @AppList
                        parameter.ParameterName = dt.TableName;
                        //DataTable.DisplayExpression is the equivalent SQLType Name. i.e. Name of the UserDefined Table type
                        //e.g: AppCollectionType
                        //parameter.TypeName = dt.DisplayExpression;
                        parameter.TypeName = dt.Namespace;
                        parameter.Value = dt;

                        command.Parameters.Add(parameter);
                    }
                }
                int result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }


        }

        public static DataTable GetTableRawQuery(string connectionString, string CommandText)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                connection.Open();
                command = new SqlCommand(CommandText, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);

                command.Parameters.Clear();

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }
    }
}
