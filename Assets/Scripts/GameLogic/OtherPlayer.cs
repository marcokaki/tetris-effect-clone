using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    Piece currentPiece;
    PlayField playField;

    public void UpdateMap(MapData data)
    {
        if (playField == null) playField = GetComponent<PlayField>();
        playField.MapUpdate(data.tiles);

        if (currentPiece == null)
        {
            var pieceObj = new GameObject("Piece");
            pieceObj.transform.SetParent(transform, false);
            currentPiece = pieceObj.AddComponent<Piece>();
            
            currentPiece.pos.x = data.sPosX;
            currentPiece.pos.y = data.sPosY;
            currentPiece.transform.position = new Vector3(-playField.mapSize.x / 2 + 0.5f, -playField.mapSize.y / 2 + 0.5f);
            currentPiece.transform.position += transform.position;
        }
        currentPiece.type = (Piece.Shape.Type)data.sType;
        currentPiece.pos.x = data.sPosX;
        currentPiece.pos.y = data.sPosY;
        currentPiece.dir = (Piece.Shape.Dir)data.sDir;
    }
}
