using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayField : MonoBehaviour
{
    //as tileMap
    public Vector2Int mapSize = new Vector2Int(10, 20);
    public Tile[,] TileMap { get; private set; }

    private void Awake()
    {
        TileMap = new Tile[mapSize.x, mapSize.y];
    }



    public void UpdateTileMap(Tile[] tiles)
    {
        for(int i = 0; i < 4; i++)
        {
            Tile tile = tiles[i];
            Coord coord = tile.coord;
            //Debug.Log(coord + "updated");
            TileMap[coord.x, coord.y] = tile;
            tile.transform.SetParent(transform);
        }

        CheckAnyLineClear();
        
    }

    public void CheckAnyLineClear()
    {
        List<int> clearedLines = new List<int>();
        for (int y = 0; y < mapSize.y; y++)
        {
            bool lineClear = true;
            for (int x = 0; x < mapSize.x; x++)
            {
                Tile tile = TileMap[x, y];

                if (tile == null)
                {
                    lineClear = false;
                    break;
                }
            }
            if (lineClear)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    Destroy(TileMap[x, y].gameObject);
                    TileMap[x, y] = null;
                }
                clearedLines.Add(y);
            }
        }

        Gravitate(clearedLines);

        // double, triple, tetris check
        //

    }

    public void Gravitate(List<int> clearedlines)
    {
        if (clearedlines == null || clearedlines.Count == 0) return;

        for (int y = 0; y < mapSize.y; y++)
        {
            int nullCount = 0;
            foreach (int emptyRow in clearedlines) { if (y > emptyRow) nullCount++; }

            for (int x = 0; x < mapSize.x; x++)
            {
                if (nullCount > 0)
                {
                    Tile tile = TileMap[x, y];
                    if (tile != null)
                    {
                        Coord newCoord = new Coord(tile.coord.x, tile.coord.y - nullCount);
                        TileMap[newCoord.x, newCoord.y] = tile;
                        tile.transform.position = Coord.CoordToPostion(newCoord, mapSize);
                        tile.coord = newCoord;
                        TileMap[x, y] = null;
                    }
                }
            }
        }
    }


}
