using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OtherPlayer : MonoBehaviour {

    public Piece piecePrfab;    

    [Header("UI")]
    public Transform   nextBoxTransform;
    public TextMeshPro levelTMP;
    public TextMeshPro scoreTMP;
    public TextMeshPro lineClearedTMP;
    public TextMeshPro timePassedTMP;
    public TextMeshPro awaitingTMP;

    public PlayField playField;    

    Piece currentPiece;
    Piece nextPiece;

    public void OnRecv(LoginPacket pkt) {
        awaitingTMP.gameObject.SetActive(false);

        lineClearedTMP.text = "LINES-" + 0.ToString();
        scoreTMP.text = 0.ToString("0000000");
        levelTMP.text = 0.ToString("00");
    }

    public void OnRecv(LineClearPacket pkt) {
        lineClearedTMP.text = "LINES-" + pkt.lines.ToString();
        scoreTMP.text       = pkt.score.ToString("0000000");
        levelTMP.text       = pkt.level.ToString("00");
    }

    public void OnRecv(PlayFieldPacket pkt) {

        if(currentPiece == null) {
            currentPiece = Instantiate(piecePrfab, transform);
        }

        currentPiece.pos  = new Vector2Int   (pkt.piecePosX, pkt.piecePosY);
        currentPiece.type = (Piece.Shape.Type)pkt.pieceType;
        currentPiece.dir  = (Piece.Shape.Dir) pkt.pieceDir;

        playField.MapUpdate(pkt.tileMap);

        SetPiecePosition(currentPiece);
    }

    public void OnRecv(NextPiecePacket pkt) {
        if(nextPiece == null) {
            nextPiece = Instantiate(piecePrfab, transform);
            nextPiece.transform.SetParent(nextBoxTransform);
            nextPiece.transform.localPosition = Vector3.zero;
        }

        nextPiece.type = (Piece.Shape.Type)pkt.pieceType;
        nextPiece.dir  = (Piece.Shape.Dir) pkt.pieceDir;
    }



    void SetPiecePosition(Piece p)
    {
        p.transform.localPosition = new Vector3(-playField.mapSize.x / 2 + p.pos.x + 2,
                                                -playField.mapSize.y / 2 + p.pos.y + 2);        
    }
}
