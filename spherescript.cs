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

    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 ang = rigidbody.angularVelocity;
        Vector3 vel = rigidbody.velocity;


        if (timeflag == true)
        {
            timecount += Time.deltaTime;
            timex = timecount.ToString("F2");
        }
        time.text = "Time : " + timex;

       
        count += count2;
        if (cameraflag == true)
        {
            Vector3 v = transform.position;
            v.y += 11f;
            v.z -= 11f;
            Camera.main.transform.position = v;
        }
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
        if (now.y < -20)
        {
            transform.position = new Vector3(0, 3, 0);
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            flag = false;

            cameraflag = true;
            

        }

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

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.tag == "asiba")
    //    {
    //        flag = true;
    //    }
    //}
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MoveStage")
        {
            transform.SetParent(null);
        }

        //if(collision.gameObject.tag == "asiba")
        //{
        //    flag = false;
        //}
    }

    private void warp(string str)
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
        if (other.gameObject.name.StartsWith("tenni"))
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            cameraflag = false;
            flag = false;
            this.aud.PlayOneShot(this.tenniSE);
            rigidbody.AddForce(new Vector3(0f, 200f, 0f));

        }

        if (other.gameObject.name.StartsWith("warp"))
        {
            string s = other.gameObject.name;
            string s1 = s.Remove(0, 4);
            warp(s1);
        }
        if (other.gameObject.name.StartsWith("tyakuti") || other.gameObject.name.StartsWith("stop"))
        {
            flag = true;
            flag2 = true;
        }

        if(other.gameObject.name.StartsWith("tyakuti") && tyakutiflag == true)
        {
            this.aud.PlayOneShot(this.tyakutiSE);
            tyakutiflag = false;
        }

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
       


        if (other.gameObject.name.StartsWith("goal"))
        {
           
            GameObject goal = GameObject.Find("goal");
            //ParticleSystem ps = goal.GetComponent<ParticleSystem>();
            //ps.Play();
            Scene Stage = SceneManager.GetActiveScene();
            this.thisstage = Stage.name;
            count2 = 0.01f;
            timeflag = false;
            GameObject kantoku = GameObject.Find("kantoku");
            kantoku.GetComponent<kantokukun>().setspeed(0.01f);
            Clear.text = "CLEAR!!";
            this.aud.PlayOneShot(ClearSE);
        }


        //if(count > 1f)
        //{
        //    if (this.thisstage.Contains("stage"))
        //    {
        //        SceneManager.LoadScene("title");
        //    }

        //    else if (this.thisstage.StartsWith("tutorial"))
        //    {
        //        SceneManager.LoadScene("jumptutorial");
        //    }

        //    else if (this.thisstage.StartsWith("jumptutorial"))
        //    {
        //        SceneManager.LoadScene("warptstage");
        //    }
        //}
    }
}

    
   
