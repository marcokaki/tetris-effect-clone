using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OtherPlayer : MonoBehaviour {
    public int id;
    public PlayField playField;
    public Piece piecePrfab;

    public TextMeshPro awaitingTMP;

    Piece currentPiece;

    public void OnRecv(LineClearPacket pkt) {
        Debug.Log("lineClear");
    }

    public void OnRecv(PlayFieldPacket pkt) {
        currentPiece = pkt.piece;
        playField.MapUpdate(pkt.tileMap);
    }

    public void OnRecv(LoginPacket pkt) {
        awaitingTMP.gameObject.SetActive(false);
    }

    void SetPiecePosition(Piece p)
    {
        p.transform.localPosition = new Vector3(-playField.mapSize.x / 2 + p.pos.x + 2,
                                                -playField.mapSize.y / 2 + p.pos.y + 2);
    }
}
