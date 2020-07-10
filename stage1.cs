using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1 : MonoBehaviour
{
    GameObject[] Cube;
    GameObject CubeB;
    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.FindGameObjectsWithTag("Cube");
        CubeB = GameObject.Find("CubeB");
    }

    //ステージ１の回転するオブジェクト
    void Update()
    {
        foreach(GameObject obj in Cube)
        {
            obj.transform.Rotate(new Vector3(0, 1, 0));
        }
        CubeB.transform.Rotate(new Vector3(0, -1, 0));
    }
}
