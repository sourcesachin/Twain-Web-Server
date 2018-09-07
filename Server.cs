using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System;


namespace Avs.Server
{
    class Server
    {
        public Scaner _owner;
        public bool running = false; // Is it running?
        private int timeout = 1000; // Time limit for data transfers.
        private Encoding charEncoder = Encoding.UTF8; // To encode string
        private Socket serverSocket; // Our server socket
      
        public Server(Scaner owner)
        {
            this._owner = owner;
        }
        // Content types that are supported by our
        private Dictionary<string, string> extensions = new Dictionary<string, string>()
        {
            { "html", "text/html" }
        };

        public bool start(IPAddress ipAddress, int port, int maxNOfCon)
        {
            if (running) return false; // If it is already running, exit.
            try
            {
                // A tcp/ip socket (ipv4)
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ipAddress, port));
                serverSocket.Listen(maxNOfCon);
                serverSocket.ReceiveTimeout = timeout;
                serverSocket.SendTimeout = timeout;
                running = true;
               // this.contentPath = contentPath;
            }
            catch { return false; }

            // Our thread that will listen connection requests and create new threads to handle them.
            Thread requestListenerT = new Thread(() =>
            {
                while (running)
                {
                    Socket clientSocket;
                    try
                    {
                        clientSocket = serverSocket.Accept();
                        // Create new thread to handle the request and continue to listen the socket.
                        Thread requestHandler = new Thread(() =>
                        {
                            clientSocket.ReceiveTimeout = timeout;
                            clientSocket.SendTimeout = timeout;
                            try { handleTheRequest(clientSocket); }
                            catch
                            {
                                try { clientSocket.Close(); }
                                catch { }
                            }
                        });
                        requestHandler.Start();
                    }
                    catch { }
                }
            });
            requestListenerT.Start();
            return true;
        }

        public void stop()
        {
            if (running)
            {
                running = false;
                try { serverSocket.Close(); }
                catch { }
                serverSocket = null;
            }
        }

        private void handleTheRequest(Socket clientSocket)
        {
            String _base64 = string.Empty;
            byte[] buffer = new byte[10240]; // 10 kb, just in case
            int receivedBCount = clientSocket.Receive(buffer); // Receive the request
            var request = Encoding.UTF8.GetString(buffer, 0, receivedBCount);
            string strReceived = request.ToString();
            string httpMethod = strReceived.Substring(0, strReceived.IndexOf(" "));
            if (httpMethod.Equals("POST"))
            {
                _base64= _owner._StartListner();
                SendString(clientSocket, _base64);
            }
            else if (httpMethod.Equals("GET")) { 
              //more with get method
            }
        }

        private void SendString(Socket clientSocket, string _base64)
        {
            if (_base64.Trim().Length <= 0){
                sendResponse(clientSocket, "Scanner error", "404 Not Found", "text/html");
            }
            else {
                sendResponse(clientSocket, _base64, "200 OK", "text/html");
            }
        }
        private void sendOkResponse(Socket clientSocket, byte[] bContent, string contentType)
        {
            sendResponse(clientSocket, bContent, "200 OK", contentType);
        }
        // For strings
        private void sendResponse(Socket clientSocket, string strContent, string responseCode, string contentType)
        {
            byte[] bContent = charEncoder.GetBytes(strContent);
            sendResponse(clientSocket, bContent, responseCode, contentType);
        }

        // For byte arrays
        private void sendResponse(Socket clientSocket, byte[] bContent, string responseCode, string contentType)
        {
            try
            {
                byte[] bHeader = charEncoder.GetBytes(
                                    "HTTP/1.1 " + responseCode + "\r\n"
                                  + "Access-Control-Allow-Origin:* \r\n"
                                  + "Server: Avs Twain Server\r\n"
                                  + "Content-Length: " + bContent.Length.ToString() + "\r\n"
                                  + "Connection: close\r\n"
                                  + "Content-Type: " + contentType + "\r\n\r\n");
                clientSocket.Send(bHeader);
                clientSocket.Send(bContent);
                clientSocket.Close();
            }
            catch { }
        }
    }
}
