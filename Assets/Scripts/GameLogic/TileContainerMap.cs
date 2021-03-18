using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileContainerMap : MonoBehaviour
{
    public Vector2Int mapSize;
    public TileContainer containerPrefab;
    public Transform tmp;
    

    void Start()
    {
        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                Vector3 pos = Coord.CoordToPostion(new Coord(x,y), mapSize);
                TileContainer container = Instantiate(containerPrefab, pos, Quaternion.identity, transform);
            }
        }

        for (int x = 0; x < mapSize.x; x++)
        {
            Vector3 pos = Coord.CoordToPostion(new Coord(x, -1), mapSize);
            Instantiate(tmp, pos, Quaternion.identity, transform).GetComponent<TextMeshPro>().text = x.ToString();

        }

        for (int y = 0; y < mapSize.y; y++)
        {
            Vector3 pos = Coord.CoordToPostion(new Coord(-1, y), mapSize);
            Instantiate(tmp, pos, Quaternion.identity, transform).GetComponent<TextMeshPro>().text = y.ToString();
        }

    }
}
