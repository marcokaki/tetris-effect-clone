using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Piece : MonoBehaviour
{
    public Tile tilePrefab;

    Tile[,] mapOccupiedInfo;
    Vector2Int mapSize;
    int[][,] shapes;

    int rotationID;
    int RotationID {
        get { return rotationID; }
        set { if (value >= shapes.Length) rotationID = 0; else rotationID = value; }
    }
    Coord currentPosition;
    Tile[] tiles;

    public void Init(int id, Tile[,] aMapOccupiedInfo)
    {
        mapOccupiedInfo = aMapOccupiedInfo;
        mapSize = new Vector2Int(mapOccupiedInfo.GetLength(0), mapOccupiedInfo.GetLength(1));
        shapes = GetShapes(id);
        RotationID = 0;
        currentPosition = new Coord(5, 19);
        tiles = new Tile[4];
        
        PositionUpdate(tiles);
        InitHardDropShadow();

        int[][,] GetShapes(int hello)
        {
            switch (hello)
            {
                case 0://i
                    return new int[2][,]
                    {
                    new int[4, 4]
                    {
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},

                    }, new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,1},
                        {0,0,0,0},
                    }
                    };
                case 1://j
                    return new int[4][,]
                    {
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {1,1,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {1,0,0,0},
                        {1,1,1,0},
                        {0,0,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,1,0},
                        {0,1,0,0},
                        {0,1,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,0},
                        {0,0,1,0},
                    }
                    };
                case 2://L
                    return new int[4][,]
                    {
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {1,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,1,0},
                        {1,1,1,0},
                        {0,0,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,1,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,0},
                        {1,0,0,0},
                    }
                    };
                case 3://O
                    return new int[1][,]
                    {
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {1,1,0,0},
                        {1,1,0,0},
                        {0,0,0,0},
                    },
                    };
                case 4://s
                    return new int[2][,]
                    {
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {1,0,0,0},
                        {1,1,0,0},
                        {0,1,0,0},

                    }, new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {0,1,1,0},
                        {1,1,0,0},
                    }
                    };
                case 5://t
                    return new int[4][,]
                    {
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,0,0},
                        {1,1,0,0},
                        {0,1,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,0,0},
                        {1,1,1,0},
                        {0,0,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,0,0},
                        {0,1,1,0},
                        {0,1,0,0},
                    },
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,0},
                        {0,1,0,0},
                    }
                    };
                case 6://z
                    return new int[2][,]
                    {
                    new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,1,0,0},
                        {1,1,0,0},
                        {1,0,0,0},

                    }, new int[4, 4]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,0,0},
                        {0,1,1,0},
                    }
                    };
            };
            throw new System.Exception("Invald ID");
        }
    }

    private void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame) HarDrop();
        if (Keyboard.current.aKey.wasPressedThisFrame) Slide(new Vector2Int(-1, 0));
        if (Keyboard.current.sKey.wasPressedThisFrame) Slide(new Vector2Int(0, -1));
        if (Keyboard.current.dKey.wasPressedThisFrame) Slide(new Vector2Int(1, 0));
        if (Keyboard.current.rKey.wasPressedThisFrame) Rotation();
    }

    #region Gravity

    private Action hitGroundAction;

    public void EnableGravity(float frame, Action<Tile[]> onGroundHit)
    {
        //InvokeRepeating(nameof(ApplyGravity), frame, frame);

        void lambda()
        {
            CancelInvoke(nameof(ApplyGravity));
            onGroundHit?.Invoke(tiles);
            hitGroundAction -= lambda;
        }
        hitGroundAction += lambda;
    }

    private void ApplyGravity()
    {
        Slide(new Vector2Int(0, -1));
    }

    #endregion

    #region Actions

    public void Slide(Vector2Int dir)
    {
        int[,] shape = shapes[RotationID];

        Coord[] anticipatedCoords = GetAnticipatedCoord();

        if (CollisionDetected(anticipatedCoords))
        {
            if(dir.y < 0 && hitGroundAction != null)
            {
                //wait lockDelay
                hitGroundAction.Invoke();
            }
            return;
        }

        currentPosition += dir;
        PositionUpdate(tiles, anticipatedCoords);
        SetHardDropShadowPos();

        Coord[] GetAnticipatedCoord()
        {
            Coord[] coords = new Coord[4];

            int count = 0;

            for(int x = 0; x < shape.GetLength(0); x++)
            {
                for(int y = 0; y < shape.GetLength(1); y++)
                {
                    if (shape[x, y] == 1)
                    {
                        coords[count] = new Coord(x + currentPosition.x + dir.x - 2, y + currentPosition.y + dir.y - 1);
                        count++;
                    }                    
                }
            }
            return coords;
        }
    }
    private void HarDrop()
    {
        Coord[] coords = new Coord[hardDropShadow.Length];
        for (int i = 0; i < hardDropShadow.Length; i++) { coords[i] = hardDropShadow[i].coord; }
        PositionUpdate(tiles, coords);
        hitGroundAction?.Invoke();
    }

    public void Rotation()
    {
        Coord[] anticipatedCoords = GetAnticipatedCoords();
        if (CollisionDetected(anticipatedCoords)) return;

        RotationID++;
        PositionUpdate(tiles, anticipatedCoords);
        SetHardDropShadowPos();

        Coord[] GetAnticipatedCoords()
        {
            Coord[] coords = new Coord[4];
            int rotationID = (RotationID + 1 >= shapes.Length) ? 0 : RotationID + 1;
            int[,] newShape = shapes[rotationID];
            int count = 0;

            for(int x = 0; x <newShape.GetLength(0); x++)
            {
                for(int y = 0; y < newShape.GetLength(1); y++)
                {
                    if(newShape[x, y] == 1)
                    {
                        coords[count] = new Coord(x + currentPosition.x - 2, y + currentPosition.y - 1);
                        count++;
                    }
                }
            }
            return coords;
        }
    }



    #endregion

    #region Collision

    private bool CollisionDetected(Coord[] coords)
    {
        for(int i = 0; i < coords.Length; i++)
        {
            Coord coord = coords[i];

            if (coord.x < 0 || coord.x >= mapSize.x) return true;
            if (coord.y < 0) return true;
            if (mapSize.y > coord.y && mapOccupiedInfo[coord.x, coord.y] != null) return true;
        }
        return false;
    }

    #endregion

    private void PositionUpdate(Tile[] aTiles, Coord[] aCoords = null)
    {
        if(aCoords != null)
        {
            for (int i = 0; i < aTiles.Length; i++)
            {
                Tile tile = aTiles[i];
                Vector3 pos = Coord.CoordToPostion(aCoords[i], mapSize);

                if (tile != null) { tile.transform.position = pos; }
                else{ tile = aTiles[i] = Instantiate(tilePrefab, pos, Quaternion.identity, transform); }
                tile.coord = aCoords[i];
            }
        }
        else
        {
            int[,] shape = shapes[RotationID];

            int count = 0;

            for (int x = 0; x < shape.GetLength(0); x++)
            {
                for (int y = 0; y < shape.GetLength(1); y++)
                {
                    if (shape[x, y] == 1)
                    {
                        Coord coord = new Coord(x + currentPosition.x - 2, y + currentPosition.y - 1);
                        Vector3 pos = Coord.CoordToPostion(coord, mapSize);
                        Tile tile = aTiles[count];

                        if(tile == null){ tile = aTiles[count] = Instantiate(tilePrefab, pos, Quaternion.identity, transform); }
                        else{ tile.transform.position = pos; }
                        tile.coord = coord;

                        count++;
                    }
                }
            }
        }
    }

    #region HardDrop
    Tile[] hardDropShadow;
    private void InitHardDropShadow()
    {
        GameObject shadow = new GameObject { name = "shadow" };
        shadow.transform.SetParent(transform);
        hardDropShadow = new Tile[tiles.Length];

        SetHardDropShadowPos();

        for (int i = 0; i < hardDropShadow.Length; i++) hardDropShadow[i].SetAlpha(0.3f);
    }

    private void SetHardDropShadowPos()
    {
        int[,] shape = shapes[RotationID];

        Coord[] anticipatedCoords = GetAnticipatedCoord();

        PositionUpdate(hardDropShadow, anticipatedCoords);

        Coord[] GetAnticipatedCoord()
        {
            Coord[] prevCoords = new Coord[4];
            Coord[] coords = new Coord[4];

            int dropDis = 0;
            do
            {
                int count = 0;
                for (int x = 0; x < shape.GetLength(0); x++)
                {
                    for (int y = 0; y < shape.GetLength(1); y++)
                    {
                        if (shape[x, y] == 1)
                        {
                            prevCoords[count] = coords[count];
                            coords[count] = new Coord(x + currentPosition.x - 2, y + currentPosition.y - dropDis - 1);                            
                            count++;
                        }
                    }
                }
                dropDis++;

            } while (!CollisionDetected(coords));
            return prevCoords;
        }
    }

    #endregion
}
