using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class kantokukun : MonoBehaviour
{
    GameObject panel;  //オブジェクトの宣言
    float alfa;  //透明度に設定する変数
    float speed = 0f;  //透明度を変化させる変数
    float red, green, blue;  //色に関わる変数


  
    void Start()
    {
        panel = GameObject.Find("Panel");  //オブジェクトの取得


        red = panel.GetComponent<Image>().color.r;  //パネルの色を取得
        green = panel.GetComponent<Image>().color.g;
        blue = panel.GetComponent<Image>().color.b;
    }

    
    void Update()
    {
        //speedの速度で画面をフェードアウトさせる
        panel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;
    }

    //画面をフェードアウトさせるためにspeedの値を変更
    public void setspeed(float speed)
    {
        this.speed = speed;
    }
}
