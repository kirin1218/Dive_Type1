using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// meta情報を含んだJson
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
class JsonData<T>
{
    public Meta meta;
    public T data;

    public JsonData(string metaName, T data)
    {
        this.meta = new Meta(metaName);
        this.data = data;
    }
}

/// <summary>
/// Meta情報の部分
/// </summary>
[Serializable]
class Meta
{
    public string typeName;
    public Meta(string typeName) { this.typeName = typeName; }
}

/// <summary>
/// 実際に変換したいデータ
/// </summary>
[Serializable]
class PlayerParams
{
    public string name;
    public int hp;
    public int attackPower;

    public PlayerParams(string name, int hp, int attack)
    {
        this.name = name;
        this.hp = hp;
        this.attackPower = attack;
    }
}

[SerializeField]
public class SaveData
{

    private static string filePath = Application.dataPath + "/savedata.json";//セーブデータのファイルパス
    public static string FilePath
    {//ファイルパスのプロパティ
        get { return filePath; }
    }

    public long ld;
    public string name;
    public List<int> list;
    public int[] array;
}

/// <summary>
/// 頂点情報の管理
/// </summary>
[Serializable]
public class Vertex3
{
    [SerializeField]
    private float[] k;

    public float x { get { return k[0]; } }
    public float y { get { return k[1]; } }
    public float z { get { return k[2]; } }

    //  ポリゴンの構成情報
    public Vertex3(float x, float y, float z)
    {
        k = new float[3];
        k[0] = x;
        k[1] = y;
        k[2] = z;
    }
}

[Serializable]
public class Polygon3
{
    [SerializeField]
    private int[] i;

    public int i1 { get { return i[0]; } }
    public int i2 { get { return i[1]; } }
    public int i3 { get { return i[2]; } }

    //  ポリゴンの構成情報
    public Polygon3(int i1, int i2, int i3)
    {
        i = new int[3];
        i[0] = i1;
        i[1] = i2;
        i[2] = i3;
    }
}

/// <summary>
/// 建物情報の保持
/// </summary>
[Serializable]
public class BuildInfo
{
    //  建物の頂点情報
    [SerializeField]
    private List<Vertex3> v;
    //  ポリゴンを構成する頂点のリスト
    [SerializeField]
    private List<Polygon3> p;
    [SerializeField]
    public float startPosX;
    [SerializeField]
    public float startPosY;
    [SerializeField]
    public float endPosX;
    [SerializeField]
    public float endPosY;

    public List<Vertex3> getVertex3 { get { return v; } }
    public void AddVertex3(Vertex3 item)
    {
        v.Add(item);
    }
    public int CountVertex3()
    {
        return v.Count();
    }
    public List<Polygon3> getPolygon3 { get { return p; } }
    public void AddPolygon3(Polygon3 item)
    {
        p.Add(item);
    }
    public int CountPolygon3()
    {
        return p.Count();
    }
    public BuildInfo()
    {
        this.v = new List<Vertex3>();
        this.p = new List<Polygon3>();

    }
}

/// <summary>
/// 地図情報のJsonファイル情報を保持
/// </summary>
[SerializeField]
public class MapInfoJson
{

    private static string filePath = Application.dataPath + "/mapinfo.json";//セーブデータのファイルパス
    public static string FilePath
    {//ファイルパスのプロパティ
        get { return filePath; }
    }

    [SerializeField]
    private List<BuildInfo> map;

    public void Add(BuildInfo item)
    {
        map.Add(item);
    }

    public List<BuildInfo> List { get { return map; } }

    public int Count() { return map.Count(); }

    public MapInfoJson()
    {
        this.map = new List<BuildInfo>();
    }
}

public class JsonController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// ファイル読み込みする
    /// </summary>
    /// <param name="filePath">ファイルのある場所</param>
    /// <returns></returns>
    public MapInfoJson LoadFromJson(string filePath)
    {
        if (!File.Exists(filePath))
        {//ファイルがない場合FALSE.
            Debug.Log("FileEmpty!");
            return new MapInfoJson();//ファイルが無いときは適当に処す.
        }

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                MapInfoJson sd = JsonUtility.FromJson<MapInfoJson>(sr.ReadToEnd());
                if (sd == null) return new MapInfoJson();
                return sd;
            }
        }
    }
}
