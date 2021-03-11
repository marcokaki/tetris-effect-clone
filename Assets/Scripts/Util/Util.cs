using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Util
{
    public static bool Any<T>(this T[,] ts, Func<T, bool> anyPred)
    {
        if (anyPred == null) throw new ArgumentException("predicate is Null");
        if (ts == null) throw new ArgumentException("tileMap is Null");

        for(int x = 0; x < ts.GetLength(0); x++)
        {
            for(int y = 0; y < ts.GetLength(1); y++)
            {
                T item = ts[x, y];

                if (item == null) continue;

                if (anyPred(item))
                {
                    return true;
                }
            }
        }
        return false;
    }





}
