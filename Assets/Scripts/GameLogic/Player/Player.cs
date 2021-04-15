using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour//tetrmino controller
{
    public PlayField playField;

    Piece currentPiece;

    public float moveCooldown = 0.05f;
    float _moveCooldownRemain = 0;

    LevelControl control;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        control = GetComponent<LevelControl>();
        control.Init();
        GetPiece();
    } 

    private void Update() {
        _moveCooldownRemain -= Time.deltaTime;

        var kb = Keyboard.current;

        if (kb.sKey.isPressed) MovePiece( 0, -1, 0);
        if (kb.wKey.isPressed) MovePiece( 0,  1, 0);
        if (kb.aKey.isPressed) MovePiece(-1,  0, 0);
        if (kb.dKey.isPressed) MovePiece( 1,  0, 0);
        if (kb.rKey.wasPressedThisFrame) MovePiece(0, 0, 1);
    }

    void GetPiece() {
        if (currentPiece != null) return;
        currentPiece = control.GetNextPiece();
        currentPiece.pos.x = 3;
        currentPiece.pos.y = 18;

        currentPiece.transform.SetParent(transform);
        OnPieceUpdate();
    }

    void MovePiece(int x, int y, int rotate) { MovePiece(new Vector2Int(x, y), rotate); }

    void MovePiece(Vector2Int offset, int rotate) {
        if (_moveCooldownRemain > 0) return;
        _moveCooldownRemain = moveCooldown;

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
        currentPiece.dir = newDir;

        OnPieceUpdate();
    }

    private void OnPieceUpdate() {
        SetPiecePosition(currentPiece);

        var pkt = new PlayFieldPacket() {
            piece = currentPiece,
            tileMap = playField.tiles,
        };

        NetManager.Instance.Client.SendToServer(pkt);
    }

    void SetPiecePosition(Piece p) {
        p.transform.localPosition = new Vector3(-playField.mapSize.x / 2  + p.pos.x + 2,
                                                -playField.mapSize.y / 2  + p.pos.y + 2);
    }

    
}




