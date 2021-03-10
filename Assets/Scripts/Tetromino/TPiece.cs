using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPiece : Tetromino
{
    public override Tuple<Coord, Coord[]> GetInheritedPieceInfo()
    {
        Coord pivotCoord = new Coord(5, 19);

        Coord[] localCoords = new Coord[4];
        localCoords[0] = new Coord(0, 0);
        localCoords[1] = new Coord(1, 0);
        localCoords[2] = new Coord(-1, 0);
        localCoords[3] = new Coord(0, -1);

        return Tuple.Create(pivotCoord, localCoords);
    }


    //correct rotation
}
