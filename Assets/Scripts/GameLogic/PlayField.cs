using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayField : MonoBehaviour
{
    public Vector2Int mapSize = new Vector2Int(10, 20);

    private int mapWidth => mapSize.x;
    private int mapHeight => mapSize.y;

    public int[][] tiles { get; private set; }

    private void Awake() {

        tiles = new int[mapSize.y][];
        for (int y = 0; y < mapSize.y; y++) tiles[y] = new int[mapSize.x];
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

    public void OnPieceGroundHit(Piece piece)
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
        CheckLineClear();
    }

    private void CheckLineClear()
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
                //Array.Clear(tiles[y], 0, tiles[y].Length); <= Bug i.e. all tiles got cleared dun know why?
                tiles[y] = new int[mapSize.x]; // temperory code;                
            }
        }
/*        if (nullCount == 0) return; <= BUG

        for(int y = mapSize.y - nullCount; y < mapSize.y; y++)
        {
            tiles[y] = new int[mapSize.x];
        }*/
    }

    public void MapUpdate(int[] flattenTiles)
    {
        int count = 0;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                tiles[y][x] = flattenTiles[count];
                count++;
            }
        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        var drawSize = Piece.drawSize;

        if (tiles == null) return;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (tiles[y][x] == 0) continue;

                var pos = Vector3.Scale(new Vector3(-mapSize.x / 2 + x + 0.5f, -mapSize.y / 2 + y + 0.5f, 0), drawSize);
                Gizmos.DrawWireCube(pos, drawSize);
            }
        }
    }


}
