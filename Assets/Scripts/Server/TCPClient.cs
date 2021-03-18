using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.InputSystem;
using System;

public class TCPClient : MonoBehaviour
{
    private static TCPClient instance;
    public static TCPClient Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<TCPClient>();
            return instance;
        }
    }

    bool isInit;
    Socket sender;
    public Action<MapData> othersMapUpdate;

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb.jKey.wasPressedThisFrame) SendDataToServer();
        if (kb.kKey.wasPressedThisFrame) ShutDownClient();

        if (isInit && sender.Available != 0)
        {
            byte[] bytes = new byte[1024];
            int bytesRec = sender.Receive(bytes);
            string r = Encoding.ASCII.GetString(bytes, 0, bytesRec);

            try
            {
                MapData d = JsonUtility.FromJson<MapData>(r.Remove(r.Length - 5));
                if (typeof(MapData).IsInstanceOfType(d))
                {
                    othersMapUpdate?.Invoke(d);
                }
            }
            catch(ArgumentException e)
            {
                Debug.Log(e);
            }
          
        }
    }   

    

    public void OnMapUpdate(int[][] tiles, Piece piece)
    {
        if (!isInit) return;

        var data = new MapData(tiles, piece);
        string json = JsonUtility.ToJson(data);
        json += "<EOF>";
        SendDataToServer(json);
    }

    public void Connect()
    {
        try
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(remoteEP);
                Debug.Log(string.Format("Socket connected to {0}", sender.RemoteEndPoint.ToString()));
                isInit = true;
            }

            #region Catch
            catch (System.ArgumentNullException ane)
            {
                Debug.Log(string.Format("ArgumentNullException : {0}", ane.ToString()));
            }
            catch (SocketException se)
            {
                Debug.Log(string.Format("SocketException : {0}", se.ToString()));
            }
            catch (System.Exception e)
            {
                Debug.Log(string.Format("Unexpected exception : {0}", e.ToString()));
            }

            #endregion

        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }



    private void SendDataToServer(string s = null)
    {
        try 
        {
            if (s == null) s = "try sending message<EOF>";
            byte[] msg = Encoding.ASCII.GetBytes(s);
            int bytesSent = sender.Send(msg);
        }
        #region Catch
        catch (System.ArgumentNullException ane)
        {
            Debug.Log(string.Format("ArgumentNullException : {0}", ane.ToString()));
        }
        catch (SocketException se)
        {
            Debug.Log(string.Format("SocketException : {0}", se.ToString()));
        }
        catch (System.Exception e)
        {
            Debug.Log(string.Format("Unexpected exception : {0}", e.ToString()));
        }
        #endregion
    }

    private void ShutDownClient()
    {
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
    }








    
}




