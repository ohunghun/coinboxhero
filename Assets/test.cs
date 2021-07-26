using mTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public stick[] s;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        a = GetComponent<AudioSource>();
        for(int i=0;i<s.Length;i++)
        {
            s[i].init(10);
        }*/
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            keyDown1();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            keyDown2();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            keyHold3();
        }
       
    }
    AudioSource a;
    public AudioClip clip;
    public float t = 0;
    void keyHold3()
    {
       
        
    }
    public eyeflickAni e;
    void keyDown1()
    {
        e.flick();


    }
    void keyDown2()
    {
        stickManager.Instance.addStick("stick2");
    }
}
