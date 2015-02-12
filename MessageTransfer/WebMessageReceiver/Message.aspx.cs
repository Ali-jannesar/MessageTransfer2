using MessageReceiverWebService.classes;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MessageReceiverWebService
{
    public partial class Message : System.Web.UI.Page
    {
        DataLayerClass DataLayer = new DataLayerClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Clear_Error();
                    Get_and_Save_Data();
                    FillGrid();
                }
            }
            catch (Exception Ex)
            {
                lbl_Error.Text = "Error: " + Ex.Message;

            }
        }

        private void Clear_Error()
        {
            lbl_Error.Text = "";
        }



        private void Get_and_Save_Data()
        {
            if (Request.InputStream.Length > 0)
            {
                StreamReader Stream = new StreamReader(Request.InputStream);
                string EncodedData = Stream.ReadToEnd();
                string Data = HttpUtility.UrlDecode(EncodedData);
                char Separator = '&';
                string Message = "";
                string DateTime_string;
                DateTime datetime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(Data))
                {
                    if (Data.StartsWith("message=") && Data.Contains("datetime=")) //checks if it's the message which is sent by our client
                    {
                        string[] splited_data = Data.Split(Separator);
                        string messagepart = splited_data[0];
                        string datetimepart = splited_data[1];
                        if (!string.IsNullOrEmpty(messagepart))
                        {

                            if (messagepart.IndexOf("message=") != -1)
                            {
                                Message = messagepart.Remove(0, "message=".Length);
                            }

                        }

                        if (!string.IsNullOrEmpty(datetimepart))
                        {
                            if (datetimepart.IndexOf("datetime=") != -1)
                            {
                                DateTime_string = datetimepart.Remove(0, "datetime=".Length);
                                DateTime datetimeformat;
                                if (DateTime.TryParse(DateTime_string, out datetimeformat))
                                {
                                    datetime = DateTime.Parse(DateTime_string);
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(Message) && datetime != DateTime.MinValue)
                        {
                          DataLayer.InsertMessage(datetime, Message);
                        }

                    }
                }
            }

        }



        private void FillGrid()
        {
            
            //Retrieve the messages from the database and show them in Gridview

            try
            {
                Clear_Error();
                DataTable Table = DataLayer.DataFill();
                BindData(Table);
            }
            catch (Exception Ex)
            {
                lbl_Error.Text = Ex.Message;
            }
        }




        private void BindData(DataTable Table)
        {
            //Binds a datatable to the Gridview
            
            if (Table != null && Table.Rows.Count > 0)
            {
                grid_Messages.DataSource = Table;
                grid_Messages.DataBind();
            }
            else
            {
                ShowEmptyGrid();
            }
        }



        private void ShowEmptyGrid()
        {
           //Create a Data table and binds it to the Gridview in case the database is empty

            DataTable dtempty = new DataTable();
            dtempty.Columns.Add("RowID", typeof(string));
            dtempty.Columns.Add("DateTime", typeof(string));
            dtempty.Columns.Add("Message", typeof(string));
            dtempty.Rows.Add(dtempty.NewRow());
            grid_Messages.DataSource = dtempty;
            grid_Messages.DataBind();
            int TotalColumns = grid_Messages.Rows[0].Cells.Count;
            grid_Messages.Rows[0].Cells.Clear();
            grid_Messages.Rows[0].Cells.Add(new TableCell());
            grid_Messages.Rows[0].Cells[0].ColumnSpan = TotalColumns;
            grid_Messages.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grid_Messages.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Orange;
            grid_Messages.Rows[0].Cells[0].Font.Size = 20;
            grid_Messages.Rows[0].Cells[0].Font.Bold = true;
            grid_Messages.Rows[0].Cells[0].Text = "No Message Found";
        }


        protected void grid_Messages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Deleting messages from Gridview
            
            try
            {
                DataLayerClass DataLayer = new DataLayerClass();
                Clear_Error();
                int id = int.Parse((grid_Messages.DataKeys[e.RowIndex].Value).ToString());
                DataLayer.DeleteMessage(id);
                FillGrid();
            }
            catch(Exception Ex)
            {
                lbl_Error.Text = Ex.Message;
            }

        }

        protected void button_Refresh_Click(object sender, EventArgs e)
        {
            //Refresh the Gridview by calling FillGrid Method

            FillGrid();
        }

   
    }
}