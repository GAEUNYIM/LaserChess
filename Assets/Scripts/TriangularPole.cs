using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TriangularPole : MonoBehaviour { 

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeMeshData();
        CreateMesh();
        
    }

    void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), 
                new Vector3(0,1,0), new Vector3(0,1,1), new Vector3(1,1,0)};
        triangles = new int[] 
        {
            0, 2, 1,
            0, 1, 3,
            1, 4, 3,
            3, 5, 2,
            3, 2, 0,
            4, 1, 2,
            5, 4, 2,
            3, 4, 5
        };
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
