using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour//tetrmino controller
{
    public int seed;

    System.Random rnd;
    PlayField playField;
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
        playField = GetComponent<PlayField>();
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
        var pieceObj = new GameObject("Piece");
        pieceObj.transform.SetParent(transform, false);
        Piece piece = pieceObj.AddComponent<Piece>();

        piece.RandomShape(rnd.Next(0, Piece.Shape.typeCount));
        piece.pos.x = 12;// <= Next Box Position
        piece.pos.y = 9;

        piece.transform.position = GetPieceWorldPosition();

        return piece;
    }

    void GetPiece()
    {
        if (currentPiece != null) return;
        currentPiece = nextPieceQueue.Dequeue();
        currentPiece.pos.x = 3;
        currentPiece.pos.y = 18;
        currentPiece.transform.position = GetPieceWorldPosition();

        nextPieceQueue.Enqueue(NewPiece());

        UpdateMapToServerOnRefresh();
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

        UpdateMapToServerOnRefresh();
    }

    Vector3 GetPieceWorldPosition()
    {
        return new Vector3(-playField.mapSize.x / 2 + 0.5f + transform.position.x, -playField.mapSize.y / 2 + 0.5f + transform.position.y);
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




