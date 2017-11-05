using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject stageprefab;
    int nCurSize;
    public int AreaSize;

    private void AddStage( int i, int j)
    {
        float nOffsetX;
        float nOffsetY;

        //  Stageの作成
        GameObject stageObj = Instantiate(stageprefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        StageScript ss = stageObj.GetComponent<StageScript>();
        ss.AddBuildings(i, j);

    }

    private void Awake()
    {

        nCurSize = 0;
        //  とりあえずお試しでサイズ=5
        for (nCurSize = 0; nCurSize < 1; nCurSize++)
        {
            for (int i = -nCurSize; i <= nCurSize; i++)
            {
                if (i == -nCurSize || i == nCurSize)
                {
                    for (int j = -nCurSize; j <= nCurSize; j++)
                    {
                        AddStage(i, j);
                    }
                }
                else
                {
                    AddStage(i, -nCurSize);
                    AddStage(i, nCurSize);
                }

            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = -nCurSize; i <= nCurSize; i++)
            {
                if (i == -nCurSize || i == nCurSize)
                {
                    for (int j = -nCurSize; j <= nCurSize; j++)
                    {
                        AddStage(i, j);
                    }
                }
                else
                {
                    AddStage(i, -nCurSize);
                    AddStage(i, nCurSize);
                }

            }
            nCurSize++;
        }
    }

    // Use this for initialization
    void Start () {
        ;
    }

}
