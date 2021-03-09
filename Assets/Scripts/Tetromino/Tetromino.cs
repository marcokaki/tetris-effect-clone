using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Tetromino: MonoBehaviour
{
    protected Vector2Int mapSize;
    protected Tile[,] tileMap;

    protected Coord pivotCoord;
    protected Coord anticipatePivotCoord;
    protected Tile[] tiles;
    protected bool horizontal;

    PlayerController controller;

    private void Awake()
    {
        controller = new PlayerController();
        controller.Gameplay.Down.performed += ctx => Movement(new Vector2Int(0,-1));
        controller.Gameplay.Up.performed += ctx => HardDrop();
        controller.Gameplay.Left.performed += ctx => Movement(new Vector2Int(-1,0));
        controller.Gameplay.Right.performed += ctx => Movement(new Vector2Int(1,0));
        controller.Gameplay.Rotate.performed += ctx => Rotation();        
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }

    #region Instantiate
    public void Instantiate(Tile tilePrefab)
    {
        gameObject.SetActive(false);

        (Coord pivotCoord, Coord[] localCoord) = GetInheritedPieceInfo();

        Instantiate(tilePrefab, pivotCoord, localCoord);        
    }

    public abstract  Tuple<Coord,Coord[]> GetInheritedPieceInfo();

    private void Instantiate(Tile tilePrefab, Coord aPivotCoord, Coord[] localCoords)
    {
        pivotCoord = aPivotCoord;
        anticipatePivotCoord = pivotCoord;
        tiles = new Tile[localCoords.Length];
        horizontal = true;       

        for (int i = 0; i < localCoords.Length; i++)
        {
            Coord localCoord = localCoords[i];
            Coord worldCoord = pivotCoord + localCoord;

            Vector3 pos = new Vector3(localCoord.x, localCoord.y);
            
            Tile tile = tiles[i] = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            tile.anitcipateCoord = worldCoord;
            tile.worldcoord = worldCoord;

            tile.name = tile.ToString();
        }
        name = GetType().ToString();
    }
    public void Init(Tile[,] aTileMap, Vector2Int aMapSIze)//by player
    {
        tileMap = aTileMap;
        mapSize = aMapSIze;
        transform.position = Coord.CoordToPostion(pivotCoord, mapSize);

        gameObject.SetActive(true);

        InitHardDropShadow();
    }
    #endregion

    public void Movement(Vector2Int dir)
    {
        anticipatePivotCoord += dir;

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].anitcipateCoord += dir;            
        }    

        if (!CollisionDetected(tiles))
        {
            OnSucessTransformation();            
        }
        else
        {
            OnFailTransformation();

            if (dir.y < 0)
            {
                if (hitGroundAction != null)
                {
                    //await lockDelay;

                    hitGroundAction?.Invoke();
                }
            }
        }
        SetHardDropPosition();
    }

    #region HardDrop

    Tile[] hardDropShadow;

    private void InitHardDropShadow()
    {
        GameObject shadow = new GameObject{ name = "shadow" };
        shadow.transform.SetParent(transform);

        hardDropShadow = new Tile[tiles.Length];
        for(int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];
            Tile shadowTile = hardDropShadow[i] = Instantiate(tile, tile.transform.position, Quaternion.identity, shadow.transform);
            shadowTile.worldcoord = tile.worldcoord;
            shadowTile.anitcipateCoord = tile.worldcoord;
            shadowTile.SetAlpha(.2f);
        }
        SetHardDropPosition();
    }


    private void SetHardDropPosition()
    {
        int dropDis = 0;
        do
        {
            dropDis++;
            for (int i = 0; i < tiles.Length; i++)
            {
                Coord coord = tiles[i].worldcoord;
                hardDropShadow[i].anitcipateCoord = new Coord(coord.x, coord.y - dropDis);
            }
        } while (!CollisionDetected(hardDropShadow));

        for(int i = 0; i < tiles.Length; i++)
        {
            Tile shadowTile = hardDropShadow[i];
            shadowTile.worldcoord = new Coord(shadowTile.anitcipateCoord.x, shadowTile.anitcipateCoord.y + 1);
            shadowTile.transform.position = Coord.CoordToPostion(shadowTile.worldcoord, mapSize);
        }
    }

    public void HardDrop()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];
            tile.transform.position = hardDropShadow[i].transform.position;
            tile.worldcoord = hardDropShadow[i].worldcoord;
        }
        if(hitGroundAction != null) { hitGroundAction?.Invoke(); }
    }

    #endregion

    #region Rotation

    public void Rotation()
    {
        SetRotationAnticipatedCoord();

        if (!CollisionDetected(tiles))
        {
            horizontal = !horizontal;
            OnSucessTransformation();
            SetHardDropPosition();
        }
        else
        {
            OnFailTransformation();
        }
    }

    public virtual void SetRotationAnticipatedCoord()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            Coord coord = tiles[i].worldcoord - pivotCoord;
            tiles[i].anitcipateCoord = Coord.Get90RotationCoord(coord) + pivotCoord;
        }
    }

    #endregion

    private void OnSucessTransformation()
    {
        pivotCoord = anticipatePivotCoord;

        for(int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];

            tile.worldcoord = tile.anitcipateCoord;
            tile.transform.position = Coord.CoordToPostion(tile.worldcoord, mapSize);
            tile.name = tile.ToString();
        }
    }

    private void OnFailTransformation()
    {
        anticipatePivotCoord = pivotCoord;

        for(int i = 0; i < tiles.Length; i++)
        {
            Tile tile = tiles[i];
            tile.anitcipateCoord = tile.worldcoord;
        }
    }

    protected virtual bool CollisionDetected(Tile[] aTiles)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            Coord coord = aTiles[i].anitcipateCoord;
            
            if (coord.x < 0 || coord.x >= mapSize.x) return true;
            if (coord.y < 0 || coord.y >= mapSize.y) return true;
            if (tileMap.Any(t => t.worldcoord == coord)) return true;
        }
        return false;
    }

    #region Gravitate

    private Action hitGroundAction;

    public Task<Tile[]> HitGround(int frame)
    {
        var tcs = new TaskCompletionSource<Tile[]>();

        //InvokeRepeating(nameof(ApplyGravity), frame, frame);

        void lambda()
        {
            //CancelInvoke(nameof(ApplyGravity));
            hitGroundAction -= lambda;            
            tcs.TrySetResult(tiles);                        
        }
        hitGroundAction += lambda;

        return tcs.Task;
    }

    private void ApplyGravity()
    {
        Movement(new Vector2Int(0, -1));
    }



    #endregion





}
