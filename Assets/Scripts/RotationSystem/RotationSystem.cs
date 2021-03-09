using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{
    //determine collision, wallkick?, harddrop, sofdrop etc, and also ground detection

    Vector2Int mapSize;
    Tile[,] tileMap;

    float lockDelay;

    public void Init(Tile[,] aTileMap, Vector2Int aMapSize)
    {
        mapSize = aMapSize;
        tileMap = aTileMap;
    }

    private Tuple<bool,bool> CollisionDetection(Tile[] tiles)
    {
        bool normalCollision = false;
        bool groundCollisiion = false;

        return Tuple.Create(normalCollision, groundCollisiion);
    }




}
