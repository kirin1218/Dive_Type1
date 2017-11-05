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
    //public void MakeObject( List<Vector3> vertices, List<int> triangles, float sizeX, float sizeY, float sizeZ )
    public void MakeObject( List<Vector3> vertices, List<int> triangles )
    {

#if false
        this.transform.localPosition = new Vector3(10, 10, 10);
        this.transform.localScale = new Vector3(10, 10, 10);
        Mesh _mesh = new Mesh();

        List<Vector3> listOuterFrame = new List<Vector3>();
        List<int> listOutertriangle = new List<int>();

        listOuterFrame.Add(new Vector3(5, 0, 5));
        listOuterFrame.Add(new Vector3(5, 15, 5));
        listOuterFrame.Add(new Vector3(5, 0, -5));

        listOutertriangle.Add(0);
        listOutertriangle.Add(1);
        listOutertriangle.Add(2);
        _mesh.vertices = listOuterFrame.ToArray();
        _mesh.triangles = listOutertriangle.ToArray();
        _mesh.RecalculateBounds();

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = _mesh;

        return;
#endif
#if true
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

        //var collider = GetComponent<MeshCollider>();
        //collider.sharedMesh = _mesh;

//this.transform.localScale = new Vector3( sizeX, sizeY, sizeZ);
//this.transform.position = new Vector3( sizeX/2, sizeY/2, sizeZ/2 );

//var collider = GetComponent<BoxCollider>();
//collider.transform. 


// 変更箇所 : MeshRendererからMaterialにアクセスし、Materialをセットするようにする
//var renderer = buildObj.GetComponent<MeshRenderer>();
//renderer.material = mateBuild[UnityEngine.Random.Range(0, 3)];
#endif
#if false
        Vector3 pos = this.transform.localPosition;
        Vector3 scale = this.transform.localScale;

        _mesh = new Mesh();

        List<Vector3> listOuterFrame = new List<Vector3>();
        List<int> listOutertriangle = new List<int>();

        listOuterFrame.Add(new Vector3(5, 0, 5));
        listOuterFrame.Add(new Vector3(5, 15, 5));
        listOuterFrame.Add(new Vector3(5, 0, -5));

        listOutertriangle.Add(0);
        listOutertriangle.Add(1);
        listOutertriangle.Add(2);
        _mesh.vertices = listOuterFrame.ToArray();
        _mesh.triangles = listOutertriangle.ToArray();
        _mesh.RecalculateBounds();

        //var filter = GetComponent<MeshFilter>();
        //filter.sharedMesh = _mesh;
#endif
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
            if( filter.sharedMesh != null)
            {
                //filter.sharedMesh = _mesh;
            }
            Graphics.DrawMesh(_mesh, this.transform.position, Quaternion.identity, renderer.material, 0);
        }
        else
        {
            int a;
            a = 3;
        }
    }

}
