using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;


public class StageScript : MonoBehaviour {
    public　GameObject buildprefab;
    public GameObject buildprefab2;
    public GameObject TestCube;

    public float AreaSize;
    private MapInfoJson mapinfo;

    public void AddBuildings( int x, int y )
    {
#if true
        string filePath;
        float centerX = AreaSize * x;
        float centerY = AreaSize * y;

        filePath = Application.dataPath + "/json/mapinfo_" + x.ToString() + "x" + y.ToString() + ".json";

        if (!File.Exists(filePath))
        {
            return;
        }

        JsonController mapinfoJson = GetComponent<JsonController>();
        mapinfo = mapinfoJson.LoadFromJson(filePath);

        for (int i = 0; i < mapinfo.Count(); i++)
        {
#if false
            GameObject buildObj = (GameObject)Instantiate(
                                                buildprefab2,
                                                new Vector3(0, 0, 0),
                                                Quaternion.identity
                                                );
            GameObject emptyObject = new GameObject();
            emptyObject.transform.rotation = transform.rotation;
            emptyObject.transform.parent = transform;
            buildObj.transform.parent = emptyObject.transform;

            BuildInfo build = mapinfo.List[i];

            float minX = 10000, maxX = 0;
            float minY = 10000, maxY = 0;
            float minZ = 10000, maxZ = 0;


            for (int j = 0; j < build.CountVertex3(); j++)
            {
                if (minX > build.getVertex3[j].x)
                {
                    minX = build.getVertex3[j].x;
                }
                if (maxX < build.getVertex3[j].x)
                {
                    maxX = build.getVertex3[j].x;
                }
                if (minY > build.getVertex3[j].y)
                {
                    minY = build.getVertex3[j].y;
                }
                if (maxY < build.getVertex3[j].y)
                {
                    maxY = build.getVertex3[j].y;
                }
                if (minZ > build.getVertex3[j].z)
                {
                    minZ = build.getVertex3[j].z;
                }
                if (maxZ < build.getVertex3[j].z)
                {
                    maxZ = build.getVertex3[j].z;
                }
            }

            float sizeX = maxX - minX;
            float sizeY = maxY - minY;
            float sizeZ = maxZ - minZ;
            buildObj.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
            buildObj.transform.localPosition = new Vector3(minX - centerX + (sizeX / 2), (maxY + minY) / 2, minZ - centerY + (sizeZ / 2));
#else
            GameObject buildObj = (GameObject)Instantiate(
                                                 buildprefab,
                                                 new Vector3(0, 0, 0),
                                                 Quaternion.identity
                                                 );
            GameObject emptyObject = new GameObject();
            emptyObject.transform.rotation = transform.rotation;
            emptyObject.transform.parent = transform;
            emptyObject.transform.localScale = new Vector3(0.001f, 10, 0.001f);
            buildObj.transform.parent = emptyObject.transform;

            BuildInfo build = mapinfo.List[i];

            float minX = 10000, maxX = -10000;
            float minY = 10000, maxY = -10000;
            float minZ = 10000, maxZ = -10000;


            for (int j = 0; j < build.CountVertex3(); j++)
            {
                if (minX > build.getVertex3[j].x)
                {
                    minX = build.getVertex3[j].x;
                }
                if (maxX < build.getVertex3[j].x)
                {
                    maxX = build.getVertex3[j].x;
                }
                if (minY > build.getVertex3[j].y)
                {
                    minY = build.getVertex3[j].y;
                }
                if (maxY < build.getVertex3[j].y)
                {
                    maxY = build.getVertex3[j].y;
                }
                if (minZ > build.getVertex3[j].z)
                {
                    minZ = build.getVertex3[j].z;
                }
                if (maxZ < build.getVertex3[j].z)
                {
                    maxZ = build.getVertex3[j].z;
                }
            }

            float sizeX = maxX - minX;
            float sizeY = maxY - minY;
            float sizeZ = maxZ - minZ;
            buildObj.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
#if false
            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            for (int j = 0; j < build.CountVertex3(); j++)
            {
                vertices.Add(new Vector3((build.getVertex3[j].x - centerX)/1000, (build.getVertex3[j].y)/1000, (build.getVertex3[j].z - centerY)/1000));
            }

            for (int k = 0; k < build.CountPolygon3(); k++)
            {
                Polygon3 poly = build.getPolygon3[k];
                triangles.Add(poly.i1);
                triangles.Add(poly.i2);
                triangles.Add(poly.i3);
            }

            Mesh _mesh = new Mesh();

            // (4) Meshに頂点情報を代入
            _mesh.vertices = vertices.ToArray();
            _mesh.triangles = triangles.ToArray();
            //        _mesh.normals = normals.ToArray();

            _mesh.RecalculateBounds();

            var filter = buildObj.GetComponent<MeshFilter>();
            filter.sharedMesh = _mesh;
#endif
            buildObj.transform.localPosition = new Vector3(minX - centerX + (sizeX / 2), (maxY + minY) / 2, minZ - centerY + (sizeZ / 2));
#endif
            var vertices = new List<Vector3>();
            var triangles = new List<int>();

            for (int j = 0; j < build.CountVertex3(); j++)
            {
                vertices.Add(new Vector3( build.getVertex3[j].x, build.getVertex3[j].y, build.getVertex3[j].z));
            }

            for (int k = 0; k < build.CountPolygon3(); k++)
            {
                Polygon3 poly = build.getPolygon3[k];
                triangles.Add(poly.i1);
                triangles.Add(poly.i2);
                triangles.Add(poly.i3);
            }

            BuildScript bs = buildObj.GetComponent<BuildScript>();
            bs.SetPosition((int)AreaSize, new Vector3(minX - centerX + (sizeX / 2), maxY, minZ - centerY + (sizeZ / 2)), new Vector3(minX + (sizeX / 2), maxY, minZ + (sizeZ / 2)) );
            bs.MakeObject(vertices, triangles);

        }

        transform.position = new Vector3(x * AreaSize, 0, y * AreaSize);
#endif

#if false
        string filePath;
        float centerX = AreaSize * x;
        float centerY = AreaSize * y;

        filePath = Application.dataPath + "/json/mapinfo_" + x.ToString() + "x" + y.ToString() + ".json";

        if (!File.Exists(filePath))
        {
            return;
        }

        JsonController mapinfoJson = GetComponent<JsonController>();
        mapinfo = mapinfoJson.LoadFromJson(filePath);

        for (int i = 0; i < mapinfo.Count(); i++)
        {
            GameObject buildObj = (GameObject)Instantiate(
                                                buildprefab,
                                                new Vector3(0, 0, 0),
                                                Quaternion.identity
                                                );

            GameObject emptyObject = new GameObject();
            emptyObject.transform.rotation = transform.rotation;
            emptyObject.transform.parent = transform;
            buildObj.transform.parent = emptyObject.transform;


            BuildInfo build = mapinfo.List[i];

            float minX = 10000, maxX = 0;
            float minY = 10000, maxY = 0;
            float minZ = 10000, maxZ = 0;
            //float x;
            //float y;
            //float z;
            //float centerX;
            //float centerY;
            //float centerZ;

            for (int j = 0; j < build.CountVertex3(); j++)
            {
                if (minX > build.getVertex3[j].x)
                {
                    minX = build.getVertex3[j].x;
                }
                if (maxX < build.getVertex3[j].x)
                {
                    maxX = build.getVertex3[j].x;
                }
                if (minY > build.getVertex3[j].y)
                {
                    minY = build.getVertex3[j].y;
                }
                if (maxY < build.getVertex3[j].y)
                {
                    maxY = build.getVertex3[j].y;
                }
                if (minZ > build.getVertex3[j].z)
                {
                    minZ = build.getVertex3[j].z;
                }
                if (maxZ < build.getVertex3[j].z)
                {
                    maxZ = build.getVertex3[j].z;
                }
            }

            buildObj.transform.position = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2);

            continue;

           // break;

            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            //var normals = new List<Vector3>();


            for (int j = 0; j < build.CountVertex3(); j++)
            {
                vertices.Add(new Vector3(build.getVertex3[j].x - centerX, build.getVertex3[j].y, build.getVertex3[j].z - centerY ));
            }

            for (int k = 0; k < build.CountPolygon3(); k++)
            {
                Polygon3 poly = build.getPolygon3[k];
                triangles.Add(poly.i1);
                triangles.Add(poly.i2);
                triangles.Add(poly.i3);
            }


            buildObj.transform.localScale = new Vector3( ( maxX - minX )/ AreaSize, (maxY - minY)/ AreaSize, (maxZ - minZ)/ AreaSize);
            buildObj.transform.position = new Vector3( (minX + ( maxX - minX ) /2), (minY + (maxY - minY)/2), (minZ + (maxZ - minZ)/2));

            BuildScript bs = buildObj.GetComponent<BuildScript2>();
            bs.MakeObject(vertices, triangles, maxX - minX, maxY - minY, maxZ - minZ );




            //break;
        }

        transform.position = new Vector3( x * AreaSize, 0, y * AreaSize );
#endif
        }

    // Use this for initialization
    void Start () {
        //  大きさを100x100にする
        this.transform.localScale = new Vector3( AreaSize, 0.1f, AreaSize );

        //  建物を建てていくよ
        //AddBuildings(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
