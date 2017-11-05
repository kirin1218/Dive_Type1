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
            buildObj.transform.localPosition = new Vector3(minX - centerX + (sizeX / 2), (maxY + minY) / 2, minZ - centerY + (sizeZ / 2));

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
    }

    // Use this for initialization
    void Start () {
        //  大きさを100x100にする
        this.transform.localScale = new Vector3( AreaSize, 0.1f, AreaSize );
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
