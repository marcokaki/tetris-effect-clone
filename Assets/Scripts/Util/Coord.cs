using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Coord
{
    public int x;
    public int y;

    public Coord(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public static Vector3 CoordToPostion(Coord coord, Vector2Int mapSize)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + coord.x, -mapSize.y / 2 + 0.5f + coord.y, 0);
    }

    public static Coord Get90RotationCoord(Coord coord)
    {
        return new Coord(coord.y, -1 * coord.x);
    }

    public static Coord Get270RotationCoord(Coord coord)
    {
        return new Coord(-1 * coord.y, coord.x);
    }


    #region Math Operation

    public static bool operator ==(Coord c1, Coord c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }

    public static bool operator !=(Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }

    public static Coord operator +(Coord c1, Coord c2)
    {
        return new Coord(c1.x + c2.x, c1.y + c2.y);
    }

    public static Coord operator +(Coord c, Vector2Int v)
    {       
        return new Coord(c.x + v.x, c.y + v.y);
    }

    public static Coord operator -(Coord c1, Coord c2)
    {
        return new Coord(c1.x - c2.x, c1.y - c2.y);
    }

    public static Coord operator -(Coord c, Vector2Int v)
    {
        return new Coord(c.x - v.x, c.y - v.y);
    }

    public override bool Equals(object obj)
    {
        if (obj is Coord coord)
        {
            return this == coord;
        }

        return false;
    }

    public override int GetHashCode() => new { x, y }.GetHashCode();


    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }
    #endregion



}
