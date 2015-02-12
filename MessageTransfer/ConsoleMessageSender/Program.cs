using System;
using System.IO;
using System.Net;
using System.Text;


namespace Message_sender_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run(args);
        }



        private void Run(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter your localhost address.\nFor example http://localhost:62646/Message.aspx \nPlease note that it must end with Message.aspx ");
                    string LocalhostAddress = Console.ReadLine();
                     if (LocalhostAddress.ToLower() == "exit")
                    {
                        break;
                    }
                    else if (string.IsNullOrEmpty(LocalhostAddress) || !LocalhostAddress.StartsWith("http://localhost:") || !LocalhostAddress.EndsWith("/Message.aspx"))
                    {
                        Console.WriteLine("Invalid localhost address.\n ");
                    }
                    else
                    {
                        Console.WriteLine("Please enter your message: ");
                        string Message = Console.ReadLine();
                        while(string.IsNullOrEmpty(Message))
                        {
                        Console.WriteLine("Message may not be empty. ");
                        Message = Console.ReadLine();
                        }
                        if (Message.ToLower() == "exit")
                          {
                             break;
                          }
                        Console.WriteLine("Please enter 1 to send your message using Httprequest\nOr 2 to send using uploadstring method");
                        string Method = Console.ReadLine();
                        while (Method != "1" && Method != "2")
                          {
                            Console.WriteLine("Invalid method number. Please enter a valid number: ");
                            Method = Console.ReadLine();
                          }
                        if (Method.ToLower() == "exit")
                          {
                            break;
                          }
                        DateTime DateTimeNow = System.DateTime.Now;
                        string Formatted_DateTimeNow = DateTimeNow.ToString("yyyy-MM-dd HH:mm:ss");        
                        if (Method == "1")
                          {
                            uploadstring(LocalhostAddress, "message=" + Message + "&datetime=" + Formatted_DateTimeNow);
                          }
                        else if (Method == "2")
                          {
                            HttpPostRequest(LocalhostAddress, "message=" + Message + "&datetime=" + Formatted_DateTimeNow);
                          }
                        Console.WriteLine("Message was sent. \n ");        
                     }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
           }
        }


        private string uploadstring(string uri, string message)  
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string response=client.UploadString(uri, message);
            return response;
        }



        private string HttpPostRequest(string url, string postData)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] Data = Encoding.UTF8.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = Data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(Data, 0, Data.Length);
            requestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

            Stream ResponseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(ResponseStream, Encoding.Default);

            string response = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            ResponseStream.Close();

            myHttpWebResponse.Close();

            return response;
        }
    }
}
