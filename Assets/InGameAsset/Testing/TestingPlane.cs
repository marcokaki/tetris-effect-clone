using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestingPlane : MonoBehaviour
{
    public float[] array;

    Material mat;

    private void OnEnable()
    {
        array = new float[16];
        array[3] = 1;
/*        array[0] = 1;*/
        mat = GetComponent<Renderer>().material;
        

    }

    private void Update()
    {

        mat.SetFloatArray("_ArrayY", array);
    }
}
