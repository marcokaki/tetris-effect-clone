using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour//tetrmino controller
{
    public Piece piecePrefab;
    public int seed;

    private System.Random rnd;
    private PlayField playField;

    private void Awake()
    {
        rnd = new System.Random(seed);
        playField = GetComponent<PlayField>();
    }

    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame) StartGame();
    }

    private void StartGame()
    {
        for (int x = 0; x < playField.mapSize.x; x++)
        {
            for (int y = 0; y < playField.mapSize.y; y++)
            {
                Tile tile = playField.TileMap[x, y];
                if (tile != null)
                {
                    Destroy(tile.gameObject);
                    playField.TileMap[x, y] = null;
                }
            }
        }

        SetNewTetromino();
    }

    private void SetNewTetromino()
    {
        Piece piece = Instantiate(piecePrefab, transform);
        piece.Init(rnd.Next(0, 6), playField.TileMap);

        if (!TopOut()) { GameOver(); }

        void onGroundHit(Tile[] tiles)
        {
            playField.UpdateTileMap(tiles);
            Destroy(piece.gameObject);
            piece = null;
            SetNewTetromino();
        }

        piece.EnableGravity(1, onGroundHit);
    }

    private bool TopOut()
    {
        return false;
    }

    private void GameOver()
    {

    }


    class Level
    {
        public int currentLevel;
        public int framePerGrid;
    }



}
