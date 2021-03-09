using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPiece : Tetromino
{
    public override Tuple<Coord, Coord[]> GetInheritedPieceInfo()
    {
        Coord pivotCoord = new Coord(5, 18);

        Coord[] localCoords = new Coord[4];
        localCoords[0] = new Coord(0, 0);
        localCoords[1] = new Coord(0, 1);
        localCoords[2] = new Coord(-1, 0);
        localCoords[3] = new Coord(-1, 1);

        return Tuple.Create(pivotCoord, localCoords);
    }

    public override void SetRotationAnticipatedCoord()
    {
        return;
    }

}
