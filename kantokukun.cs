using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class kantokukun : MonoBehaviour
{
    GameObject panel;
    float alfa;
    float speed = 0f;
    float red, green, blue;


  
    void Start()
    {
        panel = GameObject.Find("Panel");


        red = panel.GetComponent<Image>().color.r;
        green = panel.GetComponent<Image>().color.g;
        blue = panel.GetComponent<Image>().color.b;
    }

    //UIを変化させ画面をフェードアウトさせる
    void Update()
    {
        panel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;
    }

    public void setspeed(float speed)
    {
        this.speed = speed;
    }
}
