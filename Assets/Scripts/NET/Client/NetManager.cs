using UnityEngine;
using TMPro;
using System.Net;
using UnityEngine.InputSystem;

public class NetManager : MonoBehaviour
{
    public OtherPlayer otherPlayer; // <= temparory

    public TMP_InputField hostInput;// <= temparory
    public TMP_InputField joinInput;// <= temparory
    private NetManager() { }
    private static NetManager instance;
    public static NetManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<NetManager>();
            return instance;
        }
    }

    void Update() {

        var kb = Keyboard.current;

    }

    Server gateway;


    public void HostGame(Player player)
    {

        gateway = new GameObject().AddComponent<Server>();
        gateway.Listen();



/*        TCPListener.Instance.Open(hostInput.text);
        TCPClient.Instance.Connect(hostInput.text);*/
        hostInput.interactable = false;
        joinInput.interactable = false;

    }

    Player player;


    //map => recieve data from player;

    Client client;


    public void JoinGame(Player player) {

        client = new GameObject().AddComponent<Client>();
        client.Connect();




        {
            /*        Client.Instance.Connect(joinInput.text);
            joinInput.interactable = false;
            hostInput.interactable = false;

            //TCPClient.Instance.othersMapUpdate += otherPlayer.UpdateMap;
            //player.mapUpdate += TCPClient.Instance.OnMapUpdate;
            player.UpdateMapToServerOnRefresh();*/
        }

    }
}
