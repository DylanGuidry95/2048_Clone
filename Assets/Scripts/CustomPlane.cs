using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class CustomPlane : MonoBehaviour
{
    public int Width, Height;    

    [ContextMenu("GenMesh")]
    void GenerateMesh()
    {
        Mesh meshRef = new Mesh();
        var points = new List<Vector3>();
        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                points.Add(new Vector3(i * transform.localScale.x, 0, j * transform.localScale.z) + transform.position);
            }
        }

        meshRef.vertices = points.ToArray();

        var trianglePoints = new List<int>();
        for (var i = 0; i < points.Count; i++)
        {
            if (i % Width != Width - 1 && i < points.Count - Width)
            {
                //Triangle 1
                trianglePoints.Add(i); //Current
                trianglePoints.Add(i + 1); //Current + Right One
                trianglePoints.Add(i + Width); //Current + Up One

                //Triangle 2
                trianglePoints.Add(i + 1); //Curent + Right One
                trianglePoints.Add(i + Width + 1); //Current + Up One                
                trianglePoints.Add(i + Width); //Current + Up One
            }
        }

        meshRef.triangles = trianglePoints.ToArray();

        var normals = new List<Vector3>();
        for(int i = 0; i < points.Count; i++)
        {
            normals.Add(transform.up);
        }

        meshRef.normals = normals.ToArray();

        var uvs = new List<Vector2>();

        foreach (var verts in points)
            uvs.Add(new Vector2(verts.x / (Width - 1),
                verts.y / (Height - 1)));

        meshRef.uv = uvs.ToArray();

        GetComponent<MeshFilter>().mesh = meshRef;        
    }
}
