using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    public Text press;  //PRESS SPACE を表示するテキスト
    public Text tx;  //メニューの左側
    public Text tx2;  //メニューの右側
    public Text esc;  //escのテキスト

    GameObject Cube;  //カーソル代わりのキューブの宣言
    bool flag = true;  //二回目のスペースを押したかのフラグ
    bool flag2 = true;  //一回目のスペースを押したかのフラグ
  
    public AudioClip select;  //キューブを動かした時のSE
    public AudioClip yes;  //スペースを押した時のSE
    AudioSource aud;  //オーディオソース

    GameObject panel;  //フェードアウトのためのパネルの宣言
    float alfa;  //パネルの透明度を決める変数
    float speed = 0f;  //パネルの透明度を変化させる変数
    float red, green, blue;  //パネルの色の変数

    //タイトル画面全般
    void Start()
    {
        Cube = GameObject.Find("Cube");  //キューブの取得
        this.aud = GetComponent<AudioSource>();
        panel = GameObject.Find("Panel");

        
        red = panel.GetComponent<Image>().color.r;
        green = panel.GetComponent<Image>().color.g;
        blue = panel.GetComponent<Image>().color.b;
    }

   
    
    void Update()
    {
        panel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;                                   //speedに値があるとき、フェードアウトする
        
        //スペース押すとメニューが表示される
        if (flag2 == true &&Input.GetKeyUp(KeyCode.Space) )
        {
            press.text = null;            
            flag2 = false;
            tx.text = "Tutorial" + "\nStage2";
            tx2.text = "Stage1" + "\nStage3";
            esc.text = "ESC  終了";
            Cube.transform.position = new Vector3(-10, 0, 20);
            this.aud.PlayOneShot(this.yes);
        }

        //カーソル代わりのキューブの挙動
        Cube.transform.Rotate(new Vector3(1, 1, 1));
        Vector3 Cube_v = Cube.transform.position;

        if (flag2 == false && flag == true)  
        {
            
            if (Cube_v.y ==0  &&Input.GetKeyDown(KeyCode.DownArrow))//キューブが上側にある時、下矢印を押すと上に動く
            {
                Cube_v.y = -3f;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }

            if (Cube_v.y == -3f &&Input.GetKeyDown(KeyCode.UpArrow))//キューブが下側にある時、上矢印を押すと上に動く
            {
                Cube_v.y = 0;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }


            if (Cube_v.x == -10 &&Input.GetKeyDown(KeyCode.RightArrow))//キューブが左側にある時、右矢印を押すと右に動く
            {
                Cube_v.x = 1;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }


            if (Cube_v.x == 1&&Input.GetKeyDown(KeyCode.LeftArrow))//キューブが右側にある時、左矢印を押すと右に動く
            {
                Cube_v.x = -10;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }
        }


        //メニューが出てる状態でスペース押したらフェードアウト
        if (flag2 == false && flag == true && Input.GetKeyDown(KeyCode.Space))
        {
            this.aud.PlayOneShot(this.yes);
            speed = 0.01f;　　　　//フェードアウトのための変数に代入
            flag = false;
        }


        //押した場所(キューブの位置)に応じてシーン移動
        if (alfa > 1.5f)
        {
            if (Cube_v.x == -10f && Cube_v.y == 0)  //キューブが左上にある時、チュートリアルに移動
            {
                SceneManager.LoadScene("tutorial");
            }

            if (Cube_v.x == 1f && Cube_v.y == 0)  //キューブが右上にある時、ステージ１に移動
            {
                SceneManager.LoadScene("stage1");
            }

            if (Cube_v.x == -10f && Cube_v.y == -3f)  //キューブが左下にある時、ステージ２に移動
            {
                SceneManager.LoadScene("stage2");
            }

            if (Cube_v.x == 1 && Cube_v.y == -3f)  //キューブが右上にある時、ステージ３に移動
            {
                SceneManager.LoadScene("stage3");
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape)) Quit();  //ESCでQuitを起動

     }


    //終了
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

   



}
