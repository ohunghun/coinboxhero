using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float power;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        box.Instance.hit(power, gethitpos());
    }
    public float gethitpos()
    {
        float hitpos = transform.position.x;
        hitpos = hitpos + Random.Range(-0.2f, 0.2f);
        return hitpos;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("cc");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bbb");
    }
}
