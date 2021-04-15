using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerManager : MonoBehaviour
{
    public int clientCount = 9;

    List<Client> clients = new List<Client>();

    public void CreateFakeClients() {
        for(int i = 0; i < clientCount; i++) {
            clients.Add(new GameObject().AddComponent<Client>());
            clients[i].Connect();
        }
    }
}
