using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapMeshModifier
{
    public TileMapMeshModifier(Mesh mesh, Vector2Int size) {
        _mesh = mesh;
        _size = size;
        Init();
    }


    #region Not Working
    public void OnDrawTileMapTriangles(int[][] map) {
        for(int i = 0, y = 0; y < _size.y; y++) {
            for(int x = 0; x < _size.x; x++, i++) {

                int index = (x + _size.x * y) * 6;

                for(int n = 0; n < 6; n++) {

                    if(map[y][x] == 0) {
                        _indices[index + n] = 0;
                    }
                    else {
                        _indices[index + n] = index + n;
                    }                    
                }
            }
        }
        _mesh.triangles = _indices;
    }

    public void OnDrawTileTriangles(int x, int y, bool draw) {
        int n = (x + _size.x * y) * 6;

        for(int i = 0; i < 6; i++) {

            if (draw) {
                _indices[n + 1] = n + 1;
            }
            else {
                _indices[n + i] = 0;
            }
            
        }
        _mesh.triangles = _indices;
    }

    #endregion

    void Init() {

        Vector3[] vertices = new Vector3[_size.x * _size.y * 6];
        Vector2[] uv       = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent    = new Vector4(1f, 0f, 0f, -1f);

        Debug.Log(_size.x + " , " + _size.y);

        var d = 0.5f;

        var w = (float)_size.x;
        var h = (float)_size.y;

        for (int i = 0, j = 0, y = 0; y < _size.y; y++) {
            for (int x = 0; x < _size.x; i++, j += 6, x++) {

                var pos = new Vector3(-w / 2 + x + d, -h / 2 + y + d);

                vertices[j + 0] = new Vector3(-d, -d) + pos;
                vertices[j + 1] = new Vector3( d,  d) + pos;
                vertices[j + 2] = new Vector3( d, -d) + pos;
                vertices[j + 3] = new Vector3(-d, -d) + pos;
                vertices[j + 4] = new Vector3(-d,  d) + pos;
                vertices[j + 5] = new Vector3( d,  d) + pos;

                uv[j + 0] = new Vector2((-d + x + d) / w, (-d + y + d) / h);
                uv[j + 1] = new Vector2(( d + x + d) / w, ( d + y + d) / h);
                uv[j + 2] = new Vector2(( d + x + d) / w, (-d + y + d) / h);
                uv[j + 3] = new Vector2((-d + x + d) / w, (-d + y + d) / h);
                uv[j + 4] = new Vector2((-d + x + d) / w, ( d + y + d) / h);
                uv[j + 5] = new Vector2(( d + x + d) / w, ( d + y + d) / h);
            }
        }

        _indices = new int[_size.x * _size.y * 12];

        for (int i = 0; i < vertices.Length; i++) {
            _indices[i] = i;
            tangents[i] = tangent;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.tangents = tangents;
        _mesh.triangles = _indices;

        _mesh.MarkDynamic();
        _mesh.RecalculateNormals();
        //_mesh.Optimize();
    }


    int[] _indices;
    Mesh _mesh;
    Vector2Int _size;
}
