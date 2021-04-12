using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public PlayField playField;
    public Piece piecePrfab;

    Piece currentPiece;    

/*    public void UpdateMap(MapData data)
    {
        if (currentPiece == null)
        {
            currentPiece = Instantiate(piecePrfab, transform);
            
            currentPiece.pos.x = data.sPosX;
            currentPiece.pos.y = data.sPosY;
        }
        currentPiece.type = (Piece.Shape.Type)data.sType;
        currentPiece.pos.x = data.sPosX;
        currentPiece.pos.y = data.sPosY;
        currentPiece.dir = (Piece.Shape.Dir)data.sDir;

        SetPiecePosition(currentPiece);

        playField.MapUpdate(data.tiles);
    }
*/
    void SetPiecePosition(Piece p)
    {
        p.transform.localPosition = new Vector3(-playField.mapSize.x / 2 + p.pos.x + 2,
                                                -playField.mapSize.y / 2 + p.pos.y + 2);
    }
}
