using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ChatClient
{
    class ClientNet
    {
        public string NickName;
        private string hostIp;
        private int port;
        private TcpClient client;
        private NetworkStream stream;

        public delegate void MsgRecieved(string data);
        public event MsgRecieved MsgRecievedEvent;

        public ClientNet(string NickName, string hostIp, int port)
        {
            this.NickName = NickName;
            this.hostIp = hostIp;
            this.port = port;

            client = new TcpClient();
            try
            {
                client.Connect(hostIp, port); 
                stream = client.GetStream(); 

                string message = NickName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); 
            }
            catch 
            {
                //Disconnect();
            }
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);

        }


        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; 
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));                     
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    MsgRecievedEvent(message);

                }
                catch
                {
                    //Disconnect();
                }
            }
        }


        public void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}

