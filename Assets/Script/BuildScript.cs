using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour {
    public Mesh _mesh;
    private int AreaSize;
    private Vector3 posOnStage;
    private Vector3 posOnWorld;

    public void SetPosition( int _AreaSize, Vector3 _posOnStage, Vector3 _posOnWorld )
    {
        AreaSize = _AreaSize;
        posOnStage = _posOnStage;
        posOnWorld = _posOnWorld;
    }

    public void MakeObject( List<Vector3> vertices, List<int> triangles )
    {
        _mesh = new Mesh();

        List<Vector3> vertices_local = new List<Vector3>() ;

        for( int i = 0; i < vertices.Count; i++)
        {
            vertices_local.Add(new Vector3(vertices[i].x - posOnWorld.x, vertices[i].y - (posOnWorld.y/2), vertices[i].z - posOnWorld.z ));
        }
        // (4) Meshに頂点情報を代入
        _mesh.vertices = vertices_local.ToArray();
        _mesh.triangles = triangles.ToArray();
        //        _mesh.normals = normals.ToArray();

        _mesh.RecalculateBounds();

    }

    // Use this for initialization
    void Start () {
        Vector3 pos = this.transform.localPosition;
        Vector3 scale = this.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        var filter = GetComponent<MeshFilter>();
        var renderer = GetComponent<MeshRenderer>();

        if (_mesh != null)
        {
            Graphics.DrawMesh(_mesh, this.transform.position, Quaternion.identity, renderer.material, 0);
        }
    }

}
