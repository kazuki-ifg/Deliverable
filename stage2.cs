using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage2 : MonoBehaviour
{
   
    GameObject Cubex;
    float p = 0.4f;
   // float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
       Cubex = GameObject.Find("Cubex");
    }

    // Update is called once per frame
    void Update()
    {
        
       // Vector3 v2 = new Vector3(0, 0, p);
        Vector3 v1 = Cubex.transform.position;
        Rigidbody r = Cubex.GetComponent<Rigidbody>();
        //r.velocity = v2;
        float v1z = v1.z;
       // v1.x += p;
       // tutu.transform.position = v1;
       if(v1z > 50 )
        {
            p *= -1;
            
        }
       if(v1z <23 )
        {
            p *= -1;
        }

        v1.z += p;
        Cubex.transform.position = v1;




    }
}
