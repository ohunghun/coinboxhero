using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionPosition : MonoBehaviour
{
    public string effectName;
    castingTimeLimitChecker t = new castingTimeLimitChecker(0.05f);
    float power = 0;
    Stat stat;
    private void Start()
    {
        Horse h=transform.findComponentOnParents<Horse>();
        this.power = h.power;
        this.stat = h.stat;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "box")
        {
            if (t.check())
            {
                
               
            }
        }
    }
}
