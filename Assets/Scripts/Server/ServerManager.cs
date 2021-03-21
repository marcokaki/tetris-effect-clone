using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerManager : MonoBehaviour
{
    public OtherPlayer otherPlayer; // <= temparory

    public TMP_InputField hostInput;// <= temparory
    public TMP_InputField joinInput;// <= temparory
    private ServerManager() { }
    private static ServerManager instance;
    public static ServerManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<ServerManager>();
            return instance;
        }
    }

    public void HostGame(Player player)
    {
        Debug.Log("host game");

        TCPListener.Instance.Open(hostInput.text);
        TCPClient.Instance.Connect(hostInput.text);
        hostInput.interactable = false;
        joinInput.interactable = false;

        /*        var otherPlayer = new GameObject().AddComponent<OtherPlayer>();
                otherPlayer.AsJoinInit();*/

        TCPClient.Instance.othersMapUpdate += otherPlayer.UpdateMap;
        player.mapUpdate += TCPClient.Instance.OnMapUpdate;
        player.UpdateMapToServerOnRefresh();
    }

    public void JoinGame(Player player)
    {
        TCPClient.Instance.Connect(joinInput.text);
        joinInput.interactable = false;
        hostInput.interactable = false;

        /*        var otherPlayer = new GameObject().AddComponent<OtherPlayer>();
                otherPlayer.AsHostInit();*/

        TCPClient.Instance.othersMapUpdate += otherPlayer.UpdateMap;
        player.mapUpdate += TCPClient.Instance.OnMapUpdate;
        player.UpdateMapToServerOnRefresh();
    }
}
