using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ICSideRecviable//tetrmino controller
{
    public PlayField playField;

    Piece currentPiece;

    float _graviateCooldownRemain = 0;

    LevelControl control;

    private void Awake() {
        Application.targetFrameRate = 60;
        NetManager.Instance.Client.RegisterListener(this, CSideCmd.login);
    }

    private void Start() {
        control = GetComponent<LevelControl>();
        control.Init();
        GetPiece();

        _graviateCooldownRemain = control.currentSpeed;
    }


    private void Update() {
        _graviateCooldownRemain -= Time.deltaTime;

/*        if(_graviateCooldownRemain <= 0) {
            MovePiece(0, -1, 0);
            _graviateCooldownRemain = control.currentSpeed;
        }*/

        var kb = Keyboard.current;

        if (kb.sKey.wasPressedThisFrame) MovePiece( 0, -1, 0);
        if (kb.wKey.wasPressedThisFrame) MovePiece( 0,  1, 0);
        if (kb.aKey.wasPressedThisFrame) MovePiece(-1,  0, 0);
        if (kb.dKey.wasPressedThisFrame) MovePiece( 1,  0, 0);
        if (kb.rKey.wasPressedThisFrame) MovePiece( 0,  0, 1);
    }

    void OnPieceUpdate() {
        SetPiecePosition(currentPiece);
        NetManager.Instance.Client.SendToServer(GetPlayFieldPacket());
    }

    void ICSideRecviable.OnRecv(NEPacket<CSideCmd> pkt) {
        NetManager.Instance.Client.SendToServer(GetPlayFieldPacket());
    }

    void GetPiece() {
        if (currentPiece != null) return;
        currentPiece = control.GetNextPiece();
        currentPiece.pos.x = 3;
        currentPiece.pos.y = 18;

        currentPiece.transform.SetParent(transform);
        OnPieceUpdate();
    }

    void MovePiece(int x, int y, int rotate) => MovePiece(new Vector2Int(x, y), rotate);

    void MovePiece(Vector2Int offset, int rotate) {

        var pos = currentPiece.pos + offset;
        var newDir = currentPiece.NextDir(rotate);
        var shape = Piece.shapeTable.GetShape(currentPiece.type, newDir);

        if (playField.IsOverlapped(shape, pos)) 
        {
            if(offset.y < 0)
            {
                int lineCleared = playField.OnPieceGroundHit(currentPiece);

                if(lineCleared != 0) {
                    control.OnLineCleared(lineCleared);
                }                

                Destroy(currentPiece.gameObject);
                currentPiece = null;
                GetPiece();
            }
            return;
        }

        currentPiece.pos += offset;
        currentPiece.dir  = newDir;

        OnPieceUpdate();
    }

    void SetPiecePosition(Piece p) {
        p.transform.localPosition = new Vector3(-playField.mapSize.x / 2  + p.pos.x + 2,
                                                -playField.mapSize.y / 2  + p.pos.y + 2);
    }

    PlayFieldPacket GetPlayFieldPacket() {
        var pkt = new PlayFieldPacket() {
            piece = currentPiece,
            tileMap = playField.tiles,
        };
        return pkt;
    }

}




