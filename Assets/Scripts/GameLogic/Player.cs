using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour//tetrmino controller
{
    public Piece piecePrefab;
    public PlayField playField;

    Piece currentPiece;

    public float moveCooldown = 0.05f;
    float _moveCooldownRemain = 0;

    PlayerLevelControl control;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        control = GetComponent<PlayerLevelControl>();
        control.Init();
        GetPiece();
    } 

    private void Update()
    {
        _moveCooldownRemain -= Time.deltaTime;

        var kb = Keyboard.current;

        if (kb.sKey.isPressed) MovePiece(0, -1, 0);
        if (kb.wKey.isPressed) MovePiece(0, 1, 0);
        if (kb.aKey.isPressed) MovePiece(-1, 0, 0);
        if (kb.dKey.isPressed) MovePiece(1, 0, 0);
        if (kb.rKey.wasPressedThisFrame) MovePiece(0, 0, 1);

        if (kb.vKey.wasPressedThisFrame) {
            control.OnLineCleared(1);
        }
    }



    void GetPiece()
    {
        if (currentPiece != null) return;
        currentPiece = control.GetNextPiece();
        currentPiece.pos.x = 3;
        currentPiece.pos.y = 18;

        currentPiece.transform.SetParent(transform);
        OnPieceUpdate();
    }

    void MovePiece(int x, int y, int rotate) { MovePiece(new Vector2Int(x, y), rotate); }

    void MovePiece(Vector2Int offset, int rotate)
    {
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

    private void OnPieceUpdate()
    {        
        SetPiecePosition(currentPiece);
        UpdateMapToServerOnRefresh();
    }

    void SetPiecePosition(Piece p)
    {
        p.transform.localPosition = new Vector3(-playField.mapSize.x / 2  + p.pos.x + 2,
                                                -playField.mapSize.y / 2  + p.pos.y + 2);
    }

    #region Server Code
    public void HostGame()
    {
        NetManager.Instance.HostGame(this);
    }

    public void JoinGame()
    {
        NetManager.Instance.JoinGame(this);
    }

    public Action<int[][], Piece> mapUpdate;
    public void UpdateMapToServerOnRefresh()
    {
        mapUpdate?.Invoke(playField.tiles, currentPiece);
    }
    #endregion
}




