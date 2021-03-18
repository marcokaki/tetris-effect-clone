using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayField playFieldPrefab;
    public Player playerPrefab;
    

    private Dictionary<int, Player> players;


    private void Start()
    {
        players = new Dictionary<int, Player>();
    }



    public void InstantiatePlayer()
    {




        PlayField playField = Instantiate(playFieldPrefab);
        

/*
        player.Init(playField);*/


    }


}
