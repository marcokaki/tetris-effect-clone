using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    Player player;
    OtherPlayer otherplayer;

    public void HostGame()
    {
        TCPListener.Instance.Run();
        TCPClient.Instance.Connect();
    }

    private void JoinGame()
    {
        TCPClient.Instance.Connect();
    }
}
