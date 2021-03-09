using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour//tetrmino controller
{
    private PlayField playField;

    PlayerController controller;

    private void Awake()
    {
        playField = GetComponent<PlayField>();
        controller = new PlayerController();
        controller.Gameplay.Start.performed += cts => StartGame();
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }

    private void TestingField()
    {
        for(int x = 0; x < playField.mapSize.x - 1; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                Coord coord = new Coord(x, y);
                Vector3 pos = Coord.CoordToPostion(coord, playField.mapSize);
                playField.TileMap[x, y] = Instantiate(RefillSystem.Instance.tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }

    private async void StartGame()
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
        await TetrominoGravitateAsync();
    }

    private async Task TetrominoGravitateAsync()
    {
        Tetromino piece = RefillSystem.Instance.GetRandomTetromino();
        piece.Init(playField.TileMap, playField.mapSize);

        Tile[] tiles = await piece.HitGround(1);

        playField.UpdateTileMap(tiles);

        Destroy(piece.gameObject);

        if (!TopOut())
        {
            await TetrominoGravitateAsync();
        }
    }

    private bool TopOut()
    {
        return false;
    }


    class Level
    {
        public int currentLevel;
        public int framePerGrid;
    }



}
