using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class RefillSystem : MonoBehaviour
{
    public int seed;
    public Tile tilePrefab;

    public RotationSystem rotationSystem;

    private System.Random rnd;
    private Type[] tetrominoType;

    private static RefillSystem _instance;
    public static RefillSystem Instance
    {
        get
        {
            if(_instance == null) { _instance = FindObjectOfType<RefillSystem>(); }
            return _instance;
        }
    }

    //refactor direcion
    //player input its id and generate a queue of tetromino
    //player can then input its id and get the next random tetromino later

    private void Start()
    {        
        tetrominoType = (from Type type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                         where type.IsSubclassOf(typeof(Tetromino)) select type).ToArray();

        rnd = new System.Random();
    }

    public Tetromino GetRandomTetromino()
    {
        //Tetromino piece = new GameObject().AddComponent(typeof(IPiece)) as Tetromino;
        Tetromino piece = new GameObject().AddComponent(tetrominoType[rnd.Next(0, 6)]) as Tetromino;
        
        piece.Instantiate(tilePrefab);
        return piece;
    }


}
