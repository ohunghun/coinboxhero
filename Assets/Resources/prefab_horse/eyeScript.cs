using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine("ee");
    }
    SpriteRenderer sp;
    public Sprite eye_open,eye_close;
    IEnumerator ee()
    {
        while(true)
        {
            sp.sprite = eye_open;
            yield return new WaitForSeconds(2);
            sp.sprite = eye_close;
            yield return new WaitForSeconds(0.5f);
            sp.sprite = eye_open;
            yield return new WaitForSeconds(5);
            sp.sprite = eye_close;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
