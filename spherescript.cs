using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class spherescript : MonoBehaviour
{
    bool flag = false;   //移動のフラグ、trueだと矢印キーで移動が可能
    bool flag2 = false; //速度制限のフラグ、矢印キーでの移動で速度を制限
    bool tyakutiflag = false;  //着地フラグ、ワープ時の判定に使用
    bool cameraflag = true;  //カメラフラグ、trueだと追従する
    bool timeflag = true;  //タイムフラグ、タイムを観測しゴールしたら停止

    public AudioClip tenniSE;  //ワープパネルのSE
    public AudioClip jumpSE;  //ジャンプパネルのSE
    public AudioClip kasokuSE;  //加速パネルのSE
    public AudioClip tyakutiSE; //ワープからの着地パネルのSE
    public AudioClip ClearSE; //ステージクリア時のSE
    AudioSource aud; //オーディオソース


    float timecount = 0f; //ステージ開始からの経過時間
    float count = 0f;  //ゴール時のシーン移動までのカウント
    float count2 = 0f;  //countに値を入れるための数。ゴールしたら代入される
    string thisstage = null;  //シーンの名前。ゴール後にどのシーンに移るか判断
    string timex; //timecountを文字列に変換するための変数
    
    public Text Clear;  //クリア時に表示するテキスト
    public Text time;  //経過時間を表示するテキスト

    
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        
    }

    //ボールの挙動を記したスクリプト
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();  //ボールのリジッドボディ
        Vector3 vel = rigidbody.velocity;  //ボールの速度
        
        //timeflagがtrueの時タイムを表示。ゴールすると停止
        if (timeflag == true)
        {
            timecount += Time.deltaTime;
            timex = timecount.ToString("F2");
        }
        time.text = "Time : " + timex;

       

        //cameraflagがtrueの時、カメラがボールを追従
        if (cameraflag == true)
        {
            Vector3 v = transform.position;
            v.y += 11f;
            v.z -= 11f;
            Camera.main.transform.position = v;
        }

        //ボールのコントロール、矢印キーで操作
        if (flag == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody.AddForce(new Vector3(-4f, 0, 0f));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody.AddForce(new Vector3(4f, 0, 0f));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody.AddForce(new Vector3(0f, 0, 4f));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody.AddForce(new Vector3(0f, 0, -4f));
            }
        }

        //速度を制限
        if (flag2 == true)
        {
            if (vel.x > 15)
            {
                vel.x = 15;
                rigidbody.velocity = vel;
            }

            if (vel.x < -15)
            {
                vel.x = -15;
                rigidbody.velocity = vel;
            }

            if (vel.z > 15)
            {
                vel.z = 15;
                rigidbody.velocity = vel;
            }

            if (vel.z < -15)
            {
                vel.z = -15;
                rigidbody.velocity = vel;
            }
        }

        Vector3 now = transform.position;　　//現在位置の取得


        //落下時のリスタート
        if (now.y < -20)
        {
            transform.position = new Vector3(0, 3, 0);  //リスタートポイント
            rigidbody.angularVelocity = Vector3.zero;  //ボールの速さや回転をリセット
            rigidbody.velocity = Vector3.zero;
            flag = false;
            cameraflag = true;
        }
        
        
        count += count2;  
        //ゴール後、数秒後にシーン移動。シーンによって移動先を変更
        if (count > 2f)
        {
            if (this.thisstage.Contains("stage"))
            {
                SceneManager.LoadScene("title");
            }

            else if (this.thisstage.StartsWith("tutorial"))
            {
                SceneManager.LoadScene("jumptutorial");
            }

            else if (this.thisstage.StartsWith("jumptutorial"))
            {
                SceneManager.LoadScene("warpstage");
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        //足場タグのついたオブジェクトに触れた時、操作可能にする
        if (collision.gameObject.tag.StartsWith("asiba"))
        {
            flag = true;
            flag2 = true;
        }

        //ステージ2の移動するブロックでボールがブロックに追従するように
        if (collision.gameObject.tag == "MoveStage")
        {
            transform.SetParent(collision.transform);
        }
    }

   
    void OnCollisionExit(Collision collision)
    {
        //ステージ2の移動するブロックから離れたら追従を解除する
        if (collision.gameObject.tag == "MoveStage")
        {
            transform.SetParent(null);
        }

    }

    //ワープパネルのワープメソッド。
    //引数としてワープパネルの番号を受け取り、対応した同じ数字の着地パネルにワープする
    //ワープパネルと着地パネルの末尾に同じ数字をつける
    private void Warp(string str)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        GameObject tyakuti = GameObject.Find("tyakuti" + str);  //対応する着地パネルを取得
        Vector3 tyakuti1 = tyakuti.transform.position;  //着地パネルの座標を取得
        tyakuti1.y += 5;                                //着地パネルの少し上の座標にワープ
        transform.position = tyakuti1;
        cameraflag = true;
        tyakutiflag = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        
        //加速パネル。キー入力を停止しながら対応した方向に移動
        if (other.gameObject.tag.StartsWith("kasoku"))
        {
            rigidbody.angularVelocity = Vector3.zero; //加速パネルに踏んだ段階で速度を０に
            rigidbody.velocity = Vector3.zero;
            flag = false;                            //操作を不可にし、速度制限もなしに
            flag2 = false;
            
            if (other.gameObject.tag.StartsWith("kasokuA"))  //↑矢印
            {
                rigidbody.AddForce(new Vector3(0f, 0, 200f));
                this.aud.PlayOneShot(kasokuSE);
            }

            if (other.gameObject.tag.StartsWith("kasokuB"))  //↓矢印
            {
                rigidbody.AddForce(new Vector3(0f, 0, -200f));
                this.aud.PlayOneShot(kasokuSE);
            }

            if (other.gameObject.tag.StartsWith("kasokuC"))  //→矢印
            {
                rigidbody.AddForce(new Vector3(200f, 0, 0f));
                this.aud.PlayOneShot(kasokuSE);

            }
            if (other.gameObject.tag.StartsWith("kasokuD"))  //←矢印
            {
                rigidbody.AddForce(new Vector3(-200f, 0, 0f));
                this.aud.PlayOneShot(kasokuSE);
            }

        }

        //ワープパネルのジャンプ部分
        if (other.gameObject.name.StartsWith("tenni"))  //
        {
            rigidbody.angularVelocity = Vector3.zero;  //速度を０に
            rigidbody.velocity = Vector3.zero;
            cameraflag = false;                   //カメラの追従を切り、操作も不可に
            flag = false;
            this.aud.PlayOneShot(this.tenniSE);
            rigidbody.AddForce(new Vector3(0f, 200f, 0f));

        }

        //ワープパネルのワープ部分。ワープメソッド呼び出し
        //着地と名前を対応させるのはこのパネル
        if (other.gameObject.name.StartsWith("warp"))
        {
            string s = other.gameObject.name;
            string s1 = s.Remove(0, 4);   //warpXのwarpを取り除いて残った数字を引数として渡す
            Warp(s1);
        }

        //着地パネルやストップパネルに踏んで、ワープ後や加速後に再びキー入力を許可
        if (other.gameObject.name.StartsWith("tyakuti") || other.gameObject.name.StartsWith("stop"))
        {
            flag = true;
            flag2 = true;
        }

        //ワープ後の着地挙動
        if(other.gameObject.name.StartsWith("tyakuti") && tyakutiflag == true)
        {
            this.aud.PlayOneShot(this.tyakutiSE);
            tyakutiflag = false;
        }


        //ジャンプパネル。場所により二種類
        if (other.gameObject.tag.StartsWith("jump"))
        {
            this.aud.PlayOneShot(this.jumpSE);
            flag = false;
            if (other.gameObject.name.StartsWith("jumpx"))
            {
                rigidbody.AddForce(new Vector3(0f, 200f, 0f));
            }
            else
            {
                rigidbody.AddForce(new Vector3(0f, 150f, 0f));
            }

        }
       

        //ゴール時の挙動。フェードアウトしてからシーン名を保存
        if (other.gameObject.name.StartsWith("goal"))
        {
           
            Scene Stage = SceneManager.GetActiveScene();  //シーン名を取得
            this.thisstage = Stage.name;
            count2 = 0.01f;
            timeflag = false;
 　　　　   GameObject.Find("kantoku").GetComponent<kantokukun>().setspeed(0.01f);  //画面をフェードアウトするメソッド
            Clear.text = "CLEAR!!";
            this.aud.PlayOneShot(ClearSE);
        }


    }
}

    
   
