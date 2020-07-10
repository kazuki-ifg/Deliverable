using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class spherescript : MonoBehaviour
{
    bool flag = false;
    bool flag2 = false;
    bool tyakutiflag = false;
    bool cameraflag = true;
    bool timeflag = true;
    public AudioClip tenniSE;
    public AudioClip jumpSE;
    public AudioClip kasokuSE;
    public AudioClip tyakutiSE;
    public AudioClip ClearSE;
    AudioSource aud;


    float timecount = 0f;
    float count = 0f;
    float count2 = 0f;
    string thisstage = null;
    string timex;
    
    public Text Clear;
    public Text time;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        
    }

  
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 vel = rigidbody.velocity;
        count += count2;
        //タイムを表示


        if (timeflag == true)
        {
            timecount += Time.deltaTime;
            timex = timecount.ToString("F2");
        }
        time.text = "Time : " + timex;

       

        //カメラがボールを追従
        if (cameraflag == true)
        {
            Vector3 v = transform.position;
            v.y += 11f;
            v.z -= 11f;
            Camera.main.transform.position = v;
        }

        //ボールのコントロール
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



        Vector3 now = transform.position;


        //落下時のリスタート
        if (now.y < -20)
        {
            transform.position = new Vector3(0, 3, 0);
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            flag = false;
            cameraflag = true;
        }

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
        if (collision.gameObject.tag.StartsWith("asiba"))
        {
            flag = true;
            flag2 = true;
        }

        if (collision.gameObject.tag == "MoveStage")
        {
            transform.SetParent(collision.transform);
        }
    }

   
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MoveStage")
        {
            transform.SetParent(null);
        }

    }

    //ワープパネルのワープメソッド。対応した着地パネルにワープ
    private void Warp(string str)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        GameObject tyakuti = GameObject.Find("tyakuti" + str);
        Vector3 tyakuti1 = tyakuti.transform.position;
        tyakuti1.y += 5;
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
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            flag = false;
            flag2 = false;
            
            if (other.gameObject.tag.StartsWith("kasokuA"))
            {
                rigidbody.AddForce(new Vector3(0f, 0, 200f));
                this.aud.PlayOneShot(kasokuSE);
            }

            if (other.gameObject.tag.StartsWith("kasokuB"))
            {
                rigidbody.AddForce(new Vector3(0f, 0, -200f));
                this.aud.PlayOneShot(kasokuSE);
            }

            if (other.gameObject.tag.StartsWith("kasokuC"))
            {
                rigidbody.AddForce(new Vector3(200f, 0, 0f));
                this.aud.PlayOneShot(kasokuSE);

            }
            if (other.gameObject.tag.StartsWith("kasokuD"))
            {
                rigidbody.AddForce(new Vector3(-200f, 0, 0f));
                this.aud.PlayOneShot(kasokuSE);
            }

        }

        //ワープパネルのジャンプ部分
        if (other.gameObject.name.StartsWith("tenni"))
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            cameraflag = false;
            flag = false;
            this.aud.PlayOneShot(this.tenniSE);
            rigidbody.AddForce(new Vector3(0f, 200f, 0f));

        }

        //ワープパネルのワープ部分。ワープメソッド呼び出し
        if (other.gameObject.name.StartsWith("warp"))
        {
            string s = other.gameObject.name;
            string s1 = s.Remove(0, 4);
            Warp(s1);
        }

        //ワープ後や加速後に再びキー入力を許可
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
           
            GameObject goal = GameObject.Find("goal");
            Scene Stage = SceneManager.GetActiveScene();
            this.thisstage = Stage.name;
            count2 = 0.01f;
            timeflag = false;
 　　　　   GameObject.Find("kantoku").GetComponent<kantokukun>().setspeed(0.01f);
            Clear.text = "CLEAR!!";
            this.aud.PlayOneShot(ClearSE);
        }


    }
}

    
   
