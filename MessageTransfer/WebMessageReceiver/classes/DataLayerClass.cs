using System;
using System.Configuration;
using System.Data;

namespace MessageReceiverWebService.classes
{

   
    public class DataLayerClass
    {
        DBUtil m_dbu;
        static string connectionString = ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
        public DataLayerClass()
        {
            m_dbu = new DBUtil(connectionString);
        }


        internal DataTable DataFill()
        {
            //Store procedure which retreives messages
            m_dbu.Command.CommandText = "sp_GetMessage";
            m_dbu.Command.CommandType = CommandType.StoredProcedure;
            m_dbu.Command.Parameters.Clear();
            DataTable Table = m_dbu.executeSelectquery(m_dbu.Command);
            return Table;
        }



        internal int InsertMessage(DateTime DateTime, string Message)
        {
            //Store procedure which insert the messages into the database
            m_dbu.Command.CommandText = "sp_InsertMessage";
            m_dbu.Command.CommandType = CommandType.StoredProcedure;
            m_dbu.Command.CommandTimeout = 0;
            m_dbu.Command.Parameters.Clear();
            m_dbu.Command.Parameters.AddWithValue("@DateTime", DateTime);
            m_dbu.Command.Parameters.AddWithValue("@Message", Message);
            int result=m_dbu.executeNonQuery_With_returnvalue(m_dbu.Command);
            return result;
        }


        internal int DeleteMessage(int RowID)
        {
            //Store procedure which deletes a message from database
            m_dbu.Command.CommandText = "sp_DeleteMessage";
            m_dbu.Command.CommandType = CommandType.StoredProcedure;
            m_dbu.Command.CommandTimeout = 0;
            m_dbu.Command.Parameters.Clear();
            m_dbu.Command.Parameters.AddWithValue("@RowID", RowID);
            int result = m_dbu.executeNonQuery_With_returnvalue(m_dbu.Command);
            return result;
        }
    }
}