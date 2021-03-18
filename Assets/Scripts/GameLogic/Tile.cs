using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Coord worldcoord;
    public Coord anitcipateCoord;

    public Coord coord;

    public override string ToString()
    {
        return GetType().ToString() + worldcoord;
    }

    public void SetAlpha(float alpha)//temperary Code
    {
        Renderer renderer = GetComponent<Renderer>();

        Color color = renderer.material.color;
        color.a = alpha;

        renderer.material.color = color;
    }

}
