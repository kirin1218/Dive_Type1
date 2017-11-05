using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuildScript2 : MonoBehaviour
{
    public void MakeObject(List<Vector3> vertices, List<int> triangles, float sizeX, float sizeY, float sizeZ)
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
#if false
        Mesh _mesh = new Mesh();
        //listOuterFrame.Add( new Vector3( ))
        // (4) Meshに頂点情報を代入
        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();
        //        _mesh.normals = normals.ToArray();

        _mesh.RecalculateBounds();

        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = _mesh;

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
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var filter = GetComponent<MeshFilter>();
        var renderer = GetComponent<MeshRenderer>();

        if (filter.mesh != null)
        {
            Graphics.DrawMesh(filter.mesh, this.transform.position, Quaternion.identity, renderer.material, 0);
        }
        else
        {
            int a;
            a = 3;
        }
    }

}
