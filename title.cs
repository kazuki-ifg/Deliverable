using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    public Text press;
    public Text tx;
    public Text tx2;
    public Text esc;
    //float time = 0;
    GameObject Cube;
    bool flag = true;
    bool flag2 = true;
   // bool flag3 = false;
    public AudioClip select;
    public AudioClip yes;
    AudioSource aud;

    GameObject panel;
    float alfa;
    float speed = 0f;
    float red, green, blue;
    void Start()
    {
        Cube = GameObject.Find("Cube");
        this.aud = GetComponent<AudioSource>();
        panel = GameObject.Find("Panel");

        
        red = panel.GetComponent<Image>().color.r;
        green = panel.GetComponent<Image>().color.g;
        blue = panel.GetComponent<Image>().color.b;
    }

   
    
    void Update()
    {
        panel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;
        

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

        Cube.transform.Rotate(new Vector3(1, 1, 1));

        Vector3 Cube_v = Cube.transform.position;

        if (flag2 == false && flag == true)
        {
            
            if (Cube_v.y ==0  &&Input.GetKeyDown(KeyCode.DownArrow))
            {
                Cube_v.y = -3f;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }

            if (Cube_v.y == -3f &&Input.GetKeyDown(KeyCode.UpArrow))
            {
                Cube_v.y = 0;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }


            if (Cube_v.x == -10 &&Input.GetKeyDown(KeyCode.RightArrow))
            {
                Cube_v.x = 1;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }


            if (Cube_v.x == 1&&Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Cube_v.x = -10;
                Cube.transform.position = Cube_v;
                this.aud.PlayOneShot(this.select);
            }
        }

        if (flag2 == false && flag == true && Input.GetKeyDown(KeyCode.Space))
        {
            this.aud.PlayOneShot(this.yes);
            speed = 0.01f;
            flag = false;
        }

        if (alfa > 1.5f)
        {
            if (Cube_v.x == -10f && Cube_v.y == 0)
            {
                SceneManager.LoadScene("tutorial");
            }

            if (Cube_v.x == 1f && Cube_v.y == 0)
            {
                SceneManager.LoadScene("stage1");
            }

            if (Cube_v.x == -10f && Cube_v.y == -3f)
            {
                SceneManager.LoadScene("stage2");
            }

            if (Cube_v.x == 1 && Cube_v.y == -3f)
            {
                SceneManager.LoadScene("stage3");
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape)) Quit();

     }



    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    //time += Time.deltaTime;
    //if(time > 8)
    //{
    //    Renderer renderer = Press.GetComponent<Renderer>();
    //    renderer.material.color = Color.clear;

    //}

    //if(time == 10)
    //{
    //    time = 0;
    //    Renderer renderer = Press.GetComponent<Renderer>();
    //    Color color = renderer.material.color;
    //    color = new Color(1, 0.3f, 0, 1);
    //    renderer.material.color = color;
    //}



}
