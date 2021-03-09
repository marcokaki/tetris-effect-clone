using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPiece : Tetromino
{
    public override Tuple<Coord, Coord[]> GetInheritedPieceInfo()
    {
        Coord pivotCoord = new Coord(5, 18);

        Coord[] localCoords = new Coord[4];
        localCoords[0] = new Coord(0, 0);
        localCoords[1] = new Coord(0, 1);
        localCoords[2] = new Coord(-1, 1);
        localCoords[3] = new Coord(1, 0);

        return Tuple.Create(pivotCoord, localCoords);
    }


    public override void SetRotationAnticipatedCoord()
    {
        if(horizontal)
        {
            Vector2Int offset = new Vector2Int(0, 1);
            for (int i = 0; i < tiles.Length; i++)
            {
                Coord coord = Coord.Get90RotationCoord(tiles[i].worldcoord - pivotCoord);
                tiles[i].anitcipateCoord = new Coord(coord.x, coord.y) + pivotCoord + offset;
            }
            anticipatePivotCoord += offset;
        }
        else
        {
            Vector2Int offset = new Vector2Int(0, -1);
            for (int i = 0; i < tiles.Length; i++)
            {
                Coord coord = Coord.Get270RotationCoord(tiles[i].worldcoord- pivotCoord);
                tiles[i].anitcipateCoord = new Coord(coord.x, coord.y) + pivotCoord + offset;
            }
            anticipatePivotCoord += offset;
        }
    }
}
