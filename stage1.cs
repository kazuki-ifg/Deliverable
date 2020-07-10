using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage1 : MonoBehaviour
{
    GameObject[] Cube;  //障害物となるオブジェクト配列を宣言
    GameObject CubeB;  //上記の配列と別の動きをさせるオブジェクトを宣言
    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.FindGameObjectsWithTag("Cube");  //タグ取得
        CubeB = GameObject.Find("CubeB");
    }

    //ステージ１の回転するオブジェクト
    void Update()
    {
        foreach(GameObject obj in Cube)  //配列に入れたオブジェクトを回転させる
        {
            obj.transform.Rotate(new Vector3(0, 1, 0));
        }
        CubeB.transform.Rotate(new Vector3(0, -1, 0));  //一つだけ逆回転させる
    }
}
