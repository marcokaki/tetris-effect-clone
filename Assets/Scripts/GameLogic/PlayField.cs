using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayField : MonoBehaviour
{
    public Vector2Int mapSize = new Vector2Int(10, 20);

    public int[][] tiles { get; private set; }

    int mapWidth => mapSize.x;
    int mapHeight => mapSize.y; 

    float[] tileMapArray;
    Material mat;
    //TileMapMeshModifier _modifier;


    private void Awake() {

        tiles = new int[mapSize.y][];
        for (int y = 0; y < mapSize.y; y++) tiles[y] = new int[mapSize.x];
        tileMapArray = new float[mapSize.x * mapSize.y];
        mat = GetComponent<Renderer>().material;
        //_modifier = new TileMapMeshModifier(GetComponent<MeshFilter>().mesh, mapSize);

    }

    public bool IsOverlapped(Piece.Shape shape, Vector2Int pos)
    {
        var s = shape;
        for(int y = 0; y < 4; y++)
        {
            for(int x = 0; x < 4; x++)
            {
                if (s.cells[x, y] == 0) continue;

                int px = pos.x + x;
                int py = pos.y + y;

                if (px < 0 || px >= mapSize.x) return true;
                if (py < 0) return true;
                if (py < mapSize.y && tiles[py][px] != 0) return true;
            }
        }
        return false;
    }

    public int OnPieceGroundHit(Piece piece)
    {
        var s = piece.shape;
        var pos = piece.pos;
        for(int y = 0; y < 4; y++)
        {
            for(int x = 0; x < 4; x++)
            {
                if (s.cells[x, y] == 0) continue;

                int px = pos.x + x;
                int py = pos.y + y;

                tiles[py][px] = 1;
            }
        }

        return CheckLineClear();
    }

    int CheckLineClear()
    {
        int nullCount = 0;

        for(int y = 0; y < mapSize.y; y++) 
        {
            if(Array.TrueForAll(tiles[y], t => t == 1)) 
            {
                nullCount++;
            }
            else if (nullCount > 0)
            {
                tiles[y - nullCount] = tiles[y];
                tiles[y] = new int[mapSize.x]; // temperory code;                
            }
        }

        return nullCount;
    }

    public void MapUpdate(int[][] tiles)
    {
        this.tiles = tiles;
    }

    private void Update() => OnDrawMap();

    private void OnDrawMap()
    {
        //_modifier.OnDrawTileMapTriangles(tiles);

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                tileMapArray[x + mapWidth * y] = tiles[y][x];
            }
        }
        mat.SetFloatArray("_TileMapArray", tileMapArray);
    }

}
