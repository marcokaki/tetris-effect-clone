using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    public static NetManager Instance {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<NetManager>();
            return _instance;
        }
    }
    public Client Client {
        get {
            if (client == null)
                client = new GameObject().AddComponent<Client>();
            return client;
        }
    }

    static NetManager _instance;
    RoomServer server;
    Client client;

    private void Awake() {
        server = new GameObject().AddComponent<RoomServer>();
        server.transform.SetParent(transform);
    }

    public void Host()    => server.Listen();
    public void Connect() => client.Connect();
}
