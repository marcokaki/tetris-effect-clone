using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelControl : MonoBehaviour, ICSideRecviable {

    [Header("Prefab")]
    public Piece piecePrefab;
    public int seed = 1;
    public int queueSize = 1;
    public int startLevel = 0;

    [Header("UI")]
    public Transform   nextBoxTransform;
    public TextMeshPro levelTMP;
    public TextMeshPro scoreTMP;
    public TextMeshPro lineClearedTMP;
    public TextMeshPro timePassedTMP;

    Queue<Piece> pieceQueue;
    System.Random _rnd;

    public float currentSpeed { get; private set; } = 48f/60f;

    public void OnLineCleared(int lineCleared) {
        LineClearedAppend(lineCleared);
        ScoreAppend(lineCleared);
        LevelAppend(lineCleared);

        {//Network Code
            var pkt = new LineClearPacket() {
                lines = (short)currentLineCleared,
                level = (byte)currentLevel,
                score = currentScore,
            };

            NetManager.Instance.Client.SendToServer(pkt);
        }
    }
    void ICSideRecviable.OnRecv(NEPacket<CSideCmd> pkt) {
        SendNextPiecePacket();
    }

    public void Init() {
        _rnd = new System.Random(seed);
        pieceQueue = new Queue<Piece>();
        NetManager.Instance.Client.RegisterListener(this, CSideCmd.login);

        VisualUpdateLineCleared();
        VisualUpdateScore();
        VisualUpdateLevel();

        while (pieceQueue.Count < queueSize) {
            pieceQueue.Enqueue(NewPiece());
        }
    }

    #region Piece 

    public bool Gravitate() {
        return true;
    }

    public Piece GetNextPiece() {

        var piece = NewPiece();
        pieceQueue.Enqueue(piece);

        SendNextPiecePacket(piece);

        return pieceQueue.Dequeue();
    }

    Piece NewPiece() {

        Piece piece = Instantiate(piecePrefab);
        piece.transform.SetParent(transform, true);

        piece.RandomShape(_rnd.Next(0, Piece.Shape.typeCount));

        piece.transform.SetParent(nextBoxTransform);

        piece.transform.localPosition = Vector3.zero;      

        return piece;
    }

    void SendNextPiecePacket(Piece p = null) {
        var nextPiece = p != null ? p : pieceQueue.Peek();
        var pkt = new NextPiecePacket() {
            piece = nextPiece,
        };
        NetManager.Instance.Client.SendToServer(pkt);
    }
    #endregion

    #region UI

    #region Data Update

    
    int currentFrame       = 48;
    int currentLevel       = 0;
    int currentScore       = 0;
    int currentLineCleared = 0;
    int timePassed         = 0;

    int lineToAdvance;

    void LineClearedAppend(int lineCleared) {
        currentLineCleared += lineCleared;
        VisualUpdateLineCleared();
    }

    //Score   : https://tetris.wiki/Scoring
    void ScoreAppend(int lineCleared) {
        switch (lineCleared) {
            case 1:
                currentScore += 40   * (currentLevel + 1);
                break;
            case 2:
                currentScore += 100  * (currentLevel + 1);
                break;
            case 3:
                currentScore += 300  * (currentLevel + 1);
                break;
            case 4:
                currentScore += 1200 * (currentLevel + 1);
                break;
        }
        VisualUpdateScore();
    }

    //Speed Lv: https://tetris.wiki/Tetris_(NES,_Nintendo)
    public void LevelAppend(int lineCleared) {
        lineToAdvance += lineCleared;

        if (startLevel == -1) {
            if (lineToAdvance < 10) return;
        }
        else {
            if (lineToAdvance < startLevel * 10 + 10 &&
                lineToAdvance < Mathf.Max(startLevel * 10 - 50, 100))
                return;

            startLevel = -1;
        }

        int l = currentLevel++;

        if (l < 8) currentFrame -= 5;
        else if (l < 9) currentFrame -= 2;
        else if (l < 29 && (l == 9 || l == 12 || l == 15 || l == 18 || l == 28)) currentFrame -= 1;

        currentSpeed = currentFrame / 60f;
        lineToAdvance = 0;

        VisualUpdateLevel();
    }
    #endregion

    #region Visual Update
    void VisualUpdateLineCleared() {
        if (!lineClearedTMP) 
            return;
        lineClearedTMP.text = "LINES-" + currentLineCleared.ToString();
    }
    void VisualUpdateScore() {
        if (!scoreTMP) 
            return;
        scoreTMP.text = currentScore.ToString("0000000"); 
    }
    void VisualUpdateLevel() {
        if (!levelTMP)
            return;
        levelTMP.text = currentLevel.ToString("00"); 
    }
    #endregion
    #endregion




}
