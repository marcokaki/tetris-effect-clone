using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestQuadGeneration : MonoBehaviour
{
    public int xSize, ySize;

    private void Awake() {
        //Generate();
    }

    private Vector3[] vertices;

    private Mesh mesh;

/*    private IEnumerator Generate() {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		…
		mesh.vertices = vertices;
    }
*/
    private void OnDrawGizmos() {
        if (vertices == null) {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }


}
