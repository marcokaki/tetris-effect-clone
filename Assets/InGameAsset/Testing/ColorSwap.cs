using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorSwap : MonoBehaviour
{
	public Color Color0;
	public Color Color1;
	public Color Color2;
	public Color Color3;

	Material _mat;

	void OnEnable()
	{
		_mat = GetComponent<Renderer>().material;

	}

	void OnDisable()
	{
		if (_mat != null)
			DestroyImmediate(_mat);
	}

    private void Update()
    {
		_mat.SetMatrix("_ColorMatrix", ColorMatrix);
	}


	Matrix4x4 ColorMatrix
	{
		get
		{
			Matrix4x4 mat = new Matrix4x4();
			mat.SetRow(0, ColorToVec(Color0));
			mat.SetRow(1, ColorToVec(Color1));
			mat.SetRow(2, ColorToVec(Color2));
			mat.SetRow(3, ColorToVec(Color3));

			return mat;
		}
	}

	Vector4 ColorToVec(Color color)
	{
		return new Vector4(color.r, color.g, color.b, color.a);
	}



}
