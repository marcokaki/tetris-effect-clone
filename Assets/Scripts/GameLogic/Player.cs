using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour//tetrmino controller
{
    public Piece piecePrefab;
    public PlayField playField;
    public int seed;

    System.Random rnd;
    Piece currentPiece;

    public float moveCooldown = 0.05f;
    float _moveCooldownRemain = 0;

    Queue<Piece> nextPieceQueue;
    readonly int nextCount = 1;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        rnd = new System.Random(seed);
        nextPieceQueue = new Queue<Piece>();

        while(nextPieceQueue.Count < nextCount)
        {
            nextPieceQueue.Enqueue(NewPiece());
        }
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
    }

    Piece NewPiece()
    {
        Piece piece = Instantiate(piecePrefab);
        piece.transform.SetParent(transform, true);

        piece.RandomShape(rnd.Next(0, Piece.Shape.typeCount));

        piece.transform.SetParent(playField.NextBoxTransform);

        piece.transform.localPosition = Vector3.zero;

        return piece;
    }

    void GetPiece()
    {
        if (currentPiece != null) return;
        currentPiece = nextPieceQueue.Dequeue();
        currentPiece.pos.x = 3;
        currentPiece.pos.y = 18;

        currentPiece.transform.SetParent(transform);

        nextPieceQueue.Enqueue(NewPiece());

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
                playField.OnPieceGroundHit(currentPiece);
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
        ServerManager.Instance.HostGame(this);
    }

    public void JoinGame()
    {
        ServerManager.Instance.JoinGame(this);
    }

    public Action<int[][], Piece> mapUpdate;
    public void UpdateMapToServerOnRefresh()
    {
        mapUpdate?.Invoke(playField.tiles, currentPiece);
    }
    #endregion
}




