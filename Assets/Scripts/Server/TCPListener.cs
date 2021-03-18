using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TCPListener : MonoBehaviour
{
    private static TCPListener instance;

    public static TCPListener Instance
    {
        get {
            if (instance == null) instance = FindObjectOfType<TCPListener>();
            return instance;
        }
    }

    bool isInit = false;
    Socket listener;
    readonly int maxClientCount = 2;
    static readonly List<ClientSocket> handlers = new List<ClientSocket>();

    public void Run()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        try
        {
            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            isInit = true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    private void Update()
    {
        if (isInit)
        {
            if(listener.Poll(1, SelectMode.SelectRead))
            {
                if (handlers.Count >= maxClientCount) return;

                try
                {
                    var clientSocket = new ClientSocket(listener.Accept());
                    handlers.Add(clientSocket);
                    string welcomeString = "Greeting From the server !!!";
                    clientSocket.OnSend(welcomeString);
                }
                catch
                {
                    //close client();
                }
            }
            foreach(ClientSocket handler in handlers)
            {
                try
                {
                    handler.OnRecv();
                }
                catch
                {
                    //close client
                }
            }
        }
    }

    public class ClientSocket
    {
        public Socket handler;
        string data = null;

        public ClientSocket(Socket aHandler)
        {
            handler = aHandler;
        }

        public void OnRecv()
        {
            if (handler.Available == 0) return;

            byte[] bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes);
            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

            if(data.IndexOf("<EOF>") > 1)
            {
                foreach(ClientSocket handler in handlers)
                {
                    //if (handler == this) continue;
                    handler.OnSend(data);
                }
                data = null;
            }
        }

        public void OnSend(string msg)
        {
            handler.Send(Encoding.ASCII.GetBytes(msg));
        }
    }



    private void ShutDownServer()
    {
/*        handler.Shutdown(SocketShutdown.Both);
        handler.Close();*/
    }


   


}
