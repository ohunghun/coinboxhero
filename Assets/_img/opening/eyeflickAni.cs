using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeflickAni : MonoBehaviour
{
    public Sprite[] sprite;
    public SpriteRenderer sp;
    Sprite initsp;
    private void Start()
    {
        initsp = sp.sprite;
    }
    public void flick()
    {
        
        StopAllCoroutines();
        StartCoroutine(_flick());
    }
    IEnumerator _flick()
    {

        
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        foreach (Sprite s in sprite)
        {
            sp.sprite = s;
            yield return new WaitForSeconds(0.05f);
        }
        sp.sprite = sprite[0];
    }
}
