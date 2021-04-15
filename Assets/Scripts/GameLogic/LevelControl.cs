using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelControl : MonoBehaviour {

    [Header("Prefab")]
    public Piece piecePrefab;
    public int seed = 1;
    public int queueSize = 1;
    public int startLevel = 0;

    [Header("UI")]
    public Transform nextBoxTransform;
    public TextMeshPro levelTMP;
    public TextMeshPro scoreTMP;
    public TextMeshPro lineClearedTMP;
    public TextMeshPro timePassedTMP;

    Queue<Piece> pieceQueue;
    System.Random _rnd;

    public void OnLineCleared(int lineCleared) {
        LineClearedAppend(lineCleared);
        ScoreAppend(lineCleared);
        LevelAppend(lineCleared);

        var pkt = new LineClearPacket() {
            lines = (short)currentLineCleared,
            level = (byte)currentLevel,
            score = currentScore,
        };

        NetManager.Instance.Client.SendToServer(pkt);
    }



    public void Init() {
        _rnd = new System.Random(seed);
        pieceQueue = new Queue<Piece>();

        VisualUpdateLineCleared();
        VisualUpdateScore();
        VisualUpdateLevel();

        while (pieceQueue.Count < queueSize) {
            pieceQueue.Enqueue(NewPiece());
        }
    }

    #region Piece 

    public Piece GetNextPiece() {
        pieceQueue.Enqueue(NewPiece());
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
    #endregion

    #region UI

    #region Data Update

    int currentLevel;
    int currentScore;
    int currentLineCleared;
    int timePassed;

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
    void LevelAppend(int lineCleared) {
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

        currentLevel++;
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
