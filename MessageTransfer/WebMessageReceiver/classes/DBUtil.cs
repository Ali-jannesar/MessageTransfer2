using System.Data;
using System.Data.SqlClient;

namespace MessageReceiverWebService.classes
{
    public class DBUtil
    {
        SqlConnection connection;
        public SqlConnection Connection
        {
            get { return connection; }
        }

        SqlCommand command;

        public SqlCommand Command
        {
            get { return command; }
        }

        public DBUtil(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
          //  connection.Open();
            command.CommandTimeout = 0;
        }


        public DataTable executeSelectquery(SqlCommand cmd)
        {
            try
            {
                //Select command
                
                OpenConnection();
                cmd.Connection = connection;
                cmd.CommandTimeout = 0;
                SqlDataAdapter adpter = new SqlDataAdapter(cmd);
                adpter.SelectCommand.CommandTimeout = 0;
                DataTable Table = new DataTable();
                adpter.Fill(Table);
                if (Table != null)
                    return Table;
                return null;
            }
            catch
            {
                
                throw;
            }

            finally
            {
                CloseConnection();
            }
        }



        public int executeNonQuery_With_returnvalue(SqlCommand command)
        {
            try
            {
                //Insert, Delete, Update command

                OpenConnection();
                command.Connection = connection;
                command.CommandTimeout = 0;
                int result= command.ExecuteNonQuery();
                connection.Close();
                return result;
            }
            catch
            {
                
                throw;
            }

            finally
            {
                CloseConnection();
            }
        }


        public void OpenConnection()
        {
            //Method which opens the connection whenever necessary

            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            //Method which closes the connection whenever necessary

            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

}
