using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_box : MonoBehaviour
{
    public float hp;
    public void hit(float power)
    {
        hp -= power;
        if (hp <= 0)
            Victory();
    }
    void Victory()
    {

    }
    void Lose()
    {

    }

}
