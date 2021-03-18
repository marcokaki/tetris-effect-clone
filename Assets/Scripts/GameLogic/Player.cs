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

    public Action mapUpdate;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        rnd = new System.Random(seed);
        playField = GetComponent<PlayField>();

        NewPiece();
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


    void NewPiece()
    {
        if (!currentPiece)
        {
            var pieceObj = new GameObject("Piece");
            pieceObj.transform.SetParent(transform, false);
            currentPiece = pieceObj.AddComponent<Piece>();            
        }
        currentPiece.RandomShape(rnd.Next(0, Piece.Shape.typeCount));
        currentPiece.pos.x = 3;
        currentPiece.pos.y =18;
        currentPiece.transform.position = new Vector3(-playField.mapSize.x / 2 + 0.5f, -playField.mapSize.y / 2 + 0.5f);
        currentPiece.transform.position += transform.position;

        OnMapUpdated();
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
                NewPiece();
            }
            return;
        }

        currentPiece.pos += offset;
        currentPiece.dir = newDir;

        OnMapUpdated();
    }

    private void OnMapUpdated()
    {
        TCPClient.Instance.OnMapUpdate(playField.tiles, currentPiece);
    }
}


[Serializable]
public struct MapData
{
    public int[] tiles;
    public int sPosX;
    public int sPosY;
    public int sType;
    public int sDir;
    public int mapSizeX;
    public int mapSizeY;

    public MapData(int[][] aTiles, Piece aPiece)
    {
        tiles = aTiles.SelectMany(t => t).ToArray();// <<<
        sPosX = aPiece.pos.x;
        sPosY = aPiece.pos.y;
        sType = (int)aPiece.type;
        sDir = (int)aPiece.dir;
        mapSizeX = aTiles[0].Length;
        mapSizeY = aTiles.Length;
    }
}

