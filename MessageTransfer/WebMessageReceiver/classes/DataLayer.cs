using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace MessageReceiverWebService
{
    public class DataLayer
    {
        public DataTable DataFill()
        {

            try
            {
                String ConnectionString = WebConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
                SqlConnection Connection = new SqlConnection(ConnectionString);
                string selectSQL = "Select * from Table_Message ORDER BY DateTime DESC";
                SqlCommand Command = new SqlCommand(selectSQL, Connection);
                SqlDataAdapter Adapter = new SqlDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();
                return DT;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public int InsertMessage(DateTime DateTime, string Message)
        {
            try
            {
                string ConnectionString = WebConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
                SqlConnection Connection = new SqlConnection(ConnectionString); ;
                string InsertQuery = "Insert Into Table_Message (DateTime , Message) Values ('" + DateTime + "' , '" + Message + "')";
                SqlCommand Command = new SqlCommand(InsertQuery, Connection);
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public void DeleteFromTable_Message(int id)
        {
            string ConnectionString = WebConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            string DeleteQuery = "Delete From Table_Message Where RowID=" + id;
            SqlCommand DeleteCommand = new SqlCommand(DeleteQuery, Connection);
            Connection.Open();
            DeleteCommand.ExecuteNonQuery();
        }





    }
}