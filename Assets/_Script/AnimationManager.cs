using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public myAnimation[] animations;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startAnimation(string id)
    {
        joystick.Instance.controlDisable();
        myAnimation ani = getAnimation(id);
        ani.StartAnimation(aniend);
    }
    void aniend()
    {

    }
    myAnimation getAnimation(string id)
    {
        foreach(myAnimation ani in animations)
        {
            if (ani.id == id)
                return ani;
        }
        return null;
    }
}
