using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class MyUtil
{
    public static int EnumCount<T>()
    {
        return System.Enum.GetValues(typeof(T)).Length;
    }
}

public class Piece : MonoBehaviour
{
    public static Vector3 drawSize => Vector3.one * 1;

    public class Shape {
        public enum Type { I, J, L, O, T, S, Z}
        public enum Dir { N, E, S, W}

        public int[,] cells;

        public Shape(int[,] cells_) { cells = cells_; }

        public Matrix4x4 mat;

        public static int typeCount => MyUtil.EnumCount<Type>();
        public int dirCount;
    }
    public class ShapeTable {
        Shape[][] _shapes;

        public ShapeTable()
        {
            var typeCount = Shape.typeCount;

            _shapes = new Shape[typeCount][];

            _shapes[(int)Shape.Type.I] = new Shape[2]
            {
                new Shape(new int[4, 4] {
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                }),
                new Shape(new int[4,4] {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,1},
                        {0,0,0,0},
                })
            };

            _shapes[(int)Shape.Type.J] = new Shape[4] {
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {1,1,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {1,0,0,0},
                        {1,1,1,0},
                        {0,0,0,0},
                }),                
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,1,0},
                        {0,1,0,0},
                        {0,1,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,0},
                        {0,0,1,0},
                }),
            };

            _shapes[(int)Shape.Type.L] = new Shape[4] {
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {1,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,0,1,0},
                        {1,1,1,0},
                        {0,0,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,1,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,0},
                        {1,0,0,0},
                }),
            };

            _shapes[(int)Shape.Type.O] = new Shape[1] {
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {1,1,0,0},
                        {1,1,0,0},
                        {0,0,0,0},
                }),
            };

            _shapes[(int)Shape.Type.T] = new Shape[4] {
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,0,0},
                        {1,1,0,0},
                        {0,1,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,0,0},
                        {1,1,1,0},
                        {0,0,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,0,0},
                        {0,1,1,0},
                        {0,1,0,0},
                }),
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,0},
                        {0,1,0,0},
                }),
            };

            _shapes[(int)Shape.Type.S] = new Shape[2]
{
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {1,0,0,0},
                        {1,1,0,0},
                        {0,1,0,0},
                }),
                new Shape(new int[4,4] {
                        {0,0,0,0},
                        {0,0,0,0},
                        {0,1,1,0},
                        {1,1,0,0},
                })
};

            _shapes[(int)Shape.Type.Z] = new Shape[2]
{
                new Shape(new int[4, 4] {
                        {0,0,0,0},
                        {0,1,0,0},
                        {1,1,0,0},
                        {1,0,0,0},
                }),
                new Shape(new int[4,4] {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,0,0},
                        {0,1,1,0},
                })
};

            for(int t = 0; t < typeCount; t++)
            {
                Shape[] s = _shapes[t];
                for(int d = 0; d < s.Length; d++)
                {
                    s[d].dirCount = s.Length;
                }
            }
        }       

        public Shape GetShape(Shape.Type type, Shape.Dir dir)
        {
            var d = (int)dir % _shapes[(int)type].Length;//is this good?
            return _shapes[(int)type][d];
        }
    }

    public static ShapeTable shapeTable = new ShapeTable();

    public Vector2Int pos;
    public Shape.Type type;
    public Shape.Dir dir;

    public Shape shape => shapeTable.GetShape(type, dir);

    Material mat;

    private void Awake() => mat = GetComponent<Renderer>().material;
    private void Update() => OnPieceDraw();

    public void RandomShape(int rnd)
    {
        type = (Shape.Type)rnd;
    }

    public Shape.Dir NextDir(int rotate)
    {
        if (rotate == 0) return dir;

        var newDir = ((int)dir + 1) % shape.dirCount;

        return (Shape.Dir)newDir;
    }   

    private void OnPieceDraw()
    {
        float[] vs = new float[16];

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (shape.cells[x, y] == 0) continue;
                vs[x + 4 * y] = 1;
            }
        }
        //transform.position = new Vector3(pos.x, pos.y);
        mat.SetFloatArray("_PieceArray", vs);
    }
}
